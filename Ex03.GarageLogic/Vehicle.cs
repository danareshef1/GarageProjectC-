using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        private string m_ModelName;
        private string m_LicenseNumber;
        private float m_PrecentOfRemainingEnergy;
        private List<Tire> m_Tires = new List<Tire>();
        private Engine m_Engine;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eCarStatus m_CarStatus = eCarStatus.InRepair;

        public string LicenseNumber { get { return m_LicenseNumber; } set { m_LicenseNumber = value; } }
        public eCarStatus CarStatus { get { return m_CarStatus; } set { m_CarStatus = value; } }

        public Vehicle(string i_ModelName, float i_PrecentOfRemainingEnergy, string i_OwnerName, 
            string i_OwnerPhoneNumber)
        {
            m_ModelName = i_ModelName;
            m_PrecentOfRemainingEnergy = i_PrecentOfRemainingEnergy;
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
        }

        internal class Tire
        {
            private string m_ManufacturerName;
            private float m_TyrePressure;
            private float m_MaxTyrePressure;

            public void Infaltion(float i_HowManyAirToAdd)
            {
                //ToDo - change and check valid 
            }

        }




    }
}
