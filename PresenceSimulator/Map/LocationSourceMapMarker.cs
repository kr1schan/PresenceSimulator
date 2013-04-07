/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Drawing;
using PresenceSimulator.LocationSources;

namespace PresenceSimulator.Map
{
    [Serializable]
    public class LocationSourceMapMarker : GMap.NET.WindowsForms.GMapMarker, LocationSourceObserver
    {
        public Pen OuterPen { get; set; }
        public Brush InnerBrush { get; set; }
        public Brush TextBrush { get; set; }
        public Font TextFont { get; set; }
        public String Text { get; set; }
        private int diameter = 10;
        private LocationSource locationSource;

        public LocationSourceMapMarker(LocationSource user)
            : base(user.LatLng)
        {
            this.locationSource = user;

            this.OuterPen = new Pen(Color.Black, 2);
            this.InnerBrush = new SolidBrush(Color.Gray);
            this.Text = user.Name;
            this.TextFont = new Font("Arial", 15, FontStyle.Bold);
            this.TextBrush = Brushes.DarkMagenta;
            this.Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2);
        }

        public override void OnRender(Graphics g)
        {
            g.FillEllipse(InnerBrush, new Rectangle(LocalPosition.X - (diameter / 2), LocalPosition.Y - (diameter / 2), diameter, diameter));
            g.DrawEllipse(OuterPen, new Rectangle(LocalPosition.X - (diameter / 2), LocalPosition.Y - (diameter / 2), diameter, diameter));

            if (!String.IsNullOrEmpty(this.Text))
            {
                SizeF sizeOfString = g.MeasureString(this.Text, this.TextFont);
                int x = (LocalPosition.X + diameter / 2) - (int)(sizeOfString.Width / 2);
                int y = (LocalPosition.Y + diameter) - (int)(sizeOfString.Height / 4);
                g.DrawString(this.Text, this.TextFont, this.TextBrush, x, y);
            }
        }

        public void Update()
        {
            this.Position = locationSource.LatLng;
           
        }

        public void NameChange()
        {
            this.Text = locationSource.Name;
        }

        public void Delete()
        {
            MapOverlayForm.Instance.RemoveUserMarker(this);
        }
    }
}