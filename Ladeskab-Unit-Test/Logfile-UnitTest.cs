using Charger_Functionality_Library.Classes;
using Charger_Functionality_Library.Interfaces;
using Charger_Functionality_Library.Help_Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace Ladeskab_Unit_Test
{
    class Logfile_UnitTest
    {
        private LogFile logFile;
        private ITimeProvider time;
        private IFileWriter fileWriter;

        [SetUp]
        public void Setup()
        {
            time = Substitute.For<ITimeProvider>();
            fileWriter = Substitute.For<IFileWriter>();

            logFile = new LogFile(time,fileWriter);
        }

        /*************** Door Unlocked Log **************/
        [Test]
        public void LogFile_DoorUnlocked_CheckITimeProviderCallsRecieved()
        {
            string RFID = "abc1234";
            logFile.DoorUnlocked(RFID);
            time.Received(1);
        }

        [Test]
        public void LogFile_DoorUnlocked_CheckIFileWriterCallsRecieved()
        {
            string RFID = "abc1234";
            logFile.DoorUnlocked(RFID);
            fileWriter.Received(1);
        }

        /*************** Door Locked Log **************/
        [Test]
        public void LogFile_DoorLocked_CheckITimeProviderCallsRecieved()
        {
            string RFID = "abc1234";
            logFile.DoorLocked(RFID);
            time.Received(1);
        }

        [Test]
        public void LogFile_DoorLocked_CheckIFileWriterCallsRecieved()
        {
            string RFID = "abc1234";
            logFile.DoorUnlocked(RFID);
            fileWriter.Received(1);
        }

    }
}
