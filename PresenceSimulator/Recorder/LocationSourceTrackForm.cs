/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System.Windows.Forms;
using PresenceSimulator.Commands;
using PresenceSimulator.LocationSources;

namespace PresenceSimulator.Recorder
{
    class LocationSourceTrackForm : LocationSourceObserver
    {
        private Panel parent;
        private LocationSource locationSource;
        private GroupBox container;
        private LocationSourcePlayer locationSourcePlayer;
        private Label name;
        private Label lat;
        private Label lng;
        private CheckBox play;
        private CheckBox pause;
        private CheckBox stop;

        public LocationSourceTrackForm(LocationSource locationSource, Panel parent, LocationSourcePlayer locationSourcePlayer)
        {
            this.locationSource = locationSource;
            this.parent = parent;
            this.locationSourcePlayer = locationSourcePlayer;

            this.container = new GroupBox();
            this.container.Width = this.parent.Width - 25;
            this.container.Height = 132;
            parent.Controls.Add(container);

            Label nameLabel = new Label();
            nameLabel.Text = "Name:";
            nameLabel.Location = new System.Drawing.Point(5, 14);
            nameLabel.AutoSize = true;
            container.Controls.Add(nameLabel);

            name = new Label();
            name.Text = this.locationSource.Name;
            name.Location = new System.Drawing.Point(45, 14);
            name.AutoSize = true;
            container.Controls.Add(name);

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

            play = new CheckBox();
            play.Appearance = Appearance.Button;
            play.Text = "Play";
            play.Location = new System.Drawing.Point(5, 80);
            play.Width = 40;
            play.Click += delegate
            { 
                this.stop.Enabled = true;
                this.stop.Checked = false;
                this.play.Checked = true;
                this.play.Enabled = false;
                this.locationSourcePlayer.Play();
            };
            container.Controls.Add(play);

            pause = new CheckBox();
            pause.Appearance = Appearance.Button;
            pause.Text = "Pause";
            pause.Location = new System.Drawing.Point(46, 80);
            pause.Width = 45;
            pause.Click += delegate
            {
                if (this.pause.Checked)
                    this.locationSourcePlayer.Pause = true;
                else
                    this.locationSourcePlayer.Pause = false;
            };
            container.Controls.Add(pause);

            stop = new CheckBox();
            stop.Appearance = Appearance.Button;
            stop.Checked = true;
            stop.Enabled = false;
            stop.Text = "Stop";
            stop.Location = new System.Drawing.Point(92, 80);
            stop.Width = 40;
            stop.Click += delegate
            {
                this.pressStop();
            };
            container.Controls.Add(stop);

            Button removeUser = new Button();
            removeUser.Text = "Remove Track";
            removeUser.Location = new System.Drawing.Point(5, 103);
            removeUser.Width = 127;
            removeUser.Click += delegate { new DeleteLocationSourceCommand(this.locationSource).execute(); };
            container.Controls.Add(removeUser);

            this.locationSource.Attach(this);
            this.locationSourcePlayer.RegisterUserTrackForm(this);
        }

        public void pressStop()
        {
            if (this.stop.InvokeRequired)
            {
                try
                {
                    this.stop.Invoke((MethodInvoker)delegate()
                    {
                        this.stop.Enabled = false;
                        this.stop.Checked = true;
                    });
                }
                catch
                {
                    return;
                }
            }
            else
            {
                this.stop.Enabled = false;
                this.stop.Checked = true;
            }

            if (this.play.InvokeRequired)
            {
                try
                {
                    this.play.Invoke((MethodInvoker)delegate()
                    {
                        this.play.Enabled = true;
                        this.play.Checked = false;
                    });
                }
                catch
                {
                    return;
                }
            }
            else
            {
                this.play.Enabled = true;
                this.play.Checked = false;
            }
            this.locationSourcePlayer.Stop();
        }

        public void Update()
        {
            if (this.lat.InvokeRequired)
            {
                try
                {
                    this.lat.Invoke((MethodInvoker)delegate()
                    {
                        this.lat.Text = locationSource.LatLng.Lat.ToString();
                    });
                }
                catch
                {
                    return;
                }
            }
            else
            {
                this.lat.Text = locationSource.LatLng.Lat.ToString();
            }

            if (this.lng.InvokeRequired)
            {
                try
                {
                    this.lng.Invoke((MethodInvoker)delegate()
                    {
                        this.lng.Text = locationSource.LatLng.Lng.ToString();
                    });
                }
                catch
                {
                    return;
                }
            }
            else
            {
                this.lng.Text = locationSource.LatLng.Lat.ToString();
            }
        }

        public void NameChange()
        {
            if (this.name.InvokeRequired)
            {
                try
                {
                    this.name.Invoke((MethodInvoker)delegate()
                    {
                        this.name.Text = locationSource.Name;
                    });
                }
                catch
                {
                    return;
                }
            }
            else
            {
                this.name.Text = locationSource.Name;
            }
        }

        public void Delete()
        {
            this.parent.Controls.Remove(this.container);
        }
    }
}
