using System;
using System.Collections.Generic;
using System.Text;
using Charger_Functionality_Library.EventArgsClasses;

namespace Charger_Functionality_Library.Interfaces
{
    public interface IDoor
    {
        event EventHandler<DoorEventArgs> DoorOpenEvent;
        event EventHandler<DoorEventArgs> DoorCloseEvent;

        void LockDoor();
        void UnlockDoor();

    }
}
