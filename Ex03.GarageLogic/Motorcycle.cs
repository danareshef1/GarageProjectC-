using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

        public Motorcycle(string i_LicenseNumber) : base(i_LicenseNumber)
        {

        }

        public eLicenseType LicenseType { get { return m_LicenseType; } }
        public int EngineCapacity { get { return m_EngineCapacity; } }

        public enum eLicenseType
        {
            AA = 1,
            A1,
            A,
            B1
        }
        public override eFuelType FuelType { get { return k_FuelType; } }
        public override float MaxFuelAmount { get { return k_MaxFuelAmountInLiter; } }
        public override float MaxBatteryTime { get { return k_MaxBatteryTime; } }
        public override int NumberOfTires { get { return k_TireAmount; } }
        public override float MaxTireAirPressure { get { return k_MaxAirPressure; } }

        public override void CheckAndInsertSpecificData()
        {
            checkLicenseType();
            checkEngineCapacity();
        }

        private void checkLicenseType()
        {
            eLicenseType licenseType;

            if (eLicenseType.TryParse(m_SpecieficDetailsForEachKind[k_LicenseTypeIndex], out licenseType))
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

            if (int.TryParse(m_SpecieficDetailsForEachKind[k_EngineCapacityIndex], out engineCapacity))
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
