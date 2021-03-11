using System;
using Charger_Functionality_Library;
using Charger_Functionality_Library.Classes;

namespace runnable_console_app
{
    class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            Door door = new Door(); //Der bruges metoder (open og close) der ikke er en del af interface
           

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

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

                    //case 'R':
                    //    System.Console.WriteLine("Indtast RFID id: ");
                    //    string idString = System.Console.ReadLine();

                    //    int id = Convert.ToInt32(idString);
                    //    rfidReader.OnRfidRead(id);
                    //    break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}

