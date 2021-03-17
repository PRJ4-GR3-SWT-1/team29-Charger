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
       [Test]
       public void CurrentEvent_4mA_CallsDisplay()
       {
           usb.CurrentValueEvent += Raise.EventWith(new object(), new CurrentEventArgs(){Current = 4});
           
           display.Received(1).PhoneIsCharged();
       }

    }
}