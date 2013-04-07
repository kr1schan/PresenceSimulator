/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using PresenceSimulator.Commands;
using PresenceSimulator.Detectors;
using PresenceSimulator.LocationSources;
using PresenceSimulator.Map;
using PresenceSimulator.Network;
using PresenceSimulator.Properties;
using PresenceSimulator.Recorder;

namespace PresenceSimulator
{
    public partial class MainForm : Form
    {
        private VideoPreprocessor videoPreprocessor = new VideoPreprocessor();
        private Detector detector;
        private DetectorFactory detectorFactory;
        private MapWithQRCodeReader qrCodeEncoder;
        public ColorDiscriminator colorPicker = null;

        public MainForm()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(mainForm_FormClosing);
            this.Move += delegate { new UpdateMapSizeAndPosition(this.mainVideo, MapOverlayForm.Instance).execute(); };
            this.SizeChanged += delegate { new UpdateMapSizeAndPosition(this.mainVideo, MapOverlayForm.Instance).execute(); };
            this.Load += delegate { new UpdateMapSizeAndPosition(this.mainVideo, MapOverlayForm.Instance).execute(); };

            MapOverlayForm.Instance.Owner = this;
            MapOverlayForm.Instance.Show();

            this.setDetector(Settings.Default.SelectedDetectorIndex);
            this.detectorComboBox.SelectedIndex = Settings.Default.SelectedDetectorIndex;
            this.detectorComboBox.SelectedValueChanged += delegate { this.setDetector(this.detectorComboBox.SelectedIndex); };
            
            this.updateVideoSourceCombo();
            this.videoSourceCombo.SelectedValueChanged += delegate { new UpdateVideoFormatComboCommand(this.videoSourceCombo.SelectedIndex, this.videoFormatCombo).execute(); };
            try
            {
                this.videoSourceCombo.SelectedIndex = Settings.Default.VideoSourceIndex;
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            Command changeVideoFormatCommand = new ChangeVideoFormatCommand(this.mainVideo, this.videoSourceCombo, this.videoFormatCombo, this);
            this.videoFormatCombo.SelectedValueChanged += delegate { changeVideoFormatCommand.execute(); };
            try
            {
                this.videoFormatCombo.SelectedIndex = Settings.Default.VideoFormatIndex;
                changeVideoFormatCommand.execute();
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            this.brightness.Scroll += delegate { this.videoPreprocessor.Brightness = this.brightness.Value; };

            this.contrast.Scroll += delegate { this.videoPreprocessor.Contrast = this.contrast.Value; };

            this.saturation.Scroll += delegate { this.videoPreprocessor.Saturation = Convert.ToSingle(this.saturation.Value) / 100.0f; };
            
            this.checkServerState.CheckedChanged += delegate { if (this.checkServerState.Checked) { NetworkServer.Instance.Start(); } else { NetworkServer.Instance.Stop(); } };
            NetworkServer.Instance.serverLog = this.serverLog;
            if (Settings.Default.NetworkServerActive)
                NetworkServer.Instance.Start();
            
            this.checkBroadcast.CheckedChanged += delegate { if (this.checkBroadcast.Checked) { Broadcaster.Instance.Start(); } else { Broadcaster.Instance.Stop(); } };
            if (Settings.Default.BroadcastLocationSources)
                Broadcaster.Instance.Start();

            this.mapZoom.Scroll += delegate { new MapZoomCommand(this.mapZoom.Value, false).execute(); };
            this.mapOpacity.Scroll += delegate { MapOverlayForm.Instance.Opacity = Convert.ToDouble(this.mapOpacity.Value) / 100.0; };
            this.mapBearing.Scroll += delegate { MapOverlayForm.Instance.RotationAngle = Convert.ToSingle(this.mapBearing.Value); };

            this.showDetectorSettings.Click += delegate { this.detector.SettingsForm().Show(); };

            this.addUser.Click += delegate { new CreateLocationSourceCommand(this.userListContainer, this.detectorFactory).execute(); };

            this.checkRecordUsersTracks.CheckedChanged += delegate {
                if (this.checkRecordUsersTracks.Checked)
                {
                    RecorderManager.Instance.pauseRecording();
                }
                else
                {
                    RecorderManager.Instance.continueRecording();
                };
            };

            this.paperFormat.SelectedIndex = Settings.Default.QRCodePaperFormat;
            this.saveQR.Click += delegate
            {
                MapWithQRCodeWriter writer = new MapWithQRCodeWriter();
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.Filter = "pdf files (*.pdf)|*.pdf";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    writer.write(this.paperFormat.SelectedIndex, saveFileDialog.FileName);
                }
                saveFileDialog.Dispose();
            };

            qrCodeEncoder = new MapWithQRCodeReader();
            this.encodeQR.Click += delegate
            {
                this.qrCodeEncoder.decode(this.mainVideo.GetCurrentVideoFrame());
            };
        }

        private void setDetector(int detectorIndex)
        {
            switch(detectorIndex)
            {
                case 0:
                    this.detectorFactory = new MarkerBasedDetectorFactory();
                    break;
                case 1:
                    this.detectorFactory = new ColorBasedDetectorFactory();
                    break;
            }

            this.detector = this.detectorFactory.CreateDetector();
        }

        private void updateVideoSourceCombo()
        {
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count == 0)
            {
                this.videoSourceCombo.Items.Add("No video source found");
            }
            else
            {
                foreach (FilterInfo device in videoDevices)
                {
                    this.videoSourceCombo.Items.Add(device.Name);
                }
            }
        }

        private void mainVideo_NewFrame(object sender, ref Bitmap image)
        {
            this.videoPreprocessor.applyCorrections(ref image);
            this.detector.Detect(ref image);
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LocationSourceManager.Instance.Shutdown();
            
            this.saveSettings();
            
            this.mainVideo.SignalToStop();
            this.mainVideo.WaitForStop();

            Broadcaster.Instance.Stop();
            NetworkServer.Instance.Stop();
            
            this.detector.SettingsForm().Dispose();
            
            MapOverlayForm.Instance.Dispose();

            this.Dispose();
        }

        private void saveSettings()
        {
            Settings.Default.MapLat = MapOverlayForm.Instance.Position.Lat;
            Settings.Default.MapLng = MapOverlayForm.Instance.Position.Lng;
            Settings.Default.QRCodePaperFormat = this.paperFormat.SelectedIndex;
            Settings.Default.VideoSourceIndex = this.videoSourceCombo.SelectedIndex;
            Settings.Default.SelectedDetectorIndex = this.detectorComboBox.SelectedIndex;
            Settings.Default.VideoBrightness = this.brightness.Value;
            Settings.Default.VideoContrast = this.contrast.Value;
            Settings.Default.VideoSaturation = this.saturation.Value;

            if (this.videoFormatCombo.SelectedIndex < 0)
                Settings.Default.VideoFormatIndex = 0;
            else
                Settings.Default.VideoFormatIndex = this.videoFormatCombo.SelectedIndex;

            Settings.Default.Save();
        }

        private void addUserTrack_Click(object sender, EventArgs e)
        {
            String path = string.Empty;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "gpx files (*.xml)|*.xml|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                new CreateLocationSourceTrackCommand(this.userListContainer, path).execute();
            }
            openFileDialog1.Dispose();
        }

        private void mainVideo_Click(object sender, EventArgs e)
        {
            if (mainVideo.IsRunning && (this.colorPicker != null))
            {
                Bitmap frame = mainVideo.GetCurrentVideoFrame();
                this.colorPicker.Color = frame.GetPixel(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y);
            }
            this.colorPicker = null;
        }

        private void resetSettings_Click(object sender, EventArgs e)
        {
            this.brightness.Value = 0;
            this.videoPreprocessor.Brightness = 0;
            this.contrast.Value = 0;
            this.videoPreprocessor.Contrast = 0;
            this.saturation.Value = 0;
            this.videoPreprocessor.Saturation = 0;

            this.detector.ResetSettings();

            new MapZoomCommand(16, false).execute();
            new MapOpacityCommand(0.5).execute();
            new MapBearingCommand(0).execute();
            MapOverlayForm.Instance.Position = new GMap.NET.PointLatLng(51.4580686, 7.01476143);
            this.paperFormat.SelectedIndex = 4;

            this.checkRecordUsersTracks.Checked = true;
            this.checkBroadcast.Checked = true;
            this.checkServerState.Checked = true;
        }

        private void linkMailToKrischan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:krischan.udelhoven@gmail.com");
        }

        private void linkToOpenStreetMap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.openstreetmap.org"); 
        }

        private void linkToZXing_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/zxing/");
        }

        private void linkToPDFsharp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.pdfsharp.net/");
        }

        private void linkToAForge_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/aforge/");
        }

        private void linkToGMap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://greatmaps.codeplex.com/");
        }


    }
}
