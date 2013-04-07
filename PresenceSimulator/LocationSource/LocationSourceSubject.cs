/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;

namespace PresenceSimulator.LocationSources
{
    public abstract class LocationSourceSubject
    {
        private List<LocationSourceObserver> observers = new List<LocationSourceObserver>();

        public LocationSourceSubject()
        {
        }

        public void Attach(LocationSourceObserver observer)
        {
            this.observers.Add(observer);
        }

        public void Detach(LocationSourceObserver observer)
        {
            this.observers.Remove(observer);
        }

        public void Notify()
        {
            for (int i = this.observers.Count - 1; i >= 0; i--)
            {
                try
                {
                    this.observers[i].Update();
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }
        }

        public void NameChange()
        {
            for (int i = this.observers.Count - 1; i >= 0; i--)
            {
                this.observers[i].NameChange();
            }
        }

        public void Delete()
        {
            for (int i = this.observers.Count - 1; i >= 0; i--)
            {
                try
                {
                    this.observers[i].Delete();
                    this.observers.Remove(this.observers[i]);
                }
                catch (ArgumentOutOfRangeException)
                {
                    continue;
                }
            }
        }
    }
}
