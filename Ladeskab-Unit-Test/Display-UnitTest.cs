using System.Xml.Linq;
using Charger_Functionality_Library.Classes;
using Charger_Functionality_Library.Help_Interfaces;
using Charger_Functionality_Library.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab_Unit_Test
{
    public class DisplayUnitTests
    {
        private Display display;
        private IConsoleWriter _writer;
        [SetUp]
        public void Setup()
        {
            _writer = Substitute.For<IConsoleWriter>();
            display = new Display(_writer);
        }

        // ----- UnitTests for Display -----
        [Test]
        public void Display_MethodIsCalled_DisplayUsesInterface()
        {
            display.CabinetOccupied();
            display.RFIDError();
            display.ConnectionError();
            display.ConnectPhone();
            display.RFIDRead();
            display.RemovePhone();
            _writer.Received(6).write(Arg.Any<string>());
        }

        [Test]
        public void Display_ChargeControlMethodsIsCalled_DisplayUsesInterface()
        {
            display.PhoneChargingError();
            display.PhoneIsCharged();
            display.PhoneIsCharging();
            _writer.Received(3).write(Arg.Any<string>());
        }

    }

}
