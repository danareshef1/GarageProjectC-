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
        private float m_FuelAmountInLiters;
        private float m_MaxFuelAmountInLiters;

        public eFuelType FuelType { get {  return m_FuelType; } set { m_FuelType = value; } }
        public float FuelAmountInLiters {  get { return m_FuelAmountInLiters; } set { m_FuelAmountInLiters = value; } }
        public float MaxFuelAmountInLiter { get { return m_MaxFuelAmountInLiters; } set { m_MaxFuelAmountInLiters = value; } }
        public override void FillEngine(float i_HowMuchToAdd, eFuelType i_WhatToAdd)
        {
            //ToDo - change the amount and check
        }
    }
}
