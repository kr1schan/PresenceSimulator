/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging;

namespace PresenceSimulator.Detectors
{
    
    class MotionDetector
    {
        private Bitmap background = null;
        private byte[] backgroundFrame = null;
        BitmapData backgroundData = null;
        private int width;
        private int height;
        private int frameWidth;
        private int frameHeight;
        private int len;

        public void setBackground(Bitmap background)
        {
            if (this.background != null)
            {
                this.background.UnlockBits(this.backgroundData);
                this.background.Dispose();
            }
            this.background = background;
            this.width = background.Width;
            this.height = background.Height;
            this.frameWidth = (((width - 1) / 8) + 1);
            this.frameHeight = (((height - 1) / 8) + 1);
            this.len = frameWidth * frameHeight;

            this.backgroundFrame = new byte[len];
            this.backgroundData = this.background.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            PreprocessInputImage(backgroundData, width, height, backgroundFrame);
        }

        public void detectMotion(ref Bitmap image)
        {
            if (backgroundFrame == null)
                return;

            byte[] currentFrame = new byte[len];
            byte[] currentFrameDilatated = new byte[len];

            // current frame preprocessing
            BitmapData currentFrameLock = image.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            PreprocessInputImage(currentFrameLock, width, height, currentFrame);

            // difference and thresholding
            for (int i = 0; i < len; i++)
            {
                int t = currentFrame[i] - backgroundFrame[i];
                if (t < 0)
                    t = -t;

                if (t >= 15)
                {
                    currentFrame[i] = (byte)255;
                }
                else
                {
                    currentFrame[i] = (byte)0;
                }
            }


            for (int i = 0; i < frameHeight; i++)
            {
                for (int j = 0; j < frameWidth; j++)
                {
                    int k = i * frameWidth + j;
                    int v = currentFrame[k];

                    // left pixels
                    if (j > 0)
                    {
                        v += currentFrame[k - 1];

                        if (i > 0)
                        {
                            v += currentFrame[k - frameWidth - 1];
                        }
                        if (i < frameHeight - 1)
                        {
                            v += currentFrame[k + frameWidth - 1];
                        }
                    }
                    // right pixels
                    if (j < frameWidth - 1)
                    {
                        v += currentFrame[k + 1];

                        if (i > 0)
                        {
                            v += currentFrame[k - frameWidth + 1];
                        }
                        if (i < frameHeight - 1)
                        {
                            v += currentFrame[k + frameWidth + 1];
                        }
                    }
                    // top pixel
                    if (i > 0)
                    {
                        v += currentFrame[k - frameWidth];
                    }
                    // right pixel
                    if (i < frameHeight - 1)
                    {
                        v += currentFrame[k + frameWidth];
                    }

                    currentFrameDilatated[k] = (v != 0) ? (byte)255 : (byte)0;
                }
            }

            PostprocessInputImage(currentFrameLock, width, height, currentFrameDilatated);

            image.UnlockBits(currentFrameLock);
        }


        // Preprocess input image
        private void PreprocessInputImage(BitmapData data, int width, int height, byte[] buf)
        {
            int stride = data.Stride;
            int offset = stride - width * 3;
            int len = (int)((width - 1) / 8) + 1;
            int rem = ((width - 1) % 8) + 1;
            int[] tmp = new int[len];
            int i, j, t1, t2, k = 0;

            unsafe
            {
                byte* src = (byte*)data.Scan0.ToPointer();

                for (int y = 0; y < height; )
                {
                    Array.Clear(tmp, 0, len);
                    for (i = 0; (i < 8) && (y < height); i++, y++)
                    {
                        for (int x = 0; x < width; x++, src += 3)
                        {
                            tmp[(int)(x / 8)] += (int)(0.2125f * src[RGB.R] + 0.7154f * src[RGB.G] + 0.0721f * src[RGB.B]);
                        }
                        src += offset;
                    }
                    t1 = i * 8;
                    t2 = i * rem;

                    for (j = 0; j < len - 1; j++, k++)
                        buf[k] = (byte)(tmp[j] / t1);
                    buf[k++] = (byte)(tmp[j] / t2);
                }
            }
        }
        // Postprocess input image
        private void PostprocessInputImage(BitmapData data, int width, int height, byte[] buf)
        {
            int stride = data.Stride;
            int offset = stride - width * 3;
            int len = (int)((width - 1) / 8) + 1;
            int lenWM1 = len - 1;
            int lenHM1 = (int)((height - 1) / 8);
            int rem = ((width - 1) % 8) + 1;

            int i, j, k;

            unsafe
            {
                byte* src = (byte*)data.Scan0.ToPointer();

                // for each line
                for (int y = 0; y < height; y++)
                {
                    i = (y / 8);

                    // for each pixel
                    for (int x = 0; x < width; x++, src += 3)
                    {
                        j = x / 8;
                        k = i * len + j;

                        if (buf[k] != 255)
                        {
                            src[RGB.R] = 0;
                            src[RGB.G] = 0;
                            src[RGB.B] = 0;
                        }
                    }
                    src += offset;
                }
            }
        }
    }
}
