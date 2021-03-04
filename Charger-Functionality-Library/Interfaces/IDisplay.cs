namespace Charger_Functionality_Library
{
    public interface IDisplay
    {
        public void PhoneConnected();
        public void RFIDRead();
        public void ConnectionError();
        public void CabinetOccupied();
        public void RFIDError();
        public void RemovePhone();
    }
}