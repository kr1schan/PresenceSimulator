/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using AForge.Imaging;
using AForge.Imaging.Filters;
using PresenceSimulator.LocationSources;
using PresenceSimulator.Map;
using PresenceSimulator.Properties;

namespace PresenceSimulator.Detectors
{
    public partial class ColorBasedDetector : Form, Detector
    {
        private EuclideanColorFiltering colorFilter = new EuclideanColorFiltering();
        private Grayscale grayscaleFilter = Grayscale.CommonAlgorithms.BT709;
        private BlobCounter blobCounter = new BlobCounter();

        public ColorBasedDetector()
        {
            InitializeComponent();

            this.blobCounter.MinHeight = Convert.ToInt32(Settings.Default.ColorBasedDetectorMinObjectSize);
            this.blobCounter.MinWidth = Convert.ToInt32(Settings.Default.ColorBasedDetectorMinObjectSize);
            this.blobCounter.MaxHeight = Convert.ToInt32(Settings.Default.ColorBasedDetectorMaxObjectSize);
            this.blobCounter.MaxWidth = Convert.ToInt32(Settings.Default.ColorBasedDetectorMaxObjectSize);

            this.minObjectSize.TextChanged += delegate
            {
                int minSize;
                try
                {
                    minSize = Convert.ToInt32(this.minObjectSize.Text);
                }
                catch
                {
                    minSize = 15;
                }
                this.blobCounter.MinWidth = minSize;
                this.blobCounter.MinHeight = minSize;
            };

            this.maxObjectSize.TextChanged += delegate
            {
                int maxSize;
                try
                {
                    maxSize = Convert.ToInt32(this.maxObjectSize.Text);
                }
                catch
                {
                    maxSize = 200;
                }
                this.blobCounter.MaxHeight = maxSize;
                this.blobCounter.MaxWidth = maxSize;
            };

            this.colorTolerance.Value = Settings.Default.ColorBasedDetectorColorTolerance;
            colorFilter.Radius = (short)this.colorTolerance.Value;
            this.colorTolerance.ValueChanged += delegate
            {
                colorFilter.Radius = (short)this.colorTolerance.Value;
            };

            blobCounter.FilterBlobs = true;
            blobCounter.ObjectsOrder = ObjectsOrder.Size;

            this.FormClosing += new FormClosingEventHandler(ColorBasedDetector_FormClosing); 
        }

        public void Detect(ref Bitmap image)
        {
            lock (LocationSourceManager.Instance)
            {
                List<LocationSource> locationSources = LocationSourceManager.Instance.LocationSources;
                try
                {
                    foreach (LocationSource locationSource in locationSources)
                    {

                        if (typeof(ColorDiscriminator).Equals(locationSource.Discri.GetType()))
                        {
                            Color color = ((ColorDiscriminator)locationSource.Discri).Color;
                            RGB rgb = ((ColorDiscriminator)locationSource.Discri).RGB_color;
                            colorFilter.CenterColor = ((ColorDiscriminator)locationSource.Discri).RGB_color;

                            Bitmap objectsImage = null;
                            Bitmap mImage = null;
                            mImage = (Bitmap)image.Clone();

                            objectsImage = image;
                            colorFilter.ApplyInPlace(objectsImage);

                            BitmapData objectsData = objectsImage.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
                            UnmanagedImage grayImage = grayscaleFilter.Apply(new UnmanagedImage(objectsData));
                            objectsImage.UnlockBits(objectsData);

                            blobCounter.ProcessImage(grayImage);
                            Rectangle[] rects = blobCounter.GetObjectsRectangles();

                            if (!this.showFilteredImage.Checked)
                            {
                                image = mImage;
                            }

                            if (rects.Length > 0)
                            {
                                if (this.drawBoundingBoxes.Checked)
                                {
                                    Graphics g = Graphics.FromImage(image);
                                    using (Pen pen = new Pen(Color.FromArgb(160, 255, 160), 5))
                                    {
                                        g.DrawRectangle(pen, rects[0]);
                                    }

                                    g.Dispose();
                                }

                                Rectangle rectangle = rects[0];
                                int x = rectangle.X + (rectangle.Width / 2);
                                int y = rectangle.Y + (rectangle.Height / 2);
                                locationSource.LatLng = MapOverlayForm.Instance.FromLocalToLatLng(x, y);
                                locationSource.ScreenPos = new AForge.IntPoint(x, y);
                            }
 
                        }
                    }
                }
                catch (InvalidOperationException e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
        }

        public Form SettingsForm()
        {
            return this;
        }

        private void ColorBasedDetector_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        public void ResetSettings()
        {
            this.showFilteredImage.Checked = true;
            this.drawBoundingBoxes.Checked = true;
            this.colorTolerance.Value = 40;
            this.minObjectSize.Text = "5";
            this.maxObjectSize.Text = "200";
        }
    }
}
