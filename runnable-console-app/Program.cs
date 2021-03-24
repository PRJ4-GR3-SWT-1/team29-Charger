using System;
using Charger_Functionality_Library;
using Charger_Functionality_Library.Classes;
using Charger_Functionality_Library.Help_Interfaces;
using Charger_Functionality_Library.Interfaces;
using UsbSimulator;

namespace runnable_console_app
{
    class Program
    {
       
        static void Main(string[] args)
        {
            bool finish = false;

            //Assemble your system here from all the classes
            Door door = new Door(); //Der bruges metoder (open og close) der ikke er en del af interface
            IDisplay display = new Display(new ConsoleWriter());
            IUsbCharger usb = new UsbChargerSimulator();
            ChargeControl chargeControl = new ChargeControl(usb, display);
            ILogFile logFile = new LogFile(new TimeProvider(), new FileWriter());
            RfidReader rfidReader = new RfidReader();
            door = new Door();
            
            StationControl station = new StationControl(door, rfidReader, display, chargeControl, logFile);
            do
            {
                string input;
                System.Console.WriteLine("Indtast bogstav: E_nd, O_pen, C_lose, R_rfid, P_lug eller U_nplug: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;
                input = input.ToUpper();
                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.OpenDoor();
                        break;

                    case 'C':
                        door.CloseDoor();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();
                        
                        rfidReader.ManualScanTag(idString);
                        break;
                    case 'P':
                        chargeControl.PlugPhoneIn();
                        break;
                    case 'U':
                        chargeControl.UnPlugPhone();
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}

