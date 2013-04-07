/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using PresenceSimulator.LocationSources;
using PresenceSimulator.Properties;

namespace PresenceSimulator.Detectors
{
    public partial class MarkerBasedDetector : Form, Detector
    {
        private int binThreshold = Settings.Default.MarkerBasedDetectorThreshold;
        private SimpleShapeChecker shapeChecker = new SimpleShapeChecker();
        private BlobCounter blobCounter = new BlobCounter();

        public MarkerBasedDetector()
        {
            InitializeComponent();

            this.blobCounter.MinHeight = Convert.ToInt32(Settings.Default.MarkerBasedDetectorMinMarkerSize);
            this.blobCounter.MinWidth = Convert.ToInt32(Settings.Default.MarkerBasedDetectorMinMarkerSize);
            this.blobCounter.MaxHeight = Convert.ToInt32(Settings.Default.MarkerBasedDetectorMaxMarkerSize);
            this.blobCounter.MaxWidth = Convert.ToInt32(Settings.Default.MarkerBasedDetectorMaxMarkerSize);
            this.blobCounter.FilterBlobs = true;
            this.blobCounter.ObjectsOrder = ObjectsOrder.Size;

            this.threshold.Scroll += new EventHandler(threshold_Scroll);
            this.minMarkerSize.TextChanged += new EventHandler(minMarkerSize_TextChanged);
            this.maxMarkerSize.TextChanged += new EventHandler(maxMarkerSize_TextChanged);
            this.FormClosing += new FormClosingEventHandler(MarkerBasedDetector_FormClosing); 
        }

        public Form SettingsForm()
        {
            return this;
        }

        public void Detect(ref Bitmap image)
        {
            List<List<IntPoint>> markers = new List<List<IntPoint>>();
            Bitmap tmp = image;

            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
            UnmanagedImage unmanagedImage = new UnmanagedImage(bitmapData);

            UnmanagedImage grayImage = UnmanagedImage.Create(unmanagedImage.Width, unmanagedImage.Height, PixelFormat.Format8bppIndexed);
            Grayscale.CommonAlgorithms.BT709.Apply(unmanagedImage, grayImage);

            DifferenceEdgeDetector edgeDetector = new DifferenceEdgeDetector();
            UnmanagedImage edgesImage = edgeDetector.Apply(grayImage);

            image.UnlockBits(bitmapData);

            if (this.edgeImage.Checked)
                tmp = edgesImage.ToManagedImage().Clone(new Rectangle(0,0,edgesImage.Width, edgesImage.Height), PixelFormat.Format24bppRgb);

            Threshold thresholdFilter = new Threshold(this.binThreshold);
            thresholdFilter.ApplyInPlace(edgesImage);

            if (this.thresholdEdgeImage.Checked)
                tmp = edgesImage.ToManagedImage().Clone(new Rectangle(0, 0, edgesImage.Width, edgesImage.Height), PixelFormat.Format24bppRgb);

            this.blobCounter.ProcessImage(edgesImage);
            Blob[] blobs = blobCounter.GetObjectsInformation();

            for (int i = 0, n = blobs.Length; i < n; i++)
            {
                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);
                List<IntPoint> corners = null;


                if (this.isSquare(edgePoints, out corners))
                {
                    List<IntPoint> leftEdgePoints, rightEdgePoints;
                    blobCounter.GetBlobsLeftAndRightEdges(blobs[i],
                        out leftEdgePoints, out rightEdgePoints);

                    float diff = calculateAverageEdgesBrightnessDifference(
                        leftEdgePoints, rightEdgePoints, grayImage);

                    if (diff > 50)
                    {
                        markers.Add(corners);
                    }
                }
            }
            
            foreach (List<IntPoint> marker in markers)
            {
                Color markerColor;
                IntPoint markerOrientation = this.markerOrientation(image, marker, out markerColor);
                IntPoint center = marker[2] - marker[0];
                center.X = marker[0].X + Convert.ToInt32(center.X * 0.5);
                center.Y = marker[0].Y + Convert.ToInt32(center.Y * 0.5);

                if (this.drawMarkersOnVideo.Checked)
                {
                    if ((this.edgeImage.Checked) || (this.thresholdEdgeImage.Checked))
                        this.drawMarker(tmp, marker, markerOrientation, markerColor);
                    else
                        this.drawMarker(image, marker, markerOrientation, markerColor);
                }
                ColorDiscriminator discriminator = new ColorDiscriminator();
                discriminator.Color = markerColor;

                LocationSourceManager.Instance.updateLocationSource(discriminator, center);
            }
            image = tmp;
        }

        private IntPoint markerOrientation(Bitmap image, List<IntPoint> marker, out Color markerColor)
        {
            List<IntPoint> samples = new List<IntPoint>();
            samples.Add(new IntPoint(marker[0].X + Convert.ToInt32((marker[2] - marker[0]).X * 0.3), marker[0].Y + Convert.ToInt32((marker[2] - marker[0]).Y * 0.3)));
            samples.Add(new IntPoint(marker[1].X + Convert.ToInt32((marker[3] - marker[1]).X * 0.3), marker[1].Y + Convert.ToInt32((marker[3] - marker[1]).Y * 0.3)));
            samples.Add(new IntPoint(marker[2].X + Convert.ToInt32((marker[0] - marker[2]).X * 0.3), marker[2].Y + Convert.ToInt32((marker[0] - marker[2]).Y * 0.3)));
            samples.Add(new IntPoint(marker[3].X + Convert.ToInt32((marker[1] - marker[3]).X * 0.3), marker[3].Y + Convert.ToInt32((marker[1] - marker[3]).Y * 0.3)));

            IntPoint orientation = samples[0];
            float maxBrightness = -1.0f;

            foreach (IntPoint sample in samples)
            {
                float brightness = image.GetPixel(sample.X, sample.Y).GetBrightness();
                if (brightness > maxBrightness)
                {
                    maxBrightness = brightness;
                    orientation = sample;
                }
            }

            markerColor = image.GetPixel(orientation.X, orientation.Y);
            return orientation;
        }


        private bool isSquare(List<IntPoint> edgePoints, out List<IntPoint> corners)
        {
            if (shapeChecker.IsQuadrilateral(edgePoints, out corners))
            {
                // edges same length?
                IntPoint v1 = corners[0] - corners[1];
                IntPoint v2 = corners[1] - corners[2];
                float t1 = v1.EuclideanNorm() - v2.EuclideanNorm();
                if (t1 < 0)
                    t1 = -t1;
                if (t1 > 3)
                    return false;

                IntPoint v3 = corners[2] - corners[3];
                float t2 = v2.EuclideanNorm() - v3.EuclideanNorm();
                if (t2 < 0)
                    t2 = -t2;
                if (t2 > 3)
                    return false;

                IntPoint v4 = corners[3] - corners[0];
                float t3 = v3.EuclideanNorm() - v4.EuclideanNorm();
                if (t3 < 0)
                    t3 = -t3;
                if (t3 > 3)
                    return false;

                return true;
            }
            else
                return false;
        }

        const int stepSize = 3;
        private float calculateAverageEdgesBrightnessDifference(List<IntPoint> leftEdgePoints, List<IntPoint> rightEdgePoints, UnmanagedImage image)
        {
            List<IntPoint> leftEdgePoints1 = new List<IntPoint>();
            List<IntPoint> leftEdgePoints2 = new List<IntPoint>();
            List<IntPoint> rightEdgePoints1 = new List<IntPoint>();
            List<IntPoint> rightEdgePoints2 = new List<IntPoint>();

            int tx1, tx2, ty;
            int widthM1 = image.Width - 1;

            for (int k = 0; k < leftEdgePoints.Count; k++)
            {
                tx1 = leftEdgePoints[k].X - stepSize;
                tx2 = leftEdgePoints[k].X + stepSize;
                ty = leftEdgePoints[k].Y;

                leftEdgePoints1.Add(new IntPoint((tx1 < 0) ? 0 : tx1, ty));
                leftEdgePoints2.Add(new IntPoint((tx2 > widthM1) ? widthM1 : tx2, ty));

                tx1 = rightEdgePoints[k].X - stepSize;
                tx2 = rightEdgePoints[k].X + stepSize;
                ty = rightEdgePoints[k].Y;

                rightEdgePoints1.Add(new IntPoint((tx1 < 0) ? 0 : tx1, ty));
                rightEdgePoints2.Add(new IntPoint((tx2 > widthM1) ? widthM1 : tx2, ty));
            }

            byte[] leftValues1 = image.Collect8bppPixelValues(leftEdgePoints1);
            byte[] leftValues2 = image.Collect8bppPixelValues(leftEdgePoints2);
            byte[] rightValues1 = image.Collect8bppPixelValues(rightEdgePoints1);
            byte[] rightValues2 = image.Collect8bppPixelValues(rightEdgePoints2);

            float diff = 0;
            int pixelCount = 0;

            for (int k = 0; k < leftEdgePoints.Count; k++)
            {
                if (rightEdgePoints[k].X - leftEdgePoints[k].X > stepSize * 2)
                {
                    diff += (leftValues1[k] - leftValues2[k]);
                    diff += (rightValues2[k] - rightValues1[k]);
                    pixelCount += 2;
                }
            }

            return diff / pixelCount;
        }

        private void drawMarker(Bitmap image, List<IntPoint> marker, IntPoint markerOrientation, Color markerColor)
        {
            Graphics g = Graphics.FromImage(image);

            using (Pen pen = new Pen(markerColor, 5))
            {
                System.Drawing.Point[] markerOutline = new System.Drawing.Point[]{new System.Drawing.Point(marker[0].X, marker[0].Y),
                    new System.Drawing.Point(marker[1].X, marker[1].Y),
                    new System.Drawing.Point(marker[2].X, marker[2].Y),
                    new System.Drawing.Point(marker[3].X, marker[3].Y)};
                g.DrawPolygon(pen, markerOutline);
            }

            g.Dispose();
        }

        private void threshold_Scroll(object sender, System.EventArgs e)
        {
            this.binThreshold = this.threshold.Value;
        }

        private void minMarkerSize_TextChanged(object sender, System.EventArgs e)
        {
            int minSize;
            try
            {
                minSize = Convert.ToInt32(this.minMarkerSize.Text);
            }
            catch
            {
                minSize = 15;
            }
            this.blobCounter.MinHeight = minSize;
            this.blobCounter.MinHeight = minSize;
        }

        private void maxMarkerSize_TextChanged(object sender, System.EventArgs e)
        {
            int maxSize;
            try
            {
                maxSize = Convert.ToInt32(this.maxMarkerSize.Text);
            }
            catch
            {
                maxSize = 40;
            }
            this.blobCounter.MaxHeight = maxSize;
            this.blobCounter.MaxWidth = maxSize;
        }

        private void MarkerBasedDetector_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }


        public void ResetSettings()
        {
            this.edgeImage.Checked = false;
            this.thresholdEdgeImage.Checked = false;
            this.originalImage.Checked = true;
            this.threshold.Value = 70;
            this.minMarkerSize.Text = "5";
            this.maxMarkerSize.Text = "60";
        }
    }
}

