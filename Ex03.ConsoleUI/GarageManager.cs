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
            Console.WriteLine("Garage Menu:");
            Console.WriteLine("==============================================");
            Console.WriteLine("Choose your option:");

            Console.WriteLine(@"(1) Add new vehicle to the garage
(2) Present all the license numbers in the garage
(3) Change vehicle status
(4) Infalte your vehicle's tires
(5) Add gas to your vehicle - if you have a vehicle on fuel
(6) Charge your vehicle - if you have an electric vehicle
(7) Present full details about a vehicle");

        }

        public void MenuChoice(eMenuOptions i_UserInput)
        {
            switch (i_UserInput)
            {
                case eMenuOptions.AddNewVehicle:
                    GetVehicleAndAddToTheGarage();
                    break;
                case eMenuOptions.PresentAllLicense:
                    PresentLicenseNumbersInTheGarage();
                    break;



            }
        }
        private void printOptionalVehicleTypes()
        {
            Console.WriteLine("Choose a vehicle type option:");

            for (int i = 1; i < r_VehicleBuilder.VehicleOptionalTypes.Length +1; i++)
            {
                Console.WriteLine("({0}) {1}", i + 1, r_VehicleBuilder.VehicleOptionalTypes[i]);
            }
            Console.Write("Your choice is: ");

        }

        private void printEnumOptions<T>()
        {
            for (int i = 1; i < Enum.GetValues(typeof(T)).Length+1; i++)
            {
                Console.WriteLine("({0}) {1}", i, Enum.GetName(typeof(T), i));
            }
        }
        public void GetVehicleAndAddToTheGarage()
        {
            Console.Write("Please Enter your vehicle license: ");
            string carLicense = Console.ReadLine();
            if (r_Garage.CheckIfTheVehicleIsInGarage(carLicense))
            {
                Console.Write("Car license {0} is already exist in the garage!", carLicense);
                r_Garage.ChangeVehicleStatusToInRepair(carLicense);
            }
            else
            {
                getNewVehicle(carLicense);
            }

        }

        private void getNewVehicle(string i_CarLicense)
        {
            printOptionalVehicleTypes();
            string ownerVehicleType = Console.ReadLine();
            isValidChoice(ownerVehicleType, 1, r_VehicleBuilder.VehicleOptionalTypes.Length);
            VehicleBuilder.eVehicleType vehicleType = (VehicleBuilder.eVehicleType)int.Parse(ownerVehicleType);
            Vehicle newVehicle = r_VehicleBuilder.CreateVehicleObject(vehicleType, i_CarLicense);

            setPersonalInformation(newVehicle);
            setModel(newVehicle);
            setSpesicifVehicleData(newVehicle);
            setReaminEnergy(newVehicle);
            setTires(newVehicle);
        }
        private void setTires(Vehicle i_NewVehicle)
        {
            string manufacturerName = getManufacturerNameTires();

            Console.Write(@"Do you want to add tire air pressure of all the tires together?
(1) yes
(2) no
your choice: ");
            string userTirePressureChoice = Console.ReadLine();

            isValidChoice(userTirePressureChoice, 1, 2);

            setTiresPressure(i_NewVehicle, isGetAllTirePressureTogether(userTirePressureChoice), manufacturerName);
        }

        private string getManufacturerNameTires()
        {
            Console.Write("Please enter tire manufactor name: ");
            string tireManufacturerName = Console.ReadLine();
            return tireManufacturerName;
        }
        private bool isGetAllTirePressureTogether(string i_UserTirePressureChoice)
        {
            return (i_UserTirePressureChoice == "1");
        }
        private void setReaminEnergy(Vehicle i_NewVehicle)
        {
            Console.Write("Please enter the current energy situation in your car: ");
            string remainingEnergy = Console.ReadLine();
            float energyRemaining;
            checkIfNumber(remainingEnergy, out energyRemaining);
            i_NewVehicle.Engine.EnergyRemaining = energyRemaining;
        }

        private void setSpesicifVehicleData(Vehicle i_NewVehicle)
        {
            List<string> specificData = new List<string>();
            for (int i = 0; i < i_NewVehicle.SpecificData().Length; i++)
            {
                Console.Write("please enter {0}: ", i_NewVehicle.SpecificData()[i]);
                specificData.Add(Console.ReadLine());
            }
            i_NewVehicle.SpecieficDetailsForEachKind = specificData;
        }
        private void setPersonalInformation(Vehicle i_NewVehicle)
        {
            Console.Write("Please enter the owner name: ");
            string vehicleOwnerName = Console.ReadLine();

            Console.Write("Please enter the owner phone: ");
            string vehicleOwnerPhone = Console.ReadLine();
            r_Garage.AddVehicle(i_NewVehicle, vehicleOwnerName, vehicleOwnerPhone);
        }

        private void setModel(Vehicle i_NewVehicle)
        {
            Console.Write("Please enter the vehicle model: ");
            string vehicleModel = Console.ReadLine();
            r_Garage.Vehicles[i_NewVehicle.LicenseNumber].Vehicle.ModelName = vehicleModel;
        }

        private void setTiresPressure(Vehicle i_Vehicle,
                                                    bool i_IsGetAllTirePressureTogether, string i_ManufacturerName)
        {
            float tireAirPressuerNumber;

            Console.Write("Please enter tires air pressure: ");
            if (i_IsGetAllTirePressureTogether)
            {
                checkIfNumber(Console.ReadLine(), out tireAirPressuerNumber);

                for (int i = 0; i < i_Vehicle.NumberOfTires; i++)
                {
                    i_Vehicle.AddNewTireToTireList(i_ManufacturerName, tireAirPressuerNumber);
                }
            }
            else
            {
                for (int i = 1; i < i_Vehicle.NumberOfTires +1; i++)
                {
                    Console.Write("Please enter tire number {0} pressure: ", i);
                    checkIfNumber(Console.ReadLine(), out tireAirPressuerNumber);
                    i_Vehicle.AddNewTireToTireList(i_ManufacturerName, tireAirPressuerNumber);
                }
            }
            i_Vehicle.checkIfTireNumberMatchVehicle();
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

            if (toFilterBy == 1)
            {
                eVehicleStatus chosenStatus;
                chosenStatus = ChooseFilter();
                PresentLicenseNumbersInTheGarageFiltered(chosenStatus);
            }
            else
            {
                GarageMenu();
            }
        }
        public eVehicleStatus ChooseFilter()
        {
            Console.WriteLine(@"By which status you would like to see?
                              (1) fixed
                              (2) inRepair
                              (3) paid");
            string whichStatus = Console.ReadLine();

            isValidChoice(whichStatus, 1, 3);
            int whichStatusChoice = int.Parse(whichStatus);

            eVehicleStatus ChosenStatus;

            switch (whichStatusChoice)
            {
                case 1:
                    ChosenStatus = eVehicleStatus.Fixed;
                    break;
                case 2:
                    ChosenStatus = eVehicleStatus.InRepair;
                    break;
                case 3:
                    ChosenStatus = eVehicleStatus.Paid;
                    break;
                default:
                    throw new FormatException("Invalid status type");

            }

            return ChosenStatus;
        }
        private void PresentLicenseNumbersInTheGarageFiltered(eVehicleStatus i_CarStatus)
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

            eVehicleStatus vehicleStatus = GetVehicleStatusFromTheUser(vehicleLicense);

            r_Garage.ChangeVehicleStatus(vehicleLicense, vehicleStatus);
        }

        public string GetLicenseNumberFromUser()
        {
            Console.Write("Please Enter your vehicle license number: ");
            string vehicleLicense = Console.ReadLine();

            return vehicleLicense;
        }

        public eVehicleStatus GetVehicleStatusFromTheUser(string i_LicenseNumber)
        {
            Console.WriteLine("Please enter the new status for the car:");
            string vehicleStatus = Console.ReadLine();

            eVehicleStatus chosenCarStatus;
            checkIfCarStatus(vehicleStatus, out chosenCarStatus);

            return chosenCarStatus;
        }

        private void checkIfCarStatus(string i_CarStatus, out eVehicleStatus o_UserInput)
        {
            if (!eVehicleStatus.TryParse(i_CarStatus, out o_UserInput))
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