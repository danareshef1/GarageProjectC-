using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const int k_CarColorIndex = 1;
        private const int k_HowManyDoorsIndex = 0;
        private const int k_TireAmount = 5;
        private const int k_MaxAirPressure = 31;
        private const eFuelType k_FuelType = eFuelType.Octan95;
        private const float k_MaxFuelAmountInLiter = 45f;
        private const float k_MaxBatteryTime = 3.5f;
        private const int k_MinDoorAmount = 2;
        private const int k_MaxDoorAmount = 5;
        private eCarColor m_CarColor;
        private int m_HowManyDoors;

        public eCarColor CarColor { get { return m_CarColor; } }
        public int HowManyDoors { get { return m_HowManyDoors; } }


        public Car(string i_LicenseNumber) : base(i_LicenseNumber)
        {

        }

        public override eFuelType FuelType { get { return k_FuelType; } }
        public override float MaxFuelAmount { get { return k_MaxFuelAmountInLiter; } }
        public override float MaxBatteryTime { get { return k_MaxBatteryTime; } }
        public override int NumberOfTires { get { return k_TireAmount; } }
        public override float MaxTireAirPressure { get { return k_MaxAirPressure; } }

        public enum eCarColor
        {
            Yellow = 1,
            White,
            Red,
            Black
        }
        public override void CheckAndInsertSpecificData()
        {
            checkDoorsNum();
            checkCarColor();
        }

        public void checkDoorsNum()
        {
            int doorsNum;

            if (int.TryParse(m_SpecificDetailsForVehicle[k_HowManyDoorsIndex], out doorsNum))
            {
                if (doorsNum < k_MinDoorAmount || doorsNum > k_MaxDoorAmount)
                {
                    throw new ValueOutOfRangeException(k_MaxDoorAmount, k_MinDoorAmount);
                }
                else
                {
                    m_HowManyDoors = doorsNum;
                }
            }
            else
            {
                throw new FormatException("Door amount should be a number.");
            }
        }

        private void checkCarColor()
        {
            eCarColor carColor;
            string colorOfCar = ConvertInputToEnumFormat(m_SpecificDetailsForVehicle[k_CarColorIndex]);

            if (Enum.TryParse<eCarColor>(colorOfCar, out carColor) && 
                !int.TryParse(colorOfCar , out int result))
            {
                m_CarColor = carColor;
            }
            else
            {
                throw new FormatException("Not valid color.");
            }
        }
        public override string[] SpecificData()
        {
            return new string[] {$"How Many Doors from ({k_MinDoorAmount} - {k_MaxDoorAmount})",
                $"Car Color, options are {eCarColor.White}, {eCarColor.Red}, {eCarColor.Yellow}, {eCarColor.Black}" };
        }

    }
}
