/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System.Drawing;
using System.Windows.Forms;
using AForge.Controls;
using AForge.Video.DirectShow;
using PresenceSimulator.Map;
using PresenceSimulator.LocationSources;

namespace PresenceSimulator.Commands
{
    class ChangeVideoFormatCommand : Command
    {
        private VideoSourcePlayer videoPlayer;
        private ComboBox videoFormatCombo;
        private ComboBox videoSourceCombo;
        private MainForm mainForm;

        public ChangeVideoFormatCommand(VideoSourcePlayer videoPlayer, ComboBox videoSourceCombo, ComboBox videoFormatCombo, MainForm mainForm)
        {
            this.videoPlayer = videoPlayer;
            this.videoFormatCombo = videoFormatCombo;
            this.videoSourceCombo = videoSourceCombo;
            this.mainForm = mainForm;
        }

        public void execute()
        {
            LocationSourceManager.Instance.Shutdown();
            this.videoPlayer.SignalToStop();
            this.videoPlayer.WaitForStop();

            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[this.videoSourceCombo.SelectedIndex].MonikerString);
            
            int i = this.videoFormatCombo.SelectedIndex;
            videoSource.DesiredFrameSize = videoSource.VideoCapabilities[i].FrameSize;

            Size videoPlayerSize = videoSource.VideoCapabilities[i].FrameSize;
            videoPlayer.Size = videoPlayerSize;
            Size videoPlayerParentSize = videoPlayer.Parent.Size;
            videoPlayer.Location = new Point((videoPlayerParentSize.Width / 2) - (videoPlayerSize.Width / 2), (videoPlayerParentSize.Height / 2) - (videoPlayerSize.Height / 2));

            new UpdateMapSizeAndPosition(this.videoPlayer, MapOverlayForm.Instance).execute();

            Size mainFormMinSize = videoPlayerSize;
            mainFormMinSize.Width += 230;
            mainFormMinSize.Height += 50;
            if (mainFormMinSize.Height < 475)
                mainFormMinSize.Height = 475;
            this.mainForm.MinimumSize = mainFormMinSize;

            this.videoPlayer.VideoSource = videoSource;
            this.videoPlayer.Start();
        }
    }
}
