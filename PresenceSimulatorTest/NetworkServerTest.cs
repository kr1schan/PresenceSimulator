using PresenceSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.IO;
using GMap.NET;
using System.Xml;
using PresenceSimulator.Network;
using PresenceSimulator.LocationSources;
using PresenceSimulator.Detectors;
using System.Threading;

namespace PresenceSimulatorTest
{
    [TestClass()]
    public class NetworkServerTest
    {
        #region Zusätzliche Testattribute

        private static NetworkServer target;
        private static TcpClient client;
        private static IPEndPoint locationUpdateServerEndPoint;
        private static IPEndPoint statusServerEndPoint;

        #endregion

        [TestMethod()]
        public void receiveUserListTest()
        {
            target = NetworkServer.Instance;
            target.Start();

            client = new TcpClient();
            statusServerEndPoint = new IPEndPoint(IPAddress.Parse(Broadcaster.getOwnIP()), PresenceSimulator.Properties.Settings.Default.statusProviderPort);

            LocationSourceManager.Instance.Shutdown();
            LocationSourceManager.Instance.createLocationSource("Foo", new ColorDiscriminator());

            client.Connect(statusServerEndPoint);

            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            String message = reader.ReadLine();		

            Assert.AreEqual("<?xml version=\"1.0\"?><status><updateService ip=\"" + Broadcaster.getOwnIP() + "\" port=\"" + PresenceSimulator.Properties.Settings.Default.updateProviderPort +"\"/><users><user id=\"" + LocationSourceManager.Instance.LocationSources[0].Id.ToString() + "\" name =\"Foo\"/></users></status>", message);

            client.Close();
            target.Stop();
        }



        [TestMethod()]
        public void fetchUpdatesFromSelectedUserTest()
        {
            target = NetworkServer.Instance;
            target.Start();

            client = new TcpClient();
            locationUpdateServerEndPoint = new IPEndPoint(IPAddress.Parse(Broadcaster.getOwnIP()), PresenceSimulator.Properties.Settings.Default.updateProviderPort);

            LocationSourceManager.Instance.Shutdown();
            LocationSource user = LocationSourceManager.Instance.createLocationSource("Foo", new ColorDiscriminator());

            client.Connect(locationUpdateServerEndPoint);

            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);

            StreamWriter writer = new StreamWriter(stream);

            string lsId = LocationSourceManager.Instance.LocationSources[0].Id.ToString();

            string answer = "<?xml version=\"1.0\"?><user id=\"" + lsId + "\"/>";
            writer.WriteLine(answer);
            writer.Flush();

            TimerCallback timerDelegate = new TimerCallback(this.changeLocationSource);
            Timer changeLSTimer = new Timer(timerDelegate, new AutoResetEvent(false), 1000, 250);
            string update = reader.ReadLine();
            update = reader.ReadLine();
            changeLSTimer.Dispose();

            Assert.AreEqual("<trkpt lat=\"1\" lon=\"2\"><time /></trkpt>", update);

            writer.Close();
            reader.Close();
            client.Close();
            target.Stop();
        }

        public void changeLocationSource(Object stateInfo)
        {
            LocationSourceManager.Instance.LocationSources[0].LatLng = new PointLatLng(1d, 2d);
        }
    }
}
