﻿using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleDataInGarage> r_Vehicles = new Dictionary<string, VehicleDataInGarage>();

        public Dictionary<string, VehicleDataInGarage> Vehicles
        {
            get
            {
                return r_Vehicles;
            }
        }

        public Dictionary<string, VehicleDataInGarage> GetLicenseNumbersByStatus(eVehicleStatus i_StatusToPresentBy)
        {
            Dictionary<string, VehicleDataInGarage> vehiclesByStatus = new Dictionary<string, VehicleDataInGarage>();

            foreach (KeyValuePair<string, VehicleDataInGarage> vehicle in r_Vehicles)
            {
                if (vehicle.Value.VehicleStatus == i_StatusToPresentBy)
                {
                    vehiclesByStatus[vehicle.Key] = vehicle.Value;
                }
            }

            return vehiclesByStatus;
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, eVehicleStatus i_NewCarStatus)
        {
            if (i_NewCarStatus == Vehicles[i_LicenseNumber].VehicleStatus)
            {
                throw new ArgumentException("You chose your current status!");
            }

            r_Vehicles[i_LicenseNumber].VehicleStatus = i_NewCarStatus;
        }

        public void InfaltionToMax(string i_LicenseNumber)
        {
            foreach (Tire tire in r_Vehicles[i_LicenseNumber].Vehicle.Tires)
            {
                tire.Inflation(tire.MaxTirePressure - tire.TirePressure);
            }
        }

        public void FuelingVehicle(string i_LicenseNumber, eFuelType i_FuelType, float i_FuelAmount)
        {
            r_Vehicles[i_LicenseNumber].Vehicle.Engine.FillEngine(i_FuelAmount, i_FuelType);
        }

        public void ChargeVehicle(string i_LicenseNumber, float i_HoursToCharge)
        {
            r_Vehicles[i_LicenseNumber].Vehicle.Engine.FillEngine(i_HoursToCharge);
        }

        public void CheckIfTheVehicleIsInGarage(string i_LicenseNumber)
        {
            if (!r_Vehicles.ContainsKey(i_LicenseNumber))
            {
                throw new ArgumentException("There is no such vehicle in the garage");
            }
        }

        public void AddVehicle(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            r_Vehicles[i_Vehicle.LicenseNumber] = new VehicleDataInGarage(i_Vehicle, i_OwnerName, i_OwnerPhoneNumber);
        }
    }
}