/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System.Drawing;
using System.Windows.Forms;
using AForge.Imaging;

namespace PresenceSimulator.Detectors
{
    public class ColorDiscriminator : Discriminator
    {
        private int threshold = 25;
        private HSL color = new HSL();
        private Button colorPickerBtn = new Button();

        public ColorDiscriminator()
        {
            this.RGB_color = new RGB(124, 83, 219);
            this.colorPickerBtn.Text = "Color";

            this.colorPickerBtn.Click += delegate
            {
                MainForm mainForm = (MainForm)this.colorPickerBtn.Parent.Parent.Parent.Parent.Parent;// TODO: DONT DO THIS!
                mainForm.colorPicker = this;
            };
        }

        override public Control GetControl()
        {
            return this.colorPickerBtn;
        }

        public HSL HSL_color
        {
            get { return this.color; }
            set { this.color = value; this.setBtnBackground(); }
        }

        public RGB RGB_color
        {
            get { return this.color.ToRGB(); }
            set { this.color = HSL.FromRGB(value); this.setBtnBackground(); }
        }

        public Color Color
        {
            get 
            {
                RGB rgb = this.color.ToRGB();
                return Color.FromArgb(rgb.Alpha, rgb.Red, rgb.Green, rgb.Blue);
            }
            set
            { 
                this.colorPickerBtn.BackColor = value;
                RGB rgb = new RGB(value);
                this.color = HSL.FromRGB(rgb);
                this.setBtnBackground();
            }
        }
        
        override public bool IsSimilar(Discriminator other)
        {
            if (other is ColorDiscriminator)
            {
                ColorDiscriminator otherColorDiscriminator = (ColorDiscriminator)other;
                int diff = otherColorDiscriminator.color.Hue - this.color.Hue;
                if (diff < 0)
                    diff = -diff;
                if (diff < this.threshold)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        private void setBtnBackground()
        {
            RGB rgb = this.color.ToRGB();
            Color backColor = Color.FromArgb(rgb.Alpha, rgb.Red, rgb.Green, rgb.Blue);
            this.colorPickerBtn.BackColor = backColor;
        }
    }
}
