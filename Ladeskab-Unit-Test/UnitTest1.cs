using System.Xml.Linq;
using Charger_Functionality_Library.Classes;
using Charger_Functionality_Library.Help_Interfaces;
using Charger_Functionality_Library.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab_Unit_Test
{
    public class DoorTests
    {
        private Door door;
        [SetUp]
        public void Setup()
        {
            
            door = new Door();
            door.DoorOpenEvent += Door_DoorOpenEvent;
            numberOfEvents = 0;
        }

        private void Door_DoorOpenEvent(object sender, Charger_Functionality_Library.EventArgsClasses.DoorEventArgs e)
        {
            numberOfEvents++;
        }

        public int numberOfEvents { get; set; }

        [Test]
        public void OpenDoor_DoorIsUnlocked_DoorIsOpen()
        {
            door.UnlockDoor();
            door.OpenDoor();
            Assert.That(door.open,Is.EqualTo(true));
        }
        [Test]
        public void OpenDoor_DoorIsUnlocked_EventIsTriggered()
        {
            door.UnlockDoor();
            door.OpenDoor();
            Assert.That(numberOfEvents, Is.EqualTo(1));
        }
        // ----- UnitTests for Display -----
        [Test]
        public void Display_MethodIsCalled_DisplayUsesInterface()
        {
            IConsoleWriter _writer = Substitute.For<IConsoleWriter>();
            Display display = new Display(_writer);

            display.CabinetOccupied();
            display.RFIDError();
            display.ConnectionError();
            display.PhoneConnected();
            display.RFIDRead();
            display.RemovePhone();
            _writer.Received(6).write(Arg.Any<string>());
        }
    }
}