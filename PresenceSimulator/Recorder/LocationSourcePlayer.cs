/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Globalization;
using System.Threading;
using System.Xml;
using System.Xml.XPath;
using GMap.NET;
using PresenceSimulator.LocationSources;

namespace PresenceSimulator.Recorder
{
    class LocationSourcePlayer
    {
        public Boolean Pause { get; set; }
        private LocationSource user;
        private XPathDocument track;
        private XPathNavigator trackNav;
        private XPathNodeIterator trkptIterator;
        private TimerCallback timerDelegate;
        private Timer timer;
        private LocationSourceTrackForm userTrackForm;
        private CultureInfo cultureInfo = new CultureInfo("en-US"); // e.g. 10.5 and not 10,5

        public LocationSourcePlayer(LocationSource user, String path)
        {
            this.user = user;
            try
            {
                track = new XPathDocument(path);
            }
            catch (XmlException xmlEcxeption)
            {
                throw xmlEcxeption;
            }

            trackNav = track.CreateNavigator();

            // extract user name from second comment
            String userNameComment = trackNav.SelectSingleNode("comment()[2]").ToString();
            String userName = userNameComment.Substring(20);
            this.user.Name = userName;

            this.setupTrkptIterator();
        }

        public void RegisterUserTrackForm(LocationSourceTrackForm userTrackForm)
        {
            this.userTrackForm = userTrackForm;
        }

        public void Play()
        {
            if (this.timer != null)
            {
                this.timer.Dispose();
            }
            this.timerDelegate = new TimerCallback(this.tick);
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            this.timer = new Timer(timerDelegate, autoEvent, 0, 1000);
        }

        public void Stop()
        {
            if (this.timer != null)
            {
                this.timer.Dispose();
                this.timer = null;
            }
            this.setupTrkptIterator();
        }

        private void setupTrkptIterator()
        {
            XPathExpression expr = trackNav.Compile("//trkpt");
            trkptIterator = trackNav.Select(expr);
            trkptIterator.MoveNext();
            long timestamp = long.Parse(trkptIterator.Current.SelectSingleNode("time").InnerXml);
        }

        private void tick(Object obj)
        {
            if (!this.Pause)
            {
                long timeTicks = long.Parse(trkptIterator.Current.SelectSingleNode("time").InnerXml);
                double lat = double.Parse(trkptIterator.Current.GetAttribute("lat", ""), this.cultureInfo);
                double lng = double.Parse(trkptIterator.Current.GetAttribute("lng", ""), this.cultureInfo);
                PointLatLng newPos = new PointLatLng(lat, lng);
                user.LatLng = newPos;

                if (!this.trkptIterator.MoveNext())
                {
                    if (this.userTrackForm != null)
                    {
                        this.userTrackForm.pressStop();
                    }
                }
                else
                {
                    long nextTimeTicks = long.Parse(trkptIterator.Current.SelectSingleNode("time").InnerXml);
                    long ticksBetweenTrkpt = nextTimeTicks - timeTicks;
                    TimeSpan ts = new TimeSpan(ticksBetweenTrkpt);
                    int msBetweenTrkpt = Convert.ToInt32(ts.TotalMilliseconds);
                    this.timer.Change(msBetweenTrkpt, msBetweenTrkpt);
                }
            }
        }
    }
}
