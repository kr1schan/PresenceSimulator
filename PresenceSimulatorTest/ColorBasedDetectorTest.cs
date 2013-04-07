using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Windows.Forms;
using PresenceSimulator.Map;
using PresenceSimulator.Detectors;
using PresenceSimulator.LocationSources;
using AForge.Imaging;
using AForge;

namespace PresenceSimulatorTest
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für QRCodeTest
    /// </summary>
    [TestClass]
    public class ColorBasedDetectorTest
    {
        public ColorBasedDetectorTest()
        {
            //
            // TODO: Konstruktorlogik hier hinzufügen
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Ruft den Textkontext mit Informationen über
        ///den aktuellen Testlauf sowie Funktionalität für diesen auf oder legt diese fest.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Zusätzliche Testattribute
        //
        // Sie können beim Schreiben der Tests folgende zusätzliche Attribute verwenden:
        //
        // Verwenden Sie ClassInitialize, um vor Ausführung des ersten Tests in der Klasse Code auszuführen.
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Verwenden Sie ClassCleanup, um nach Ausführung aller Tests in einer Klasse Code auszuführen.
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen. 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Mit TestCleanup können Sie nach jedem einzelnen Test Code ausführen.
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void colorBasedDetectorTest()
        {
            ColorBasedDetectorFactory factory = new ColorBasedDetectorFactory();
            ColorBasedDetector sut = (ColorBasedDetector)factory.CreateDetector();
            sut.ResetSettings();

            ColorDiscriminator discr = new ColorDiscriminator();
            RGB color = new RGB(181, 230, 29);
            discr.RGB_color= color;
            LocationSourceManager.Instance.Shutdown();
            LocationSourceManager.Instance.createLocationSource("foo", discr);

            Bitmap frame = new Bitmap(Properties.Resources.colorBasedDetectorTestFrame);
            sut.Detect(ref frame);
            IntPoint pos = LocationSourceManager.Instance.LocationSources[0].ScreenPos;
            Assert.AreEqual(100, pos.X);
            Assert.AreEqual(116, pos.Y);
        }
    }
}
