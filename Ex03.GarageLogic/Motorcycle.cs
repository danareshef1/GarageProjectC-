using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Motorcycle: Vehicle
    {
        private const int k_TireAmount = 2;
        private const int k_MaxAirPressure = 33;
        private const eFuelType k_FuelType = eFuelType.Octan98;
        private const float k_MaxFuelAmountInLiter = 5.5f;
        private const float k_MaxBatteryTime = 2.5f;

        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public Motorcycle(eLicenseType i_LicenseType, int i_EngineCapacity, string i_ModelName, float i_PrecentOfRemainingEnergy,
                string i_OwnerName, string i_OwnerPhoneNumber, List<float> i_IndividualTirePressures)
         : base(i_ModelName, i_PrecentOfRemainingEnergy, i_OwnerName, i_OwnerPhoneNumber, i_IndividualTirePressures)
        {
            m_LicenseType = i_LicenseType;
            m_EngineCapacity = i_EngineCapacity;

            if (m_Engine is FuelEngine)
            {
                ((FuelEngine)m_Engine).FuelType = k_FuelType;
                ((FuelEngine)m_Engine).MaxFuelAmountInLiter = k_MaxFuelAmountInLiter;
            }
            else
            {
                ((ElectricEngine)m_Engine).MaxBatteryTime = k_MaxBatteryTime;
            }

            
        }
    }
}
