using System;
using Charger_Functionality_Library.EventArgsClasses;
using Charger_Functionality_Library.Interfaces;

namespace Charger_Functionality_Library.Classes
{
    public class Door: IDoor
    {
        private bool islocked = false; 
        public bool IsLocked
        {
            get => islocked;
            set => islocked = value;
        }

        public bool open = true;
        public bool IsOpen
        {
            get => open;
            set => open = value;
        }

        
        public void LockDoor()
        {
            IsLocked = true;
        }

        public void UnlockDoor()
        {
            IsLocked = false;
        }
        
        
        public event EventHandler<DoorEventArgs> DoorOpenEvent;
        public event EventHandler<DoorEventArgs> DoorCloseEvent;

        /***************** OPEN DOOR EVENT ******************/
        public void OpenDoor()
        {
            if (!IsLocked)
            {
                IsOpen = true;
                OnDoorOpen(new DoorEventArgs{});
            }
        }

        protected virtual void OnDoorOpen(DoorEventArgs e)
        {
            DoorOpenEvent?.Invoke(this,e);
        }


        //**************** CLOSE DOOR EVENT *****************/
        public void CloseDoor()
        {
            IsOpen = false;
            OnDoorClose(new DoorEventArgs());
        }

        protected virtual void OnDoorClose(DoorEventArgs e)
        {
            DoorCloseEvent?.Invoke(this,e);
        }
    }
}
