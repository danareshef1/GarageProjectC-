using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.VehicleBuilder;

namespace Ex03.ConsoleUI
{
    public class GarageManager
    {
        private readonly Garage r_Garage = new Garage();
        private readonly VehicleBuilder r_VehicleBuilder = new VehicleBuilder();


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
            List<string> specificData = new List<string>();
            string carLicense = GetLicenseNumberFromUser();

            Vehicle vehicle = r_Garage.CheckIfTheVehicleIsInGarage(carLicense);
            if (vehicle != null)
            {
                r_Garage.ChangeVehicleStatusToInRepair(carLicense);
            }
            else
            {
                Console.Write("Please enter the vehicle model: ");
                string vehicleModel = Console.ReadLine();

                Console.Write("Please enter the owner name: ");
                string vehicleOwnerName = Console.ReadLine();

                Console.Write("Please enter the owner phone: ");
                string vehicleOwnerPhone = Console.ReadLine();

                VehicleBuilder.eVehicleType vehicleType = r_VehicleBuilder.CheckWhichkVehicleType(vehicleModel);
                Vehicle newVehicle = r_VehicleBuilder.CreateObject(vehicleType, carLicense);
                r_Garage.AddVehicle(newVehicle, vehicleOwnerName, vehicleOwnerPhone);

                for (int i = 0; i < newVehicle.SpecificData().Length; i++)
                {
                    Console.Write("please enter {0} ", newVehicle.SpecificData()[i]);
                    specificData.Add(Console.ReadLine());
                }
                newVehicle.SpecieficDetailsForEachKind = specificData;


                Console.Write("Please enter the current {0} situation in your car: ", vehicleModel[0]);
                string remainingEnergy = Console.ReadLine();
                float energyRemaining;
                checkIfNumber(remainingEnergy, out energyRemaining);
                newVehicle.Engine.EnergyRemaining = energyRemaining;

                Console.Write(@"Do you want to add tire air pressure of all the tires together?
                                (1) yes
                                (2) no");
                string userTirePressureChoice = Console.ReadLine();

                isValidChoice(userTirePressureChoice, 1, 2);
                Console.WriteLine("Please enter tire manufactor name:");
                string tireManufactorName = Console.ReadLine();

                List<Vehicle.Tire> tiresList = getTiresPressure(newVehicle, isGetAllTirePressureTogether(userTirePressureChoice));

                foreach (Vehicle.Tire tire in tiresList)
                {
                    tire.ManufacturerName = tireManufactorName;
                }

                newVehicle.Tires = tiresList;

            }

        }

        private bool isGetAllTirePressureTogether(string i_UserTirePressureChoice)
        {
            return (i_UserTirePressureChoice == "1");
        }


        private List<Vehicle.Tire> getTiresPressure(Vehicle i_Vehicle, bool i_IsGetAllTirePressureTogether)
        {
            List<Vehicle.Tire> tiresList = new List<Vehicle.Tire>();
            float tireAirPressuerNumber;

            Console.WriteLine("Please enter tires air pressure: ");
            if (i_IsGetAllTirePressureTogether)
            {
                checkIfNumber(Console.ReadLine(), out tireAirPressuerNumber);
                Vehicle.Tire newTire = new Vehicle.Tire();
                newTire.TirePressure = tireAirPressuerNumber;
                tiresList.Add(newTire);
            }
            else
            {
                List<float> tirePressure = new List<float>();
                for (int i = 0; i < i_Vehicle.NumberOfTires; i++)
                {
                    Console.Write("Please enter tire number {0} pressure:", i);
                    checkIfNumber(Console.ReadLine(), out tireAirPressuerNumber);
                    tirePressure.Add(tireAirPressuerNumber);
                    Vehicle.Tire newTire = new Vehicle.Tire();
                    newTire.TirePressure = tireAirPressuerNumber;
                    tiresList.Add(newTire);
                }
            }
            return tiresList;


        }

    
        public void PresentLicenseNumbersInTheGarage()
        {
            Console.WriteLine("Here are all the cars we have in the garage right now:");
            foreach (var vehicle in r_Garage.Vehicles)
            {
                Console.WriteLine(vehicle.Key);
            }
            Console.WriteLine(@"Do you want to filter them by status?
                              (1) yes
                              (2) no");
            string filterBYChoice = Console.ReadLine();

            isValidChoice(filterBYChoice, 1, 2);
            int toFilterBy = int.Parse(filterBYChoice);

            if(toFilterBy == 1)
            {
                eCarStatus chosenStatus;
                chosenStatus = ChooseFilter();
                PresentLicenseNumbersInTheGarageFiltered(chosenStatus);
            }
            else
            {
                GarageMenu();
            }
        }
        public eCarStatus ChooseFilter()
        {
            Console.WriteLine(@"By which status you would like to see?
                              (1) fixed
                              (2) inRepair
                              (3) paid");
            string whichStatus = Console.ReadLine();

            isValidChoice(whichStatus, 1, 3);
            int whichStatusChoice = int.Parse(whichStatus);

            eCarStatus ChosenStatus;
            switch (whichStatusChoice)
            {
                case 1:
                    ChosenStatus = eCarStatus.Fixed;
                    break;
                case 2:
                    ChosenStatus = eCarStatus.InRepair;
                    break;
                case 3:
                    ChosenStatus = eCarStatus.Paid;
                    break;
                default:
                    throw new FormatException("Invalid status type");

            }

            return ChosenStatus;
        }
        private void PresentLicenseNumbersInTheGarageFiltered(eCarStatus i_CarStatus)
        {
            Console.WriteLine("Here are all the cars we have in the garage right now in the status you chose:");
            foreach (var vehicle in r_Garage.Vehicles)
            {
                if (vehicle.Value.CarStatus == i_CarStatus)
                {
                    Console.WriteLine(vehicle.Key);
                }
            }

        }
        private void checkIfNumber(string i_UserInput, out float o_UserInput)
        {
            if (!float.TryParse(i_UserInput, out o_UserInput))
            {
                throw new FormatException("should be a number");
            }
        }
        private void isValidChoice(string i_UserInput, int i_MinOption, int i_MaxOption)
        {
            int userInput;
            if (int.TryParse(i_UserInput, out userInput))
            {
                if (userInput < i_MinOption || userInput > i_MaxOption)
                {
                    throw new ValueOutOfRangeException(i_MaxOption, i_MinOption);
                }
            }
            else
            {
                throw new FormatException("Value should be a number.");
            }
        }


        /// 3
        public void ChangeVehicleStatus()
        {
            string vehicleLicense = GetLicenseNumberFromUser();

            eCarStatus vehicleStatus = GetVehicleStatusFromTheUser(vehicleLicense);

            r_Garage.ChangeVehicleStatus(vehicleLicense, vehicleStatus);
        }

        public string GetLicenseNumberFromUser()
        {
            Console.Write("Please Enter your vehicle license number: ");
            string vehicleLicense = Console.ReadLine();

            return vehicleLicense;
        }

        public eCarStatus GetVehicleStatusFromTheUser(string i_LicenseNumber)
        {
            Console.WriteLine("Please enter the new status for the car:");
            string vehicleStatus = Console.ReadLine();

            eCarStatus chosenCarStatus;
            checkIfCarStatus(vehicleStatus, out chosenCarStatus);

            return chosenCarStatus;
        }

        private void checkIfCarStatus(string i_CarStatus, out eCarStatus o_UserInput)
        {
            if (!eCarStatus.TryParse(i_CarStatus, out o_UserInput))
            {
                throw new FormatException("should be a valid car status");
            }
        }

        /// 4
        public void InfalteTiresToMax()
        {
            string vehicleLicense = GetLicenseNumberFromUser();

            r_Garage.InfaltionToMax(vehicleLicense);
        }

        /// 5

        public void AddFuelToVehicle()
        {
            string vehicleLicense = GetLicenseNumberFromUser();

            Console.WriteLine("Please enter your fuel type:");
            string fuelType = Console.ReadLine();

            eFuelType vehicleFuleType;
            checkIfFuelType(fuelType, out vehicleFuleType);
            Console.WriteLine("Please enter how much fuel you want to add:");
            string fuelAmount = Console.ReadLine();

            float vehicleFuelAmount;
            checkIfNumber(fuelAmount, out vehicleFuelAmount);
            r_Garage.FuelingVehicle(vehicleLicense, vehicleFuleType, vehicleFuelAmount);
        }
        private void checkIfFuelType(string i_FuelType, out eFuelType o_UserInput)
        {
            if (!eFuelType.TryParse(i_FuelType, out o_UserInput))
            {
                throw new FormatException("should be a valid fuel type");
            }
        }


        /// 6
        
        public void ChargeYourVhicle()
        {
            string carLicense = GetLicenseNumberFromUser();

            Console.WriteLine("Please enter the time you want to charge in minutes:");
            string minuesAmount = Console.ReadLine();

            float minutesToCharge;
            checkIfNumber(minuesAmount, out minutesToCharge);
            float hoursToCharge = ConvertMinutesToHours(minutesToCharge);

            r_Garage.ChargeVehicle(carLicense, hoursToCharge);
        }
        public float ConvertMinutesToHours(float i_Minutes)
        {
            return i_Minutes / 60;
        }

        /// 7
        
        public void PresentAllVehicleDetails(string i_LicenseNumber)
        {
            Vehicle vehicle = r_Garage.Vehicles[i_LicenseNumber].Vehicle;

            Console.WriteLine("The vehicle you chose: {0}", vehicle.LicenseNumber);
            Console.WriteLine("Here are all the details about this vehicle:");
            Console.WriteLine(vehicle.ModelName);
            foreach (var detail in vehicle.Tires)
            {
                Console.WriteLine(detail);
            }

            Console.WriteLine(vehicle.Engine);
            Console.WriteLine(vehicle.FuelType);
            Console.WriteLine(vehicle.PrecentOfRemainingEnergy);
            foreach (var detail in vehicle.SpecieficDetailsForEachKind)
            {
                Console.WriteLine(detail);
            }

            Console.WriteLine(r_Garage.Vehicles[i_LicenseNumber].OwnerName);
            Console.WriteLine(r_Garage.Vehicles[i_LicenseNumber].OwnerPhoneNumber);
            Console.WriteLine(r_Garage.Vehicles[i_LicenseNumber].CarStatus);
        }
    }
}
