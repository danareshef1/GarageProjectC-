using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected float m_EnergyRemaining;
        protected readonly float r_MaxEnergy;

        public Engine(float i_MaxEnergy)
        {
            r_MaxEnergy = i_MaxEnergy;
        }

        public float EnergyRemaining
        {
            get { return m_EnergyRemaining; }
            set
            {
                if (ValueIsUnderMax(value))
                {
                    m_EnergyRemaining = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(r_MaxEnergy, 0);
                }
            }
        }

        public float MaxEnergy { get { return m_EnergyRemaining; } }
        public bool ValueIsUnderMax(float i_ValueEnergy)
        {
            return i_ValueEnergy < r_MaxEnergy;
        }
        public abstract void FillEngine(float i_HowMuchToAdd, eFuelType i_WhatToAdd);

    }
}
