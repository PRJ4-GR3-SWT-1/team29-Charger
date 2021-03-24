using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Charger_Functionality_Library.Help_Interfaces
{
    public interface IFileWriter
    {
        

        public void WriteToFile(string txt);
    }

    public class FileWriter : IFileWriter
    {
        
        public void WriteToFile(string txt)
        {
            using (StreamWriter w = File.AppendText("LadeskabLog.txt"))
            {
                w.Write(txt);
            }
        }
    }
}
