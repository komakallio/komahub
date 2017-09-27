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
        private UIState uiState;

        public SettingsForm(ISettingsReceiver receiver, UIState uiState)
        {
            InitializeComponent();

            this.uiState = uiState;
            this.settingsReceiver = receiver;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                ComboBox[] outputTypes = { output1Type, output2Type, output3Type, output4Type, output5Type, output6Type };
                for (int i = 0; i < 6; i++)
                    uiState.Outputs[i].type = convertType(outputTypes[i].GetItemText(outputTypes[i].SelectedItem));

/*                float voltageResistorRatio = float.Parse(this.textBoxVoltageResistorRatio.Text);
                float sqmOffset = float.Parse(this.textBoxSQMOffset.Text); */
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
