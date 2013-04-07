namespace PresenceSimulator.Detectors
{
    partial class ColorBasedDetector
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.drawBoundingBoxes = new System.Windows.Forms.CheckBox();
            this.showFilteredImage = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.minObjectSize = new System.Windows.Forms.TextBox();
            this.maxObjectSize = new System.Windows.Forms.TextBox();
            this.colorTolerance = new System.Windows.Forms.TrackBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorTolerance)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Color Tolerance:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Max Object Size (Pixel):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Min Object Size (Pixel):";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.drawBoundingBoxes);
            this.groupBox1.Controls.Add(this.showFilteredImage);
            this.groupBox1.Location = new System.Drawing.Point(15, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 74);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Appearance";
            // 
            // drawBoundingBoxes
            // 
            this.drawBoundingBoxes.AutoSize = true;
            this.drawBoundingBoxes.Checked = global::PresenceSimulator.Properties.Settings.Default.ColorBasedDetectorDrawBoundingBoxed;
            this.drawBoundingBoxes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawBoundingBoxes.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PresenceSimulator.Properties.Settings.Default, "ColorBasedDetectorDrawBoundingBoxed", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.drawBoundingBoxes.Location = new System.Drawing.Point(7, 44);
            this.drawBoundingBoxes.Name = "drawBoundingBoxes";
            this.drawBoundingBoxes.Size = new System.Drawing.Size(131, 17);
            this.drawBoundingBoxes.TabIndex = 1;
            this.drawBoundingBoxes.Text = "Draw Bounding Boxes";
            this.drawBoundingBoxes.UseVisualStyleBackColor = true;
            // 
            // showFilteredImage
            // 
            this.showFilteredImage.AutoSize = true;
            this.showFilteredImage.Checked = global::PresenceSimulator.Properties.Settings.Default.ColorBasedDetectorShowColorImage;
            this.showFilteredImage.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PresenceSimulator.Properties.Settings.Default, "ColorBasedDetectorShowColorImage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.showFilteredImage.Location = new System.Drawing.Point(7, 20);
            this.showFilteredImage.Name = "showFilteredImage";
            this.showFilteredImage.Size = new System.Drawing.Size(149, 17);
            this.showFilteredImage.TabIndex = 0;
            this.showFilteredImage.Text = "Show Color-Filtered Image";
            this.showFilteredImage.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.maxObjectSize);
            this.groupBox2.Controls.Add(this.minObjectSize);
            this.groupBox2.Controls.Add(this.colorTolerance);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(15, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(257, 112);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Functionality";
            // 
            // minObjectSize
            // 
            this.minObjectSize.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PresenceSimulator.Properties.Settings.Default, "ColorBasedDetectorMinObjectSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.minObjectSize.Location = new System.Drawing.Point(128, 52);
            this.minObjectSize.Name = "minObjectSize";
            this.minObjectSize.Size = new System.Drawing.Size(100, 20);
            this.minObjectSize.TabIndex = 8;
            this.minObjectSize.Text = global::PresenceSimulator.Properties.Settings.Default.ColorBasedDetectorMinObjectSize;
            // 
            // maxObjectSize
            // 
            this.maxObjectSize.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PresenceSimulator.Properties.Settings.Default, "ColorBasedDetectorMaxObjectSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.maxObjectSize.Location = new System.Drawing.Point(128, 76);
            this.maxObjectSize.Name = "maxObjectSize";
            this.maxObjectSize.Size = new System.Drawing.Size(100, 20);
            this.maxObjectSize.TabIndex = 9;
            this.maxObjectSize.Text = global::PresenceSimulator.Properties.Settings.Default.ColorBasedDetectorMaxObjectSize;
            // 
            // colorTolerance
            // 
            this.colorTolerance.AutoSize = false;
            this.colorTolerance.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::PresenceSimulator.Properties.Settings.Default, "ColorBasedDetectorColorTolerance", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.colorTolerance.Location = new System.Drawing.Point(90, 17);
            this.colorTolerance.Maximum = 255;
            this.colorTolerance.Name = "colorTolerance";
            this.colorTolerance.Size = new System.Drawing.Size(161, 27);
            this.colorTolerance.TabIndex = 7;
            this.colorTolerance.TickFrequency = 10;
            this.colorTolerance.Value = global::PresenceSimulator.Properties.Settings.Default.ColorBasedDetectorColorTolerance;
            // 
            // ColorBasedDetector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 226);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ColorBasedDetector";
            this.Text = "ColorBasedDetector";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorTolerance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox drawBoundingBoxes;
        private System.Windows.Forms.CheckBox showFilteredImage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar colorTolerance;
        private System.Windows.Forms.TextBox maxObjectSize;
        private System.Windows.Forms.TextBox minObjectSize;
    }
}