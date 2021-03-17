using System.Xml.Linq;
using Charger_Functionality_Library;
using Charger_Functionality_Library.Classes;
using Charger_Functionality_Library.EventArgsClasses;
using Charger_Functionality_Library.Interfaces;
using NUnit.Framework;

namespace Ladeskab_Unit_Test
{
    public class RfidTests
    {
        private RfidReader reader;
        [SetUp]
        public void Setup()
        {

            reader = new RfidReader();
            reader.TagReadEvent += RfidReader_TagReadEvent;
            numberOfEvents = 0;
        }

        private void RfidReader_TagReadEvent(object sender, RfidEventArgs e)
        {
            numberOfEvents++;
            lastReceivedID = e.Id;
        }

        public int numberOfEvents { get; set; }
        public string lastReceivedID { get; set; }

        [Test]
        public void ManualReadTag_EmptyId_EventIsTriggered()
        {
            reader.ManualScanTag("");
            Assert.That(numberOfEvents, Is.EqualTo(1));
        }
        [Test]
        public void ManualReadTag_EmptyId_IdIsCorrect()
        {
            reader.ManualScanTag("");
            Assert.That(lastReceivedID, Is.EqualTo(""));
        }
        [Test]
        public void ManualReadTag_IntId_IdIsCorrect()
        {
            reader.ManualScanTag("132789");
            Assert.That(lastReceivedID, Is.EqualTo("132789"));
        }
        [Test]
        public void ManualReadTag_AsciiId_IdIsCorrect()
        {
            reader.ManualScanTag("asd-?=(&¤");
            Assert.That(lastReceivedID, Is.EqualTo("asd-?=(&¤"));
        }
    }
}