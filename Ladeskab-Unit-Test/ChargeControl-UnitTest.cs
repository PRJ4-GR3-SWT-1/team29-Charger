using System;
using System.Xml.Linq;
using Charger_Functionality_Library;
using Charger_Functionality_Library.Classes;
using Charger_Functionality_Library.EventArgsClasses;
using Charger_Functionality_Library.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab_Unit_Test
{
    public class ChargeControlTests
    {
        private ChargeControl uut;
        private IDisplay display;
        private IUsbCharger usb;
        [SetUp]
        public void Setup()
        {
            display = NSubstitute.Substitute.For<IDisplay>();
            usb = NSubstitute.Substitute.For<IUsbCharger>();
            uut = new ChargeControl(usb,display);
        }

        

       [Test]
       public void IsConnected_NotConnected_ReturnsFalse()
       {
           Assert.That(uut.IsConnected,Is.EqualTo(false));
       }
       [Test]
       public void IsConnected_Connected_ReturnsTrue()
       {
           uut.PlugPhoneIn();
           Assert.That(uut.IsConnected, Is.EqualTo(true));
       }
       [Test]
       public void IsConnected_PluggedInPluggedOut_ReturnsFalse()
       {
           uut.PlugPhoneIn();
           uut.UnPlugPhone();
           Assert.That(uut.IsConnected, Is.EqualTo(false));
       }
       [Test]
       public void StartCharge_PluggedIn_CallsUSB()
       {
           uut.PlugPhoneIn();
           uut.StartCharge();
           usb.Received(1).StartCharge();
       }
       [Test]
       public void StartCharge_NotPluggedIn_CallsUSB()
       {
           //uut.PlugPhoneIn();
           uut.StartCharge();
           usb.Received(0).StartCharge();
       }
       [Test]
       public void StoptCharge_Charging_CallsUSB()
       {
           uut.PlugPhoneIn();
           uut.StartCharge();
           uut.StopCharge();
           usb.Received(1).StopCharge();
       }
       [Test]
       public void StoptCharge_NotCharging_CallsUSB()
       {
           uut.StopCharge();
           usb.Received(1).StopCharge();
       }

       //Tilføj en masse fede bounday value test her :D
       [TestCase(1)]
       [TestCase(5)]
        public void CurrentEvent_lowAmp_CallsDisplay(double amp)
       {
           usb.CurrentValueEvent += Raise.EventWith(new object(), new CurrentEventArgs(){Current = amp});
           
           display.Received(1).PhoneIsCharged();
       }

        [TestCase(6)]
        [TestCase(500)]
        public void CurrentEvent_MediumAmp_CallsDisplay(double amp)
        {
            usb.CurrentValueEvent += Raise.EventWith(new object(), new CurrentEventArgs() { Current = amp });

            display.Received(1).PhoneIsCharging();
        }
        [TestCase(501)]
        [TestCase(1500)]
        public void CurrentEvent_HighAmp_CallsDisplay(double amp)
        {
            usb.CurrentValueEvent += Raise.EventWith(new object(), new CurrentEventArgs() { Current = amp });

            display.Received(1).PhoneChargingError();
        }
    }
}