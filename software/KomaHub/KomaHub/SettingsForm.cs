using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KomaHub
{
    public partial class SettingsForm : Form
    {
        private ISettingsReceiver settingsReceiver;

        public SettingsForm(ISettingsReceiver receiver, UIState uiState)
        {
            InitializeComponent();

            this.settingsReceiver = receiver;

            LoadStateFromUI(uiState);
        }

        private void LoadStateFromUI(UIState uiState)
        {
            ComboBox[] outputTypes = { output1Type, output2Type, output3Type, output4Type, output5Type, output6Type };
            TextBox[] outputNames = { output1Name, output2Name, output3Name, output4Name, output5Name, output6Name };
            ComboBox[] fuseCurrents = { output1Fuse, output2Fuse, output3Fuse, output4Fuse, output5Fuse, output6Fuse };

            for (int i = 0; i < 6; i++)
            {
                outputNames[i].Text = uiState.Outputs[i].name;
                outputTypes[i].SelectedIndex = (int)uiState.Outputs[i].type;
                fuseCurrents[i].SelectedIndex = (int)uiState.Outputs[i].fuseCurrent;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                UIState uiState = new UIState();
                ComboBox[] outputTypes = { output1Type, output2Type, output3Type, output4Type, output5Type, output6Type };
                TextBox[] outputNames = { output1Name, output2Name, output3Name, output4Name, output5Name, output6Name };
                ComboBox[] fuseCurrents = { output1Fuse, output2Fuse, output3Fuse, output4Fuse, output5Fuse, output6Fuse };
                float[] fuseValues = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                for (int i = 0; i < 6; i++)
                {
                    uiState.Outputs[i].name = outputNames[i].Text;
                    uiState.Outputs[i].fuseCurrent = fuseValues[fuseCurrents[i].SelectedIndex];
                    uiState.Outputs[i].type = convertType(outputTypes[i].GetItemText(outputTypes[i].SelectedItem));
                }

                this.settingsReceiver.ApplySettings(uiState);
            }
            catch (Exception)
            {
                MessageBox.Show("Value must be a number!");
            }
            Close();
        }

        private KomahubOutput.OutputType convertType(string type) {
            switch (type) {
                case "12V": return KomahubOutput.OutputType.DC;
                case "PWM": return KomahubOutput.OutputType.PWM;
                default:
                    return KomahubOutput.OutputType.OFF;
            }
        }
    }
}
