namespace KomaHub
{
    partial class KomaHubForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonRelay1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxRelayControl = new System.Windows.Forms.GroupBox();
            this.buttonRelay6 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.buttonRelay5 = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.buttonRelay4 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.buttonRelay3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonRelay2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelInputVoltage = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxSensors = new System.Windows.Forms.GroupBox();
            this.labelSQM = new System.Windows.Forms.Label();
            this.labelSQMTitle = new System.Windows.Forms.Label();
            this.labelSkyTemperatureTitle = new System.Windows.Forms.Label();
            this.labelExternalTemperature = new System.Windows.Forms.Label();
            this.labelSkyTemperature = new System.Windows.Forms.Label();
            this.labelExternalTemperatureTitle = new System.Windows.Forms.Label();
            this.labelSkyTemperatureDelta = new System.Windows.Forms.Label();
            this.labelSkyTemperatureDeltaTitle = new System.Windows.Forms.Label();
            this.labelPressure = new System.Windows.Forms.Label();
            this.labelHumidity = new System.Windows.Forms.Label();
            this.labelTemperature = new System.Windows.Forms.Label();
            this.labelPressureTitle = new System.Windows.Forms.Label();
            this.labelHumidityTitle = new System.Windows.Forms.Label();
            this.labelTemperatureTitle = new System.Windows.Forms.Label();
            this.labelStatusText = new System.Windows.Forms.Label();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.buttonSettings = new System.Windows.Forms.Button();
            this.serialLabel = new System.Windows.Forms.Label();
            this.firmwareLabel = new System.Windows.Forms.Label();
            this.titlePictureBox = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxRelayControl.SuspendLayout();
            this.groupBoxSensors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.titlePictureBox)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRelay1
            // 
            this.buttonRelay1.BackColor = System.Drawing.Color.Lime;
            this.buttonRelay1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonRelay1.Location = new System.Drawing.Point(432, 33);
            this.buttonRelay1.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRelay1.Name = "buttonRelay1";
            this.buttonRelay1.Size = new System.Drawing.Size(96, 45);
            this.buttonRelay1.TabIndex = 0;
            this.buttonRelay1.Text = "On";
            this.buttonRelay1.UseVisualStyleBackColor = true;
            this.buttonRelay1.Click += new System.EventHandler(this.buttonRelay1On_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Camera";
            // 
            // groupBoxRelayControl
            // 
            this.groupBoxRelayControl.Controls.Add(this.buttonRelay6);
            this.groupBoxRelayControl.Controls.Add(this.label13);
            this.groupBoxRelayControl.Controls.Add(this.label14);
            this.groupBoxRelayControl.Controls.Add(this.label15);
            this.groupBoxRelayControl.Controls.Add(this.label16);
            this.groupBoxRelayControl.Controls.Add(this.label17);
            this.groupBoxRelayControl.Controls.Add(this.buttonRelay5);
            this.groupBoxRelayControl.Controls.Add(this.label18);
            this.groupBoxRelayControl.Controls.Add(this.label19);
            this.groupBoxRelayControl.Controls.Add(this.label20);
            this.groupBoxRelayControl.Controls.Add(this.buttonRelay4);
            this.groupBoxRelayControl.Controls.Add(this.label10);
            this.groupBoxRelayControl.Controls.Add(this.label11);
            this.groupBoxRelayControl.Controls.Add(this.label12);
            this.groupBoxRelayControl.Controls.Add(this.buttonRelay3);
            this.groupBoxRelayControl.Controls.Add(this.label2);
            this.groupBoxRelayControl.Controls.Add(this.label8);
            this.groupBoxRelayControl.Controls.Add(this.label9);
            this.groupBoxRelayControl.Controls.Add(this.buttonRelay2);
            this.groupBoxRelayControl.Controls.Add(this.label7);
            this.groupBoxRelayControl.Controls.Add(this.label6);
            this.groupBoxRelayControl.Controls.Add(this.label1);
            this.groupBoxRelayControl.Controls.Add(this.buttonRelay1);
            this.groupBoxRelayControl.Location = new System.Drawing.Point(15, 170);
            this.groupBoxRelayControl.Margin = new System.Windows.Forms.Padding(15, 10, 2, 2);
            this.groupBoxRelayControl.Name = "groupBoxRelayControl";
            this.groupBoxRelayControl.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxRelayControl.Size = new System.Drawing.Size(568, 354);
            this.groupBoxRelayControl.TabIndex = 7;
            this.groupBoxRelayControl.TabStop = false;
            this.groupBoxRelayControl.Text = "Power Outputs";
            // 
            // buttonRelay6
            // 
            this.buttonRelay6.BackColor = System.Drawing.Color.Lime;
            this.buttonRelay6.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonRelay6.Location = new System.Drawing.Point(432, 293);
            this.buttonRelay6.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRelay6.Name = "buttonRelay6";
            this.buttonRelay6.Size = new System.Drawing.Size(96, 45);
            this.buttonRelay6.TabIndex = 37;
            this.buttonRelay6.Text = "Reset";
            this.buttonRelay6.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(11, 303);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(25, 25);
            this.label13.TabIndex = 36;
            this.label13.Text = "6";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(70, 303);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(96, 25);
            this.label14.TabIndex = 35;
            this.label14.Text = "Disabled";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.OrangeRed;
            this.label15.Location = new System.Drawing.Point(303, 251);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(75, 25);
            this.label15.TabIndex = 33;
            this.label15.Text = "OVER";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.OrangeRed;
            this.label16.Location = new System.Drawing.Point(11, 251);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(25, 25);
            this.label16.TabIndex = 32;
            this.label16.Text = "5";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.OrangeRed;
            this.label17.Location = new System.Drawing.Point(69, 251);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(90, 25);
            this.label17.TabIndex = 31;
            this.label17.Text = "Focuser";
            // 
            // buttonRelay5
            // 
            this.buttonRelay5.BackColor = System.Drawing.Color.Lime;
            this.buttonRelay5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonRelay5.Location = new System.Drawing.Point(432, 241);
            this.buttonRelay5.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRelay5.Name = "buttonRelay5";
            this.buttonRelay5.Size = new System.Drawing.Size(96, 45);
            this.buttonRelay5.TabIndex = 30;
            this.buttonRelay5.Text = "Reset";
            this.buttonRelay5.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(303, 199);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(62, 25);
            this.label18.TabIndex = 29;
            this.label18.Text = "1,5 A";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(11, 199);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(25, 25);
            this.label19.TabIndex = 28;
            this.label19.Text = "4";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(69, 199);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(187, 25);
            this.label20.TabIndex = 27;
            this.label20.Text = "Dew Heater Guide";
            // 
            // buttonRelay4
            // 
            this.buttonRelay4.BackColor = System.Drawing.Color.Lime;
            this.buttonRelay4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonRelay4.Location = new System.Drawing.Point(432, 189);
            this.buttonRelay4.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRelay4.Name = "buttonRelay4";
            this.buttonRelay4.Size = new System.Drawing.Size(96, 45);
            this.buttonRelay4.TabIndex = 26;
            this.buttonRelay4.Text = "On";
            this.buttonRelay4.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(303, 148);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 25);
            this.label10.TabIndex = 25;
            this.label10.Text = "0,0 A";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(11, 148);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 25);
            this.label11.TabIndex = 24;
            this.label11.Text = "3";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(69, 148);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(177, 25);
            this.label12.TabIndex = 23;
            this.label12.Text = "Dew Heater Main";
            // 
            // buttonRelay3
            // 
            this.buttonRelay3.BackColor = System.Drawing.Color.Lime;
            this.buttonRelay3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonRelay3.Location = new System.Drawing.Point(432, 137);
            this.buttonRelay3.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRelay3.Name = "buttonRelay3";
            this.buttonRelay3.Size = new System.Drawing.Size(96, 45);
            this.buttonRelay3.TabIndex = 22;
            this.buttonRelay3.Text = "25 %";
            this.buttonRelay3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(303, 96);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 25);
            this.label2.TabIndex = 21;
            this.label2.Text = "0,3 A";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(11, 96);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 25);
            this.label8.TabIndex = 20;
            this.label8.Text = "2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(69, 96);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(127, 25);
            this.label9.TabIndex = 19;
            this.label9.Text = "Filter Wheel";
            // 
            // buttonRelay2
            // 
            this.buttonRelay2.BackColor = System.Drawing.Color.Lime;
            this.buttonRelay2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonRelay2.Location = new System.Drawing.Point(432, 85);
            this.buttonRelay2.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRelay2.Name = "buttonRelay2";
            this.buttonRelay2.Size = new System.Drawing.Size(96, 45);
            this.buttonRelay2.TabIndex = 18;
            this.buttonRelay2.Text = "On";
            this.buttonRelay2.UseVisualStyleBackColor = true;
            this.buttonRelay2.Click += new System.EventHandler(this.buttonRelay2On_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(303, 44);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 25);
            this.label7.TabIndex = 17;
            this.label7.Text = "1,5 A";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(11, 44);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 25);
            this.label6.TabIndex = 16;
            this.label6.Text = "1";
            // 
            // labelInputVoltage
            // 
            this.labelInputVoltage.ForeColor = System.Drawing.Color.Red;
            this.labelInputVoltage.Location = new System.Drawing.Point(418, 150);
            this.labelInputVoltage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelInputVoltage.Name = "labelInputVoltage";
            this.labelInputVoltage.Size = new System.Drawing.Size(121, 25);
            this.labelInputVoltage.TabIndex = 11;
            this.labelInputVoltage.Text = "11,70 V";
            this.labelInputVoltage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(324, 150);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "Voltage";
            // 
            // groupBoxSensors
            // 
            this.groupBoxSensors.Controls.Add(this.labelSQM);
            this.groupBoxSensors.Controls.Add(this.labelInputVoltage);
            this.groupBoxSensors.Controls.Add(this.labelSQMTitle);
            this.groupBoxSensors.Controls.Add(this.labelSkyTemperatureTitle);
            this.groupBoxSensors.Controls.Add(this.label4);
            this.groupBoxSensors.Controls.Add(this.labelExternalTemperature);
            this.groupBoxSensors.Controls.Add(this.labelSkyTemperature);
            this.groupBoxSensors.Controls.Add(this.labelExternalTemperatureTitle);
            this.groupBoxSensors.Controls.Add(this.labelSkyTemperatureDelta);
            this.groupBoxSensors.Controls.Add(this.labelSkyTemperatureDeltaTitle);
            this.groupBoxSensors.Controls.Add(this.labelPressure);
            this.groupBoxSensors.Controls.Add(this.labelHumidity);
            this.groupBoxSensors.Controls.Add(this.labelTemperature);
            this.groupBoxSensors.Controls.Add(this.labelPressureTitle);
            this.groupBoxSensors.Controls.Add(this.labelHumidityTitle);
            this.groupBoxSensors.Controls.Add(this.labelTemperatureTitle);
            this.groupBoxSensors.Location = new System.Drawing.Point(15, 536);
            this.groupBoxSensors.Margin = new System.Windows.Forms.Padding(15, 10, 2, 2);
            this.groupBoxSensors.Name = "groupBoxSensors";
            this.groupBoxSensors.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxSensors.Size = new System.Drawing.Size(568, 200);
            this.groupBoxSensors.TabIndex = 9;
            this.groupBoxSensors.TabStop = false;
            this.groupBoxSensors.Text = "Sensors";
            // 
            // labelSQM
            // 
            this.labelSQM.Location = new System.Drawing.Point(434, 115);
            this.labelSQM.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSQM.Name = "labelSQM";
            this.labelSQM.Size = new System.Drawing.Size(84, 25);
            this.labelSQM.TabIndex = 28;
            this.labelSQM.Text = "21,57";
            this.labelSQM.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSQMTitle
            // 
            this.labelSQMTitle.AutoSize = true;
            this.labelSQMTitle.Location = new System.Drawing.Point(324, 115);
            this.labelSQMTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSQMTitle.Name = "labelSQMTitle";
            this.labelSQMTitle.Size = new System.Drawing.Size(60, 25);
            this.labelSQMTitle.TabIndex = 27;
            this.labelSQMTitle.Text = "SQM";
            // 
            // labelSkyTemperatureTitle
            // 
            this.labelSkyTemperatureTitle.AutoSize = true;
            this.labelSkyTemperatureTitle.Location = new System.Drawing.Point(324, 80);
            this.labelSkyTemperatureTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSkyTemperatureTitle.Name = "labelSkyTemperatureTitle";
            this.labelSkyTemperatureTitle.Size = new System.Drawing.Size(108, 25);
            this.labelSkyTemperatureTitle.TabIndex = 24;
            this.labelSkyTemperatureTitle.Text = "Sky Temp";
            // 
            // labelExternalTemperature
            // 
            this.labelExternalTemperature.Location = new System.Drawing.Point(173, 150);
            this.labelExternalTemperature.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelExternalTemperature.Name = "labelExternalTemperature";
            this.labelExternalTemperature.Size = new System.Drawing.Size(102, 25);
            this.labelExternalTemperature.TabIndex = 23;
            this.labelExternalTemperature.Text = "+5,5 °C";
            this.labelExternalTemperature.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSkyTemperature
            // 
            this.labelSkyTemperature.Location = new System.Drawing.Point(436, 80);
            this.labelSkyTemperature.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSkyTemperature.Name = "labelSkyTemperature";
            this.labelSkyTemperature.Size = new System.Drawing.Size(103, 25);
            this.labelSkyTemperature.TabIndex = 22;
            this.labelSkyTemperature.Text = "-25,3 °C";
            this.labelSkyTemperature.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelExternalTemperatureTitle
            // 
            this.labelExternalTemperatureTitle.AutoSize = true;
            this.labelExternalTemperatureTitle.Location = new System.Drawing.Point(21, 150);
            this.labelExternalTemperatureTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelExternalTemperatureTitle.Name = "labelExternalTemperatureTitle";
            this.labelExternalTemperatureTitle.Size = new System.Drawing.Size(103, 25);
            this.labelExternalTemperatureTitle.TabIndex = 21;
            this.labelExternalTemperatureTitle.Text = "Ext Temp";
            this.labelExternalTemperatureTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelSkyTemperatureDelta
            // 
            this.labelSkyTemperatureDelta.Location = new System.Drawing.Point(436, 45);
            this.labelSkyTemperatureDelta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSkyTemperatureDelta.Name = "labelSkyTemperatureDelta";
            this.labelSkyTemperatureDelta.Size = new System.Drawing.Size(103, 35);
            this.labelSkyTemperatureDelta.TabIndex = 20;
            this.labelSkyTemperatureDelta.Text = "-20,3 °C";
            this.labelSkyTemperatureDelta.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSkyTemperatureDeltaTitle
            // 
            this.labelSkyTemperatureDeltaTitle.AutoSize = true;
            this.labelSkyTemperatureDeltaTitle.Location = new System.Drawing.Point(324, 45);
            this.labelSkyTemperatureDeltaTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSkyTemperatureDeltaTitle.Name = "labelSkyTemperatureDeltaTitle";
            this.labelSkyTemperatureDeltaTitle.Size = new System.Drawing.Size(68, 25);
            this.labelSkyTemperatureDeltaTitle.TabIndex = 19;
            this.labelSkyTemperatureDeltaTitle.Text = "Sky Δ";
            // 
            // labelPressure
            // 
            this.labelPressure.Location = new System.Drawing.Point(142, 115);
            this.labelPressure.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPressure.Name = "labelPressure";
            this.labelPressure.Size = new System.Drawing.Size(151, 25);
            this.labelPressure.TabIndex = 18;
            this.labelPressure.Text = "1001 hPa";
            this.labelPressure.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelHumidity
            // 
            this.labelHumidity.Location = new System.Drawing.Point(172, 80);
            this.labelHumidity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelHumidity.Name = "labelHumidity";
            this.labelHumidity.Size = new System.Drawing.Size(100, 25);
            this.labelHumidity.TabIndex = 17;
            this.labelHumidity.Text = "75 %";
            this.labelHumidity.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelTemperature
            // 
            this.labelTemperature.Location = new System.Drawing.Point(173, 45);
            this.labelTemperature.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTemperature.Name = "labelTemperature";
            this.labelTemperature.Size = new System.Drawing.Size(102, 25);
            this.labelTemperature.TabIndex = 16;
            this.labelTemperature.Text = "+3,5 °C";
            this.labelTemperature.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelPressureTitle
            // 
            this.labelPressureTitle.AutoSize = true;
            this.labelPressureTitle.Location = new System.Drawing.Point(21, 115);
            this.labelPressureTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPressureTitle.Name = "labelPressureTitle";
            this.labelPressureTitle.Size = new System.Drawing.Size(98, 25);
            this.labelPressureTitle.TabIndex = 15;
            this.labelPressureTitle.Text = "Pressure";
            // 
            // labelHumidityTitle
            // 
            this.labelHumidityTitle.AutoSize = true;
            this.labelHumidityTitle.Location = new System.Drawing.Point(21, 80);
            this.labelHumidityTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelHumidityTitle.Name = "labelHumidityTitle";
            this.labelHumidityTitle.Size = new System.Drawing.Size(95, 25);
            this.labelHumidityTitle.TabIndex = 14;
            this.labelHumidityTitle.Text = "Humidity";
            // 
            // labelTemperatureTitle
            // 
            this.labelTemperatureTitle.AutoSize = true;
            this.labelTemperatureTitle.Location = new System.Drawing.Point(21, 45);
            this.labelTemperatureTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTemperatureTitle.Name = "labelTemperatureTitle";
            this.labelTemperatureTitle.Size = new System.Drawing.Size(134, 25);
            this.labelTemperatureTitle.TabIndex = 13;
            this.labelTemperatureTitle.Text = "Temperature";
            // 
            // labelStatusText
            // 
            this.labelStatusText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelStatusText.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelStatusText.Location = new System.Drawing.Point(136, 756);
            this.labelStatusText.Margin = new System.Windows.Forms.Padding(2, 10, 2, 0);
            this.labelStatusText.Name = "labelStatusText";
            this.labelStatusText.Size = new System.Drawing.Size(447, 28);
            this.labelStatusText.TabIndex = 10;
            this.labelStatusText.Text = "Device Connected";
            this.labelStatusText.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonSettings
            // 
            this.buttonSettings.Location = new System.Drawing.Point(15, 748);
            this.buttonSettings.Margin = new System.Windows.Forms.Padding(15, 10, 3, 3);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(116, 41);
            this.buttonSettings.TabIndex = 11;
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // serialLabel
            // 
            this.serialLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.serialLabel.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.SetFlowBreak(this.serialLabel, true);
            this.serialLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.serialLabel.Location = new System.Drawing.Point(2, 797);
            this.serialLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.serialLabel.Name = "serialLabel";
            this.serialLabel.Size = new System.Drawing.Size(182, 25);
            this.serialLabel.TabIndex = 39;
            this.serialLabel.Text = "Device #000";
            this.serialLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // firmwareLabel
            // 
            this.firmwareLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.firmwareLabel.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.SetFlowBreak(this.firmwareLabel, true);
            this.firmwareLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.firmwareLabel.Location = new System.Drawing.Point(2, 827);
            this.firmwareLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.firmwareLabel.Name = "firmwareLabel";
            this.firmwareLabel.Size = new System.Drawing.Size(188, 35);
            this.firmwareLabel.TabIndex = 38;
            this.firmwareLabel.Text = "Firmware v0.0";
            this.firmwareLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // titlePictureBox
            // 
            this.titlePictureBox.BackgroundImage = global::KomaHub.Properties.Resources.komahub;
            this.titlePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.titlePictureBox.Location = new System.Drawing.Point(0, 0);
            this.titlePictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.titlePictureBox.Name = "titlePictureBox";
            this.titlePictureBox.Size = new System.Drawing.Size(600, 160);
            this.titlePictureBox.TabIndex = 40;
            this.titlePictureBox.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.titlePictureBox);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxRelayControl);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxSensors);
            this.flowLayoutPanel1.Controls.Add(this.buttonSettings);
            this.flowLayoutPanel1.Controls.Add(this.labelStatusText);
            this.flowLayoutPanel1.Controls.Add(this.serialLabel);
            this.flowLayoutPanel1.Controls.Add(this.firmwareLabel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(594, 845);
            this.flowLayoutPanel1.TabIndex = 41;
            // 
            // KomaHubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 845);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "KomaHubForm";
            this.Text = "KomaHub";
            this.Load += new System.EventHandler(this.KomaHubForm_Load);
            this.groupBoxRelayControl.ResumeLayout(false);
            this.groupBoxRelayControl.PerformLayout();
            this.groupBoxSensors.ResumeLayout(false);
            this.groupBoxSensors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.titlePictureBox)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRelay1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxRelayControl;
        private System.Windows.Forms.Label labelInputVoltage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBoxSensors;
        private System.Windows.Forms.Label labelSkyTemperatureDeltaTitle;
        private System.Windows.Forms.Label labelPressure;
        private System.Windows.Forms.Label labelHumidity;
        private System.Windows.Forms.Label labelTemperature;
        private System.Windows.Forms.Label labelPressureTitle;
        private System.Windows.Forms.Label labelHumidityTitle;
        private System.Windows.Forms.Label labelTemperatureTitle;
        private System.Windows.Forms.Label labelSkyTemperature;
        private System.Windows.Forms.Label labelExternalTemperatureTitle;
        private System.Windows.Forms.Label labelSkyTemperatureDelta;
        private System.Windows.Forms.Label labelStatusText;
        private System.Windows.Forms.Label labelSQM;
        private System.Windows.Forms.Label labelSQMTitle;
        private System.Windows.Forms.Label labelSkyTemperatureTitle;
        private System.Windows.Forms.Label labelExternalTemperature;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button buttonRelay3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonRelay2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button buttonRelay5;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button buttonRelay4;
        private System.Windows.Forms.Button buttonRelay6;
        private System.Windows.Forms.Label serialLabel;
        private System.Windows.Forms.Label firmwareLabel;
        private System.Windows.Forms.PictureBox titlePictureBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

