﻿using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.Vehicle;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private string m_LicenseNumber;
        private List<Tire> m_Tires = new List<Tire>();
        protected Engine m_Engine;
        protected List<string> m_SpecificDetailsForVehicle = new List<string>();
        private float m_PrecentOfRemainingEnergy;

        public Vehicle(string i_LicenseNumber)
        {
            m_LicenseNumber = i_LicenseNumber;
        }

        public float PrecentOfRemainingEnergy
        {
            get
            {
                return m_PrecentOfRemainingEnergy;
            }
            set
            {
                m_PrecentOfRemainingEnergy = value;
            }
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
            set
            {
                m_ModelName = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }
        }

        public List<string> SpecieficDetailsForVehicle
        {
            get
            {
                return m_SpecificDetailsForVehicle;
            }
            set
            {
                m_SpecificDetailsForVehicle = value;
                CheckAndInsertSpecificData();
            }
        }

        public List<Tire> Tires
        {
            get
            {
                return m_Tires;
            }
        }

        public Engine Engine
        {
            get
            {
                return m_Engine;
            }
            set
            {
                m_Engine = value;
            }
        }

        public void CalculatePrecentRemainingEnergy()
        {
            m_PrecentOfRemainingEnergy = m_Engine.EnergyRemaining / m_Engine.MaxEnergy * 100;
        }

        public void AddNewTireToTireList(string i_ManufacturerName, float i_TirePressure)
        {
            Tire newTire = new Tire(i_ManufacturerName, i_TirePressure, MaxTireAirPressure);

            IsLowerThanTireAirPressureeMax(newTire);
            newTire.ManufacturerName = i_ManufacturerName;
            m_Tires.Add(newTire);
        }

        public abstract float MaxTireAirPressure { get; }

        public abstract int NumberOfTires { get; }

        public abstract float MaxFuelAmount { get; }

        public abstract float MaxBatteryTime { get; }

        public abstract eFuelType FuelType { get; }

        public void CheckIfTireNumberMatchVehicle()
        {
            if (!IsNumberOfTiresMatchVehicle(m_Tires))
            {
                throw new ArgumentException("Invalid number of tires according to the vehicle");
            }
        }

        protected bool IsNumberOfTiresMatchVehicle(List<Tire> i_Tires)
        {
            return i_Tires.Count == NumberOfTires;
        }

        protected void IsLowerThanTireAirPressureeMax(Tire i_Tire)
        {
            if (i_Tire.TirePressure > MaxTireAirPressure || i_Tire.TirePressure < 0)
            {
                throw new ValueOutOfRangeException(MaxTireAirPressure, 0);
            }
        }

        public abstract void CheckAndInsertSpecificData();

        public abstract string[] SpecificData();

        protected string ConvertInputToEnumFormat(string i_Input)
        {
            string sentence = i_Input.ToLower();

            sentence = sentence.Substring(1);
            string firstLetter = i_Input[0].ToString().ToUpper();

            return firstLetter + sentence;
        }
    }
}