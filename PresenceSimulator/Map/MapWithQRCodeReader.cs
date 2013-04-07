/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Drawing;
using com.google.zxing;
using com.google.zxing.common;
using GMap.NET;

namespace PresenceSimulator.Map
{
    class MapWithQRCodeReader
    {
        private com.google.zxing.qrcode.QRCodeReader reader;

        public MapWithQRCodeReader()
        {
            this.reader = new com.google.zxing.qrcode.QRCodeReader();
        }

        public String getQRCodeData(Bitmap bitmap)
        {
            LuminanceSource source = new RGBLuminanceSource(bitmap, bitmap.Width, bitmap.Height);
            BinaryBitmap binaryBitmap = new BinaryBitmap(new HybridBinarizer(source));
            return this.reader.decode(binaryBitmap).Text;
        }

        public String decode(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                return null;
            }

            LuminanceSource source = new RGBLuminanceSource(bitmap, bitmap.Width, bitmap.Height);
            BinaryBitmap binaryBitmap = new BinaryBitmap(new HybridBinarizer(source));

            try
            {
                Result result = this.reader.decode(binaryBitmap);

                if (result != null)
                { 
                    String[] resultToken = result.Text.Split(';');

                    double lat = Convert.ToDouble(resultToken[0]);
                    double lng = Convert.ToDouble(resultToken[1]);

                    double distance = Convert.ToDouble(resultToken[2]);
                    GPoint pointA = new GPoint(Convert.ToInt32(result.ResultPoints[1].X), Convert.ToInt32(result.ResultPoints[1].Y));
                    GPoint pointB = new GPoint(Convert.ToInt32(result.ResultPoints[2].X), Convert.ToInt32(result.ResultPoints[2].Y));

                    MapOverlayForm.Instance.SetMapScale(pointA, pointB, distance);
                    MapOverlayForm.Instance.RotationAngle = Convert.ToSingle(this.computeRotation(result.ResultPoints));
                    MapOverlayForm.Instance.MoveMapToLocalPosition(this.computeCenter(result.ResultPoints), new GMap.NET.PointLatLng(lat, lng));
                }
                return result.Text;
            }
            catch (ReaderException)
            {
                return null;
            }
        }

        private double computeRotation(ResultPoint[] resultPoints)
        {
            double radians = Math.Atan2((resultPoints[1].Y - resultPoints[2].Y), (resultPoints[1].X - resultPoints[2].X));
            double degree = radians * (180 / Math.PI);
            return -(degree - 180);
        }

        private GPoint computeCenter(ResultPoint[] resultPoints)
        {
            GPoint center = new GPoint();
            center.X = Math.Abs(Convert.ToInt32(resultPoints[0].X - (resultPoints[0].X - resultPoints[2].X) * 0.5));
            center.Y = Math.Abs(Convert.ToInt32(resultPoints[0].Y - (resultPoints[0].Y - resultPoints[2].Y) * 0.5));
            return center;
        }
    }
}
