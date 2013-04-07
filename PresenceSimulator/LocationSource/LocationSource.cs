/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using AForge;
using GMap.NET;
using PresenceSimulator.Detectors;
using PresenceSimulator.Map;

namespace PresenceSimulator.LocationSources
{
    [Serializable]
    public class LocationSource : LocationSourceSubject
    {
        public IntPoint ScreenPos { get; set; }
        private String name;
        public String Name {
            get { return this.name;}
            set { this.name = value; this.NameChange(); }
        }
        public Guid Id { get; private set; }
        public Discriminator Discri { get; set; }
        private PointLatLng latLng = new PointLatLng();
        public PointLatLng LatLng
        {
            get { return this.latLng; }
            set { this.latLng = value; this.Notify(); }
        }
        public LocationSourceMapMarker mapMarker { get; set; }

        public LocationSource()
        {
            this.Id = System.Guid.NewGuid();
            this.mapMarker = new LocationSourceMapMarker(this);
            this.Name = "unknown";
        }

        public LocationSource(String name, Discriminator discrimintor)
        {
            this.Id = System.Guid.NewGuid();
            this.Name = name;
            this.Discri = discrimintor;
            this.mapMarker = new LocationSourceMapMarker(this);
        }
    }
}
