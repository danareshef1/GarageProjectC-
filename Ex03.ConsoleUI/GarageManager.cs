using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

        private void clearScreen()
        {
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
        }

        public void StartGarageWork()
        {
            bool stayInTheGarage = true;

            Console.WriteLine("Welcome to our garage!");
            while (stayInTheGarage)
            {
                PresentGarageMenu();
                eMenuOptions userInput = (eMenuOptions)int.Parse(Console.ReadLine());

                MenuChoice(userInput, ref stayInTheGarage);
            }
        }

        public void PresentGarageMenu()
        {
            clearScreen();
            Console.WriteLine("Garage Menu:");
            Console.WriteLine("==============================================");
            Console.WriteLine("Choose your option:");

            Console.WriteLine(@"(1) Add new vehicle to the garage
(2) Present all the license numbers in the garage
(3) Change vehicle status
(4) Infalte your vehicle's tires
(5) Add gas to your vehicle - if you have a vehicle on fuel
(6) Charge your vehicle - if you have an electric vehicle
(7) Present full details about a vehicle
(8) Exit");
            Console.Write("Your choice is: ");
        }

        public void MenuChoice(eMenuOptions i_UserInput, ref bool io_Exit)
        {
            try
            {
                Console.WriteLine();
                switch (i_UserInput)
                {
                    case eMenuOptions.AddNewVehicle:
                        clearScreen();
                        Console.WriteLine($"Add new vehicle to the garage:{Environment.NewLine}" +
                                          $"=============================== ");
                        GetVehicleAndAddToTheGarage();
                        break;
                    case eMenuOptions.PresentAllLicense:
                        clearScreen();
                        Console.WriteLine($"Present all vehicles in the garage:{Environment.NewLine}" +
                                          $"=============================== ");
                        PresentLicenseNumbersInTheGarage();
                        break;
                    case eMenuOptions.ChangeVehicleStatus:
                        clearScreen();
                        Console.WriteLine($"Change vehicle status:{Environment.NewLine}" +
                                          $"=============================== ");
                        ChangeVehicleStatus();
                        break;
                    case eMenuOptions.InfalteTires:
                        clearScreen();
                        Console.WriteLine($"Inflate tires to max:{Environment.NewLine}" +
                                          $"=============================== ");
                        InfalateTiresToMax();
                        break;
                    case eMenuOptions.AddGas:
                        clearScreen();
                        Console.WriteLine($"Add fuel:{Environment.NewLine}" +
                                          $"=============================== ");
                        AddFuelToVehicle();
                        break;
                    case eMenuOptions.ChargeBattery:
                        clearScreen();
                        Console.WriteLine($"Add battery:{Environment.NewLine}" +
                                          $"=============================== ");
                        ChargeYourVehicle();
                        break;
                    case eMenuOptions.PresentFullDetails:
                        clearScreen();
                        Console.WriteLine($"Present full vehicle details:{Environment.NewLine}" +
                                          $"=============================== ");
                        PresentAllVehicleDetails();
                        break;
                    case eMenuOptions.Exit:
                        io_Exit = false;
                        break;
                    default:
                        throw new ArgumentException("Your choice is not from the menu");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}. Please try again.", ex.Message);
            }
        }

        private void printEnumOptions<T>()
        {
           int enumLength = Enum.GetValues(typeof(T)).Length;

            for (int i = 0; i < enumLength; i++)
            {
                Console.WriteLine("({0}) {1}", i + 1, Enum.GetName(typeof(T), i + 1));
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
        
        public void GetVehicleAndAddToTheGarage()
        {
            string vehicleLicense = GetLicenseNumberFromUser();

            if (r_Garage.Vehicles.ContainsKey(vehicleLicense))
            {
                Console.WriteLine("Vehicle license {0} is already exist in the garage!", vehicleLicense);
                r_Garage.ChangeVehicleStatus(vehicleLicense, eVehicleStatus.InRepair);
            }
            else
            {
                getNewVehicle(vehicleLicense);
                clearScreen();
                Console.WriteLine("Vehicle with {0} license added to the garage successfully! ", vehicleLicense);
                Console.WriteLine();
            }
        }

        private void printOptionalVehicleTypes()
        {
            Console.WriteLine("Choose a vehicle type option:");
            for (int i = 0; i < r_VehicleBuilder.VehicleOptionalTypes.Length; i++)
            {
                Console.WriteLine("({0}) {1}", i + 1, r_VehicleBuilder.VehicleOptionalTypes[i]);
            }
            Console.Write("Your choice is: ");
        }

        private void getNewVehicle(string i_CarLicense)
        {
            bool notValid = true;

            printOptionalVehicleTypes();
            while (notValid)
            {
                try
                {
                    string ownerVehicleType = Console.ReadLine();

                    isValidChoice(ownerVehicleType, 1, r_VehicleBuilder.VehicleOptionalTypes.Length);
                    VehicleBuilder.eVehicleType vehicleType = (VehicleBuilder.eVehicleType)int.Parse(ownerVehicleType);
                    Vehicle newVehicle = r_VehicleBuilder.CreateVehicleObject(vehicleType, i_CarLicense);

                    setPersonalInformation(newVehicle);
                    setModel(newVehicle);
                    setSpecificVehicleData(newVehicle);
                    setRemainEnergy(newVehicle);
                    setTires(newVehicle);
                    notValid = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}. Please try again.", ex.Message);
                }
            }
        }

        private void setTires(Vehicle i_NewVehicle)
        {
            bool notValid = true;
            string manufacturerName = getManufacturerNameTires();

            while (notValid)
            {
                try
                {
                    Console.Write(@"Do you want to add tire air pressure of all the tires together?
(1) yes
(2) no
your choice: ");
                    string userTirePressureChoice = Console.ReadLine();

                    isValidChoice(userTirePressureChoice, 1, 2);
                    setTiresPressure(i_NewVehicle, isGetAllTirePressureTogether(userTirePressureChoice), manufacturerName);
                    notValid = false;
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}. Please try again.", ex.Message);
                    Console.WriteLine();
                }
            }
        }

        private string getManufacturerNameTires()
        {
            Console.Write("Please enter tire manufacturer name: ");
            string tireManufacturerName = Console.ReadLine();

            return tireManufacturerName;
        }

        private bool isGetAllTirePressureTogether(string i_UserTirePressureChoice)
        {
            return (i_UserTirePressureChoice == "1");
        }

        private void setRemainEnergy(Vehicle i_NewVehicle)
        {
            bool notValid = true;

            while (notValid)
            {
                try
                {
                    Console.Write("Please enter the current energy situation in your car: ");
                    string remainingEnergy = Console.ReadLine();
                    float energyRemaining;

                    checkIfNumber(remainingEnergy, out energyRemaining);
                    i_NewVehicle.Engine.EnergyRemaining = energyRemaining;
                    i_NewVehicle.CalculatePrecentRemainingEnergy();
                    notValid = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}. Please try again.", ex.Message);
                }
            }
        }

        private void setSpecificVehicleData(Vehicle i_NewVehicle)
        {
            bool notValid = true;

            while (notValid)
            {
                try
                {
                    List<string> specificData = new List<string>();

                    for (int i = 0; i < i_NewVehicle.SpecificData().Length; i++)
                    {
                        Console.Write("please enter {0}: ", i_NewVehicle.SpecificData()[i]);
                        specificData.Add(Console.ReadLine());
                    }

                    i_NewVehicle.SpecieficDetailsForVehicle = specificData;
                    notValid = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}. Please try again.", ex.Message);
                }
            }
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
            bool notValid = true;

            while (notValid)
            {
                try
                {
                    float tireAirPressuerNumber;

                    Console.Write("Please enter tires air pressure: ");
                    if (i_IsGetAllTirePressureTogether)
                    {
                        checkIfNumber(Console.ReadLine(), out tireAirPressuerNumber);
                        setAllTiresTogether(i_Vehicle, i_ManufacturerName, tireAirPressuerNumber);
                    }
                    else
                    {
                        setTiresSeparate(i_Vehicle, i_ManufacturerName);
                    }
                    i_Vehicle.CheckIfTireNumberMatchVehicle();
                    notValid = false;
                }
                catch (Exception ex)
                {
                    i_Vehicle.Tires.Clear();
                    Console.WriteLine("Error: {0}. Please try again.", ex.Message);
                }
            }
        }

        private void setAllTiresTogether(Vehicle i_Vehicle, string i_ManufacturerName,
                                        float tireAirPressuerNumber)
        {
            for (int i = 0; i < i_Vehicle.NumberOfTires; i++)
            {
                i_Vehicle.AddNewTireToTireList(i_ManufacturerName, tireAirPressuerNumber);
            }
        }

        private void setTiresSeparate(Vehicle i_Vehicle, string i_ManufacturerName)
        {
            float tireAirPressuerNumber;

            for (int i = 0; i < i_Vehicle.NumberOfTires; i++)
            {
                Console.Write("Please enter tire number {0} pressure: ", i + 1);
                checkIfNumber(Console.ReadLine(), out tireAirPressuerNumber);
                i_Vehicle.AddNewTireToTireList(i_ManufacturerName, tireAirPressuerNumber);
            }
        }

        public void PresentLicenseNumbersInTheGarage()
        {
            bool notValid;

            checkIfGarageIsEmpty(out notValid);
            while (notValid)
            {
                presentAllVehiclesInTheGarage();
                try
                {
                    string filterByChoice = checkIfWantToFilter();
                    if (int.Parse(filterByChoice) == 1)
                    {
                        eVehicleStatus chosenStatus;
                        chosenStatus = ChooseFilter();
                        PresentLicenseNumbersInTheGarageFiltered(chosenStatus);
                    }
                    notValid = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}. Please try again.", ex.Message);
                    Console.WriteLine();
                }
            }
        }

        private string checkIfWantToFilter()
        {
            Console.WriteLine(@"Do you want to filter them by status?
(1) yes
(2) no");
            Console.WriteLine("Your choice: ");
            string filterByChoice = Console.ReadLine();

            Console.WriteLine();

            isValidChoice(filterByChoice, 1, 2);

            return filterByChoice;
        }

        private void checkIfGarageIsEmpty(out bool o_NotValid)
        {
            bool continueCheck = true;

            o_NotValid = true;
            while (continueCheck)
            {
                try
                {
                    if (r_Garage.Vehicles.Count == 0)
                    {
                        o_NotValid = false;
                        continueCheck = false;
                        throw new ArgumentException("There are no vehicles in the garage");
                    }
                    continueCheck = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}. Please try again.", ex.Message);
                    Console.WriteLine();
                }
            }
        }

        private void presentAllVehiclesInTheGarage()
        {
            Console.WriteLine("Here are all the cars we have in the garage right now: ");
            printVehicles(r_Garage.Vehicles);
        }

        public eVehicleStatus ChooseFilter()
        {
            Console.WriteLine("By which status you would like to filter by? ");

            return getVehicleStatus();
        }

        private void PresentLicenseNumbersInTheGarageFiltered(eVehicleStatus i_CarStatus)
        {
            Dictionary<string, VehicleDataInGarage> newListSortedByStatus = 
                                        r_Garage.GetLicenseNumbersByStatus(i_CarStatus);
            if (newListSortedByStatus.Count == 0)
            {
                Console.WriteLine("There are no vehicles in the garage in {0} status", i_CarStatus);
            }
            else
            {
                Console.WriteLine("Here are all the vehicles we have in the garage right now in {0} status:", i_CarStatus);
                printVehicles(newListSortedByStatus);
            }
        }

        private void printVehicles(Dictionary<string, VehicleDataInGarage> i_Vehicles)
        {
            int index = 1;

            foreach (var vehicle in i_Vehicles)
            {
                Console.WriteLine("{0}. {1} ", index, vehicle.Key);
                index++;
            }
            Console.WriteLine(" ");
        }

        private eVehicleStatus getVehicleStatus()
        {
            printEnumOptions<eVehicleStatus>();
            string whichStatus = Console.ReadLine();

            isValidChoice(whichStatus, 1, Enum.GetValues(typeof(eVehicleStatus)).Length);

            return (eVehicleStatus)int.Parse(whichStatus);
        }

        public void ChangeVehicleStatus()
        {
            bool notValid;

            checkIfGarageIsEmpty(out notValid);
            if (notValid)
            {
                string vehicleLicense = GetLicenseNumberFromUser();

                checkIfVehicleIsInTheGarage(ref vehicleLicense);
                while (notValid)
                {
                    try
                    {
                        Console.WriteLine("Vehicle with {0} license - current vehicle status is {1}",
                                           vehicleLicense, r_Garage.Vehicles[vehicleLicense].VehicleStatus);
                        eVehicleStatus vehicleStatus = GetVehicleStatusFromTheUserForChange();

                        r_Garage.ChangeVehicleStatus(vehicleLicense, vehicleStatus);
                        clearScreen();
                        Console.WriteLine("Vehicle with {0} license - vehicle status changed to {1} successfully",
                                          vehicleLicense, vehicleStatus);
                        Console.WriteLine();
                        notValid = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: {0}. Please try again.", ex.Message);
                        Console.WriteLine();
                    }
                }
            }
        }

        private void checkIfVehicleIsInTheGarage(ref string io_VehicleLicense)
        {
            bool isVehicleInTheGarage = false;

            while (!isVehicleInTheGarage)
            {
                try
                {
                    r_Garage.CheckIfTheVehicleIsInGarage(io_VehicleLicense);
                    isVehicleInTheGarage = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}. Please try again.", ex.Message);
                    Console.WriteLine();
                    io_VehicleLicense = GetLicenseNumberFromUser();
                }
            }
        }

        public string GetLicenseNumberFromUser()
        {
            Console.Write("Please Enter your vehicle license number: ");
            string vehicleLicense = Console.ReadLine();

            return vehicleLicense;
        }

        public eVehicleStatus GetVehicleStatusFromTheUserForChange()
        {
            Console.Write("Please enter the new status for the car: ");

            return getVehicleStatus();
        }

        public void InfalateTiresToMax()
        {
            bool notValid;

            checkIfGarageIsEmpty(out notValid);
            if (notValid)
            {
                string vehicleLicense = GetLicenseNumberFromUser();

                checkIfVehicleIsInTheGarage(ref vehicleLicense);
                while (notValid)
                {
                    try
                    {
                        r_Garage.InfaltionToMax(vehicleLicense);
                        clearScreen();
                        Console.WriteLine("Vehicle with {0} license - the tires were successfully inflated",vehicleLicense);
                        Console.WriteLine();
                        notValid = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: {0}. Please try again.", ex.Message);
                        Console.WriteLine();
                    }
                }
            }
        }

        public void AddFuelToVehicle()
        {
            bool notValid;

            checkIfGarageIsEmpty(out notValid);
            if (notValid)
            {
                string vehicleLicense = GetLicenseNumberFromUser();

                checkIfVehicleIsInTheGarage(ref vehicleLicense);
                try
                {
                    checkIfEngineMatchTheType<FuelEngine>(vehicleLicense, notValid);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                    notValid = false;
                }
                while (notValid)
                {
                    try
                    {
                        string fuelType;
                        float vehicleFuelAmount = userInputForFueling(out fuelType, vehicleLicense);
                        r_Garage.FuelingVehicle(vehicleLicense, (eFuelType)int.Parse(fuelType), vehicleFuelAmount);
                        clearScreen();
                        Console.WriteLine("Vehicle with {0} license - added {1} fuel successfully", vehicleLicense, vehicleFuelAmount);
                        Console.WriteLine("Your current amount of fuel is {0}",
                                           r_Garage.Vehicles[vehicleLicense].Vehicle.Engine.EnergyRemaining);
                        Console.WriteLine();
                        notValid = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: {0}. Please try again.", ex.Message);
                        Console.WriteLine();
                    }
                }
            }
        }

        private float userInputForFueling(out string o_FuelType, string i_VehicleLicense)
        {
            Console.WriteLine(@"Please choose your fuel type: 
Pay attention that you fuel type is {0}", r_Garage.Vehicles[i_VehicleLicense].Vehicle.FuelType);
            printEnumOptions<eFuelType>();
            o_FuelType = Console.ReadLine();
            isValidChoice(o_FuelType, 1, Enum.GetValues(typeof(eFuelType)).Length - 1);
            Console.Write("Please enter how much fuel you want to add:");
            string fuelAmount = Console.ReadLine();
            float vehicleFuelAmount;

            checkIfNumber(fuelAmount, out vehicleFuelAmount);

            return vehicleFuelAmount;
        }

        
        public void ChargeYourVehicle()
        {
            bool notValid;

            checkIfGarageIsEmpty(out notValid);
            if (notValid)
            {
                string vehicleLicense = GetLicenseNumberFromUser();

                checkIfVehicleIsInTheGarage(ref vehicleLicense);
                try
                {
                    checkIfEngineMatchTheType<ElectricEngine>(vehicleLicense, notValid);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                    notValid = false;
                }
                while (notValid)
                {
                    try
                    {
                        float hoursToCharge = userInputForCharging();

                        r_Garage.ChargeVehicle(vehicleLicense, hoursToCharge);
                        clearScreen();
                        Console.WriteLine("Vehicle with {0} license - added {1} battery hours successfully", vehicleLicense, hoursToCharge);
                        Console.WriteLine("Your current amount of battery is {0}",
                                           r_Garage.Vehicles[vehicleLicense].Vehicle.Engine.EnergyRemaining);
                        Console.WriteLine();
                        notValid = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: {0}. Please try again.", ex.Message);
                        Console.WriteLine();
                    }
                }
            }
        }

        private float userInputForCharging()
        {
            Console.Write("Please enter the time you want to charge in minutes:");
            string minuesAmount = Console.ReadLine();
            float minutesToCharge;

            checkIfNumber(minuesAmount, out minutesToCharge);

            return ConvertMinutesToHours(minutesToCharge);
        }
        private void checkIfEngineMatchTheType<T>(string i_VehicleLicense, bool i_NotValid)
        {
            if (i_NotValid)
            {
                if (!(r_Garage.Vehicles[i_VehicleLicense].Vehicle.Engine is T))
                {
                    throw new ArgumentException("You cant do this action with your engine type");
                }
            }
        }

        public float ConvertMinutesToHours(float i_Minutes)
        {
            return i_Minutes / 60f;
        }

        public void PresentAllVehicleDetails()
        {
            bool notValid;

            checkIfGarageIsEmpty(out notValid);

            if (notValid)
            {
                try
                {
                    string vehicleLicense = GetLicenseNumberFromUser();

                    checkIfVehicleIsInTheGarage(ref vehicleLicense);
                    Vehicle vehicle = r_Garage.Vehicles[vehicleLicense].Vehicle;

                    Console.WriteLine("The vehicle you chose: {0}", vehicle.LicenseNumber);
                    Console.WriteLine("Here are all the details about this vehicle:");
                    Console.WriteLine("Owner name: {0}", r_Garage.Vehicles[vehicleLicense].OwnerName);
                    Console.WriteLine("Owner phone number: {0}", r_Garage.Vehicles[vehicleLicense].OwnerPhoneNumber);
                    Console.WriteLine("Vehicle status: {0}", r_Garage.Vehicles[vehicleLicense].VehicleStatus);
                    Console.WriteLine("Model name: {0}", vehicle.ModelName);
                    Console.WriteLine("Tires manufacture name: {0}", vehicle.Tires[0].ManufacturerName);
                    Console.WriteLine("Tire pressure for each tire");
                    foreach (var detail in vehicle.Tires)
                    {
                        Console.WriteLine(detail.TirePressure);
                    }

                    Console.WriteLine("Fuel type: {0}", vehicle.FuelType);
                    Console.WriteLine("Precent Of Remaining Energy: {0}%", vehicle.PrecentOfRemainingEnergy);
                    Console.WriteLine("Speciefic details for the vehicle");
                    foreach (var detail in vehicle.SpecieficDetailsForVehicle)
                    {
                        Console.WriteLine(detail);
                    }

                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }
            }
        }
    }
}
