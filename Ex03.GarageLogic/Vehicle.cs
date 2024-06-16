using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private string m_LicenseNumber;
        private float m_PrecentOfRemainingEnergy;
        private List<Tire> m_Tires = new List<Tire>();
        protected Engine m_Engine;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eCarStatus m_CarStatus = eCarStatus.InRepair;

        public List<Tire> Tires { get { return m_Tires; } }
        public string LicenseNumber { get { return m_LicenseNumber; } set { m_LicenseNumber = value; } }
        public eCarStatus CarStatus { get { return m_CarStatus; } set { m_CarStatus = value; } }
        public Engine Engine { get { return m_Engine; } set { m_Engine = value; } }

        public Vehicle(string i_ModelName, float i_PrecentOfRemainingEnergy, string i_OwnerName,
                       string i_OwnerPhoneNumber, List<float> i_TirePressures, int i_NumOfTires, float i_MaxAirPressure)
        {
            m_ModelName = i_ModelName;
            m_PrecentOfRemainingEnergy = i_PrecentOfRemainingEnergy;
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;

            if (i_NumOfTires != i_TirePressures.Count)
            {
                //throw new ArgumentException("Number of tire pressures does not match number of tires.");
            }

            for (int i = 0; i < i_NumOfTires; i++)
            {
                m_Tires[i].Infaltion(i_TirePressures[i]);
                m_Tires[i].MaxTirePressue = i_MaxAirPressure;

            }
        }


        public class Tire
        {
            private string m_ManufacturerName;
            private float m_TirePressure;
            private float m_MaxTirePressure;

            public string ManufacturerName { get { return m_ManufacturerName; } set { m_ManufacturerName = value; } }
            public float TirePressure { get { return m_TirePressure; } set { m_TirePressure = value; } }
            public float MaxTirePressue { get { return m_MaxTirePressure; } set { m_MaxTirePressure = value; } }
            public void Infaltion(float i_HowManyAirToAdd)
            {
                //ToDo - change and check valid 
            }

        }




    }
}
