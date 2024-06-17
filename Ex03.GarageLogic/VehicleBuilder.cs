using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleBuilder
    {

        public enum eVehicleType
        {
            ElectricCar,
            FuelCar,
            ElectricMotorcycle,
            FuelMotorcycle,
            Truck
        }

        public eVehicleType CheckWhichkVehicleType(string i_VehicleType)
        {
            eVehicleType vehicleType;
            switch (i_VehicleType)
            {
                case "Fuel car":
                    vehicleType = eVehicleType.FuelCar;
                    break;
                case "Electric car":
                    vehicleType = eVehicleType.ElectricCar;
                    break;
                case "Fuel motorcycle":
                    vehicleType = eVehicleType.FuelMotorcycle;
                    break;
                case "Electric motorcycle":
                    vehicleType = eVehicleType.ElectricMotorcycle;
                    break;
                case "Truck":
                    vehicleType = eVehicleType.Truck;
                    break;
                default:
                    throw new FormatException("Invalid vehicle type");

            }
            return vehicleType;
        }
        public Vehicle CreateObject(eVehicleType i_VehicleType, string i_LicenseNumber)
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
                    vehicle.Engine = new FuelEngine(vehicle.MaxFuelAmount);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    vehicle = new Motorcycle(i_LicenseNumber);
                    vehicle.Engine = new ElectricEngine(vehicle.MaxBatteryTime);
                    break;
                case eVehicleType.FuelMotorcycle:
                    vehicle = new Motorcycle(i_LicenseNumber);
                    vehicle.Engine = new FuelEngine(vehicle.MaxFuelAmount);
                    break;
                case eVehicleType.Truck:
                    vehicle = new Truck(i_LicenseNumber);
                    vehicle.Engine = new FuelEngine(vehicle.MaxFuelAmount);
                    break;
               default:
                        throw new FormatException("Vehicle type is not valid.");
            }
            return vehicle;
        }
    }
}
