/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PresenceSimulator.Network
{
    class Broadcaster : IDisposable
    {
        private static readonly Broadcaster instance = new Broadcaster();
        public static Broadcaster Instance
        {
            get { return instance; }
        }

        private TimerCallback timerDelegate;
        private Timer timer;
        private Socket sock;
        private int period = Properties.Settings.Default.BroadcastPeriod;
        private string ip;
        private byte[] data;
        private IPEndPoint dest;

        private Broadcaster()
        {
        }

        public void Start()
        {
            if (this.timer != null)
            {
                this.timer.Dispose();
                this.sock.Close();
                this.sock.Dispose();
            }

            this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            this.sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            this.ip = Broadcaster.getOwnIP();
            this.data = Encoding.ASCII.GetBytes("PresenceSimulator:" + ip);
            this.dest = new IPEndPoint(IPAddress.Broadcast, Properties.Settings.Default.BroadcastPort);

            AutoResetEvent autoEvent = new AutoResetEvent(false);
            this.timerDelegate = new TimerCallback(this.broadcast);
            this.timer = new Timer(timerDelegate, autoEvent, 0, this.period);
        }

        public void Stop()
        {
            if (this.timer != null)
                this.timer.Dispose();
            if (this.sock != null)
                this.sock.Close();
        }

        private void broadcast(Object obj)
        {
            byte[] statusData = Encoding.ASCII.GetBytes(NetworkServer.Instance.composeStatusMessage() + "\n");
            sock.SendTo(statusData, this.dest);
        }

        public static string getOwnIP()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

        public void Dispose()
        {
            this.sock.Dispose();
            this.timer.Dispose();
        }
    }
}
