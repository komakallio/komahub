using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace KomaHub
{
    public partial class KomaHubForm : Form, ISettingsReceiver
    {
        private UIState uiState = new UIState();
        private bool Connected = false;
        private string StatusText = "";
        private BackgroundWorker backgroundWorker = new BackgroundWorker();
        private BackgroundWorker statusWorker = new BackgroundWorker();
        private KomaHubHID komaHub = new KomaHubHID();
        private WebServer webServer;
        private bool disableUpdates = false;
        private ToolTip sqmToolTip = new ToolTip();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        private const int WM_SETREDRAW = 11; 

        public KomaHubForm()
        {
            InitializeComponent();

            webServer = new WebServer(this);

            var firmwarePos = new Point(216, 75);
            firmwareLabel.Parent = titlePictureBox;
            firmwareLabel.Location = firmwarePos;
            firmwareLabel.BringToFront();

            var serialPos = new Point(222, 90);
            serialLabel.Parent = titlePictureBox;
            serialLabel.Location = serialPos;
            serialLabel.BringToFront();

            Connected = false;

            StartDeviceDetection();
        }

        private void StartDeviceDetection()
        {
            StatusText = "Searching for device";
            Update(uiState);
            
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += DetectDevice;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.RunWorkerAsync();

            statusWorker = new BackgroundWorker();
            statusWorker.DoWork += UpdateStatus;
            statusWorker.RunWorkerAsync();
        }

        private void DetectDevice(object sender, DoWorkEventArgs args)
        {
            try { 
                while (!komaHub.openDevice())
                {
                    StatusText = "No device found";
                    this.Invoke((MethodInvoker)delegate
                    {
                        Update(uiState);
                    });
                    Thread.Sleep(1000);
                }
            } catch (Exception e) 
            {
                this.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show("Error: " + e.Message);
                });
            }

            var factorySettings = komaHub.readFactorySettings();
            serialLabel.Text = String.Format("S/N #{0,4:D4}", factorySettings.SerialNumber);
            serialLabel.Visible = true;
            firmwareLabel.Text = String.Format("Firmware v{0}.{1}", factorySettings.FirmwareVersion >> 8, factorySettings.FirmwareVersion & 0xFF);
            firmwareLabel.Visible = true;

            for (int i = 0; i < 6; i++)
            {
                uiState.Outputs[i] = komaHub.readOutput(i);
            }

            uiState.Status = komaHub.readStatus();
 
            Connected = true;
            StatusText = "Device Connected";

            webServer.start();
        }

        private void UpdateStatus(object sender, DoWorkEventArgs args)
        {
            while (true)
            {
                if (Connected)
                {
                    uiState.Status = komaHub.readStatus();
                    this.Invoke((MethodInvoker)delegate
                    {
                        Update(uiState);
                    });
                }

                Thread.Sleep(1000);
            }
        }

        public void UpdateRelayUI(int output, Label outputNumber, Label outputName, Label currentLabel, DomainUpDown pwmDuty, Button button)
        {
            outputName.Text = (uiState.Outputs[output].type != KomahubOutput.OutputType.OFF ? uiState.Outputs[output].name : "Disabled");
            button.Visible = (uiState.Outputs[output].type != KomahubOutput.OutputType.OFF);
            pwmDuty.Visible = (uiState.Outputs[output].type == KomahubOutput.OutputType.PWM);
            int index = 20 - uiState.Status.pwmDuty[output] / 5;
            if (index == 20) index = 19;
            disableUpdates = true;
            pwmDuty.SelectedIndex = index;
            disableUpdates = false;
            button.Enabled = Connected;

            if (!Connected)
                return;

            if (uiState.Status.fuseIsBlown[output]) 
            {
                button.BackColor = Color.Salmon;
                button.Text = "Reset";

                currentLabel.Text = "FUSE";
                currentLabel.ForeColor = Color.OrangeRed;
                currentLabel.Visible = true;
                outputName.ForeColor = Color.OrangeRed;
                outputNumber.ForeColor = Color.OrangeRed;
            } 
            else 
            {
                button.BackColor = uiState.Status.relayIsOpen[output] ? Color.LimeGreen : Color.Salmon;
                button.Text = uiState.Status.relayIsOpen[output] ? "On" : "Off";

                currentLabel.Text = String.Format("{0,3:0.0} A", uiState.Status.outputCurrent[output]);
                currentLabel.ForeColor = Color.Black;
                currentLabel.Visible = (uiState.Outputs[output].type != KomahubOutput.OutputType.OFF);
                outputName.ForeColor = (uiState.Outputs[output].type != KomahubOutput.OutputType.OFF ? Color.Black : Color.Gray);
                outputNumber.ForeColor = (uiState.Outputs[output].type != KomahubOutput.OutputType.OFF ? Color.Black : Color.Gray);
            }
        }

        public void Update(UIState uiState)
        {
            SendMessage(this.Handle, WM_SETREDRAW, false, 0);

            this.groupBoxRelayControl.Enabled = Connected;
            this.groupBoxSensors.Enabled = true;

            UpdateRelayUI(0, this.output1Number, this.output1Name, this.output1Current, this.pwmDuty1, this.buttonRelay1);
            UpdateRelayUI(1, this.output2Number, this.output2Name, this.output2Current, this.pwmDuty2, this.buttonRelay2);
            UpdateRelayUI(2, this.output3Number, this.output3Name, this.output3Current, this.pwmDuty3, this.buttonRelay3);
            UpdateRelayUI(3, this.output4Number, this.output4Name, this.output4Current, this.pwmDuty4, this.buttonRelay4);
            UpdateRelayUI(4, this.output5Number, this.output5Name, this.output5Current, this.pwmDuty5, this.buttonRelay5);
            UpdateRelayUI(5, this.output6Number, this.output6Name, this.output6Current, this.pwmDuty6, this.buttonRelay6);
    
            if (uiState.Status.pthPresent)
                this.labelTemperature.Text = string.Format("{0:F1} °C", uiState.Status.temperature);
            this.labelTemperature.Enabled = uiState.Status.pthPresent;
            this.labelTemperatureTitle.Enabled = uiState.Status.pthPresent;
            if (uiState.Status.pthPresent)
                this.labelHumidity.Text = string.Format("{0:F1} %", uiState.Status.humidity);
            this.labelHumidity.Enabled = uiState.Status.pthPresent;
            this.labelHumidityTitle.Enabled = uiState.Status.pthPresent;
            if (uiState.Status.pthPresent)
                this.labelPressure.Text = string.Format("{0:F0} hPa", Math.Round(uiState.Status.pressure));
            this.labelPressure.Enabled = uiState.Status.pthPresent;
            this.labelPressureTitle.Enabled = uiState.Status.pthPresent;
            if (uiState.Status.numberOfExternalTemperatures > 0)
                this.labelExternalTemperature.Text = string.Format("{0:F1} °C", uiState.Status.externalTemperatures[0]);
            this.labelExternalTemperature.Enabled = uiState.Status.numberOfExternalTemperatures > 0;
            this.labelExternalTemperatureTitle.Enabled = uiState.Status.numberOfExternalTemperatures > 0;

            if (uiState.Status.skyTemperaturePresent)
                this.labelSkyTemperature.Text = string.Format("{0:F1} °C", uiState.Status.skyTemperature);
            this.labelSkyTemperature.Enabled = uiState.Status.skyTemperaturePresent;
            this.labelSkyTemperatureTitle.Enabled = uiState.Status.skyTemperaturePresent;
            if (uiState.Status.skyTemperaturePresent)
                this.labelSkyTemperatureDelta.Text = string.Format("{0:F1} °C", uiState.Status.skyTemperature - uiState.Status.skyTemperatureAmbient);
            this.labelSkyTemperatureDelta.Enabled = uiState.Status.skyTemperaturePresent;
            this.labelSkyTemperatureDeltaTitle.Enabled = uiState.Status.skyTemperaturePresent;

            if (uiState.Status.skyQualityPresent)
            {
                this.labelSQM.Text = string.Format("{0:F1} mag/as²", uiState.Status.skyQuality);
                this.sqmToolTip.SetToolTip(this.labelSQM, string.Format("{0:F1} Hz", uiState.Status.skyQualityFreq));
            }

            this.labelSQM.Enabled = uiState.Status.skyQualityPresent;
            this.labelSQMTitle.Enabled = uiState.Status.skyQualityPresent;

            this.labelInputVoltage.Text = string.Format("{0:F1} V", uiState.Status.inputVoltage);
            this.labelInputVoltage.ForeColor = uiState.Status.inputVoltage < 12.0 || uiState.Status.inputVoltage >= 14.0 ? Color.Red : Color.Black;

            this.labelStatusText.Text = StatusText;

            SendMessage(this.Handle, WM_SETREDRAW, true, 0);
            this.Refresh();
        }

        public void ApplySettings(UIState newUiState)
        {
            for (int outputNumber = 0; outputNumber < 6; outputNumber++)
            {
                if (!uiState.Outputs[outputNumber].Equals(newUiState.Outputs[outputNumber]))
                    komaHub.configureOutput(outputNumber, newUiState.Outputs[outputNumber]);
            }

            this.uiState = newUiState;
            Update(uiState);
        }

        public void enableRelay(int n)
        {
            uiState.Status.relayIsOpen[n] = true;
            komaHub.setRelay(n, uiState.Status.relayIsOpen[n]);
            Update(uiState);
        }

        public void disableRelay(int n)
        {
            uiState.Status.relayIsOpen[n] = false;
            komaHub.setRelay(n, uiState.Status.relayIsOpen[n]);
            Update(uiState);
        }

        private void toggleRelay(int n)
        {
            if (uiState.Status.fuseIsBlown[n])
            {
                komaHub.resetFuse(n);
                uiState.Status.fuseIsBlown[n] = false;
                uiState.Status.relayIsOpen[n] = false;
            }
            else
            {
                uiState.Status.relayIsOpen[n] = !uiState.Status.relayIsOpen[n];
                komaHub.setRelay(n, uiState.Status.relayIsOpen[n]);
            }
        }

        private void buttonRelay1On_Click(object sender, EventArgs e)
        {
            toggleRelay(0);
            Update(uiState);
        }

        private void buttonRelay2On_Click(object sender, EventArgs e)
        {
            toggleRelay(1);
            Update(uiState);
        }

        private void buttonRelay3_Click(object sender, EventArgs e)
        {
            toggleRelay(2);
            Update(uiState);
        }

        private void buttonRelay4_Click(object sender, EventArgs e)
        {
            toggleRelay(3);
            Update(uiState);
        }

        private void buttonRelay5_Click(object sender, EventArgs e)
        {
            toggleRelay(4);
            Update(uiState);
        }

        private void buttonRelay6_Click(object sender, EventArgs e)
        {
            toggleRelay(5);
            Update(uiState);
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            new SettingsForm(this, uiState).Show();
        }

        private void pwmDuty1_SelectedItemChanged(object sender, EventArgs e)
        {
            if (disableUpdates)
                return;
            uiState.Status.pwmDuty[0] = (byte)(100 - pwmDuty1.SelectedIndex * 5);
            komaHub.setPwmDuty(0, uiState.Status.pwmDuty[0]);
        }

        private void pwmDuty2_SelectedItemChanged(object sender, EventArgs e)
        {
            if (disableUpdates)
                return;
            uiState.Status.pwmDuty[1] = (byte)(100 - pwmDuty2.SelectedIndex * 5);
            komaHub.setPwmDuty(1, uiState.Status.pwmDuty[1]);
        }

        private void pwmDuty3_SelectedItemChanged(object sender, EventArgs e)
        {
            if (disableUpdates)
                return;
            uiState.Status.pwmDuty[2] = (byte)(100 - pwmDuty3.SelectedIndex * 5);
            komaHub.setPwmDuty(2, uiState.Status.pwmDuty[2]);
        }

        private void pwmDuty4_SelectedItemChanged(object sender, EventArgs e)
        {
            if (disableUpdates)
                return;
            uiState.Status.pwmDuty[3] = (byte)(100 - pwmDuty4.SelectedIndex * 5);
            komaHub.setPwmDuty(3, uiState.Status.pwmDuty[3]);
        }

        private void pwmDuty5_SelectedItemChanged(object sender, EventArgs e)
        {
            if (disableUpdates)
                return;
            uiState.Status.pwmDuty[4] = (byte)(100 - pwmDuty5.SelectedIndex * 5);
            komaHub.setPwmDuty(4, uiState.Status.pwmDuty[4]);
        }

        private void pwmDuty6_SelectedItemChanged(object sender, EventArgs e)
        {
            if (disableUpdates)
                return;
            uiState.Status.pwmDuty[5] = (byte)(100 - pwmDuty6.SelectedIndex * 5);
            komaHub.setPwmDuty(5, uiState.Status.pwmDuty[5]);
        }
    }
}
