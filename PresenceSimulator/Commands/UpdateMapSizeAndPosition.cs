/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System.Windows.Forms;
using PresenceSimulator.Map;
using System.Drawing;

namespace PresenceSimulator.Commands
{
    class UpdateMapSizeAndPosition : Command
    {
        private Control video;
        private MapOverlayForm map;

        public UpdateMapSizeAndPosition(Control video, MapOverlayForm map)
        {
            this.video = video;
            this.map = map;
        }

        public void execute()
        {
            System.Drawing.Point newMapLocation = this.video.PointToScreen(this.video.Location);
            newMapLocation.X = newMapLocation.X - this.video.Left;
            newMapLocation.Y = newMapLocation.Y - this.video.Top;
            this.map.Location = newMapLocation;
            Size newMapSize = this.video.Size;
            this.map.ChangeSize(newMapSize);
        }
    }
}
