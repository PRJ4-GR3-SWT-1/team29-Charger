using System;
using System.Collections.Generic;
using System.Text;
using Charger_Functionality_Library.EventArgsClasses;
using Charger_Functionality_Library.Interfaces;

namespace Charger_Functionality_Library.Classes
{
    public class Door: IDoor
    {
        private bool locked = true; 
        public bool Locked
        {
            get => locked;
            set => locked = Locked;
        }

        public bool open = true;
        public bool Open
        {
            get => open;
            set => open = Open;
        }

        public event EventHandler<DoorEventArgs> DoorOpenEvent;
        public event EventHandler<DoorEventArgs> DoorCloseEvent;

        public void LockDoor()
        {
            Locked = true;
        }

        public void UnlockDoor()
        {
            Locked = false;
        }

        public void OpenDoor()
        {
            if (Locked)
            {
                Open = true;
                OnDoorOpen(new DoorEventArgs{});
            }
        }

        protected virtual void OnDoorOpen(DoorEventArgs e)
        {
            DoorOpenEvent?.Invoke(this,e);
        }

        public void CloseDoor()
        {
            throw new NotImplementedException();
        }
    }
}
