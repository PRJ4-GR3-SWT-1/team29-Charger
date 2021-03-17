using System;
using Charger_Functionality_Library.Help_Interfaces;
using Charger_Functionality_Library.Interfaces;

namespace Charger_Functionality_Library.Classes
{
    public class Display : IDisplay
    {
        private IConsoleWriter Writer;
        public Display(IConsoleWriter writer)
        {
            Writer = writer;
        }
        public void PhoneConnected()
        {
            Writer.write("Phone is connected to USBcharger");
        }

        public void RFIDRead()
        {
            Writer.write("RFID Tag is read");
        }

        public void ConnectionError()
        {
            Writer.write("A phone is not connected");
        }

        public void CabinetOccupied()
        {
            Writer.write("The cabinet is currently in use");
        }

        public void RFIDError()
        {
            Writer.write("Error with reading RFID");
        }

        public void RemovePhone()
        {
            Writer.write("Please remove the phone from the cabinet");
        }
    }
}