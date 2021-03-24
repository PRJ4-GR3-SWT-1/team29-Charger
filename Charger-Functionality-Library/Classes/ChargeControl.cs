using System;
using System.Collections.Generic;
using System.Text;
using Charger_Functionality_Library.EventArgsClasses;
using Charger_Functionality_Library.Interfaces;

namespace Charger_Functionality_Library.Classes
{
    public class ChargeControl: IChargeControl
    {
        public ChargeControl(IUsbCharger charger, IDisplay display)
        {
            this.charger = charger;
            this.display = display;
            this.charger.CurrentValueEvent += Charger_CurrentValueEvent;//Subscribe to events
        }

        private void Charger_CurrentValueEvent(object sender, CurrentEventArgs e)
        {
            if (e.Current > 0 && e.Current <= 5) display.PhoneIsCharged();
            else if (e.Current > 5 && e.Current <= 500) display.PhoneIsCharging();
            else if (e.Current > 500)
            {
                StopCharge();
                display.PhoneChargingError();
            }
        }

        public bool IsConnected()
        {
            return isConnected;
        }

        public void StartCharge()
        {
           if(isConnected) charger.StartCharge();
        }

        public void StopCharge()
        {
            charger.StopCharge();
        }

        public void PlugPhoneIn()
        {
            isConnected = true;
        }

        public void UnPlugPhone()
        {
            isConnected = false;
        }

        private bool isConnected=false;
        private IUsbCharger charger;
        private IDisplay display;


    }
}
