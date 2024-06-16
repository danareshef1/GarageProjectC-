using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class VehicleDataInGarage
    {
        private Vehicle m_Vehicle;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eCarStatus m_CarStatus = eCarStatus.InRepair;

        public VehicleDataInGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber
           ,eCarStatus i_CarStatus)
        {
            m_Vehicle = i_Vehicle;
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_CarStatus = i_CarStatus;
        }
        public eCarStatus CarStatus { get { return m_CarStatus; } set { m_CarStatus = value; } }

    }
}
