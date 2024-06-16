using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsMoveHazardMaterials;
        private float m_CargoCapacity;

        public Truck(bool i_IsMoveHazardMaterials, float i_CargoCapacity, string i_ModelName, float i_PrecentOfRemainingEnergy,
              string i_OwnerName, string i_OwnerPhoneNumber, List<float> i_IndividualTirePressures)
       : base(i_ModelName, i_PrecentOfRemainingEnergy, i_OwnerName, i_OwnerPhoneNumber, i_IndividualTirePressures)
        {
            m_IsMoveHazardMaterials = i_IsMoveHazardMaterials;
            m_CargoCapacity = i_CargoCapacity;
        }
    }
}
