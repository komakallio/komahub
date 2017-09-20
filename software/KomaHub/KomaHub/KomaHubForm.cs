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
        private KomaHubHID komaHub = new KomaHubHID();
        private float voltageResistorRatio = 6.0f;
        private float sqmOffset = 21;

        public KomaHubForm()
        {
            InitializeComponent();

            var firmwarePos = titlePictureBox.PointToClient(this.PointToScreen(firmwareLabel.Location));
            firmwareLabel.Parent = titlePictureBox;
            firmwareLabel.Location = firmwarePos;

            var serialPos = titlePictureBox.PointToClient(this.PointToScreen(serialLabel.Location));
            serialLabel.Parent = titlePictureBox;
            serialLabel.Location = serialPos;



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
        }

        private void DetectDevice(object sender, DoWorkEventArgs args)
        {
            bool found = false;
            while (!found)
            {
                found = komaHub.openDevice();
                if (found)
                {
//                    var factorySettings = komaHub.readFactorySettings();
//                    serialLabel.Text = String.Format("Device #{0,3,D3}", factorySettings.SerialNumber);
                    serialLabel.Visible = true;
//                    firmwareLabel.Text = String.Format("Firmware v{0}.{1}", factorySettings.FirmwareVersion >> 8, factorySettings.FirmwareVersion & 0xFF);
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

        private void PollStatus(object sender, DoWorkEventArgs args)
        {
            while (true)
            {
                try
                {
//                    komaHubComm.Send(serialPort, new KomaHubComm.Command("STATUS", null));
                }
                catch (IOException)
                {
                    return;
                }
                Thread.Sleep(5000);
            }
        }

        public void UpdateRelayButtons(UIState.Output output, Button button)
        {
            button.Visible = output.Enabled;
            button.Enabled = uiState.Connected;

            if (!uiState.Connected)
                return;

            if (output.isOverload) 
            {
                button.BackColor = Color.Salmon;
                button.Text = "Reset";
            } 
            else 
            {
                button.BackColor = output.Active ? Color.LimeGreen : Color.Salmon;
                button.Text = output.Active ? "On" : "Off";
            }
        }

        public void Update(UIState uiState)
        {
//            this.groupBoxRelayControl.Enabled = uiState.Connected;
            this.groupBoxSensors.Enabled = true;

            UpdateRelayButtons(uiState.Outputs[0], this.buttonRelay1);
            UpdateRelayButtons(uiState.Outputs[1], this.buttonRelay2);
            UpdateRelayButtons(uiState.Outputs[2], this.buttonRelay3);
            UpdateRelayButtons(uiState.Outputs[3], this.buttonRelay4);
            UpdateRelayButtons(uiState.Outputs[4], this.buttonRelay5);
            UpdateRelayButtons(uiState.Outputs[5], this.buttonRelay6);
            
            this.labelTemperature.Text = string.Format("{0:F1} °C", uiState.Temperature);
            this.labelTemperature.Enabled = uiState.EnabledSensors.Contains("TA");
            this.labelTemperatureTitle.Enabled = uiState.EnabledSensors.Contains("TA");
            this.labelHumidity.Text = string.Format("{0:F1} %", uiState.Humidity);
            this.labelHumidity.Enabled = uiState.EnabledSensors.Contains("RH");
            this.labelHumidityTitle.Enabled = uiState.EnabledSensors.Contains("RH");
            this.labelPressure.Text = string.Format("{0:F0} hPa", Math.Round(uiState.Pressure));
            this.labelPressure.Enabled = uiState.EnabledSensors.Contains("P");
            this.labelPressureTitle.Enabled = uiState.EnabledSensors.Contains("P");
            this.labelExternalTemperature.Text = string.Format("{0:F1} °C", uiState.ExternalTemperature);
            this.labelExternalTemperature.Enabled = uiState.EnabledSensors.Contains("TDS");
            this.labelExternalTemperatureTitle.Enabled = uiState.EnabledSensors.Contains("TDS");

            this.labelSkyTemperature.Text = string.Format("{0:F1} °C", uiState.SkyTemperature);
            this.labelSkyTemperature.Enabled = uiState.EnabledSensors.Contains("TMO");
            this.labelSkyTemperatureTitle.Enabled = uiState.EnabledSensors.Contains("TMO");
            this.labelSkyTemperatureDelta.Text = string.Format("{0:F1} °C", uiState.SkyTemperatureDelta);
            this.labelSkyTemperatureDelta.Enabled = uiState.EnabledSensors.Contains("TMA") && uiState.EnabledSensors.Contains("TMO");
            this.labelSkyTemperatureDeltaTitle.Enabled = uiState.EnabledSensors.Contains("TMA") && uiState.EnabledSensors.Contains("TMO");

            this.labelSQM.Text = string.Format("{0:F2}", uiState.SQM);
            this.labelSQM.Enabled = uiState.EnabledSensors.Contains("SQM");
            this.labelSQMTitle.Enabled = uiState.EnabledSensors.Contains("SQM");

            this.labelInputVoltage.Text = string.Format("{0:F2} V", uiState.InputVoltage);
            this.labelInputVoltage.ForeColor = uiState.InputVoltage < 12.0 || uiState.InputVoltage >= 14.0 ? Color.Red : Color.Black;

            this.labelStatusText.Text = uiState.StatusText;
        }

        private float sqm(float rawSqmValue, float offset)
        {
            return -2.5f * (float)Math.Log10(rawSqmValue) + offset;
        }

        private float current(float rawValue)
        {
            const float REFERENCE_VOLTAGE = 4.096f;
            const float SYSTEM_VOLTAGE = 5.0f;
            const float ADC_MAX = 1024.0f;
            const float SENSITIVITY_mV_PER_A = 100.0f;
            const float SENSITIVITY_V_PER_A = 1000.0f / SENSITIVITY_mV_PER_A;

            float volts = REFERENCE_VOLTAGE * rawValue / ADC_MAX;
            float zero = SYSTEM_VOLTAGE / 2.0f;
            return (volts - zero) * SENSITIVITY_V_PER_A;
        }

        public void ApplySettings(float voltageResistorRatio, float sqmOffset)
        {
            this.voltageResistorRatio = voltageResistorRatio;
            this.sqmOffset = sqmOffset;
            Update(uiState);

            Properties.Settings.Default["voltageResistorRatio"] = voltageResistorRatio;
            Properties.Settings.Default["sqmOffset"] = sqmOffset;
            Properties.Settings.Default.Save();
        }

        private void buttonRelay1On_Click(object sender, EventArgs e)
        {
            uiState.Outputs[0].Active = !uiState.Outputs[0].Active;
            komaHub.setRelay(0, uiState.Outputs[0].Active);
            Update(uiState);
        }

        private void buttonRelay2On_Click(object sender, EventArgs e)
        {
            uiState.Outputs[1].Active = !uiState.Outputs[1].Active;
            komaHub.setRelay(1, uiState.Outputs[1].Active);
            Update(uiState);
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
        }

        private void KomaHubForm_Load(object sender, EventArgs e)
        {

        }
    }
}
