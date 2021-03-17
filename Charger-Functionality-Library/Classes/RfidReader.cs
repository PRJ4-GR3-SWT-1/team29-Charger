using System;
using System.Collections.Generic;
using System.Text;
using Charger_Functionality_Library.EventArgsClasses;

namespace Charger_Functionality_Library.Classes
{
    public class RfidReader: IRFIDReader
    {
        public event EventHandler<RfidEventArgs> TagReadEvent;

        public void ManualScanTag(string id)
        {
            OnScanTag(new RfidEventArgs { Id=id });
            
        }

        protected virtual void OnScanTag(RfidEventArgs e)
        {
            TagReadEvent?.Invoke(this, e);
        }
    }
}
