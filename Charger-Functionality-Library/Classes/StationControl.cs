using System;
using Charger_Functionality_Library.Interfaces;

namespace Charger_Functionality_Library
{
    public class StationControl
    {
        private IDoor door;
        private IRFIDReader rfidReader;
        private IDisplay display;
        private IChargeControl chargeControl;
        private ILogFile logFile;

        public StationControl(IDoor Do, IRFIDReader RFID,IDisplay Dis, IChargeControl Charge,ILogFile log)
        {
            door = Do;
            rfidReader = RFID;
            display = Dis;
        }
        public void DoorOpened()
        {
            throw new System.NotImplementedException();
        }
        public void DoorClosed()
        {
            throw new System.NotImplementedException();
        }
        public void RfidDetected(int Id)
        {
            throw new System.NotImplementedException();
        }
    }
}
