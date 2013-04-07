/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System.Windows.Forms;
using PresenceSimulator.Commands;

namespace PresenceSimulator.LocationSources
{
    class LocationSourceFormView : LocationSourceObserver
    {
        private Panel parent;
        private LocationSource locationSource;
        private GroupBox container;
        private Label lat;
        private Label lng;

        public LocationSourceFormView(LocationSource locationSource, Panel parent)
        {
            this.locationSource = locationSource;
            this.parent = parent;

            this.container = new GroupBox();
            this.container.Width = this.parent.Width - 25;
            this.container.Height = 159;
            parent.Controls.Add(container);

            Label nameLabel = new Label();
            nameLabel.Text = "Name:";
            nameLabel.Location = new System.Drawing.Point(5,14);
            nameLabel.AutoSize = true;
            container.Controls.Add(nameLabel);

            TextBox nameInput = new TextBox();
            nameInput.Text = this.locationSource.Name;
            nameInput.Width = 85;
            nameInput.Location = new System.Drawing.Point(45, 12);
            container.Controls.Add(nameInput);
            nameInput.TextChanged += delegate { this.locationSource.Name = nameInput.Text; };

            Label latLabel = new Label();
            latLabel.Text = "Lat:";
            latLabel.Location = new System.Drawing.Point(5, 40);
            latLabel.AutoSize = true;
            container.Controls.Add(latLabel);

            lat = new Label();
            lat.Text = "unknown";
            lat.Location = new System.Drawing.Point(45, 40);
            lat.Width = 85;
            lat.Height = 13;
            container.Controls.Add(lat);

            Label lngLabel = new Label();
            lngLabel.Text = "Lng:";
            lngLabel.Location = new System.Drawing.Point(5, 60);
            lngLabel.AutoSize = true;
            container.Controls.Add(lngLabel);

            lng = new Label();
            lng.Text = "unknown";
            lng.Location = new System.Drawing.Point(45, 60);
            lng.Width = 85;
            lng.Height = 13;
            container.Controls.Add(lng);

            Control discriControl = this.locationSource.Discri.GetControl();
            discriControl.Size = new System.Drawing.Size(50, 50);
            discriControl.Location = new System.Drawing.Point(45, 77);
            container.Controls.Add(discriControl);

            Button removeLocationSource = new Button();
            removeLocationSource.Text = "Remove";
            removeLocationSource.Location = new System.Drawing.Point(5, 130);
            removeLocationSource.Width = 127;
            removeLocationSource.Click += delegate { new DeleteLocationSourceCommand(this.locationSource).execute(); };
            container.Controls.Add(removeLocationSource);
        }

        public void Update()
        {
            if (this.lat.InvokeRequired)
            {
                try
                {
                    this.lat.Invoke((MethodInvoker)delegate()
                    {
                        this.lat.Text = this.locationSource.LatLng.Lat.ToString();
                    });
                }
                catch
                {
                    return;
                }
            }
            else
            {
                this.lat.Text = this.locationSource.LatLng.Lat.ToString();
            }

            if (this.lng.InvokeRequired)
            {
                try
                {
                    this.lng.Invoke((MethodInvoker)delegate()
                    {
                        this.lng.Text = this.locationSource.LatLng.Lng.ToString();
                    });
                }
                catch
                {
                    return;
                }
            }
            else
            {
                this.lng.Text = this.locationSource.LatLng.Lng.ToString();
            }
        }

        public void NameChange()
        {
            // unimplemented
        }

        public void Delete()
        {
            this.container.Dispose();
        }
    }
}
