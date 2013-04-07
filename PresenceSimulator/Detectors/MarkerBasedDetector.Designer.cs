namespace PresenceSimulator.Detectors
{
    partial class MarkerBasedDetector
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
            this.thresholdEdgeImage = new System.Windows.Forms.RadioButton();
            this.edgeImage = new System.Windows.Forms.RadioButton();
            this.originalImage = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.drawMarkersOnVideo = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.maxMarkerSize = new System.Windows.Forms.TextBox();
            this.minMarkerSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.threshold = new System.Windows.Forms.TrackBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.threshold)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.thresholdEdgeImage);
            this.groupBox1.Controls.Add(this.edgeImage);
            this.groupBox1.Controls.Add(this.originalImage);
            this.groupBox1.Location = new System.Drawing.Point(18, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(153, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // thresholdEdgeImage
            // 
            this.thresholdEdgeImage.AutoSize = true;
            this.thresholdEdgeImage.Checked = global::PresenceSimulator.Properties.Settings.Default.MarkerBasedDetectorThresholdedImage;
            this.thresholdEdgeImage.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PresenceSimulator.Properties.Settings.Default, "MarkerBasedDetectorThresholdedImage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.thresholdEdgeImage.Location = new System.Drawing.Point(6, 65);
            this.thresholdEdgeImage.Name = "thresholdEdgeImage";
            this.thresholdEdgeImage.Size = new System.Drawing.Size(144, 17);
            this.thresholdEdgeImage.TabIndex = 5;
            this.thresholdEdgeImage.Text = "Thresholded Edge Image";
            this.thresholdEdgeImage.UseVisualStyleBackColor = true;
            // 
            // edgeImage
            // 
            this.edgeImage.AutoSize = true;
            this.edgeImage.Checked = global::PresenceSimulator.Properties.Settings.Default.MarkerBasedDetectorEdgeImage;
            this.edgeImage.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PresenceSimulator.Properties.Settings.Default, "MarkerBasedDetectorEdgeImage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.edgeImage.Location = new System.Drawing.Point(6, 42);
            this.edgeImage.Name = "edgeImage";
            this.edgeImage.Size = new System.Drawing.Size(82, 17);
            this.edgeImage.TabIndex = 4;
            this.edgeImage.Text = "Edge Image";
            this.edgeImage.UseVisualStyleBackColor = true;
            // 
            // originalImage
            // 
            this.originalImage.AutoSize = true;
            this.originalImage.Checked = global::PresenceSimulator.Properties.Settings.Default.MarkerBasedDetectorOriginalImage;
            this.originalImage.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PresenceSimulator.Properties.Settings.Default, "MarkerBasedDetectorOriginalImage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.originalImage.Location = new System.Drawing.Point(6, 19);
            this.originalImage.Name = "originalImage";
            this.originalImage.Size = new System.Drawing.Size(92, 17);
            this.originalImage.TabIndex = 3;
            this.originalImage.TabStop = true;
            this.originalImage.Text = "Original Image";
            this.originalImage.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.drawMarkersOnVideo);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 148);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Appearance";
            // 
            // drawMarkersOnVideo
            // 
            this.drawMarkersOnVideo.AutoSize = true;
            this.drawMarkersOnVideo.Checked = global::PresenceSimulator.Properties.Settings.Default.MarkerBasedDetectorDrawMarkersOnVideo;
            this.drawMarkersOnVideo.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PresenceSimulator.Properties.Settings.Default, "MarkerBasedDetectorDrawMarkersOnVideo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.drawMarkersOnVideo.Location = new System.Drawing.Point(24, 125);
            this.drawMarkersOnVideo.Name = "drawMarkersOnVideo";
            this.drawMarkersOnVideo.Size = new System.Drawing.Size(137, 17);
            this.drawMarkersOnVideo.TabIndex = 1;
            this.drawMarkersOnVideo.Text = "Draw Markers on Video";
            this.drawMarkersOnVideo.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.maxMarkerSize);
            this.groupBox3.Controls.Add(this.minMarkerSize);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.threshold);
            this.groupBox3.Location = new System.Drawing.Point(12, 166);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(268, 121);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Functionality";
            // 
            // maxMarkerSize
            // 
            this.maxMarkerSize.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PresenceSimulator.Properties.Settings.Default, "MarkerBasedDetectorMaxMarkerSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.maxMarkerSize.Location = new System.Drawing.Point(130, 86);
            this.maxMarkerSize.Name = "maxMarkerSize";
            this.maxMarkerSize.Size = new System.Drawing.Size(100, 20);
            this.maxMarkerSize.TabIndex = 5;
            this.maxMarkerSize.Text = global::PresenceSimulator.Properties.Settings.Default.MarkerBasedDetectorMaxMarkerSize;
            // 
            // minMarkerSize
            // 
            this.minMarkerSize.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PresenceSimulator.Properties.Settings.Default, "MarkerBasedDetectorMinMarkerSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.minMarkerSize.Location = new System.Drawing.Point(130, 59);
            this.minMarkerSize.Name = "minMarkerSize";
            this.minMarkerSize.Size = new System.Drawing.Size(100, 20);
            this.minMarkerSize.TabIndex = 4;
            this.minMarkerSize.Text = global::PresenceSimulator.Properties.Settings.Default.MarkerBasedDetectorMinMarkerSize;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Max Marker Size (Pixel):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Min Marker Size (Pixel):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Threshold:";
            // 
            // threshold
            // 
            this.threshold.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::PresenceSimulator.Properties.Settings.Default, "MarkerBasedDetectorThreshold", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.threshold.Location = new System.Drawing.Point(69, 30);
            this.threshold.Maximum = 255;
            this.threshold.Name = "threshold";
            this.threshold.Size = new System.Drawing.Size(193, 45);
            this.threshold.TabIndex = 0;
            this.threshold.TickStyle = System.Windows.Forms.TickStyle.None;
            this.threshold.Value = global::PresenceSimulator.Properties.Settings.Default.MarkerBasedDetectorThreshold;
            // 
            // MarkerBasedDetector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 299);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "MarkerBasedDetector";
            this.Text = "Marker Based Detector Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.threshold)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox drawMarkersOnVideo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton thresholdEdgeImage;
        private System.Windows.Forms.RadioButton edgeImage;
        private System.Windows.Forms.RadioButton originalImage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar threshold;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox maxMarkerSize;
        private System.Windows.Forms.TextBox minMarkerSize;

    }
}