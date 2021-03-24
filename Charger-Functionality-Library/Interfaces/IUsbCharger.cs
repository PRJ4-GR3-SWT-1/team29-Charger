using System;
using System.Collections.Generic;
using System.Text;
using Charger_Functionality_Library.EventArgsClasses;

namespace Charger_Functionality_Library.Interfaces
{


    public interface IUsbCharger
    {
        // Event triggered on new current value
        event EventHandler<CurrentEventArgs> CurrentValueEvent;

        // Direct access to the current current value
        double CurrentValue { get; }

        // Require connection status of the phone
        bool Connected { get; }

        // Start charging
        void StartCharge();
        // Stop charging
        void StopCharge();
    }
}
