using System;
using Charger_Functionality_Library.EventArgsClasses;

namespace Charger_Functionality_Library
{
    public interface IRFIDReader
    {
        event EventHandler<RfidEventArgs> TagReadEvent;
    }
}