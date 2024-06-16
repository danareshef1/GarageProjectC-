using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    public class GarageManager
    {
        private Garage m_Garage = new Garage();
        private VehicleBuilder m_VehicleBuilder = new VehicleBuilder();

        public void GarageMenu()
        {
            Console.WriteLine(@"(1) Add new vehicle to the garage
                                (2) Present all the license numbers in the garage
                                (3) Change vehicle status
                                (4) Infalte your vehicle's tires
                                (5) Add gas to your vehicle - if you have a vehicle on fuel
                                (6) Charge your vehicle - if you have an electric vehicle
                                (7) Present full details about a vehicle");

        }
        public void GetVehicle()
        {
            Console.Write("Please Enter your car license: ");
            string carLicense = Console.ReadLine();

            Vehicle vehicle = m_Garage.CheckIfTheVehicleIsInGarage(carLicense);
            if (vehicle != null)
            {
                m_Garage.ChangeVehicleStatusToInRepair(carLicense);
            }
            else
            {
                Console.Write("Please enter the vehicle model: ");
                string vehicleModel = Console.ReadLine();

                Console.Write("Please enter the owner name: ");
                string vehicleOwnerName = Console.ReadLine();

                Console.Write("Please enter the owner phone: ");
                string vehicleOwnerPhone = Console.ReadLine();

                VehicleBuilder.eVehicleType vehicleType = m_VehicleBuilder.CheckWhichkVehicleType(vehicleModel);
                Vehicle newVehicle = m_VehicleBuilder.CreateObject(vehicleType, carLicense);
                m_Garage.AddVehicle(newVehicle, vehicleOwnerName, vehicleOwnerPhone);
               // newVehicle.SpecificData =; 
            }

        }



    }
}
