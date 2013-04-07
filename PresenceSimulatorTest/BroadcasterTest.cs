using PresenceSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using PresenceSimulator.Map;
using PresenceSimulator.Network;
using PresenceSimulator.LocationSources;

namespace PresenceSimulatorTest
{
    [TestClass()]
    public class BroadcasterTest
    {
        [TestMethod()]
        public void receiveBroadcastTest()
        {
            LocationSourceManager.Instance.Shutdown();
            Broadcaster target = Broadcaster.Instance;
            target.Start();

            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 9123);
            sock.Bind(iep);
            EndPoint ep = (EndPoint)iep;

            byte[] data = new byte[1024];
            sock.ReceiveTimeout = 5000;

            int recv = sock.ReceiveFrom(data, ref ep);
            string stringData =  Encoding.ASCII.GetString(data, 0, recv);
            stringData = stringData.Trim();

            Assert.AreEqual("<?xml version=\"1.0\"?><status><updateService ip=\"" + getOwnIP() + "\" port=\"9125\"/><users></users></status>", stringData);

            sock.Close();
            target.Stop();
        }

        private string getOwnIP()
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
    }
}
