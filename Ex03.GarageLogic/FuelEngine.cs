﻿using System;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private eFuelType m_FuelType;

        public FuelEngine(float i_MaxEnergy, eFuelType i_FuelType = 0) : base(i_MaxEnergy)
        {
            m_FuelType = i_FuelType;
        }

        public override void FillEngine(float i_HowMuchToAdd, eFuelType i_WhatToAdd)
        {
            if (i_HowMuchToAdd + m_EnergyRemaining > r_MaxEnergy || i_HowMuchToAdd < 0)
            {
                throw new ValueOutOfRangeException(r_MaxEnergy - m_EnergyRemaining, 0);
            }

            if (i_WhatToAdd != m_FuelType)
            {
                throw new ArgumentException("The fuel type you want to add not match the engine fuel type.");
            }

            m_EnergyRemaining += i_HowMuchToAdd;
        }
    }
}