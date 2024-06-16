using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {

        private float m_RemainingBatteryTime;
        private float m_MaxBatteryTime;

        public float MaxBatteryTime { get { return m_MaxBatteryTime; } set { m_MaxBatteryTime = value; } }
        public float RemainingBatteryTime { get { return m_RemainingBatteryTime; } set { m_RemainingBatteryTime = value; } }


        public override void FillEngine(float i_HowMuchToAdd, eFuelType i_WhatToAdd)
        {
            //ToDo 
        }
    }
}
