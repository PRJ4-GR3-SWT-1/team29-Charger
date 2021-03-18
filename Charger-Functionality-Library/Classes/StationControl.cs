using System;
using System.IO;
using Charger_Functionality_Library.Interfaces;

namespace Charger_Functionality_Library
{
    public class StationControl
    {
        private IDoor _door;
        private IRFIDReader _rfidReader;
        private IDisplay _display;
        private IChargeControl _chargeControl;
        private ILogFile _logFile;
        private string _oldId;
        private LadeskabState _state;
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        

        public StationControl(IDoor Do, IRFIDReader RFID,IDisplay Dis, IChargeControl Charge,ILogFile log)
        {
            _door = Do;
            _rfidReader = RFID;
            _display = Dis;
            _chargeControl = Charge;
            _logFile = log;

            //Subscribe to events:
            _door.DoorOpenEvent += Door_DoorOpenEvent;
            _door.DoorCloseEvent += Door_DoorCloseEvent;
            _rfidReader.TagReadEvent += RfidReader_TagReadEvent;
        }

        //Event Handlers:
        private void RfidReader_TagReadEvent(object sender, EventArgsClasses.RfidEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_chargeControl.IsConnected())
                    {
                        _door.LockDoor();
                        _chargeControl.StartCharge();
                        _oldId = e.Id;
                        _logFile.DoorLocked(_oldId);

                        _display.PhoneConnected();
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        //Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                        _display.ConnectionError();
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (e.Id == _oldId)
                    {
                        _chargeControl.StopCharge();
                        _door.UnlockDoor();
                        //using (var writer = File.AppendText(_logFile))
                        //{
                        //    writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        //}
                        _logFile.DoorUnlocked(e.Id);

                       // Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                       _display.RemovePhone();
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

        private void Door_DoorCloseEvent(object sender, EventArgsClasses.DoorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Door_DoorOpenEvent(object sender, EventArgsClasses.DoorEventArgs e)
        {
            throw new NotImplementedException();
        }
        
    }
}
