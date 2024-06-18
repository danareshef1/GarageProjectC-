using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxEnergy) : base(i_MaxEnergy)
        { }
        public override void FillEngine(float i_HowMuchToAdd, eFuelType i_WhatToAdd)
        {
            if (i_HowMuchToAdd + m_EnergyRemaining > r_MaxEnergy || i_HowMuchToAdd < 0)
            {
                throw new ValueOutOfRangeException(r_MaxEnergy, 0);
            }
            m_EnergyRemaining += i_HowMuchToAdd;
        }
    }
}
