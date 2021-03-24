using System;
using System.Xml.Linq;
using Charger_Functionality_Library;
using Charger_Functionality_Library.Classes;
using Charger_Functionality_Library.EventArgsClasses;
using Charger_Functionality_Library.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace Ladeskab_Unit_Test
{
    public class StationControlTests
    {
        private StationControl uut;
        private IDisplay display;
        private IUsbCharger usb;
        private IChargeControl chargeControl;
        private ILogFile logFile;
        private IRFIDReader rfidReader;
        private IDoor door;
        [SetUp]
        public void Setup()
        {
            //Create substitutes
                display = NSubstitute.Substitute.For<IDisplay>();
                usb = NSubstitute.Substitute.For<IUsbCharger>();
                chargeControl = NSubstitute.Substitute.For<IChargeControl>();
                logFile = NSubstitute.Substitute.For<ILogFile>();
                rfidReader = NSubstitute.Substitute.For<IRFIDReader>();
                door = NSubstitute.Substitute.For<IDoor>();
            //Create UUT:
            uut = new StationControl(door,rfidReader,display,chargeControl,logFile);
        }


//RFID event (Lock ladeskab)
        [TestCase("123")]
        [TestCase(null)]
        [TestCase("abcdefg")]
        public void RfidEvent_LadeskabAvaliable_DisplayIsCalled(string SimulatedId)
        {
            door.DoorCloseEvent += Raise.EventWith(null, new DoorEventArgs());

            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs(){Id=SimulatedId});

            display.Received(1).ConnectionError();
        }

        [TestCase("123")]
        [TestCase(null)]
        [TestCase("abcdefg")]
        public void RfidEvent_LadeskabAvaliablePhoneConnected_DisplayIsCalled(string SimulatedId)
        {
            chargeControl.IsConnected().Returns(true);
            door.DoorCloseEvent += Raise.EventWith(null, new DoorEventArgs());

            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs() { Id = SimulatedId });
            display.Received(1).CabinetOccupied();
        }

        [TestCase("123")]
        [TestCase(null)]
        [TestCase("abcdefg")]
        public void RfidEvent_LadeskabAvaliablePhoneConnected_ChargingStarts(string SimulatedId)
        {
            chargeControl.IsConnected().Returns(true);
            door.DoorCloseEvent += Raise.EventWith(null, new DoorEventArgs());
            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs() { Id = SimulatedId });
            chargeControl.Received(1).StartCharge();
        }

        [TestCase("123")]
        [TestCase(null)]
        [TestCase("abcdefg")]
        public void RfidEvent_LadeskabAvaliablePhoneConnected_DoorIsLocked(string SimulatedId)
        {
            chargeControl.IsConnected().Returns(true);
            door.DoorCloseEvent += Raise.EventWith(null, new DoorEventArgs());
            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs() { Id = SimulatedId });
            door.Received(1).LockDoor();
        }
        [TestCase("123")]
        [TestCase(null)]
        [TestCase("abcdefg")]
        public void RfidEvent_LadeskabAvaliablePhoneConnected_LogIsUpdated(string SimulatedId)
        {
            chargeControl.IsConnected().Returns(true);
            door.DoorCloseEvent += Raise.EventWith(null, new DoorEventArgs());

            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs() { Id = SimulatedId });
            logFile.Received(1).DoorLocked(SimulatedId);
        }
//RFID event - unlock ladeskab
        [TestCase("123")]
        [TestCase(null)]
        [TestCase("abcdefg")]
        public void RfidEvent_LockedWrongId_DisplayCalled(string SimulatedId)
        {
            chargeControl.IsConnected().Returns(true);
            door.DoorCloseEvent += Raise.EventWith(null, new DoorEventArgs());

            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs() { Id = "This id was used to lock ladeskab" });

            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs() {Id = SimulatedId});
            display.Received(1).RFIDError();
        }

        [TestCase("123")]
        [TestCase(null)]
        [TestCase("abcdefg")]
        public void RfidEvent_LockedWrongId_DoorStillLocked(string SimulatedId)
        {
            chargeControl.IsConnected().Returns(true);
            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs() { Id = "This id was used to lock ladeskab" });

            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs() { Id = SimulatedId });
            door.Received(0).UnlockDoor();
        }

        [TestCase("123")]
        [TestCase(null)]
        [TestCase("abcdefg")]
        public void RfidEvent_LockedCorrectId_DoorUnlocked(string SimulatedId)
        {
            chargeControl.IsConnected().Returns(true);
            door.DoorCloseEvent += Raise.EventWith(null, new DoorEventArgs());

            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs() { Id = SimulatedId });

            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs() { Id = SimulatedId });
            door.Received(1).UnlockDoor();
        }

        [TestCase("123")]
        [TestCase(null)]
        [TestCase("abcdefg")]
        public void RfidEvent_LockedCorrectId_DisplayUpdated(string SimulatedId)
        {
            chargeControl.IsConnected().Returns(true);
            door.DoorCloseEvent += Raise.EventWith(null, new DoorEventArgs());

            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs() { Id = SimulatedId });

            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs() { Id = SimulatedId });
            display.Received(1).RemovePhone();
        }

        [TestCase("123")]
        [TestCase(null)]
        [TestCase("abcdefg")]
        public void RfidEvent_LockedCorrectId_LogUpdated(string SimulatedId)
        {
            chargeControl.IsConnected().Returns(true);
            door.DoorCloseEvent += Raise.EventWith(null, new DoorEventArgs());

            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs() { Id = SimulatedId });

            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs() { Id = SimulatedId });
            logFile.Received(1).DoorUnlocked(SimulatedId);
        }
 //Door opened event
        [Test]
        public void DoorOpenedEvent__DisplayIsCalled()
        {
            door.DoorOpenEvent += Raise.EventWith(null, new DoorEventArgs());
            display.Received(1).ConnectPhone();
        }
 //Door closed event
        [Test]
        public void DoorClosedEvent__DisplayIsCalled()
        {
            door.DoorCloseEvent += Raise.EventWith(null, new DoorEventArgs());
            display.Received(1).RFIDRead();
        }

        //Open door - rfid read
        [Test]
        public void RfidEvent_DoorOpen_NothingHappens()
        {
            door.DoorOpenEvent += Raise.EventWith(null, new DoorEventArgs());
            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs() { Id = "123" });

            door.Received(0).LockDoor();
        }
        [Test]
        public void RfidEvent_OneCycleCompletedNewStarted_DoorIsLocked()
        {
            //First cycle:
            door.DoorOpenEvent += Raise.EventWith(null, new DoorEventArgs());
            chargeControl.IsConnected().Returns(true);
            door.DoorCloseEvent += Raise.EventWith(null, new DoorEventArgs());
            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs() { Id = "123" });
            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs() { Id = "123" });
            door.DoorOpenEvent += Raise.EventWith(null, new DoorEventArgs());
            chargeControl.IsConnected().Returns(false);
            door.DoorCloseEvent += Raise.EventWith(null, new DoorEventArgs());
            //New cycle:
            door.DoorOpenEvent += Raise.EventWith(null, new DoorEventArgs());
            chargeControl.IsConnected().Returns(true);
            door.DoorCloseEvent += Raise.EventWith(null, new DoorEventArgs());
            rfidReader.TagReadEvent += Raise.EventWith(null, new RfidEventArgs() { Id = "567" });

            door.Received(2).LockDoor();
        }
    }
    
}