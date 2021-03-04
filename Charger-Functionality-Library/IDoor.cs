using System;
using System.Collections.Generic;
using System.Text;

namespace Charger_Functionality_Library
{
    interface IDoor
    {
        bool locked { get; set; }
        void lockDoor();
        void unlockDoor();
        void setState(bool state);
        void initialise();
    }
}
