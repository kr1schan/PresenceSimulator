using System.Windows.Forms;
namespace PresenceSimulator
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainVideo = new AForge.Controls.VideoSourcePlayer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.video = new System.Windows.Forms.TabPage();
            this.saturation = new System.Windows.Forms.TrackBar();
            this.contrast = new System.Windows.Forms.TrackBar();
            this.brightness = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.videoFormatCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.videoSourceCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.users = new System.Windows.Forms.TabPage();
            this.detectorComboBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.addUserTrack = new System.Windows.Forms.Button();
            this.userListContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.showDetectorSettings = new System.Windows.Forms.Button();
            this.addUser = new System.Windows.Forms.Button();
            this.Map = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.paperFormat = new System.Windows.Forms.ComboBox();
            this.saveQR = new System.Windows.Forms.Button();
            this.encodeQR = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.mapBearing = new System.Windows.Forms.TrackBar();
            this.mapOpacity = new System.Windows.Forms.TrackBar();
            this.mapZoom = new System.Windows.Forms.TrackBar();
            this.Network = new System.Windows.Forms.TabPage();
            this.serverLog = new System.Windows.Forms.RichTextBox();
            this.checkServerState = new System.Windows.Forms.CheckBox();
            this.checkBroadcast = new System.Windows.Forms.CheckBox();
            this.Recorder = new System.Windows.Forms.TabPage();
            this.checkRecordUsersTracks = new System.Windows.Forms.CheckBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.linkToOpenStreetMap = new System.Windows.Forms.LinkLabel();
            this.linkToZXing = new System.Windows.Forms.LinkLabel();
            this.linkToPDFsharp = new System.Windows.Forms.LinkLabel();
            this.linkToAForge = new System.Windows.Forms.LinkLabel();
            this.linkToGMap = new System.Windows.Forms.LinkLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.linkMailToKrischan = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.video.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.brightness)).BeginInit();
            this.users.SuspendLayout();
            this.Map.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapBearing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapOpacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapZoom)).BeginInit();
            this.Network.SuspendLayout();
            this.Recorder.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainVideo
            // 
            this.mainVideo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mainVideo.Location = new System.Drawing.Point(60, 46);
            this.mainVideo.Name = "mainVideo";
            this.mainVideo.Size = new System.Drawing.Size(640, 480);
            this.mainVideo.TabIndex = 0;
            this.mainVideo.Text = "mainVideo";
            this.mainVideo.VideoSource = null;
            this.mainVideo.NewFrame += new AForge.Controls.VideoSourcePlayer.NewFrameHandler(this.mainVideo_NewFrame);
            this.mainVideo.Click += new System.EventHandler(this.mainVideo_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.video);
            this.tabControl1.Controls.Add(this.users);
            this.tabControl1.Controls.Add(this.Map);
            this.tabControl1.Controls.Add(this.Network);
            this.tabControl1.Controls.Add(this.Recorder);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(200, 572);
            this.tabControl1.TabIndex = 8;
            // 
            // video
            // 
            this.video.BackColor = System.Drawing.SystemColors.Control;
            this.video.Controls.Add(this.saturation);
            this.video.Controls.Add(this.contrast);
            this.video.Controls.Add(this.brightness);
            this.video.Controls.Add(this.label5);
            this.video.Controls.Add(this.label4);
            this.video.Controls.Add(this.label3);
            this.video.Controls.Add(this.videoFormatCombo);
            this.video.Controls.Add(this.label1);
            this.video.Controls.Add(this.videoSourceCombo);
            this.video.Controls.Add(this.label2);
            this.video.Location = new System.Drawing.Point(23, 4);
            this.video.Name = "video";
            this.video.Padding = new System.Windows.Forms.Padding(3);
            this.video.Size = new System.Drawing.Size(173, 564);
            this.video.TabIndex = 0;
            this.video.Text = "Video";
            // 
            // saturation
            // 
            this.saturation.AutoSize = false;
            this.saturation.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::PresenceSimulator.Properties.Settings.Default, "VideoSaturation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.saturation.Location = new System.Drawing.Point(9, 196);
            this.saturation.Maximum = 100;
            this.saturation.Minimum = -100;
            this.saturation.Name = "saturation";
            this.saturation.Size = new System.Drawing.Size(157, 25);
            this.saturation.TabIndex = 15;
            this.saturation.TabStop = false;
            this.saturation.TickStyle = System.Windows.Forms.TickStyle.None;
            this.saturation.Value = global::PresenceSimulator.Properties.Settings.Default.VideoSaturation;
            // 
            // contrast
            // 
            this.contrast.AutoSize = false;
            this.contrast.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::PresenceSimulator.Properties.Settings.Default, "VideoContrast", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.contrast.Location = new System.Drawing.Point(9, 154);
            this.contrast.Maximum = 127;
            this.contrast.Minimum = -127;
            this.contrast.Name = "contrast";
            this.contrast.Size = new System.Drawing.Size(157, 23);
            this.contrast.TabIndex = 14;
            this.contrast.TickStyle = System.Windows.Forms.TickStyle.None;
            this.contrast.Value = global::PresenceSimulator.Properties.Settings.Default.VideoContrast;
            // 
            // brightness
            // 
            this.brightness.AutoSize = false;
            this.brightness.BackColor = System.Drawing.SystemColors.Control;
            this.brightness.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::PresenceSimulator.Properties.Settings.Default, "VideoBrightness", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.brightness.Location = new System.Drawing.Point(9, 109);
            this.brightness.Maximum = 255;
            this.brightness.Minimum = -255;
            this.brightness.Name = "brightness";
            this.brightness.Size = new System.Drawing.Size(157, 26);
            this.brightness.TabIndex = 13;
            this.brightness.TickStyle = System.Windows.Forms.TickStyle.None;
            this.brightness.Value = global::PresenceSimulator.Properties.Settings.Default.VideoBrightness;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Saturation:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Contrast:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Brightness:";
            // 
            // videoFormatCombo
            // 
            this.videoFormatCombo.FormattingEnabled = true;
            this.videoFormatCombo.Location = new System.Drawing.Point(9, 59);
            this.videoFormatCombo.Name = "videoFormatCombo";
            this.videoFormatCombo.Size = new System.Drawing.Size(157, 21);
            this.videoFormatCombo.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Video Format:";
            // 
            // videoSourceCombo
            // 
            this.videoSourceCombo.FormattingEnabled = true;
            this.videoSourceCombo.Location = new System.Drawing.Point(9, 19);
            this.videoSourceCombo.Name = "videoSourceCombo";
            this.videoSourceCombo.Size = new System.Drawing.Size(157, 21);
            this.videoSourceCombo.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Video Source:";
            // 
            // users
            // 
            this.users.BackColor = System.Drawing.SystemColors.Control;
            this.users.Controls.Add(this.detectorComboBox);
            this.users.Controls.Add(this.label10);
            this.users.Controls.Add(this.addUserTrack);
            this.users.Controls.Add(this.userListContainer);
            this.users.Controls.Add(this.showDetectorSettings);
            this.users.Controls.Add(this.addUser);
            this.users.Location = new System.Drawing.Point(23, 4);
            this.users.Name = "users";
            this.users.Padding = new System.Windows.Forms.Padding(3);
            this.users.Size = new System.Drawing.Size(173, 564);
            this.users.TabIndex = 1;
            this.users.Text = "Location Sources";
            // 
            // detectorComboBox
            // 
            this.detectorComboBox.FormattingEnabled = true;
            this.detectorComboBox.Items.AddRange(new object[] {
            "Marker Based Detector",
            "Color Based Detector"});
            this.detectorComboBox.Location = new System.Drawing.Point(6, 27);
            this.detectorComboBox.Name = "detectorComboBox";
            this.detectorComboBox.Size = new System.Drawing.Size(161, 21);
            this.detectorComboBox.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Detector:";
            // 
            // addUserTrack
            // 
            this.addUserTrack.Location = new System.Drawing.Point(6, 112);
            this.addUserTrack.Name = "addUserTrack";
            this.addUserTrack.Size = new System.Drawing.Size(161, 23);
            this.addUserTrack.TabIndex = 15;
            this.addUserTrack.Text = "Add Track";
            this.addUserTrack.UseVisualStyleBackColor = true;
            this.addUserTrack.Click += new System.EventHandler(this.addUserTrack_Click);
            // 
            // userListContainer
            // 
            this.userListContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.userListContainer.AutoScroll = true;
            this.userListContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.userListContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.userListContainer.Location = new System.Drawing.Point(6, 141);
            this.userListContainer.Name = "userListContainer";
            this.userListContainer.Size = new System.Drawing.Size(161, 417);
            this.userListContainer.TabIndex = 14;
            this.userListContainer.WrapContents = false;
            // 
            // showDetectorSettings
            // 
            this.showDetectorSettings.Location = new System.Drawing.Point(6, 54);
            this.showDetectorSettings.Name = "showDetectorSettings";
            this.showDetectorSettings.Size = new System.Drawing.Size(161, 23);
            this.showDetectorSettings.TabIndex = 13;
            this.showDetectorSettings.Text = "Detector Settings";
            this.showDetectorSettings.UseVisualStyleBackColor = true;
            // 
            // addUser
            // 
            this.addUser.Location = new System.Drawing.Point(6, 83);
            this.addUser.Name = "addUser";
            this.addUser.Size = new System.Drawing.Size(161, 23);
            this.addUser.TabIndex = 12;
            this.addUser.Text = "Add Location Source";
            this.addUser.UseVisualStyleBackColor = true;
            // 
            // Map
            // 
            this.Map.BackColor = System.Drawing.SystemColors.Control;
            this.Map.Controls.Add(this.groupBox1);
            this.Map.Controls.Add(this.encodeQR);
            this.Map.Controls.Add(this.label6);
            this.Map.Controls.Add(this.label8);
            this.Map.Controls.Add(this.label7);
            this.Map.Controls.Add(this.mapBearing);
            this.Map.Controls.Add(this.mapOpacity);
            this.Map.Controls.Add(this.mapZoom);
            this.Map.Location = new System.Drawing.Point(23, 4);
            this.Map.Name = "Map";
            this.Map.Size = new System.Drawing.Size(173, 564);
            this.Map.TabIndex = 2;
            this.Map.Text = "Map ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.paperFormat);
            this.groupBox1.Controls.Add(this.saveQR);
            this.groupBox1.Location = new System.Drawing.Point(6, 146);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 96);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Map2PDF";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 29;
            this.label9.Text = "Paper Format:";
            // 
            // paperFormat
            // 
            this.paperFormat.FormattingEnabled = true;
            this.paperFormat.Items.AddRange(new object[] {
            "DIN A0",
            "DIN A1",
            "DIN A2",
            "DIN A3",
            "DIN A4"});
            this.paperFormat.Location = new System.Drawing.Point(6, 40);
            this.paperFormat.Name = "paperFormat";
            this.paperFormat.Size = new System.Drawing.Size(152, 21);
            this.paperFormat.TabIndex = 28;
            this.paperFormat.Text = "Paper Format";
            // 
            // saveQR
            // 
            this.saveQR.Location = new System.Drawing.Point(6, 67);
            this.saveQR.Name = "saveQR";
            this.saveQR.Size = new System.Drawing.Size(152, 23);
            this.saveQR.TabIndex = 27;
            this.saveQR.Text = "Save Position";
            this.saveQR.UseVisualStyleBackColor = true;
            // 
            // encodeQR
            // 
            this.encodeQR.Location = new System.Drawing.Point(6, 248);
            this.encodeQR.Name = "encodeQR";
            this.encodeQR.Size = new System.Drawing.Size(164, 23);
            this.encodeQR.TabIndex = 26;
            this.encodeQR.Text = "Detect Position";
            this.encodeQR.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Bearing:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Opacity:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Zoom:";
            // 
            // mapBearing
            // 
            this.mapBearing.AccessibleDescription = "";
            this.mapBearing.AutoSize = false;
            this.mapBearing.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::PresenceSimulator.Properties.Settings.Default, "MapBearing", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mapBearing.Location = new System.Drawing.Point(6, 104);
            this.mapBearing.Maximum = 360;
            this.mapBearing.Name = "mapBearing";
            this.mapBearing.Size = new System.Drawing.Size(164, 36);
            this.mapBearing.TabIndex = 24;
            this.mapBearing.Tag = "";
            this.mapBearing.TickFrequency = 10;
            this.mapBearing.Value = global::PresenceSimulator.Properties.Settings.Default.MapBearing;
            // 
            // mapOpacity
            // 
            this.mapOpacity.AutoSize = false;
            this.mapOpacity.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::PresenceSimulator.Properties.Settings.Default, "MapOpacity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mapOpacity.Location = new System.Drawing.Point(6, 60);
            this.mapOpacity.Maximum = 100;
            this.mapOpacity.Name = "mapOpacity";
            this.mapOpacity.Size = new System.Drawing.Size(164, 25);
            this.mapOpacity.TabIndex = 21;
            this.mapOpacity.TickFrequency = 5;
            this.mapOpacity.Value = global::PresenceSimulator.Properties.Settings.Default.MapOpacity;
            // 
            // mapZoom
            // 
            this.mapZoom.AutoSize = false;
            this.mapZoom.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::PresenceSimulator.Properties.Settings.Default, "MapZoomLvl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mapZoom.LargeChange = 1;
            this.mapZoom.Location = new System.Drawing.Point(6, 16);
            this.mapZoom.Maximum = 18;
            this.mapZoom.Minimum = 1;
            this.mapZoom.Name = "mapZoom";
            this.mapZoom.Size = new System.Drawing.Size(164, 38);
            this.mapZoom.TabIndex = 18;
            this.mapZoom.Tag = "";
            this.mapZoom.Value = global::PresenceSimulator.Properties.Settings.Default.MapZoomLvl;
            // 
            // Network
            // 
            this.Network.BackColor = System.Drawing.SystemColors.Control;
            this.Network.Controls.Add(this.serverLog);
            this.Network.Controls.Add(this.checkServerState);
            this.Network.Controls.Add(this.checkBroadcast);
            this.Network.Location = new System.Drawing.Point(23, 4);
            this.Network.Name = "Network";
            this.Network.Size = new System.Drawing.Size(173, 564);
            this.Network.TabIndex = 3;
            this.Network.Text = "Network";
            // 
            // serverLog
            // 
            this.serverLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.serverLog.Location = new System.Drawing.Point(3, 49);
            this.serverLog.Name = "serverLog";
            this.serverLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.serverLog.Size = new System.Drawing.Size(167, 512);
            this.serverLog.TabIndex = 5;
            this.serverLog.Text = "";
            // 
            // checkServerState
            // 
            this.checkServerState.AutoSize = true;
            this.checkServerState.Checked = global::PresenceSimulator.Properties.Settings.Default.NetworkServerActive;
            this.checkServerState.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkServerState.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PresenceSimulator.Properties.Settings.Default, "NetworkServerActive", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkServerState.Location = new System.Drawing.Point(3, 3);
            this.checkServerState.Name = "checkServerState";
            this.checkServerState.Size = new System.Drawing.Size(56, 17);
            this.checkServerState.TabIndex = 4;
            this.checkServerState.Text = "Active";
            this.checkServerState.UseVisualStyleBackColor = true;
            // 
            // checkBroadcast
            // 
            this.checkBroadcast.AutoSize = true;
            this.checkBroadcast.Checked = global::PresenceSimulator.Properties.Settings.Default.BroadcastLocationSources;
            this.checkBroadcast.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBroadcast.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PresenceSimulator.Properties.Settings.Default, "BroadcastLocationSources", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBroadcast.Location = new System.Drawing.Point(3, 26);
            this.checkBroadcast.Name = "checkBroadcast";
            this.checkBroadcast.Size = new System.Drawing.Size(121, 17);
            this.checkBroadcast.TabIndex = 3;
            this.checkBroadcast.Text = "Broadcast Server IP";
            this.checkBroadcast.UseVisualStyleBackColor = true;
            // 
            // Recorder
            // 
            this.Recorder.BackColor = System.Drawing.SystemColors.Control;
            this.Recorder.Controls.Add(this.checkRecordUsersTracks);
            this.Recorder.Location = new System.Drawing.Point(23, 4);
            this.Recorder.Name = "Recorder";
            this.Recorder.Size = new System.Drawing.Size(173, 564);
            this.Recorder.TabIndex = 4;
            this.Recorder.Text = "Recorder";
            // 
            // checkRecordUsersTracks
            // 
            this.checkRecordUsersTracks.AutoSize = true;
            this.checkRecordUsersTracks.Checked = global::PresenceSimulator.Properties.Settings.Default.RecordUserTracks;
            this.checkRecordUsersTracks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkRecordUsersTracks.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PresenceSimulator.Properties.Settings.Default, "RecordUserTracks", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkRecordUsersTracks.Location = new System.Drawing.Point(3, 3);
            this.checkRecordUsersTracks.Name = "checkRecordUsersTracks";
            this.checkRecordUsersTracks.Size = new System.Drawing.Size(97, 17);
            this.checkRecordUsersTracks.TabIndex = 0;
            this.checkRecordUsersTracks.Text = "Record Tracks";
            this.checkRecordUsersTracks.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(23, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(173, 564);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "Info";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.linkToOpenStreetMap);
            this.groupBox4.Controls.Add(this.linkToZXing);
            this.groupBox4.Controls.Add(this.linkToPDFsharp);
            this.groupBox4.Controls.Add(this.linkToAForge);
            this.groupBox4.Controls.Add(this.linkToGMap);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(4, 165);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(162, 226);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Thanks to";
            // 
            // linkToOpenStreetMap
            // 
            this.linkToOpenStreetMap.AutoSize = true;
            this.linkToOpenStreetMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkToOpenStreetMap.Location = new System.Drawing.Point(3, 188);
            this.linkToOpenStreetMap.Name = "linkToOpenStreetMap";
            this.linkToOpenStreetMap.Size = new System.Drawing.Size(143, 26);
            this.linkToOpenStreetMap.TabIndex = 4;
            this.linkToOpenStreetMap.TabStop = true;
            this.linkToOpenStreetMap.Text = "OpenStreetMap - Wiki World\r\nMap";
            this.linkToOpenStreetMap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkToOpenStreetMap_LinkClicked);
            // 
            // linkToZXing
            // 
            this.linkToZXing.AutoSize = true;
            this.linkToZXing.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkToZXing.Location = new System.Drawing.Point(2, 144);
            this.linkToZXing.Name = "linkToZXing";
            this.linkToZXing.Size = new System.Drawing.Size(142, 26);
            this.linkToZXing.TabIndex = 3;
            this.linkToZXing.TabStop = true;
            this.linkToZXing.Text = "ZXing - Barcorde Processing\r\nLibrary";
            this.linkToZXing.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkToZXing_LinkClicked);
            // 
            // linkToPDFsharp
            // 
            this.linkToPDFsharp.AutoSize = true;
            this.linkToPDFsharp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkToPDFsharp.Location = new System.Drawing.Point(3, 101);
            this.linkToPDFsharp.Name = "linkToPDFsharp";
            this.linkToPDFsharp.Size = new System.Drawing.Size(112, 26);
            this.linkToPDFsharp.TabIndex = 2;
            this.linkToPDFsharp.TabStop = true;
            this.linkToPDFsharp.Text = "PDFsharp - Library For\r\nProcessing PDF";
            this.linkToPDFsharp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkToPDFsharp_LinkClicked);
            // 
            // linkToAForge
            // 
            this.linkToAForge.AutoSize = true;
            this.linkToAForge.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkToAForge.Location = new System.Drawing.Point(3, 60);
            this.linkToAForge.Name = "linkToAForge";
            this.linkToAForge.Size = new System.Drawing.Size(154, 26);
            this.linkToAForge.TabIndex = 1;
            this.linkToAForge.TabStop = true;
            this.linkToAForge.Text = "AForge.NET - Computer Vision \r\nFramework";
            this.linkToAForge.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkToAForge_LinkClicked);
            // 
            // linkToGMap
            // 
            this.linkToGMap.AutoSize = true;
            this.linkToGMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkToGMap.Location = new System.Drawing.Point(2, 20);
            this.linkToGMap.Name = "linkToGMap";
            this.linkToGMap.Size = new System.Drawing.Size(146, 26);
            this.linkToGMap.TabIndex = 0;
            this.linkToGMap.TabStop = true;
            this.linkToGMap.Text = "GMap.NET - Great Maps For \r\nWindows Forms";
            this.linkToGMap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkToGMap_LinkClicked);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.linkMailToKrischan);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(4, 92);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(162, 67);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Author";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(2, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Krischan Udelhoven";
            // 
            // linkMailToKrischan
            // 
            this.linkMailToKrischan.AutoSize = true;
            this.linkMailToKrischan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkMailToKrischan.Location = new System.Drawing.Point(3, 42);
            this.linkMailToKrischan.Name = "linkMailToKrischan";
            this.linkMailToKrischan.Size = new System.Drawing.Size(158, 13);
            this.linkMailToKrischan.TabIndex = 2;
            this.linkMailToKrischan.TabStop = true;
            this.linkMailToKrischan.Text = "krischan.udelhoven@gmail.com";
            this.linkMailToKrischan.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkMailToKrischan_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(163, 83);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Presence Simulator";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 39);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Date: 24.09.2012";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Version: 1.0.0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 397);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Reset Settings";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.resetSettings_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mainVideo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(200, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(761, 572);
            this.panel1.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 572);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Presence Simulator";
            this.tabControl1.ResumeLayout(false);
            this.video.ResumeLayout(false);
            this.video.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.brightness)).EndInit();
            this.users.ResumeLayout(false);
            this.users.PerformLayout();
            this.Map.ResumeLayout(false);
            this.Map.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapBearing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapOpacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapZoom)).EndInit();
            this.Network.ResumeLayout(false);
            this.Network.PerformLayout();
            this.Recorder.ResumeLayout(false);
            this.Recorder.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AForge.Controls.VideoSourcePlayer mainVideo;
        private TabControl tabControl1;
        private TabPage video;
        private TabPage users;
        private TabPage Map;
        private TabPage Network;
        private TabPage Recorder;
        private ComboBox videoSourceCombo;
        private Label label2;
        private ComboBox videoFormatCombo;
        private Label label1;
        private TrackBar saturation;
        private TrackBar contrast;
        private Label label5;
        private Label label4;
        private Label label3;
        public TrackBar brightness;
        private RichTextBox serverLog;
        private CheckBox checkServerState;
        private CheckBox checkBroadcast;
        private Label label8;
        private Label label7;
        private Button showDetectorSettings;
        private Button addUser;
        private FlowLayoutPanel userListContainer;
        private CheckBox checkRecordUsersTracks;
        private Label label6;
        private Button encodeQR;
        private Button saveQR;
        private GroupBox groupBox1;
        private ComboBox paperFormat;
        private Panel panel1;
        public TrackBar mapZoom;
        public TrackBar mapBearing;
        private Button addUserTrack;
        private Label label9;
        private ComboBox detectorComboBox;
        private Label label10;
        private TabPage tabPage1;
        private Button button1;
        public TrackBar mapOpacity;
        private LinkLabel linkMailToKrischan;
        private Label label11;
        private GroupBox groupBox2;
        private Label label12;
        private GroupBox groupBox3;
        private Label label13;
        private GroupBox groupBox4;
        private LinkLabel linkToZXing;
        private LinkLabel linkToPDFsharp;
        private LinkLabel linkToAForge;
        private LinkLabel linkToGMap;
        private LinkLabel linkToOpenStreetMap;
    }
}

