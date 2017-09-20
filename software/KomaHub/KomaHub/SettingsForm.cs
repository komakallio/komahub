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

        public SettingsForm(ISettingsReceiver receiver, float voltageResistorRatio, float sqmOffset)
        {
            InitializeComponent();

            this.settingsReceiver = receiver;
            this.textBoxVoltageResistorRatio.Text = voltageResistorRatio.ToString();
            this.textBoxSQMOffset.Text = sqmOffset.ToString();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                float voltageResistorRatio = float.Parse(this.textBoxVoltageResistorRatio.Text);
                float sqmOffset = float.Parse(this.textBoxSQMOffset.Text);
                this.settingsReceiver.ApplySettings(voltageResistorRatio, sqmOffset);
            }
            catch (Exception)
            {
                MessageBox.Show("Value must be a number!");
            }
            Close();
        }
    }
}
