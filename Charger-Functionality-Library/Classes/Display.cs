using System;
using Charger_Functionality_Library.Interfaces;

namespace Charger_Functionality_Library.Classes
{
    public class Display : IDisplay
    {
        public void PhoneConnected()
        {
            Console.WriteLine("Phone is connected to USBcharger");
        }

        public void RFIDRead()
        {
            Console.WriteLine("RFID Tag is read");
        }

        public void ConnectionError()
        {
            Console.WriteLine("A phone is not connected");
        }

        public void CabinetOccupied()
        {
            Console.WriteLine("The cabinet is currently in use");
        }

        public void RFIDError()
        {
            Console.WriteLine("Error with reading RFID");
        }

        public void RemovePhone()
        {
            Console.WriteLine("Please remove the phone from the cabinet");
        }
    }
}