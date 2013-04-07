/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Globalization;
using System.IO;
using System.Xml;
using PresenceSimulator.LocationSources;
using System.Diagnostics;

namespace PresenceSimulator.Recorder
{
    public class LocationSourceRecorder: LocationSourceObserver
    {
        public Boolean Pause { get; set; }
        private LocationSource locationSource;
        private XmlTextWriter xmlWriter;
        private int updateInterval;
        private DateTime timeofLastMessage;
        private CultureInfo cultureInfo = new CultureInfo("en-US"); // e.g. 10.5 and not 10,5

        public LocationSourceRecorder(LocationSource locationSource, Boolean pause)
        {
            this.updateInterval = Properties.Settings.Default.locationUpdateInterval;
            Debug.WriteLine("UpdateInterval: " + updateInterval);
            this.Pause = pause;
            this.locationSource = locationSource;
            this.locationSource.Attach(this);

            if (!Directory.Exists(Properties.Settings.Default.userTrackFolder))
            {
                Directory.CreateDirectory(Properties.Settings.Default.userTrackFolder);
            }

            this.xmlWriter = new XmlTextWriter(Properties.Settings.Default.userTrackFolder + locationSource.Id + ".xml", System.Text.Encoding.UTF8);
            this.xmlWriter.Formatting = Formatting.Indented;
            this.xmlWriter.WriteStartDocument(false);
            this.xmlWriter.WriteComment("locationSourceId: " + locationSource.Id);
            this.xmlWriter.WriteComment("locationSourceName: " + locationSource.Name);
            this.xmlWriter.WriteStartElement("gpx");
            this.xmlWriter.WriteAttributeString("version", "1.1");
            this.xmlWriter.WriteAttributeString("creator", "PresenceSimulator");
            this.xmlWriter.WriteStartElement("trk");
            this.xmlWriter.WriteStartElement("trkseg");
            this.xmlWriter.Flush();
        }


        public void Update()
        {
            if (this.timeofLastMessage != null)
            {
                if ((DateTime.UtcNow - this.timeofLastMessage).Duration().TotalMilliseconds < this.updateInterval)
                {
                    return;
                }
            }

            if (!this.Pause)
            {
                try
                {
                    this.xmlWriter.WriteStartElement("trkpt");
                    this.xmlWriter.WriteAttributeString("lat", this.locationSource.LatLng.Lat.ToString(this.cultureInfo));
                    this.xmlWriter.WriteAttributeString("lng", this.locationSource.LatLng.Lng.ToString(this.cultureInfo));
                    this.xmlWriter.WriteElementString("time", DateTime.UtcNow.Ticks.ToString());
                    this.xmlWriter.WriteEndElement();
                    this.timeofLastMessage = DateTime.UtcNow;
                }
                catch
                {
                    return;
                }
            }
        }

        public void NameChange()
        {
            try
            {
                this.xmlWriter.WriteComment("userName: " + locationSource.Name);
            }
            catch
            {
                return;
            }
        }

        public void Delete()
        {
            this.xmlWriter.WriteEndElement();
            this.xmlWriter.WriteEndElement();
            this.xmlWriter.WriteEndElement();
            this.xmlWriter.Flush();
            this.xmlWriter.Close();
            RecorderManager.Instance.removeUserRecorder(this);
        }
    }
}
