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
        [SetUp]
        public void Setup()
        {

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
