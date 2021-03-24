using System;
using System.Collections.Generic;
using System.Text;

namespace Charger_Functionality_Library.EventArgsClasses
{
    public class CurrentEventArgs : EventArgs
    {
        // Value in mA (milliAmpere)
        public double Current { set; get; }
    }
}
