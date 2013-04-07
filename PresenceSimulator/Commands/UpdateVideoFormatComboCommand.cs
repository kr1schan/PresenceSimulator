/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System.Windows.Forms;
using AForge.Video.DirectShow;

namespace PresenceSimulator.Commands
{
    class UpdateVideoFormatComboCommand : Command
    {
        private int videoSourceIndex;
        private ComboBox videoFormatCombo;

        public UpdateVideoFormatComboCommand(int videoSource, ComboBox videoFormatCombo)
        {
            this.videoSourceIndex = videoSource;
            this.videoFormatCombo = videoFormatCombo;
        }

        public void execute()
        {
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0)
            {
                return;
            }
            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[this.videoSourceIndex].MonikerString);

            videoFormatCombo.Items.Clear();
            foreach (VideoCapabilities videoCapability in videoSource.VideoCapabilities)
            {
                videoFormatCombo.Items.Add(videoCapability.FrameSize.Width.ToString() + "x" + videoCapability.FrameSize.Height.ToString() + "@" + videoCapability.FrameRate.ToString() + "FPS");
            }
            videoFormatCombo.SelectedIndex = 0;
        }
    }
}
