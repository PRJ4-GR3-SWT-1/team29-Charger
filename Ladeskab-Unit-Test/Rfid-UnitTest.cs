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
        public void ManualReadTag_StringId_EventIsTriggered()
        {
            reader.ManualScanTag("Dankort:(&%64%¤764373/");
            Assert.That(numberOfEvents, Is.EqualTo(1));
        }
        [TestCase("")]
        [TestCase("123789")]
        [TestCase("asd-?=(&¤")]
        public void ManualReadTag_EmptyId_IdIsCorrect(string id)
        {
            reader.ManualScanTag(id);
            Assert.That(lastReceivedID, Is.EqualTo(id));
        }
       
    }
}