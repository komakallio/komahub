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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KomaHubForm));
            this.buttonRelay1 = new System.Windows.Forms.Button();
            this.output1Name = new System.Windows.Forms.Label();
            this.groupBoxRelayControl = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pwmDuty6 = new System.Windows.Forms.DomainUpDown();
            this.pwmDuty5 = new System.Windows.Forms.DomainUpDown();
            this.pwmDuty4 = new System.Windows.Forms.DomainUpDown();
            this.pwmDuty3 = new System.Windows.Forms.DomainUpDown();
            this.pwmDuty2 = new System.Windows.Forms.DomainUpDown();
            this.output1Number = new System.Windows.Forms.Label();
            this.output3Current = new System.Windows.Forms.Label();
            this.output4Current = new System.Windows.Forms.Label();
            this.output2Current = new System.Windows.Forms.Label();
            this.output5Current = new System.Windows.Forms.Label();
            this.output6Number = new System.Windows.Forms.Label();
            this.output5Name = new System.Windows.Forms.Label();
            this.output5Number = new System.Windows.Forms.Label();
            this.output6Current = new System.Windows.Forms.Label();
            this.output4Name = new System.Windows.Forms.Label();
            this.output4Number = new System.Windows.Forms.Label();
            this.output6Name = new System.Windows.Forms.Label();
            this.output3Name = new System.Windows.Forms.Label();
            this.output3Number = new System.Windows.Forms.Label();
            this.output2Name = new System.Windows.Forms.Label();
            this.output2Number = new System.Windows.Forms.Label();
            this.buttonRelay6 = new System.Windows.Forms.Button();
            this.output1Current = new System.Windows.Forms.Label();
            this.buttonRelay2 = new System.Windows.Forms.Button();
            this.buttonRelay3 = new System.Windows.Forms.Button();
            this.buttonRelay4 = new System.Windows.Forms.Button();
            this.buttonRelay5 = new System.Windows.Forms.Button();
            this.pwmDuty1 = new System.Windows.Forms.DomainUpDown();
            this.labelInputVoltage = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxSensors = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelTemperatureTitle = new System.Windows.Forms.Label();
            this.labelSkyTemperatureDelta = new System.Windows.Forms.Label();
            this.labelSkyTemperature = new System.Windows.Forms.Label();
            this.labelSQM = new System.Windows.Forms.Label();
            this.labelHumidityTitle = new System.Windows.Forms.Label();
            this.labelPressureTitle = new System.Windows.Forms.Label();
            this.labelSQMTitle = new System.Windows.Forms.Label();
            this.labelExternalTemperatureTitle = new System.Windows.Forms.Label();
            this.labelSkyTemperatureTitle = new System.Windows.Forms.Label();
            this.labelExternalTemperature = new System.Windows.Forms.Label();
            this.labelPressure = new System.Windows.Forms.Label();
            this.labelHumidity = new System.Windows.Forms.Label();
            this.labelTemperature = new System.Windows.Forms.Label();
            this.labelSkyTemperatureDeltaTitle = new System.Windows.Forms.Label();
            this.labelStatusText = new System.Windows.Forms.Label();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.serialLabel = new System.Windows.Forms.Label();
            this.firmwareLabel = new System.Windows.Forms.Label();
            this.titlePictureBox = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxRelayControl.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxSensors.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.titlePictureBox)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRelay1
            // 
            this.buttonRelay1.BackColor = System.Drawing.Color.Lime;
            this.buttonRelay1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonRelay1.Location = new System.Drawing.Point(639, 7);
            this.buttonRelay1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.buttonRelay1.Name = "buttonRelay1";
            this.buttonRelay1.Size = new System.Drawing.Size(122, 41);
            this.buttonRelay1.TabIndex = 0;
            this.buttonRelay1.Text = "On";
            this.buttonRelay1.UseVisualStyleBackColor = true;
            this.buttonRelay1.Click += new System.EventHandler(this.buttonRelay1On_Click);
            // 
            // output1Name
            // 
            this.output1Name.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output1Name.AutoSize = true;
            this.output1Name.Location = new System.Drawing.Point(59, 13);
            this.output1Name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.output1Name.Name = "output1Name";
            this.output1Name.Size = new System.Drawing.Size(96, 32);
            this.output1Name.TabIndex = 2;
            this.output1Name.Text = "Camera";
            // 
            // groupBoxRelayControl
            // 
            this.groupBoxRelayControl.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxRelayControl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxRelayControl.Location = new System.Drawing.Point(20, 240);
            this.groupBoxRelayControl.Margin = new System.Windows.Forms.Padding(20, 20, 4, 4);
            this.groupBoxRelayControl.Name = "groupBoxRelayControl";
            this.groupBoxRelayControl.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxRelayControl.Size = new System.Drawing.Size(781, 349);
            this.groupBoxRelayControl.TabIndex = 7;
            this.groupBoxRelayControl.TabStop = false;
            this.groupBoxRelayControl.Text = "Power Outputs";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.675973F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.27043F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.02792F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.01284F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.01284F));
            this.tableLayoutPanel1.Controls.Add(this.pwmDuty6, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.pwmDuty5, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.pwmDuty4, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.pwmDuty3, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.pwmDuty2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.output1Number, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.output3Current, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.output4Current, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.output2Current, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.output5Current, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.output6Number, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.output5Name, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.output5Number, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.output6Current, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.output4Name, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.output4Number, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.output6Name, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.output3Name, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.output3Number, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.output1Name, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.output2Name, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.output2Number, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonRelay6, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.output1Current, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonRelay1, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonRelay2, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonRelay3, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonRelay4, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.buttonRelay5, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.pwmDuty1, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 36);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(773, 309);
            this.tableLayoutPanel1.TabIndex = 39;
            // 
            // pwmDuty6
            // 
            this.pwmDuty6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwmDuty6.AutoSize = true;
            this.pwmDuty6.Items.Add("100 %");
            this.pwmDuty6.Items.Add("95 %");
            this.pwmDuty6.Items.Add("90 %");
            this.pwmDuty6.Items.Add("85 %");
            this.pwmDuty6.Items.Add("80 %");
            this.pwmDuty6.Items.Add("75 %");
            this.pwmDuty6.Items.Add("70 %");
            this.pwmDuty6.Items.Add("65 %");
            this.pwmDuty6.Items.Add("60 %");
            this.pwmDuty6.Items.Add("55 %");
            this.pwmDuty6.Items.Add("50 %");
            this.pwmDuty6.Items.Add("45 %");
            this.pwmDuty6.Items.Add("40 %");
            this.pwmDuty6.Items.Add("35 %");
            this.pwmDuty6.Items.Add("30 %");
            this.pwmDuty6.Items.Add("25 %");
            this.pwmDuty6.Items.Add("20 %");
            this.pwmDuty6.Items.Add("15 %");
            this.pwmDuty6.Items.Add("10 %");
            this.pwmDuty6.Items.Add("5 %");
            this.pwmDuty6.Location = new System.Drawing.Point(509, 257);
            this.pwmDuty6.Name = "pwmDuty6";
            this.pwmDuty6.Size = new System.Drawing.Size(113, 39);
            this.pwmDuty6.TabIndex = 44;
            this.pwmDuty6.SelectedItemChanged += new System.EventHandler(this.pwmDuty6_SelectedItemChanged);
            // 
            // pwmDuty5
            // 
            this.pwmDuty5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwmDuty5.AutoSize = true;
            this.pwmDuty5.Items.Add("100 %");
            this.pwmDuty5.Items.Add("95 %");
            this.pwmDuty5.Items.Add("90 %");
            this.pwmDuty5.Items.Add("85 %");
            this.pwmDuty5.Items.Add("80 %");
            this.pwmDuty5.Items.Add("75 %");
            this.pwmDuty5.Items.Add("70 %");
            this.pwmDuty5.Items.Add("65 %");
            this.pwmDuty5.Items.Add("60 %");
            this.pwmDuty5.Items.Add("55 %");
            this.pwmDuty5.Items.Add("50 %");
            this.pwmDuty5.Items.Add("45 %");
            this.pwmDuty5.Items.Add("40 %");
            this.pwmDuty5.Items.Add("35 %");
            this.pwmDuty5.Items.Add("30 %");
            this.pwmDuty5.Items.Add("25 %");
            this.pwmDuty5.Items.Add("20 %");
            this.pwmDuty5.Items.Add("15 %");
            this.pwmDuty5.Items.Add("10 %");
            this.pwmDuty5.Items.Add("5 %");
            this.pwmDuty5.Location = new System.Drawing.Point(509, 206);
            this.pwmDuty5.Name = "pwmDuty5";
            this.pwmDuty5.Size = new System.Drawing.Size(113, 39);
            this.pwmDuty5.TabIndex = 43;
            this.pwmDuty5.SelectedItemChanged += new System.EventHandler(this.pwmDuty5_SelectedItemChanged);
            // 
            // pwmDuty4
            // 
            this.pwmDuty4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwmDuty4.AutoSize = true;
            this.pwmDuty4.Items.Add("100 %");
            this.pwmDuty4.Items.Add("95 %");
            this.pwmDuty4.Items.Add("90 %");
            this.pwmDuty4.Items.Add("85 %");
            this.pwmDuty4.Items.Add("80 %");
            this.pwmDuty4.Items.Add("75 %");
            this.pwmDuty4.Items.Add("70 %");
            this.pwmDuty4.Items.Add("65 %");
            this.pwmDuty4.Items.Add("60 %");
            this.pwmDuty4.Items.Add("55 %");
            this.pwmDuty4.Items.Add("50 %");
            this.pwmDuty4.Items.Add("45 %");
            this.pwmDuty4.Items.Add("40 %");
            this.pwmDuty4.Items.Add("35 %");
            this.pwmDuty4.Items.Add("30 %");
            this.pwmDuty4.Items.Add("25 %");
            this.pwmDuty4.Items.Add("20 %");
            this.pwmDuty4.Items.Add("15 %");
            this.pwmDuty4.Items.Add("10 %");
            this.pwmDuty4.Items.Add("5 %");
            this.pwmDuty4.Location = new System.Drawing.Point(509, 157);
            this.pwmDuty4.Name = "pwmDuty4";
            this.pwmDuty4.Size = new System.Drawing.Size(113, 39);
            this.pwmDuty4.TabIndex = 42;
            this.pwmDuty4.SelectedItemChanged += new System.EventHandler(this.pwmDuty4_SelectedItemChanged);
            // 
            // pwmDuty3
            // 
            this.pwmDuty3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwmDuty3.AutoSize = true;
            this.pwmDuty3.Items.Add("100 %");
            this.pwmDuty3.Items.Add("95 %");
            this.pwmDuty3.Items.Add("90 %");
            this.pwmDuty3.Items.Add("85 %");
            this.pwmDuty3.Items.Add("80 %");
            this.pwmDuty3.Items.Add("75 %");
            this.pwmDuty3.Items.Add("70 %");
            this.pwmDuty3.Items.Add("65 %");
            this.pwmDuty3.Items.Add("60 %");
            this.pwmDuty3.Items.Add("55 %");
            this.pwmDuty3.Items.Add("50 %");
            this.pwmDuty3.Items.Add("45 %");
            this.pwmDuty3.Items.Add("40 %");
            this.pwmDuty3.Items.Add("35 %");
            this.pwmDuty3.Items.Add("30 %");
            this.pwmDuty3.Items.Add("25 %");
            this.pwmDuty3.Items.Add("20 %");
            this.pwmDuty3.Items.Add("15 %");
            this.pwmDuty3.Items.Add("10 %");
            this.pwmDuty3.Items.Add("5 %");
            this.pwmDuty3.Location = new System.Drawing.Point(509, 108);
            this.pwmDuty3.Name = "pwmDuty3";
            this.pwmDuty3.Size = new System.Drawing.Size(113, 39);
            this.pwmDuty3.TabIndex = 41;
            this.pwmDuty3.SelectedItemChanged += new System.EventHandler(this.pwmDuty3_SelectedItemChanged);
            // 
            // pwmDuty2
            // 
            this.pwmDuty2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwmDuty2.AutoSize = true;
            this.pwmDuty2.Items.Add("100 %");
            this.pwmDuty2.Items.Add("95 %");
            this.pwmDuty2.Items.Add("90 %");
            this.pwmDuty2.Items.Add("85 %");
            this.pwmDuty2.Items.Add("80 %");
            this.pwmDuty2.Items.Add("75 %");
            this.pwmDuty2.Items.Add("70 %");
            this.pwmDuty2.Items.Add("65 %");
            this.pwmDuty2.Items.Add("60 %");
            this.pwmDuty2.Items.Add("55 %");
            this.pwmDuty2.Items.Add("50 %");
            this.pwmDuty2.Items.Add("45 %");
            this.pwmDuty2.Items.Add("40 %");
            this.pwmDuty2.Items.Add("35 %");
            this.pwmDuty2.Items.Add("30 %");
            this.pwmDuty2.Items.Add("25 %");
            this.pwmDuty2.Items.Add("20 %");
            this.pwmDuty2.Items.Add("15 %");
            this.pwmDuty2.Items.Add("10 %");
            this.pwmDuty2.Items.Add("5 %");
            this.pwmDuty2.Location = new System.Drawing.Point(509, 59);
            this.pwmDuty2.Name = "pwmDuty2";
            this.pwmDuty2.Size = new System.Drawing.Size(113, 39);
            this.pwmDuty2.TabIndex = 40;
            this.pwmDuty2.SelectedItemChanged += new System.EventHandler(this.pwmDuty2_SelectedItemChanged);
            // 
            // output1Number
            // 
            this.output1Number.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output1Number.AutoSize = true;
            this.output1Number.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output1Number.Location = new System.Drawing.Point(9, 13);
            this.output1Number.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.output1Number.Name = "output1Number";
            this.output1Number.Size = new System.Drawing.Size(29, 32);
            this.output1Number.TabIndex = 16;
            this.output1Number.Text = "1";
            // 
            // output3Current
            // 
            this.output3Current.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output3Current.AutoSize = true;
            this.output3Current.Location = new System.Drawing.Point(358, 111);
            this.output3Current.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.output3Current.Name = "output3Current";
            this.output3Current.Size = new System.Drawing.Size(0, 32);
            this.output3Current.TabIndex = 25;
            // 
            // output4Current
            // 
            this.output4Current.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output4Current.AutoSize = true;
            this.output4Current.Location = new System.Drawing.Point(358, 160);
            this.output4Current.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.output4Current.Name = "output4Current";
            this.output4Current.Size = new System.Drawing.Size(0, 32);
            this.output4Current.TabIndex = 29;
            // 
            // output2Current
            // 
            this.output2Current.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output2Current.AutoSize = true;
            this.output2Current.Location = new System.Drawing.Point(358, 62);
            this.output2Current.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.output2Current.Name = "output2Current";
            this.output2Current.Size = new System.Drawing.Size(0, 32);
            this.output2Current.TabIndex = 21;
            // 
            // output5Current
            // 
            this.output5Current.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output5Current.AutoSize = true;
            this.output5Current.Location = new System.Drawing.Point(358, 209);
            this.output5Current.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.output5Current.Name = "output5Current";
            this.output5Current.Size = new System.Drawing.Size(0, 32);
            this.output5Current.TabIndex = 33;
            // 
            // output6Number
            // 
            this.output6Number.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output6Number.AutoSize = true;
            this.output6Number.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output6Number.Location = new System.Drawing.Point(9, 261);
            this.output6Number.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.output6Number.Name = "output6Number";
            this.output6Number.Size = new System.Drawing.Size(29, 32);
            this.output6Number.TabIndex = 36;
            this.output6Number.Text = "6";
            // 
            // output5Name
            // 
            this.output5Name.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output5Name.AutoSize = true;
            this.output5Name.Location = new System.Drawing.Point(59, 209);
            this.output5Name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.output5Name.Name = "output5Name";
            this.output5Name.Size = new System.Drawing.Size(97, 32);
            this.output5Name.TabIndex = 31;
            this.output5Name.Text = "Focuser";
            // 
            // output5Number
            // 
            this.output5Number.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output5Number.AutoSize = true;
            this.output5Number.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output5Number.Location = new System.Drawing.Point(9, 209);
            this.output5Number.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.output5Number.Name = "output5Number";
            this.output5Number.Size = new System.Drawing.Size(29, 32);
            this.output5Number.TabIndex = 32;
            this.output5Number.Text = "5";
            // 
            // output6Current
            // 
            this.output6Current.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output6Current.AutoSize = true;
            this.output6Current.Location = new System.Drawing.Point(358, 261);
            this.output6Current.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.output6Current.Name = "output6Current";
            this.output6Current.Size = new System.Drawing.Size(0, 32);
            this.output6Current.TabIndex = 38;
            // 
            // output4Name
            // 
            this.output4Name.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output4Name.AutoSize = true;
            this.output4Name.Location = new System.Drawing.Point(59, 160);
            this.output4Name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.output4Name.Name = "output4Name";
            this.output4Name.Size = new System.Drawing.Size(210, 32);
            this.output4Name.TabIndex = 27;
            this.output4Name.Text = "Dew Heater Guide";
            // 
            // output4Number
            // 
            this.output4Number.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output4Number.AutoSize = true;
            this.output4Number.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output4Number.Location = new System.Drawing.Point(9, 160);
            this.output4Number.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.output4Number.Name = "output4Number";
            this.output4Number.Size = new System.Drawing.Size(29, 32);
            this.output4Number.TabIndex = 28;
            this.output4Number.Text = "4";
            // 
            // output6Name
            // 
            this.output6Name.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output6Name.AutoSize = true;
            this.output6Name.Location = new System.Drawing.Point(59, 261);
            this.output6Name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.output6Name.Name = "output6Name";
            this.output6Name.Size = new System.Drawing.Size(107, 32);
            this.output6Name.TabIndex = 35;
            this.output6Name.Text = "Disabled";
            // 
            // output3Name
            // 
            this.output3Name.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output3Name.AutoSize = true;
            this.output3Name.Location = new System.Drawing.Point(59, 111);
            this.output3Name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.output3Name.Name = "output3Name";
            this.output3Name.Size = new System.Drawing.Size(201, 32);
            this.output3Name.TabIndex = 23;
            this.output3Name.Text = "Dew Heater Main";
            // 
            // output3Number
            // 
            this.output3Number.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output3Number.AutoSize = true;
            this.output3Number.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output3Number.Location = new System.Drawing.Point(9, 111);
            this.output3Number.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.output3Number.Name = "output3Number";
            this.output3Number.Size = new System.Drawing.Size(29, 32);
            this.output3Number.TabIndex = 24;
            this.output3Number.Text = "3";
            // 
            // output2Name
            // 
            this.output2Name.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output2Name.AutoSize = true;
            this.output2Name.Location = new System.Drawing.Point(59, 62);
            this.output2Name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.output2Name.Name = "output2Name";
            this.output2Name.Size = new System.Drawing.Size(143, 32);
            this.output2Name.TabIndex = 19;
            this.output2Name.Text = "Filter Wheel";
            // 
            // output2Number
            // 
            this.output2Number.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output2Number.AutoSize = true;
            this.output2Number.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output2Number.Location = new System.Drawing.Point(9, 62);
            this.output2Number.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.output2Number.Name = "output2Number";
            this.output2Number.Size = new System.Drawing.Size(29, 32);
            this.output2Number.TabIndex = 20;
            this.output2Number.Text = "2";
            // 
            // buttonRelay6
            // 
            this.buttonRelay6.BackColor = System.Drawing.Color.Lime;
            this.buttonRelay6.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonRelay6.Location = new System.Drawing.Point(639, 252);
            this.buttonRelay6.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.buttonRelay6.Name = "buttonRelay6";
            this.buttonRelay6.Size = new System.Drawing.Size(122, 46);
            this.buttonRelay6.TabIndex = 37;
            this.buttonRelay6.Text = "On";
            this.buttonRelay6.UseVisualStyleBackColor = true;
            this.buttonRelay6.Click += new System.EventHandler(this.buttonRelay6_Click);
            // 
            // output1Current
            // 
            this.output1Current.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output1Current.AutoSize = true;
            this.output1Current.Location = new System.Drawing.Point(358, 13);
            this.output1Current.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.output1Current.Name = "output1Current";
            this.output1Current.Size = new System.Drawing.Size(0, 32);
            this.output1Current.TabIndex = 17;
            // 
            // buttonRelay2
            // 
            this.buttonRelay2.BackColor = System.Drawing.Color.Lime;
            this.buttonRelay2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonRelay2.Location = new System.Drawing.Point(639, 56);
            this.buttonRelay2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.buttonRelay2.Name = "buttonRelay2";
            this.buttonRelay2.Size = new System.Drawing.Size(122, 41);
            this.buttonRelay2.TabIndex = 18;
            this.buttonRelay2.Text = "On";
            this.buttonRelay2.UseVisualStyleBackColor = true;
            this.buttonRelay2.Click += new System.EventHandler(this.buttonRelay2On_Click);
            // 
            // buttonRelay3
            // 
            this.buttonRelay3.BackColor = System.Drawing.Color.Lime;
            this.buttonRelay3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonRelay3.Location = new System.Drawing.Point(639, 105);
            this.buttonRelay3.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.buttonRelay3.Name = "buttonRelay3";
            this.buttonRelay3.Size = new System.Drawing.Size(122, 41);
            this.buttonRelay3.TabIndex = 22;
            this.buttonRelay3.Text = "On";
            this.buttonRelay3.UseVisualStyleBackColor = true;
            this.buttonRelay3.Click += new System.EventHandler(this.buttonRelay3_Click);
            // 
            // buttonRelay4
            // 
            this.buttonRelay4.BackColor = System.Drawing.Color.Lime;
            this.buttonRelay4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonRelay4.Location = new System.Drawing.Point(639, 154);
            this.buttonRelay4.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.buttonRelay4.Name = "buttonRelay4";
            this.buttonRelay4.Size = new System.Drawing.Size(122, 41);
            this.buttonRelay4.TabIndex = 26;
            this.buttonRelay4.Text = "On";
            this.buttonRelay4.UseVisualStyleBackColor = true;
            this.buttonRelay4.Click += new System.EventHandler(this.buttonRelay4_Click);
            // 
            // buttonRelay5
            // 
            this.buttonRelay5.BackColor = System.Drawing.Color.Lime;
            this.buttonRelay5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonRelay5.Location = new System.Drawing.Point(639, 203);
            this.buttonRelay5.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.buttonRelay5.Name = "buttonRelay5";
            this.buttonRelay5.Size = new System.Drawing.Size(122, 41);
            this.buttonRelay5.TabIndex = 30;
            this.buttonRelay5.Text = "On";
            this.buttonRelay5.UseVisualStyleBackColor = true;
            this.buttonRelay5.Click += new System.EventHandler(this.buttonRelay5_Click);
            // 
            // pwmDuty1
            // 
            this.pwmDuty1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwmDuty1.AutoSize = true;
            this.pwmDuty1.Items.Add("100 %");
            this.pwmDuty1.Items.Add("95 %");
            this.pwmDuty1.Items.Add("90 %");
            this.pwmDuty1.Items.Add("85 %");
            this.pwmDuty1.Items.Add("80 %");
            this.pwmDuty1.Items.Add("75 %");
            this.pwmDuty1.Items.Add("70 %");
            this.pwmDuty1.Items.Add("65 %");
            this.pwmDuty1.Items.Add("60 %");
            this.pwmDuty1.Items.Add("55 %");
            this.pwmDuty1.Items.Add("50 %");
            this.pwmDuty1.Items.Add("45 %");
            this.pwmDuty1.Items.Add("40 %");
            this.pwmDuty1.Items.Add("35 %");
            this.pwmDuty1.Items.Add("30 %");
            this.pwmDuty1.Items.Add("25 %");
            this.pwmDuty1.Items.Add("20 %");
            this.pwmDuty1.Items.Add("15 %");
            this.pwmDuty1.Items.Add("10 %");
            this.pwmDuty1.Items.Add("5 %");
            this.pwmDuty1.Location = new System.Drawing.Point(509, 10);
            this.pwmDuty1.Name = "pwmDuty1";
            this.pwmDuty1.Size = new System.Drawing.Size(113, 39);
            this.pwmDuty1.TabIndex = 39;
            this.pwmDuty1.SelectedItemChanged += new System.EventHandler(this.pwmDuty1_SelectedItemChanged);
            // 
            // labelInputVoltage
            // 
            this.labelInputVoltage.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelInputVoltage.AutoSize = true;
            this.labelInputVoltage.ForeColor = System.Drawing.Color.Red;
            this.labelInputVoltage.Location = new System.Drawing.Point(666, 119);
            this.labelInputVoltage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInputVoltage.Name = "labelInputVoltage";
            this.labelInputVoltage.Size = new System.Drawing.Size(94, 32);
            this.labelInputVoltage.TabIndex = 11;
            this.labelInputVoltage.Text = "11,70 V";
            this.labelInputVoltage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(388, 119);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 32);
            this.label4.TabIndex = 9;
            this.label4.Text = "Input Voltage";
            // 
            // groupBoxSensors
            // 
            this.groupBoxSensors.Controls.Add(this.tableLayoutPanel2);
            this.groupBoxSensors.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxSensors.Location = new System.Drawing.Point(20, 613);
            this.groupBoxSensors.Margin = new System.Windows.Forms.Padding(20, 20, 20, 4);
            this.groupBoxSensors.Name = "groupBoxSensors";
            this.groupBoxSensors.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxSensors.Size = new System.Drawing.Size(777, 200);
            this.groupBoxSensors.TabIndex = 9;
            this.groupBoxSensors.TabStop = false;
            this.groupBoxSensors.Text = "Sensors";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.05128F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.94872F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.05128F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.94872F));
            this.tableLayoutPanel2.Controls.Add(this.labelTemperatureTitle, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelSkyTemperatureDelta, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelSkyTemperature, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelSQM, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.labelHumidityTitle, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelInputVoltage, 3, 3);
            this.tableLayoutPanel2.Controls.Add(this.labelPressureTitle, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label4, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.labelSQMTitle, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.labelExternalTemperatureTitle, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.labelSkyTemperatureTitle, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelExternalTemperature, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.labelPressure, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.labelHumidity, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelTemperature, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelSkyTemperatureDeltaTitle, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 36);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(769, 160);
            this.tableLayoutPanel2.TabIndex = 29;
            // 
            // labelTemperatureTitle
            // 
            this.labelTemperatureTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTemperatureTitle.AutoSize = true;
            this.labelTemperatureTitle.Location = new System.Drawing.Point(9, 7);
            this.labelTemperatureTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTemperatureTitle.Name = "labelTemperatureTitle";
            this.labelTemperatureTitle.Size = new System.Drawing.Size(150, 32);
            this.labelTemperatureTitle.TabIndex = 13;
            this.labelTemperatureTitle.Text = "Temperature";
            // 
            // labelSkyTemperatureDelta
            // 
            this.labelSkyTemperatureDelta.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelSkyTemperatureDelta.AutoSize = true;
            this.labelSkyTemperatureDelta.Location = new System.Drawing.Point(660, 7);
            this.labelSkyTemperatureDelta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSkyTemperatureDelta.Name = "labelSkyTemperatureDelta";
            this.labelSkyTemperatureDelta.Size = new System.Drawing.Size(100, 32);
            this.labelSkyTemperatureDelta.TabIndex = 20;
            this.labelSkyTemperatureDelta.Text = "-20,3 °C";
            this.labelSkyTemperatureDelta.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSkyTemperature
            // 
            this.labelSkyTemperature.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelSkyTemperature.AutoSize = true;
            this.labelSkyTemperature.Location = new System.Drawing.Point(660, 44);
            this.labelSkyTemperature.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSkyTemperature.Name = "labelSkyTemperature";
            this.labelSkyTemperature.Size = new System.Drawing.Size(100, 32);
            this.labelSkyTemperature.TabIndex = 22;
            this.labelSkyTemperature.Text = "-25,3 °C";
            this.labelSkyTemperature.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSQM
            // 
            this.labelSQM.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelSQM.AutoSize = true;
            this.labelSQM.Location = new System.Drawing.Point(688, 81);
            this.labelSQM.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSQM.Name = "labelSQM";
            this.labelSQM.Size = new System.Drawing.Size(72, 32);
            this.labelSQM.TabIndex = 28;
            this.labelSQM.Text = "21,57";
            this.labelSQM.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelHumidityTitle
            // 
            this.labelHumidityTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelHumidityTitle.AutoSize = true;
            this.labelHumidityTitle.Location = new System.Drawing.Point(9, 44);
            this.labelHumidityTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelHumidityTitle.Name = "labelHumidityTitle";
            this.labelHumidityTitle.Size = new System.Drawing.Size(113, 32);
            this.labelHumidityTitle.TabIndex = 14;
            this.labelHumidityTitle.Text = "Humidity";
            // 
            // labelPressureTitle
            // 
            this.labelPressureTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelPressureTitle.AutoSize = true;
            this.labelPressureTitle.Location = new System.Drawing.Point(9, 81);
            this.labelPressureTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPressureTitle.Name = "labelPressureTitle";
            this.labelPressureTitle.Size = new System.Drawing.Size(104, 32);
            this.labelPressureTitle.TabIndex = 15;
            this.labelPressureTitle.Text = "Pressure";
            // 
            // labelSQMTitle
            // 
            this.labelSQMTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelSQMTitle.AutoSize = true;
            this.labelSQMTitle.Location = new System.Drawing.Point(388, 81);
            this.labelSQMTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSQMTitle.Name = "labelSQMTitle";
            this.labelSQMTitle.Size = new System.Drawing.Size(68, 32);
            this.labelSQMTitle.TabIndex = 27;
            this.labelSQMTitle.Text = "SQM";
            // 
            // labelExternalTemperatureTitle
            // 
            this.labelExternalTemperatureTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelExternalTemperatureTitle.AutoSize = true;
            this.labelExternalTemperatureTitle.Location = new System.Drawing.Point(9, 119);
            this.labelExternalTemperatureTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelExternalTemperatureTitle.Name = "labelExternalTemperatureTitle";
            this.labelExternalTemperatureTitle.Size = new System.Drawing.Size(112, 32);
            this.labelExternalTemperatureTitle.TabIndex = 21;
            this.labelExternalTemperatureTitle.Text = "Ext Temp";
            this.labelExternalTemperatureTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelSkyTemperatureTitle
            // 
            this.labelSkyTemperatureTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelSkyTemperatureTitle.AutoSize = true;
            this.labelSkyTemperatureTitle.Location = new System.Drawing.Point(388, 44);
            this.labelSkyTemperatureTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSkyTemperatureTitle.Name = "labelSkyTemperatureTitle";
            this.labelSkyTemperatureTitle.Size = new System.Drawing.Size(118, 32);
            this.labelSkyTemperatureTitle.TabIndex = 24;
            this.labelSkyTemperatureTitle.Text = "Sky Temp";
            // 
            // labelExternalTemperature
            // 
            this.labelExternalTemperature.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelExternalTemperature.AutoSize = true;
            this.labelExternalTemperature.Location = new System.Drawing.Point(287, 119);
            this.labelExternalTemperature.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelExternalTemperature.Name = "labelExternalTemperature";
            this.labelExternalTemperature.Size = new System.Drawing.Size(93, 32);
            this.labelExternalTemperature.TabIndex = 23;
            this.labelExternalTemperature.Text = "+5,5 °C";
            this.labelExternalTemperature.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelPressure
            // 
            this.labelPressure.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelPressure.AutoSize = true;
            this.labelPressure.Location = new System.Drawing.Point(268, 81);
            this.labelPressure.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPressure.Name = "labelPressure";
            this.labelPressure.Size = new System.Drawing.Size(112, 32);
            this.labelPressure.TabIndex = 18;
            this.labelPressure.Text = "1001 hPa";
            this.labelPressure.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelHumidity
            // 
            this.labelHumidity.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelHumidity.AutoSize = true;
            this.labelHumidity.Location = new System.Drawing.Point(312, 44);
            this.labelHumidity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelHumidity.Name = "labelHumidity";
            this.labelHumidity.Size = new System.Drawing.Size(68, 32);
            this.labelHumidity.TabIndex = 17;
            this.labelHumidity.Text = "75 %";
            this.labelHumidity.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelTemperature
            // 
            this.labelTemperature.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelTemperature.AutoSize = true;
            this.labelTemperature.Location = new System.Drawing.Point(287, 7);
            this.labelTemperature.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTemperature.Name = "labelTemperature";
            this.labelTemperature.Size = new System.Drawing.Size(93, 32);
            this.labelTemperature.TabIndex = 16;
            this.labelTemperature.Text = "+3,5 °C";
            this.labelTemperature.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSkyTemperatureDeltaTitle
            // 
            this.labelSkyTemperatureDeltaTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelSkyTemperatureDeltaTitle.AutoSize = true;
            this.labelSkyTemperatureDeltaTitle.Location = new System.Drawing.Point(388, 7);
            this.labelSkyTemperatureDeltaTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSkyTemperatureDeltaTitle.Name = "labelSkyTemperatureDeltaTitle";
            this.labelSkyTemperatureDeltaTitle.Size = new System.Drawing.Size(74, 32);
            this.labelSkyTemperatureDeltaTitle.TabIndex = 19;
            this.labelSkyTemperatureDeltaTitle.Text = "Sky Δ";
            // 
            // labelStatusText
            // 
            this.labelStatusText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelStatusText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatusText.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelStatusText.Location = new System.Drawing.Point(187, 836);
            this.labelStatusText.Margin = new System.Windows.Forms.Padding(4, 19, 4, 0);
            this.labelStatusText.Name = "labelStatusText";
            this.labelStatusText.Size = new System.Drawing.Size(602, 50);
            this.labelStatusText.TabIndex = 10;
            this.labelStatusText.Text = "Device Connected";
            this.labelStatusText.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonSettings
            // 
            this.buttonSettings.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSettings.Location = new System.Drawing.Point(25, 836);
            this.buttonSettings.Margin = new System.Windows.Forms.Padding(25, 19, 6, 4);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(152, 45);
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
            this.serialLabel.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serialLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.serialLabel.Location = new System.Drawing.Point(4, 895);
            this.serialLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.serialLabel.Name = "serialLabel";
            this.serialLabel.Size = new System.Drawing.Size(364, 48);
            this.serialLabel.TabIndex = 39;
            this.serialLabel.Text = "Device #000";
            this.serialLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // firmwareLabel
            // 
            this.firmwareLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.firmwareLabel.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.SetFlowBreak(this.firmwareLabel, true);
            this.firmwareLabel.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firmwareLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.firmwareLabel.Location = new System.Drawing.Point(4, 953);
            this.firmwareLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.firmwareLabel.Name = "firmwareLabel";
            this.firmwareLabel.Size = new System.Drawing.Size(376, 67);
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
            this.titlePictureBox.Size = new System.Drawing.Size(821, 220);
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
            this.flowLayoutPanel1.Size = new System.Drawing.Size(818, 896);
            this.flowLayoutPanel1.TabIndex = 41;
            // 
            // KomaHubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 896);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KomaHubForm";
            this.Text = "KomaHub";
            this.groupBoxRelayControl.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBoxSensors.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.titlePictureBox)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRelay1;
        private System.Windows.Forms.Label output1Name;
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
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Label output1Current;
        private System.Windows.Forms.Label output1Number;
        private System.Windows.Forms.Label output3Current;
        private System.Windows.Forms.Label output3Number;
        private System.Windows.Forms.Label output3Name;
        private System.Windows.Forms.Button buttonRelay3;
        private System.Windows.Forms.Label output2Current;
        private System.Windows.Forms.Label output2Number;
        private System.Windows.Forms.Label output2Name;
        private System.Windows.Forms.Button buttonRelay2;
        private System.Windows.Forms.Label output5Current;
        private System.Windows.Forms.Label output5Number;
        private System.Windows.Forms.Label output5Name;
        private System.Windows.Forms.Button buttonRelay5;
        private System.Windows.Forms.Label output4Current;
        private System.Windows.Forms.Label output4Number;
        private System.Windows.Forms.Label output4Name;
        private System.Windows.Forms.Button buttonRelay4;
        private System.Windows.Forms.Label serialLabel;
        private System.Windows.Forms.Label firmwareLabel;
        private System.Windows.Forms.PictureBox titlePictureBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label output6Number;
        private System.Windows.Forms.Label output6Current;
        private System.Windows.Forms.Label output6Name;
        private System.Windows.Forms.Button buttonRelay6;
        private System.Windows.Forms.DomainUpDown pwmDuty6;
        private System.Windows.Forms.DomainUpDown pwmDuty5;
        private System.Windows.Forms.DomainUpDown pwmDuty4;
        private System.Windows.Forms.DomainUpDown pwmDuty3;
        private System.Windows.Forms.DomainUpDown pwmDuty2;
        private System.Windows.Forms.DomainUpDown pwmDuty1;
    }
}

