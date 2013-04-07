/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using PresenceSimulator.LocationSources;

namespace PresenceSimulator.Network
{
    class NetworkServer
    {
        private static readonly NetworkServer instance = new NetworkServer();
        public static NetworkServer Instance
        {
            get { return instance; }
        }

        private TcpListener updateProviderListener;
        private TcpListener statusProviderListener;

        private Thread updateProviderThread;
        private Thread statusProviderThread;

        private List<ClientConnection> connections = new List<ClientConnection>();

        public RichTextBox serverLog { get; set;}

        private NetworkServer()
        {
            this.updateProviderListener = new TcpListener(IPAddress.Any, Properties.Settings.Default.updateProviderPort);
            this.statusProviderListener = new TcpListener(IPAddress.Any, Properties.Settings.Default.statusProviderPort);
        }

        public void Start()
        {
            if ((this.updateProviderThread != null) && (this.updateProviderThread.IsAlive))
            {
                return;
            }
            else
            {
                this.updateProviderThread = new Thread(new ThreadStart(ListenForUpdateClients));
                this.statusProviderThread = new Thread(new ThreadStart(HandleStatusRequests));

                this.updateProviderThread.Start();
                this.statusProviderThread.Start();
                this.Log("Server started on " + Broadcaster.getOwnIP() + ":" + Properties.Settings.Default.statusProviderPort);
            }
        }

        public void Stop()
        {
            try
            {
                foreach (ClientConnection connection in connections)
                    connection.Close();
            }
            catch
            {
            }

            if (this.updateProviderThread != null)
                this.updateProviderThread.Abort();

            if (this.statusProviderThread != null)
                this.statusProviderThread.Abort();

            if (this.updateProviderListener != null)
                this.updateProviderListener.Stop();
            
            if (this.statusProviderListener != null)
                this.statusProviderListener.Stop();
            
            this.Log("Server stopped");
        }

        public void removeConnection(ClientConnection connection)
        {
            if (connection != null)
            {
                this.Log("client " + connection.clientIp + " disconnected");
                if (connection.locationSource != null)
                    connection.locationSource.Detach(connection);
                this.connections.Remove(connection);
            }
        }

        public void Log(string message)
        {
            if (this.serverLog == null)
                return;

            if (this.serverLog.InvokeRequired)
            {
                try
                {
                    this.serverLog.Invoke((MethodInvoker)delegate()
                    {
                        this.serverLog.Text += "[" + DateTime.Now.ToShortTimeString() + "] " + message + "\n";
                    });
                }
                catch
                {
                    return;
                }
            }
            else
            {
                this.serverLog.Text += "[" + DateTime.Now.ToShortTimeString() + "] " + message + "\n";
            }
        }

        public String composeStatusMessage()
        {
            String status = "<?xml version=\"1.0\"?><status>";
            status = status + "<updateService ip=\"" + Broadcaster.getOwnIP() + "\" port=\"" + Properties.Settings.Default.updateProviderPort + "\"/>";
            status = status + LocationSourceManager.Instance.getLocationSourcesAsXml();
            status = status + "</status>";
            return status;
        }

        private void HandleStatusRequests()
        {
            try
            {
                this.statusProviderListener.Start();

                while (true)
                {
                    TcpClient client = this.statusProviderListener.AcceptTcpClient();
                    StreamWriter writer = new StreamWriter(client.GetStream());
                    writer.WriteLine(this.composeStatusMessage());
                    writer.Flush();
                    client.Close();
                }
            }
            catch (ThreadAbortException)
            {
                return;
            }
        }

        private void ListenForUpdateClients()
        {
            try
            {
                this.updateProviderListener.Start();

                while (true)
                {
                    this.Log("listening for clients...");
                    TcpClient client = this.updateProviderListener.AcceptTcpClient();
                    ClientConnection connection = new ClientConnection(client);
                    this.connections.Add(connection);
                    this.Log("client " + connection.clientIp + " connected");
                }
            }
            catch (ThreadAbortException)
            {
                return;
            }
        }
    }
}
