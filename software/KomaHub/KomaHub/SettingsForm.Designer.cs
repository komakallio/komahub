namespace KomaHub
{
    partial class SettingsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxSkyTemperature = new System.Windows.Forms.CheckBox();
            this.checkBoxAmbientPTH = new System.Windows.Forms.CheckBox();
            this.checkBoxSkyQuality = new System.Windows.Forms.CheckBox();
            this.checkBoxExtTemperature = new System.Windows.Forms.CheckBox();
            this.textBoxSQMOffset = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.output6Name = new System.Windows.Forms.TextBox();
            this.output5Name = new System.Windows.Forms.TextBox();
            this.output4Name = new System.Windows.Forms.TextBox();
            this.output3Name = new System.Windows.Forms.TextBox();
            this.output2Name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.output1Name = new System.Windows.Forms.TextBox();
            this.output6Fuse = new System.Windows.Forms.ComboBox();
            this.output6Type = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.output5Fuse = new System.Windows.Forms.ComboBox();
            this.output5Type = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.output4Fuse = new System.Windows.Forms.ComboBox();
            this.output4Type = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.output3Fuse = new System.Windows.Forms.ComboBox();
            this.output3Type = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.output2Fuse = new System.Windows.Forms.ComboBox();
            this.output2Type = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.output1Fuse = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.output1Type = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.fuseDelay = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxSkyTemperature);
            this.groupBox1.Controls.Add(this.checkBoxAmbientPTH);
            this.groupBox1.Controls.Add(this.checkBoxSkyQuality);
            this.groupBox1.Controls.Add(this.checkBoxExtTemperature);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 407);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(563, 121);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connected sensors";
            // 
            // checkBoxSkyTemperature
            // 
            this.checkBoxSkyTemperature.AutoSize = true;
            this.checkBoxSkyTemperature.Location = new System.Drawing.Point(310, 71);
            this.checkBoxSkyTemperature.Name = "checkBoxSkyTemperature";
            this.checkBoxSkyTemperature.Size = new System.Drawing.Size(223, 36);
            this.checkBoxSkyTemperature.TabIndex = 7;
            this.checkBoxSkyTemperature.Text = "Sky temperature";
            this.checkBoxSkyTemperature.UseVisualStyleBackColor = true;
            // 
            // checkBoxAmbientPTH
            // 
            this.checkBoxAmbientPTH.AutoSize = true;
            this.checkBoxAmbientPTH.Location = new System.Drawing.Point(310, 36);
            this.checkBoxAmbientPTH.Name = "checkBoxAmbientPTH";
            this.checkBoxAmbientPTH.Size = new System.Drawing.Size(188, 36);
            this.checkBoxAmbientPTH.TabIndex = 6;
            this.checkBoxAmbientPTH.Text = "Ambient PTH";
            this.checkBoxAmbientPTH.UseVisualStyleBackColor = true;
            // 
            // checkBoxSkyQuality
            // 
            this.checkBoxSkyQuality.AutoSize = true;
            this.checkBoxSkyQuality.Location = new System.Drawing.Point(26, 71);
            this.checkBoxSkyQuality.Name = "checkBoxSkyQuality";
            this.checkBoxSkyQuality.Size = new System.Drawing.Size(239, 36);
            this.checkBoxSkyQuality.TabIndex = 5;
            this.checkBoxSkyQuality.Text = "Sky quality sensor";
            this.checkBoxSkyQuality.UseVisualStyleBackColor = true;
            // 
            // checkBoxExtTemperature
            // 
            this.checkBoxExtTemperature.AutoSize = true;
            this.checkBoxExtTemperature.Location = new System.Drawing.Point(26, 36);
            this.checkBoxExtTemperature.Name = "checkBoxExtTemperature";
            this.checkBoxExtTemperature.Size = new System.Drawing.Size(276, 36);
            this.checkBoxExtTemperature.TabIndex = 4;
            this.checkBoxExtTemperature.Text = "Temperature probe(s)";
            this.checkBoxExtTemperature.UseVisualStyleBackColor = true;
            // 
            // textBoxSQMOffset
            // 
            this.textBoxSQMOffset.Location = new System.Drawing.Point(306, 30);
            this.textBoxSQMOffset.Name = "textBoxSQMOffset";
            this.textBoxSQMOffset.Size = new System.Drawing.Size(121, 39);
            this.textBoxSQMOffset.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(254, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "SQM calibration offset";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(277, 716);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(141, 46);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(434, 716);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(141, 46);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.output6Name);
            this.groupBox2.Controls.Add(this.output5Name);
            this.groupBox2.Controls.Add(this.output4Name);
            this.groupBox2.Controls.Add(this.output3Name);
            this.groupBox2.Controls.Add(this.output2Name);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.output1Name);
            this.groupBox2.Controls.Add(this.output6Fuse);
            this.groupBox2.Controls.Add(this.output6Type);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.output5Fuse);
            this.groupBox2.Controls.Add(this.output5Type);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.output4Fuse);
            this.groupBox2.Controls.Add(this.output4Type);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.output3Fuse);
            this.groupBox2.Controls.Add(this.output3Type);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.output2Fuse);
            this.groupBox2.Controls.Add(this.output2Type);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.output1Fuse);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.output1Type);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(563, 375);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Power Outputs";
            // 
            // output6Name
            // 
            this.output6Name.Location = new System.Drawing.Point(61, 316);
            this.output6Name.Name = "output6Name";
            this.output6Name.Size = new System.Drawing.Size(220, 39);
            this.output6Name.TabIndex = 37;
            // 
            // output5Name
            // 
            this.output5Name.Location = new System.Drawing.Point(61, 268);
            this.output5Name.Name = "output5Name";
            this.output5Name.Size = new System.Drawing.Size(220, 39);
            this.output5Name.TabIndex = 36;
            // 
            // output4Name
            // 
            this.output4Name.Location = new System.Drawing.Point(61, 220);
            this.output4Name.Name = "output4Name";
            this.output4Name.Size = new System.Drawing.Size(220, 39);
            this.output4Name.TabIndex = 35;
            // 
            // output3Name
            // 
            this.output3Name.Location = new System.Drawing.Point(61, 172);
            this.output3Name.Name = "output3Name";
            this.output3Name.Size = new System.Drawing.Size(220, 39);
            this.output3Name.TabIndex = 34;
            // 
            // output2Name
            // 
            this.output2Name.Location = new System.Drawing.Point(61, 122);
            this.output2Name.Name = "output2Name";
            this.output2Name.Size = new System.Drawing.Size(220, 39);
            this.output2Name.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 32);
            this.label4.TabIndex = 32;
            this.label4.Text = "Name";
            // 
            // output1Name
            // 
            this.output1Name.Location = new System.Drawing.Point(61, 73);
            this.output1Name.Name = "output1Name";
            this.output1Name.Size = new System.Drawing.Size(220, 39);
            this.output1Name.TabIndex = 31;
            // 
            // output6Fuse
            // 
            this.output6Fuse.FormattingEnabled = true;
            this.output6Fuse.Items.AddRange(new object[] {
            "None",
            "1.0 A",
            "2.0 A",
            "3.0 A",
            "4.0 A",
            "5.0 A",
            "6.0 A",
            "7.0 A",
            "8.0 A",
            "9.0 A",
            "10.0 A"});
            this.output6Fuse.Location = new System.Drawing.Point(437, 314);
            this.output6Fuse.Name = "output6Fuse";
            this.output6Fuse.Size = new System.Drawing.Size(107, 40);
            this.output6Fuse.TabIndex = 30;
            // 
            // output6Type
            // 
            this.output6Type.FormattingEnabled = true;
            this.output6Type.Items.AddRange(new object[] {
            "OFF",
            "12V",
            "PWM"});
            this.output6Type.Location = new System.Drawing.Point(310, 314);
            this.output6Type.Name = "output6Type";
            this.output6Type.Size = new System.Drawing.Size(99, 40);
            this.output6Type.TabIndex = 28;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(21, 321);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 32);
            this.label12.TabIndex = 27;
            this.label12.Text = "6";
            // 
            // output5Fuse
            // 
            this.output5Fuse.FormattingEnabled = true;
            this.output5Fuse.Items.AddRange(new object[] {
            "None",
            "1.0 A",
            "2.0 A",
            "3.0 A",
            "4.0 A",
            "5.0 A",
            "6.0 A",
            "7.0 A",
            "8.0 A",
            "9.0 A",
            "10.0 A"});
            this.output5Fuse.Location = new System.Drawing.Point(437, 266);
            this.output5Fuse.Name = "output5Fuse";
            this.output5Fuse.Size = new System.Drawing.Size(107, 40);
            this.output5Fuse.TabIndex = 26;
            // 
            // output5Type
            // 
            this.output5Type.FormattingEnabled = true;
            this.output5Type.Items.AddRange(new object[] {
            "OFF",
            "12V",
            "PWM"});
            this.output5Type.Location = new System.Drawing.Point(310, 266);
            this.output5Type.Name = "output5Type";
            this.output5Type.Size = new System.Drawing.Size(99, 40);
            this.output5Type.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(21, 273);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 32);
            this.label11.TabIndex = 23;
            this.label11.Text = "5";
            // 
            // output4Fuse
            // 
            this.output4Fuse.FormattingEnabled = true;
            this.output4Fuse.Items.AddRange(new object[] {
            "None",
            "1.0 A",
            "2.0 A",
            "3.0 A",
            "4.0 A",
            "5.0 A",
            "6.0 A",
            "7.0 A",
            "8.0 A",
            "9.0 A",
            "10.0 A"});
            this.output4Fuse.Location = new System.Drawing.Point(437, 218);
            this.output4Fuse.Name = "output4Fuse";
            this.output4Fuse.Size = new System.Drawing.Size(107, 40);
            this.output4Fuse.TabIndex = 22;
            // 
            // output4Type
            // 
            this.output4Type.FormattingEnabled = true;
            this.output4Type.Items.AddRange(new object[] {
            "OFF",
            "12V",
            "PWM"});
            this.output4Type.Location = new System.Drawing.Point(310, 218);
            this.output4Type.Name = "output4Type";
            this.output4Type.Size = new System.Drawing.Size(99, 40);
            this.output4Type.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(21, 225);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 32);
            this.label10.TabIndex = 19;
            this.label10.Text = "4";
            // 
            // output3Fuse
            // 
            this.output3Fuse.FormattingEnabled = true;
            this.output3Fuse.Items.AddRange(new object[] {
            "None",
            "1.0 A",
            "2.0 A",
            "3.0 A",
            "4.0 A",
            "5.0 A",
            "6.0 A",
            "7.0 A",
            "8.0 A",
            "9.0 A",
            "10.0 A"});
            this.output3Fuse.Location = new System.Drawing.Point(437, 170);
            this.output3Fuse.Name = "output3Fuse";
            this.output3Fuse.Size = new System.Drawing.Size(107, 40);
            this.output3Fuse.TabIndex = 18;
            // 
            // output3Type
            // 
            this.output3Type.FormattingEnabled = true;
            this.output3Type.Items.AddRange(new object[] {
            "OFF",
            "12V",
            "PWM"});
            this.output3Type.Location = new System.Drawing.Point(310, 170);
            this.output3Type.Name = "output3Type";
            this.output3Type.Size = new System.Drawing.Size(99, 40);
            this.output3Type.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(21, 177);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 32);
            this.label9.TabIndex = 15;
            this.label9.Text = "3";
            // 
            // output2Fuse
            // 
            this.output2Fuse.FormattingEnabled = true;
            this.output2Fuse.Items.AddRange(new object[] {
            "None",
            "1.0 A",
            "2.0 A",
            "3.0 A",
            "4.0 A",
            "5.0 A",
            "6.0 A",
            "7.0 A",
            "8.0 A",
            "9.0 A",
            "10.0 A"});
            this.output2Fuse.Location = new System.Drawing.Point(437, 120);
            this.output2Fuse.Name = "output2Fuse";
            this.output2Fuse.Size = new System.Drawing.Size(107, 40);
            this.output2Fuse.TabIndex = 14;
            // 
            // output2Type
            // 
            this.output2Type.FormattingEnabled = true;
            this.output2Type.Items.AddRange(new object[] {
            "OFF",
            "12V",
            "PWM"});
            this.output2Type.Location = new System.Drawing.Point(310, 120);
            this.output2Type.Name = "output2Type";
            this.output2Type.Size = new System.Drawing.Size(99, 40);
            this.output2Type.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(21, 127);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 32);
            this.label8.TabIndex = 11;
            this.label8.Text = "2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 32);
            this.label7.TabIndex = 10;
            this.label7.Text = "#";
            // 
            // output1Fuse
            // 
            this.output1Fuse.FormattingEnabled = true;
            this.output1Fuse.Items.AddRange(new object[] {
            "None",
            "1.0 A",
            "2.0 A",
            "3.0 A",
            "4.0 A",
            "5.0 A",
            "6.0 A",
            "7.0 A",
            "8.0 A",
            "9.0 A",
            "10.0 A"});
            this.output1Fuse.Location = new System.Drawing.Point(437, 71);
            this.output1Fuse.Name = "output1Fuse";
            this.output1Fuse.Size = new System.Drawing.Size(107, 40);
            this.output1Fuse.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(432, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 32);
            this.label6.TabIndex = 8;
            this.label6.Text = "Fuse";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(305, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 32);
            this.label5.TabIndex = 7;
            this.label5.Text = "Type";
            // 
            // output1Type
            // 
            this.output1Type.FormattingEnabled = true;
            this.output1Type.Items.AddRange(new object[] {
            "OFF",
            "12V",
            "PWM"});
            this.output1Type.Location = new System.Drawing.Point(310, 71);
            this.output1Type.Name = "output1Type";
            this.output1Type.Size = new System.Drawing.Size(99, 40);
            this.output1Type.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 32);
            this.label3.TabIndex = 4;
            this.label3.Text = "1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.fuseDelay);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.textBoxSQMOffset);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 553);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(563, 129);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Advanced";
            // 
            // fuseDelay
            // 
            this.fuseDelay.FormattingEnabled = true;
            this.fuseDelay.Items.AddRange(new object[] {
            "100 ms",
            "200 ms",
            "300 ms",
            "400 ms",
            "500 ms"});
            this.fuseDelay.Location = new System.Drawing.Point(306, 76);
            this.fuseDelay.Name = "fuseDelay";
            this.fuseDelay.Size = new System.Drawing.Size(121, 40);
            this.fuseDelay.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 76);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(271, 32);
            this.label13.TabIndex = 4;
            this.label13.Text = "Fuse tripping delay (ms)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(444, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 32);
            this.label1.TabIndex = 6;
            this.label1.Text = "(def: 21)";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 774);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox1);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxSQMOffset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox output1Type;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox output1Fuse;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox output6Fuse;
        private System.Windows.Forms.ComboBox output6Type;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox output5Fuse;
        private System.Windows.Forms.ComboBox output5Type;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox output4Fuse;
        private System.Windows.Forms.ComboBox output4Type;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox output3Fuse;
        private System.Windows.Forms.ComboBox output3Type;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox output2Fuse;
        private System.Windows.Forms.ComboBox output2Type;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBoxSkyTemperature;
        private System.Windows.Forms.CheckBox checkBoxAmbientPTH;
        private System.Windows.Forms.CheckBox checkBoxSkyQuality;
        private System.Windows.Forms.CheckBox checkBoxExtTemperature;
        private System.Windows.Forms.TextBox output6Name;
        private System.Windows.Forms.TextBox output5Name;
        private System.Windows.Forms.TextBox output4Name;
        private System.Windows.Forms.TextBox output3Name;
        private System.Windows.Forms.TextBox output2Name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox output1Name;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox fuseDelay;
        private System.Windows.Forms.Label label1;
    }
}