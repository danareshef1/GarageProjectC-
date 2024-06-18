using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleBuilder
    {
        private string[] vehicleOptionalTypes = {"Fuel car", "Fuel motorcycle",
                                                "Electric car", "Electric motorcycle",
                                                 "Truck"};
        public string[] VehicleOptionalTypes
        {
            get
            {
                return vehicleOptionalTypes;
            }
        }

        public enum eVehicleType
        {
            FuelCar = 1,
            FuelMotorcycle,
            ElectricCar,
            ElectricMotorcycle,
            Truck
        }

        public Vehicle CreateVehicleObject(eVehicleType i_VehicleType, string i_LicenseNumber)
        {
            Vehicle vehicle;
            switch (i_VehicleType)
            {
                case eVehicleType.ElectricCar:
                    vehicle = new Car(i_LicenseNumber);
                    vehicle.Engine = new ElectricEngine(vehicle.MaxBatteryTime);
                    break;
                case eVehicleType.FuelCar:
                    vehicle = new Car(i_LicenseNumber);
                    vehicle.Engine = new FuelEngine(vehicle.MaxFuelAmount, vehicle.FuelType);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    vehicle = new Motorcycle(i_LicenseNumber);
                    vehicle.Engine = new ElectricEngine(vehicle.MaxBatteryTime);
                    break;
                case eVehicleType.FuelMotorcycle:
                    vehicle = new Motorcycle(i_LicenseNumber);
                    vehicle.Engine = new FuelEngine(vehicle.MaxFuelAmount, vehicle.FuelType);
                    break;
                case eVehicleType.Truck:
                    vehicle = new Truck(i_LicenseNumber);
                    vehicle.Engine = new FuelEngine(vehicle.MaxFuelAmount, vehicle.FuelType);
                    break;
                default:
                    throw new FormatException("Vehicle type is not valid.");
            }

            return vehicle;
        }
    }
}
