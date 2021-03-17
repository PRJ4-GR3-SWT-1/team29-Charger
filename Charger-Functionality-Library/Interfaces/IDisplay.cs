namespace Charger_Functionality_Library.Interfaces
{
    public interface IDisplay
    {
        public void PhoneConnected();
        public void RFIDRead();
        public void ConnectionError();
        public void CabinetOccupied();
        public void RFIDError();
        public void RemovePhone();
        public void PhoneIsCharged();
        public void PhoneIsCharging();
        public void PhoneChargingError();
    }
}