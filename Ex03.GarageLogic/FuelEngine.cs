using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private eFuelType m_FuelType;
        public FuelEngine(float i_MaxEnergy) : base(i_MaxEnergy)
        { }
        public eFuelType FuelType { get { return m_FuelType; } set { m_FuelType = value; } }
        public override void FillEngine(float i_HowMuchToAdd, eFuelType i_WhatToAdd)
        {
            if (i_HowMuchToAdd + m_EnergyRemaining > r_MaxEnergy)
            {
                //  throw new ValueOutOfRangeException("Amount of fuel is bigger than the max capacity.");
            }
            if (i_WhatToAdd != m_FuelType)
            {
                throw new ArgumentException("The Fuel type you want to add not match the engine fuel type.");
            }
            m_EnergyRemaining += i_HowMuchToAdd;

        }
    }
}
