/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Windows.Forms;
using System.Xml;
using PresenceSimulator.Detectors;
using PresenceSimulator.LocationSources;
using PresenceSimulator.Map;
using PresenceSimulator.Recorder;

namespace PresenceSimulator.Commands
{
    class CreateLocationSourceTrackCommand : Command
    {
        private Panel userListContainer;
        private String path;

        public CreateLocationSourceTrackCommand(Panel userListContainer, String path)
        {
            this.userListContainer = userListContainer;
            this.path = path;
        }

        public void execute()
        {
            LocationSource locationSource = LocationSourceManager.Instance.createLocationSource("LocationSource" + LocationSourceManager.Instance.LocationSources.Count, new DummyDiscriminator());
            //user.Discri = new ColorDiscriminator();
            LocationSourcePlayer locationSourcePlayer = null;
            try
            {
                locationSourcePlayer = new LocationSourcePlayer(locationSource, path);
            }
            catch (XmlException)
            {
                LocationSourceManager.Instance.deleteLocationSource(locationSource);
                System.Windows.Forms.MessageBox.Show("There is an error in your XML file. ", "Format Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }
            MapOverlayForm.Instance.AddUserMarker(locationSource.mapMarker);
            locationSource.Attach(locationSource.mapMarker);
            LocationSourceTrackForm locationSourceView = new LocationSourceTrackForm(locationSource, userListContainer, locationSourcePlayer);
        }
    }
}
