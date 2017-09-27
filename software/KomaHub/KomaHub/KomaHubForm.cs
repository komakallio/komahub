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

namespace KomaHub
{
    public partial class KomaHubForm : Form, ISettingsReceiver
    {
        private UIState uiState = new UIState();
        private BackgroundWorker backgroundWorker = new BackgroundWorker();
        private BackgroundWorker statusWorker = new BackgroundWorker();
        private KomaHubHID komaHub = new KomaHubHID();
        private float voltageResistorRatio = 6.0f;
        private float sqmOffset = 21;

        public KomaHubForm()
        {
            InitializeComponent();

            var firmwarePos = new Point(204, 48);
            firmwareLabel.Parent = titlePictureBox;
            firmwareLabel.Location = firmwarePos;
            firmwareLabel.BringToFront();

            var serialPos = new Point(207, 62);
            serialLabel.Parent = titlePictureBox;
            serialLabel.Location = serialPos;
            serialLabel.BringToFront();

            uiState.Connected = false;

            voltageResistorRatio = (float)Properties.Settings.Default["voltageResistorRatio"];
            sqmOffset = (float)Properties.Settings.Default["sqmOffset"];

            Update(uiState);

            StartDeviceDetection();
        }

        private void StartDeviceDetection()
        {
            uiState.StatusText = "Searching for device";
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
            bool found = false;
            while (!found)
            {
                found = komaHub.openDevice();
                if (found)
                {
                    var factorySettings = komaHub.readFactorySettings();
                    serialLabel.Text = String.Format("S/N #{0,4:D4}", factorySettings.SerialNumber);
                    serialLabel.Visible = true;
                    firmwareLabel.Text = String.Format("Firmware v{0}.{1}", factorySettings.FirmwareVersion >> 8, factorySettings.FirmwareVersion & 0xFF);
                    firmwareLabel.Visible = true;
                    break;
                }

                uiState.StatusText = "No device found";
                Update(uiState);
                Thread.Sleep(1000);
            }
             
            uiState.Connected = true;
            uiState.StatusText = "Device Connected";
            this.Invoke((MethodInvoker)delegate
            {
                Update(uiState);
            });
        }

        private void UpdateStatus(object sender, DoWorkEventArgs args)
        {
            while (true)
            {
                if (komaHub.Connected)
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

        public void UpdateRelayUI(int output, Label currentLabel, Button button)
        {
            button.Visible = uiState.Outputs[output].type != KomahubOutput.OutputType.OFF;
            button.Enabled = uiState.Connected;

            if (!uiState.Connected)
                return;

            if (uiState.Status.fuseIsBlown[output]) 
            {
                button.BackColor = Color.Salmon;
                button.Text = "Reset";

                currentLabel.Text = "FUSE";
                currentLabel.ForeColor = Color.OrangeRed;
            } 
            else 
            {
                button.BackColor = uiState.Status.relayIsOpen[output] ? Color.LimeGreen : Color.Salmon;
                button.Text = uiState.Status.relayIsOpen[output] ? "On" : "Off";

                currentLabel.Text = String.Format("{0,3:0.0} A", uiState.Status.outputCurrent[output]);
                currentLabel.ForeColor = Color.Black;
            }
        }

        public void Update(UIState uiState)
        {
            this.groupBoxRelayControl.Enabled = uiState.Connected;
            this.groupBoxSensors.Enabled = true;

            UpdateRelayUI(0, this.output1Current, this.buttonRelay1);
            UpdateRelayUI(1, this.output2Current, this.buttonRelay2);
            UpdateRelayUI(2, this.output3Current, this.buttonRelay3);
            UpdateRelayUI(3, this.output4Current, this.buttonRelay4);
            UpdateRelayUI(4, this.output5Current, this.buttonRelay5);
            UpdateRelayUI(5, this.output6Current, this.buttonRelay6);
            
            this.labelTemperature.Text = string.Format("{0:F1} °C", uiState.Status.temperature);
            this.labelTemperature.Enabled = uiState.FactorySettings.featureAmbientPTH;
            this.labelTemperatureTitle.Enabled = uiState.FactorySettings.featureAmbientPTH;
            this.labelHumidity.Text = string.Format("{0:F1} %", uiState.Status.humidity);
            this.labelHumidity.Enabled = uiState.FactorySettings.featureAmbientPTH;
            this.labelHumidityTitle.Enabled = uiState.FactorySettings.featureAmbientPTH;
            this.labelPressure.Text = string.Format("{0:F0} hPa", Math.Round(uiState.Status.pressure));
            this.labelPressure.Enabled = uiState.FactorySettings.featureAmbientPTH;
            this.labelPressureTitle.Enabled = uiState.FactorySettings.featureAmbientPTH;
            this.labelExternalTemperature.Text = string.Format("{0:F1} °C", uiState.Status.externalTemperature);
            this.labelExternalTemperature.Enabled = uiState.FactorySettings.featureTempProbe;
            this.labelExternalTemperatureTitle.Enabled = uiState.FactorySettings.featureTempProbe;

            this.labelSkyTemperature.Text = string.Format("{0:F1} °C", uiState.Status.skyTemperature);
            this.labelSkyTemperature.Enabled = uiState.FactorySettings.featureSkyTemperature;
            this.labelSkyTemperatureTitle.Enabled = uiState.FactorySettings.featureSkyTemperature;
            this.labelSkyTemperatureDelta.Text = string.Format("{0:F1} °C", uiState.Status.skyTemperatureAmbient - uiState.Status.skyTemperature);
            this.labelSkyTemperatureDelta.Enabled = uiState.FactorySettings.featureSkyTemperature;
            this.labelSkyTemperatureDeltaTitle.Enabled = uiState.FactorySettings.featureSkyTemperature;

            this.labelSQM.Text = string.Format("{0:F2}", uiState.Status.skyQuality);
            this.labelSQM.Enabled = uiState.FactorySettings.featureSkyQuality;
            this.labelSQMTitle.Enabled = uiState.FactorySettings.featureSkyQuality;

            this.labelInputVoltage.Text = string.Format("{0:F2} V", uiState.Status.inputVoltage);
            this.labelInputVoltage.ForeColor = uiState.Status.inputVoltage < 12.0 || uiState.Status.inputVoltage >= 14.0 ? Color.Red : Color.Black;

            this.labelStatusText.Text = uiState.StatusText;
        }

        private float sqm(float rawSqmValue, float offset)
        {
            return -2.5f * (float)Math.Log10(rawSqmValue) + offset;
        }

        public void ApplySettings(UIState uiState)
        {
            this.uiState = uiState;
            Update(uiState);
        }

        private void toggleRelay(int n)
        {
            uiState.Status.relayIsOpen[n] = !uiState.Status.relayIsOpen[n];
            komaHub.setRelay(n, uiState.Status.relayIsOpen[n]);
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
    }
}
