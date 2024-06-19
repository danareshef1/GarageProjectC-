using System;
using static Ex03.GarageLogic.Motorcycle;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private const int k_LicenseTypeIndex = 0;
        private const int k_EngineCapacityIndex = 1;
        private const int k_TireAmount = 2;
        private const int k_MaxAirPressure = 33;
        private const eFuelType k_FuelType = eFuelType.Octan98;
        private const float k_MaxFuelAmountInLiter = 5.5f;
        private const float k_MaxBatteryTime = 2.5f;
        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public enum eLicenseType
        {
            AA = 1,
            A1,
            A,
            B1
        }

        public Motorcycle(string i_LicenseNumber) : base(i_LicenseNumber)
        {

        }

        public override eFuelType FuelType
        {
            get
            {
                return k_FuelType;
            }
        }

        public override float MaxFuelAmount
        {
            get
            {
                return k_MaxFuelAmountInLiter;
            }
        }

        public override float MaxBatteryTime
        {
            get
            {
                return k_MaxBatteryTime;
            }
        }

        public override int NumberOfTires
        {
            get
            {
                return k_TireAmount;
            }
        }

        public override float MaxTireAirPressure
        {
            get
            {
                return k_MaxAirPressure;
            }
        }

        public override void CheckAndInsertSpecificData()
        {
            checkLicenseType();
            checkEngineCapacity();
        }

        private void checkLicenseType()
        {
            eLicenseType licenseType;
            string specialData = m_SpecificDetailsForVehicle[k_LicenseTypeIndex];

            if (eLicenseType.TryParse(specialData, out licenseType)
                && !int.TryParse(specialData, out int result))
            {
                m_LicenseType = licenseType;
            }
            else
            {
                throw new FormatException("License type is not valid.");
            }
        }

        private void checkEngineCapacity()
        {
            int engineCapacity;

            if (int.TryParse(m_SpecificDetailsForVehicle[k_EngineCapacityIndex], out engineCapacity))
            {
                if (engineCapacity < 0)
                {
                    throw new ArgumentException("The engine capacity should be positive");
                }
                else
                {
                    m_EngineCapacity = engineCapacity;
                }
            }
            else
            {
                throw new FormatException("Egine capacity should be a number.");
            }
        }

        public override string[] SpecificData()
        {
            return new string[] { $"License Type, options are: " +
                                $"{eLicenseType.B1}, {eLicenseType.A1},  {eLicenseType.AA}, {eLicenseType.A}" ,
                                "Engine Capacity" };
        }
    }
}