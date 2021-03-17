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
            door.DoorCloseEvent += Door_DoorCloseEvent;
            numberOfEvents = 0;
        }

        private void Door_DoorOpenEvent(object sender, Charger_Functionality_Library.EventArgsClasses.DoorEventArgs e)
        {
            numberOfEvents++;
        }
        private void Door_DoorCloseEvent(object sender, Charger_Functionality_Library.EventArgsClasses.DoorEventArgs e)
        {
            numberOfEvents++;
        }



        public int numberOfEvents { get; set; }

        /****************** TEST OpenDoor ******************/
        [Test]
        public void OpenDoor_DoorIsUnlocked_DoorIsOpen()
        {
            door.UnlockDoor();
            door.OpenDoor();
            Assert.That(door.Open,Is.EqualTo(true));
        }

        [Test]
        public void OpenDoor_DoorIsLocked_DoorIsOpen()
        {
            door.CloseDoor();
            door.LockDoor();
            door.OpenDoor();
            Assert.That(door.Open,Is.EqualTo(false));
        }

        [Test]
        public void OpenDoor_DoorIsUnlocked_EventIsTriggered()
        {
            door.UnlockDoor();
            door.OpenDoor();
            Assert.That(numberOfEvents, Is.EqualTo(1));
        }

        /****************** TEST CloseDoor ******************/
        [Test]
        public void CloseDoor_DoorOpen_ChangeInState()
        {
            door.CloseDoor();
            Assert.That(door.Open,Is.EqualTo(false));
        }

        [Test]
        public void CloseDoor_DoorIsOpen_EventIsTriggered()
        {
            door.CloseDoor();
            Assert.That(numberOfEvents,Is.EqualTo(1));
        }

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