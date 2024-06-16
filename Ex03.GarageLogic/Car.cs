using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eCarColor m_CarColor;
        private int m_HowManyDoors;

        public eCarColor CarColor { get { return m_CarColor; } }
        public int HowManyDoors { get { return m_HowManyDoors; } }


        public Car(eCarColor i_CarColor, int i_HowManyDoors, string i_ModelName, float i_PrecentOfRemainingEnergy,
                   string i_OwnerName, string i_OwnerPhoneNumber, List<float> i_IndividualTirePressures)
            : base(i_ModelName, i_PrecentOfRemainingEnergy, i_OwnerName, i_OwnerPhoneNumber, i_IndividualTirePressures)
        {
            m_CarColor = i_CarColor;
            m_HowManyDoors = i_HowManyDoors;
        }

    }
}
