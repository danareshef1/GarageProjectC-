using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_IsMoveHazardMaterialsIndex = 0;
        private const int m_CargoCapacityIndex = 1;
        private const int k_TireAmount = 12;
        private const int k_MaxAirPressure = 28;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private const float k_MaxFuelAmountInLiter = 120f;
        private bool m_IsMoveHazardMaterials;
        private float m_CargoCapacity;

        public Truck(string i_LicenseNumber) : base(i_LicenseNumber)
        {
        }

        public override float MaxBatteryTime => throw new NotImplementedException();

        public bool IsMoveHazardMaterials { get { return m_IsMoveHazardMaterials; } }
        public float CargoCapacity
        {
            get { return m_CargoCapacity; }
        }


        public override eFuelType FuelType { get { return k_FuelType; } }
        public override float MaxFuelAmount { get { return k_MaxFuelAmountInLiter; } }
        public override int NumberOfTires { get { return k_TireAmount; } }
        public override float MaxTireAirPressure { get { return k_MaxAirPressure; } }

        public override void CheckAndInsertSpecificData()
        {
            checkIsMoveHazard();
            checkCargoCapacity();
        }

        private void checkIsMoveHazard()
        {
            string isMoveHazard = ConvertInputToEnumFormat(SpecieficDetailsForVehicle[k_IsMoveHazardMaterialsIndex]);
            if (isMoveHazard == "Yes")
            {
                m_IsMoveHazardMaterials = true;
            }
            else if (isMoveHazard == "No")
            {
                m_IsMoveHazardMaterials = false;

            }
            else
            {
                throw new FormatException("Move hazard materials should be yes/no.");
            }
        }

        private void checkCargoCapacity()
        {
            float cargoCapacity;
            if (float.TryParse(m_SpecificDetailsForVehicle[m_CargoCapacityIndex], out cargoCapacity))
            {
                if (cargoCapacity < 0)
                {
                    throw new ArgumentException("The cargo capacity should be positive");
                }
                m_CargoCapacity = cargoCapacity;
            }
            else
            {
                throw new FormatException("Capacity should be a number.");
            }
        }
        public override string[] SpecificData()
        {
            return new string[] { "Is MoveHazard Materials", "Cargo Capacity" };
        }


    }
}
