using System;
using System.Collections.Generic;
using System.Text;
using Charger_Functionality_Library.Help_Interfaces;
using Charger_Functionality_Library.Interfaces;

namespace Charger_Functionality_Library.Classes
{
    public class LogFile: ILogFile
    {
        public ITimeProvider timestamp;
        public IFileWriter filewriter;

        public LogFile(ITimeProvider tp, IFileWriter fw)
        {
            timestamp = tp;
            filewriter = fw; 
        }

        public void DoorLocked(string RFid_num)
        {
            string log = timestamp.CurrentTime().ToString() + "Door locked with ID: " + RFid_num;
            filewriter.WriteToFile(log);
        }

        public void DoorUnlocked(string RFid_num)
        {
            string log = timestamp.CurrentTime().ToString() + "Door unlocked with ID: " + RFid_num;
            filewriter.WriteToFile(log);
        }

    }
}
