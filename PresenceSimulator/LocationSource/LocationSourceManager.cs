/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using AForge;
using PresenceSimulator.Commands;
using PresenceSimulator.Detectors;
using PresenceSimulator.Map;

namespace PresenceSimulator.LocationSources
{
    public sealed partial class LocationSourceManager
    {
        private static readonly LocationSourceManager instance = new LocationSourceManager();
        public static LocationSourceManager Instance
        {
            get { return instance; }
        }

        private List<LocationSource> locationSources;

        private LocationSourceManager()
        {
            this.locationSources = new List<LocationSource>();
        }

        public void updateLocationSource(Discriminator discriminator, IntPoint screenPos)
        {
            if (locationSources.Count == 0)
                return;
            lock (this.locationSources)
            {
                for (int i = 0; i < locationSources.Count; i++)
                {
                    if (locationSources[i].Discri.IsSimilar(discriminator))
                    {
                        locationSources[i].ScreenPos = screenPos;
                        locationSources[i].LatLng = MapOverlayForm.Instance.FromLocalToLatLng(screenPos.X, screenPos.Y);
                    }
                }
            }
        }



        public LocationSource getLocationSourceById(Guid id)
        {
            foreach (LocationSource locationSource in locationSources)
            {
                if (id.Equals(locationSource.Id))
                    return locationSource;
            }
            return null;
        }

        public List<LocationSource> LocationSources
        {
            get { return this.locationSources; }
        }

        public string getLocationSourcesAsXml()
        {
            string xmlDoc = "<users>";

            foreach (LocationSource user in LocationSourceManager.Instance.LocationSources)
            {
                xmlDoc = xmlDoc + "<user id=\"" + user.Id + "\" name =\"" + user.Name + "\"/>";
            }
            xmlDoc = xmlDoc + "</users>";

            return xmlDoc;
        }

        public LocationSource createLocationSource(string name, Discriminator discriminator)
        {
            LocationSource user = new LocationSource(name, discriminator);
            this.LocationSources.Add(user);
            return user;
        }

        public void deleteLocationSource(LocationSource locationSource)
        {
            if (this.locationSources.Contains(locationSource))
            {
                locationSource.Delete();
                this.locationSources.Remove(locationSource);
            }
        }

        public void Shutdown()
        {
            List<LocationSource> locationSourcesToRemove = new List<LocationSource>();

            foreach (LocationSource locationSource in this.locationSources)
            {
                locationSourcesToRemove.Add(locationSource);
            }

            foreach( LocationSource locationSource in locationSourcesToRemove)
            {
                new DeleteLocationSourceCommand(locationSource).execute();
            }
        }
    }
}