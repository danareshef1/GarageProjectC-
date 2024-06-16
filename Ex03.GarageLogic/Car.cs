using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const int k_TireAmount = 5;
        private const int k_MaxAirPressure = 31;
        private const eFuelType k_FuelType = eFuelType.Octan95;
        private const float k_MaxFuelAmountInLiter = 45f;
        private const float k_MaxBatteryTime = 3.5f;

        private eCarColor m_CarColor;
        private int m_HowManyDoors;

        public eCarColor CarColor { get { return m_CarColor; } }
        public int HowManyDoors { get { return m_HowManyDoors; } }


        public Car(eCarColor i_CarColor, int i_HowManyDoors, string i_ModelName, float i_PrecentOfRemainingEnergy,
                   string i_OwnerName, string i_OwnerPhoneNumber, List<float> i_IndividualTirePressures)
            : base(i_ModelName, i_PrecentOfRemainingEnergy, i_OwnerName, i_OwnerPhoneNumber, i_IndividualTirePressures,
                  k_TireAmount,k_MaxAirPressure)
        {
            m_CarColor = i_CarColor;
            m_HowManyDoors = i_HowManyDoors;

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
