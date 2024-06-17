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
        private List<Tire> m_Tires = new List<Tire>();
        protected Engine m_Engine;
        protected List<string> m_SpecieficDetailsForEachKind = new List<string>();
        private float m_PrecentOfRemainingEnergy;

        public float PrecentOfRemainingEnergy { get { return m_PrecentOfRemainingEnergy; } set { m_PrecentOfRemainingEnergy = value; } }
        public string ModelName { get { return m_ModelName; } set { m_ModelName = value; } }
        public string LicenseNumber { get { return m_LicenseNumber; } }
        public List<string> SpecieficDetailsForEachKind
        { get { return m_SpecieficDetailsForEachKind; } set { 
                m_SpecieficDetailsForEachKind = value;
                CheckAndInsertSpecificData();
            } }

        public List<Tire> Tires
        {
            get
            {
                return m_Tires;
            }
            set
            {
                if (!IsNumberOfTiresMatchVehicle(value))
                {
                    throw new ArgumentException("Invalid number of tires according to the vehicle");
                }
                IsTiresPressureMatchVehicleMax(value);
                m_Tires = value;

            }
        }

        public Engine Engine { get { return m_Engine; } set { m_Engine = value; } }

        public Vehicle(string i_LicenseNumber)
        { 
            m_LicenseNumber = i_LicenseNumber;
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

        public abstract float MaxTireAirPressure { get; }

        public abstract int NumberOfTires { get; }

        public abstract float MaxFuelAmount { get; }

        public abstract float MaxBatteryTime { get; }

        public abstract eFuelType FuelType { get; }

        protected bool IsNumberOfTiresMatchVehicle(List<Tire> i_Tires)
        {
            return i_Tires.Count == NumberOfTires;
        }
        protected void IsTiresPressureMatchVehicleMax(List<Tire> i_Tires) //check
        {
            foreach (Tire tire in i_Tires)
            {
                if (tire.MaxTirePressue != MaxTireAirPressure)
                {
                    throw new ArgumentException("Tire pressure is not in maximum pressure");
                }
            }
        }

        public abstract void CheckAndInsertSpecificData();

        public abstract string[] SpecificData();
    }
}
