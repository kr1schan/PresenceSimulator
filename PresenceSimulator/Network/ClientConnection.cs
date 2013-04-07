/*Copyright (C) 2012 Krischan Udelhoven
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using PresenceSimulator.LocationSources;

namespace PresenceSimulator.Network
{
    public class ClientConnection : LocationSourceObserver
    {
        public TcpClient TcpClient { get; private set; }
        public string clientIp { get; private set; }
        public LocationSource locationSource { get; private set; } 

        private StreamReader reader;
        private XmlWriter xmlWriter;
        private CultureInfo cultureInfo = new CultureInfo("en-US"); // e.g. 10.5 and not 10,5
        private int updateInterval;
        private DateTime timeofLastMessage;
        
        public ClientConnection(TcpClient client)
        {
            this.updateInterval = Properties.Settings.Default.locationUpdateInterval;
            this.TcpClient = client;
            this.clientIp = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            NetworkStream stream = TcpClient.GetStream();
            this.reader = new StreamReader(stream);
            XmlWriterSettings settings = new XmlWriterSettings();
            this.xmlWriter = XmlWriter.Create(stream);

            Thread waitForUserSelection = new Thread(new ThreadStart(this.userSelection));
            waitForUserSelection.Start();
        }

        private void userSelection()
        {
            if (!TcpClient.Connected)
            {
                NetworkServer.Instance.removeConnection(this);
                return;
            }

            // wait for user selection
            XmlDocument doc = new XmlDocument();
            string userSelection = this.reader.ReadLine();
            if (userSelection == null)
            {
                NetworkServer.Instance.removeConnection(this);
                return;
            }
            doc.InnerXml = userSelection;
            XmlElement root = doc.DocumentElement;

            // fetch selected user from user manager
            String selectedId = root.GetAttribute("id");
            Guid userId = System.Guid.Parse(selectedId);
            LocationSource selectedUser = LocationSourceManager.Instance.getLocationSourceById(userId);

            if (selectedUser != null)
            {
                this.sendXmlHeader();
                this.locationSource = selectedUser;
                this.locationSource.Attach(this);
                NetworkServer.Instance.Log("client selected user: " + this.locationSource.Name);
            }
            else
            {
                NetworkServer.Instance.removeConnection(this);
                return;
            }
        }

        private void sendXmlHeader()
        {
            this.xmlWriter.WriteStartDocument(false);
            this.xmlWriter.WriteStartElement("gpx");
            this.xmlWriter.WriteAttributeString("version", "1.1");
            this.xmlWriter.WriteAttributeString("creator", "PresenceSimulator");
            this.xmlWriter.WriteStartElement("trk");
            this.xmlWriter.WriteStartElement("trkseg");
        }

        public void Update()
        {
            if (this.timeofLastMessage != null)
            {
                if ((DateTime.UtcNow - this.timeofLastMessage).Duration().TotalMilliseconds < this.updateInterval)
                {
                    return;
                }
            }

            if (this.locationSource != null)
            {
                try
                {
                    this.xmlWriter.WriteStartElement("trkpt");
                    this.xmlWriter.WriteAttributeString("lat", this.locationSource.LatLng.Lat.ToString(cultureInfo));
                    this.xmlWriter.WriteAttributeString("lon", this.locationSource.LatLng.Lng.ToString(cultureInfo));
                    this.xmlWriter.WriteElementString("time", "");
                    this.xmlWriter.WriteEndElement();
                    this.xmlWriter.WriteWhitespace("\n");
                    this.xmlWriter.Flush();
                    this.timeofLastMessage = DateTime.UtcNow;
                }
                catch (IOException)
                {
                    NetworkServer.Instance.removeConnection(this);
                    return;
                }
            }
        }

        public void NameChange()
        {
            // unimplemented
        }

        public void Delete()
        {
            this.Close();
        }

        public void Close()
        {
            NetworkServer.Instance.removeConnection(this);
            try
            {
                this.xmlWriter.WriteEndElement();
                this.xmlWriter.WriteEndElement();
                this.xmlWriter.WriteEndElement();
                this.xmlWriter.Flush();
            }
            catch
            {
            }

            try
            {
                this.xmlWriter.Close();
                this.TcpClient.Close();
            }
            catch
            {
            }
            this.locationSource.Detach(this);
        }
    }
}
