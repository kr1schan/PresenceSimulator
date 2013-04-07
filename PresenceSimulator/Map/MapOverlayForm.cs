/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.ObjectModel;
using GMap.NET.WindowsForms;
using PresenceSimulator.Properties;
using PresenceSimulator.Commands;

namespace PresenceSimulator.Map
{
    public partial class MapOverlayForm : Form
    {
        private static readonly MapOverlayForm instance = new MapOverlayForm();
        public static MapOverlayForm Instance 
        {
            get { return instance; }
        }

        private GMapOverlay userMarkerOverlay;

        public int Zoom
        {
            get { return Convert.ToInt32(this.gMapControl.Zoom); }
            set
            {
                if ((value <= this.gMapControl.MaxZoom) && (value >= this.gMapControl.MinZoom))
                    this.gMapControl.Zoom = value;
            }
        }

        public float RotationAngle
        { 
            get { return this.gMapControl.Bearing; }
            set { this.gMapControl.Invalidate(); this.gMapControl.Bearing = value; ((MainForm)this.Owner).mapBearing.Value = (int)value; }
        }

        public PointLatLng Position
        {
            get { return this.gMapControl.Position; }
            set { this.gMapControl.Position = value; }
        }

        private MapOverlayForm()
        {
            InitializeComponent();

            this.Opacity = Convert.ToDouble(Settings.Default.MapOpacity) / 100.0; ;

            this.gMapControl.MapProvider = GMapProviders.OpenStreetMap;
            this.gMapControl.Position = new PointLatLng(Settings.Default.MapLat, Settings.Default.MapLng);

            this.gMapControl.MinZoom = 1;
            this.gMapControl.MaxZoom = 18;
            this.gMapControl.Zoom = Settings.Default.MapZoomLvl;

            this.gMapControl.Bearing = Convert.ToSingle(Settings.Default.MapBearing);

            this.gMapControl.DragButton = System.Windows.Forms.MouseButtons.Left;

            this.gMapControl.MouseWheel += new MouseEventHandler(this.gMapControl_MouseWheel);

            this.userMarkerOverlay = new GMapOverlay(this.gMapControl, "User Markers Overlay");
            this.gMapControl.Overlays.Add(userMarkerOverlay);
        }

        public void AddUserMarker(GMapMarker marker)
        {
            userMarkerOverlay.Markers.Add(marker);
        }

        public void RemoveUserMarker(GMapMarker marker)
        {
            userMarkerOverlay.Markers.Remove(marker);
        }

        public ObservableCollectionThreadSafe<GMapMarker> GetMarkers()
        {
            return this.userMarkerOverlay.Markers;
        }

        public PointLatLng FromLocalToLatLng(int x, int y)
        {
            return gMapControl.FromLocalToLatLng(x, y);
        }

        public void ChangeSize(Size size)
        {
            this.Size = size;
            this.gMapControl.Size = size;
        }

        public void MoveMapToLocalPosition(GPoint local, PointLatLng position)
        {
            PointLatLng actualPosition = this.gMapControl.FromLocalToLatLng(local.X, local.Y);
            
            double latDiff = actualPosition.Lat - position.Lat;
            double lngDiff = actualPosition.Lng - position.Lng;

            this.gMapControl.Position = new PointLatLng(this.gMapControl.Position.Lat - latDiff, this.gMapControl.Position.Lng - lngDiff);
        }

        public void SetMapScale(GPoint pointA, GPoint pointB, double soughtedScale)
        {
            double bestScaleDiff = Double.MaxValue;
            int bestScale = 0;

            for (int scale = 0; scale <= 18; scale++)
            {
                gMapControl.Zoom = scale;
                double currentScale = this.gMapControl.MapProvider.Projection.GetDistance(this.gMapControl.FromLocalToLatLng(pointA.X, pointA.Y), this.gMapControl.FromLocalToLatLng(pointB.X, pointB.Y));
                double scaleDiff = Math.Abs(currentScale - soughtedScale);
                if (scaleDiff < bestScaleDiff)
                {
                    bestScaleDiff = scaleDiff;
                    bestScale = scale;
                }
            }
            gMapControl.Zoom = bestScale;
            ((MainForm)this.Owner).mapZoom.Value = bestScale;
        }

        public Image GetMapImage()
        {
            double mapOpacity = this.Opacity;
            this.Opacity = 1;
            Image map = this.gMapControl.ToImage();
            this.Opacity = mapOpacity;
            return map;
        }

        private void gMapControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta == 120)
            {
                // zoomIn
                if (this.gMapControl.Zoom < this.gMapControl.MaxZoom)
                {
                    new MapZoomCommand(Convert.ToInt32(this.gMapControl.Zoom) + 1, true).execute();
                }
            }
            else
            {
                // e.Delta == -120 -> zoomOut
                if (this.gMapControl.Zoom > this.gMapControl.MinZoom)
                {
                    new MapZoomCommand(Convert.ToInt32(this.gMapControl.Zoom) - 1, true).execute();
                }
            }
        }
    }
}
