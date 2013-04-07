/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System.Drawing;
using AForge.Imaging.Filters;
using System.Diagnostics;
using PresenceSimulator.Properties;
using System;
namespace PresenceSimulator
{
    class VideoPreprocessor
    {
        public int Brightness
        { 
            get 
            { 
                return this.brightnessCorrection.AdjustValue;
            }
            set 
            { 
                this.brightnessCorrection.AdjustValue = value;
            }
        }

        public int Contrast
        {
            get
            {
                return this.contrastCorrection.Factor;
            }
            set
            {
                this.contrastCorrection.Factor = value;
            }
        }

        public float Saturation
        {
            get
            {
                return this.saturationCorrection.AdjustValue;
            }
            set
            {
                this.saturationCorrection.AdjustValue = value;
            }
        }

        private BrightnessCorrection brightnessCorrection = new BrightnessCorrection();
        private ContrastCorrection contrastCorrection = new ContrastCorrection();
        private SaturationCorrection saturationCorrection = new SaturationCorrection();

        public VideoPreprocessor()
        {
            this.brightnessCorrection.AdjustValue = Settings.Default.VideoBrightness;
            this.contrastCorrection.Factor = Settings.Default.VideoContrast;
            this.saturationCorrection.AdjustValue = Convert.ToSingle( Settings.Default.VideoSaturation) / 100.0f;
        }

        public void applyCorrections(ref Bitmap image)
        {
            // make everything black, except the map area
            //Rectangle rect = new Rectangle(130, 80, 550, 320);
            //Graphics g = Graphics.FromImage(image);
            //g.FillRectangle(Brushes.Black, new Rectangle(0, 0, image.Width, rect.Y));
            //g.FillRectangle(Brushes.Black, new Rectangle(0,rect.Y, rect.X, image.Height - rect.Y));
            //g.FillRectangle(Brushes.Black, new Rectangle(rect.X, rect.Y + rect.Height, image.Width - rect.X ,(image.Height - rect.Y + rect.Height)));
            //g.FillRectangle(Brushes.Black, new Rectangle(rect.X + rect.Width, rect.Y, image.Width - rect.X + rect.Width, image.Height));
            //g.Dispose();
            
            if (this.brightnessCorrection.AdjustValue != 0)
                this.brightnessCorrection.ApplyInPlace(image);
            if (this.contrastCorrection.Factor != 0)
                this.contrastCorrection.ApplyInPlace(image);
            if (this.saturationCorrection.AdjustValue != 0.0f)
                this.saturationCorrection.ApplyInPlace(image);
        }
    }
}
