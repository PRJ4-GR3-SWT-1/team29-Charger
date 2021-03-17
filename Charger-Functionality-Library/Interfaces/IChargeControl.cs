using System;
using System.Collections.Generic;
using System.Text;

namespace Charger_Functionality_Library.Interfaces
{
    public interface IChargeControl
    {
        public bool IsConnected();

        public void StartCharge();

        public void StopCharge();


    }

    
}
