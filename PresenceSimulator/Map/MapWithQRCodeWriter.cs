/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using com.google.zxing.common;
using GMap.NET;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.IO;
using System.Diagnostics;

namespace PresenceSimulator.Map
{
    class MapWithQRCodeWriter
    {
        private Image map;
        private int codeSize = 450;

        public MapWithQRCodeWriter()
        {
            this.map = MapOverlayForm.Instance.GetMapImage();
        }

        public void write(int paperFormatIndex, String fileName)
        {
            PageSize pageSize;
            paperFormatIndex++;
            
            switch (paperFormatIndex)
            {
                case (int)PageSize.A0:
                    pageSize = PageSize.A0;
                    break;
                case (int)PageSize.A1:
                    pageSize = PageSize.A1;
                    break;
                case (int)PageSize.A2:
                    pageSize = PageSize.A2;
                    break;
                case (int)PageSize.A3:
                    pageSize = PageSize.A3;
                    break;
                default:
                    pageSize = PageSize.A4;
                    break;
            }

            //Grayscale Map
            //Image grayscaledMap = MapWithQRCodeWriter.MakeGrayscale(map);

            // scale map
            Image scaledMap = this.scaleImageToFitFormat(pageSize, map);
            this.drawFrame(ref scaledMap);

            // get location
            int locationX = Convert.ToInt32(((this.codeSize / 2.0f) / scaledMap.Width) * map.Width);
            int locationY = Convert.ToInt32(((this.codeSize / 2.0f) / scaledMap.Height) * map.Height);
            PointLatLng location = MapOverlayForm.Instance.FromLocalToLatLng(locationX, locationY);

            // get scale
            int p1X = Convert.ToInt32((92.0f / scaledMap.Width) * map.Width);
            int p1Y = Convert.ToInt32((92.0f / scaledMap.Width) * map.Width);
            PointLatLng p1Location = MapOverlayForm.Instance.FromLocalToLatLng(p1X, p1Y);
            int p2X = Convert.ToInt32((356.0f / scaledMap.Width) * map.Width);
            int p2Y = Convert.ToInt32((92.0f / scaledMap.Width) * map.Width);
            PointLatLng p2Location = MapOverlayForm.Instance.FromLocalToLatLng(p2X, p2Y);
            double scale = MapOverlayForm.Instance.gMapControl.MapProvider.Projection.GetDistance(p1Location, p2Location);

            // create qr code
            com.google.zxing.qrcode.QRCodeWriter qrCode = new com.google.zxing.qrcode.QRCodeWriter();
            String locationString = location.Lat.ToString() + ";" + location.Lng.ToString() + ";" + scale.ToString().Substring(0, 6);
            ByteMatrix byteIMG = qrCode.encode(locationString, com.google.zxing.BarcodeFormat.QR_CODE, this.codeSize, this.codeSize);

            // draw qr code on map image
            this.drawQRCodeOnImage(byteIMG, ref scaledMap);

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            page.Size = pageSize;
            page.Orientation = PageOrientation.Landscape;
            XGraphics gfx = XGraphics.FromPdfPage(page);


            XImage image = XImage.FromGdiPlusImage(scaledMap);
            
            XUnit imgHeight = XUnit.FromInch(image.PixelHeight / image.VerticalResolution);
            XUnit imgWidth = XUnit.FromInch(image.PixelWidth / image.HorizontalResolution);

            double width = image.PixelWidth * 72 / image.HorizontalResolution;
            double height = image.PixelHeight * 72 / image.VerticalResolution;

            gfx.DrawImage(image, (page.Width.Point - imgWidth.Point) / 2.0, (page.Height.Point - imgHeight.Point) / 2.0, width, height);
            try
            {
                document.Save(fileName);
            }
            catch (IOException)
            {
                System.Windows.Forms.MessageBox.Show("Could not save the file. Maybe the file is used by another program. ", "IO Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            document.Dispose();
        }


        private void drawQRCodeOnImage(ByteMatrix qrCode, ref Image image)
        {
            sbyte[][] img = qrCode.Array;
  
            Graphics g = Graphics.FromImage(image);

            int pixelSize = 1;

            int i = 0;
            int j = 0;
            for (i = 0; i < img.Length; i += pixelSize)
            {
               for (j = 0; j < img[i].Length; j += pixelSize)
                {
                    if (img[j][i] == 0)
                    {
                        g.FillRectangle(Brushes.Black, i * pixelSize, j * pixelSize, pixelSize, pixelSize);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.White, i * pixelSize, j * pixelSize, pixelSize, pixelSize);
                    }
                }
            }
            Debug.WriteLine("X: " + i * pixelSize, "Y: " + j  * pixelSize);
            g.Dispose();
        }

        private Image scaleImageToFitFormat(PageSize pageSize, Image img) 
        {
            float hScale = img.HorizontalResolution * 10.1f;
            float vScale = img.VerticalResolution * 6.7f;
            switch (pageSize)
            {
                case PageSize.A0:
                    hScale = Convert.ToSingle(hScale * Math.Pow(1.45f, 4)) / (img.Width / img.HorizontalResolution);
                    vScale = Convert.ToSingle(vScale * Math.Pow(1.35f, 4)) / (img.Height / img.VerticalResolution);
                    break;
                case PageSize.A1:
                    hScale = Convert.ToSingle(hScale * Math.Pow(1.45f, 3)) / (img.Width / img.HorizontalResolution);
                    vScale = Convert.ToSingle(vScale * Math.Pow(1.35f, 3)) / (img.Height / img.VerticalResolution);
                    break;
                case PageSize.A2:
                    hScale = Convert.ToSingle(hScale * Math.Pow(1.45f, 2)) / (img.Width / img.HorizontalResolution);
                    vScale = Convert.ToSingle(vScale * Math.Pow(1.35f, 2)) / (img.Height / img.VerticalResolution);
                    break;
                case PageSize.A3:
                    hScale = (hScale * 1.5f) / (img.Width / img.HorizontalResolution);
                    vScale = (vScale * 1.4f) / (img.Height / img.VerticalResolution);
                    break;
                default:
                    hScale = hScale / (img.Width / img.HorizontalResolution);
                    vScale = vScale / (img.Height / img.VerticalResolution);
                    break;
            }



            if (hScale > vScale)
                return MapWithQRCodeWriter.ScaleByPercent(img, hScale);
            else
                return MapWithQRCodeWriter.ScaleByPercent(img, vScale);
        }

        public static Image MakeGrayscale(Image image)
        {
            Bitmap original = new Bitmap(image);
            //make an empty bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    Color originalColor = original.GetPixel(i, j);

                    //create the grayscale version of the pixel
                    int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59)
                        + (originalColor.B * .11));

                    //create the color object
                    Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, newColor);
                }
            }

            return newBitmap;
        }

        static Image ScaleByPercent(Image imgPhoto, float Percent)
        {

            float nPercent = ((float)Percent / 100);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight,
                                     PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                                    imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        private void drawFrame(ref Image framelessMap)
        {
            Graphics g = Graphics.FromImage(framelessMap);
            Point[] corners = new Point[7];
            corners[0] = new Point(this.codeSize, 0);
            corners[1] = new Point(framelessMap.Width, 0);
            corners[2] = new Point(framelessMap.Width, framelessMap.Height);
            corners[3] = new Point(0, framelessMap.Height);
            corners[4] = new Point(0, this.codeSize);
            corners[5] = new Point(this.codeSize, this.codeSize);
            corners[6] = new Point(this.codeSize, 0);
            Pen pen = new Pen(Brushes.Gray,5.0f);
            g.DrawLines(pen, corners);
            g.Dispose();
        }
    }
}
