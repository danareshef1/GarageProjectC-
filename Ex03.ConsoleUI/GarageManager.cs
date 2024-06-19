using Ex03.GarageLogic;
using System;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    public class GarageManager
    {
        private const int k_MessagesSleepTime = 3200;
        private const int k_GettingSleepTime = 800;
        private readonly Garage r_Garage = new Garage();
        private readonly VehicleBuilder r_VehicleBuilder = new VehicleBuilder();

        private void clearScreen(int i_SleepTime)
        {
            System.Threading.Thread.Sleep(i_SleepTime);
            Console.Clear();
        }

        public void StartGarageWork()
        {
            bool stayInTheGarage = true;
            int userChoice;
            bool isValid = false;

            Console.WriteLine("Welcome to our garage!");
            while (stayInTheGarage)
            {
                isValid = false;
                while (!isValid)
                {
                    try
                    {
                        clearScreen(k_MessagesSleepTime);
                        presentGarageMenu();
                        if (!int.TryParse(Console.ReadLine(), out userChoice))
                        {
                            throw new FormatException("Your choice is not from the menu");
                        }

                        menuChoice((eMenuOptions)userChoice, ref stayInTheGarage);
                        isValid = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: {0}. Please try again.", ex.Message);
                    }
                }
            }
        }

        private void presentGarageMenu()
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
(7) Present full details about a vehicle
(8) Exit");
            Console.Write("Your choice is: ");
        }

        private void menuChoice(eMenuOptions i_UserInput, ref bool io_Exit)
        {
            Console.WriteLine();
            switch (i_UserInput)
            {
                case eMenuOptions.AddNewVehicle:
                    clearScreen(k_GettingSleepTime);
                    Console.WriteLine($"Add new vehicle to the garage:{Environment.NewLine}" +
                                      $"=============================== ");
                    getVehicleAndAddToTheGarage();
                    break;
                case eMenuOptions.PresentAllLicense:
                    clearScreen(k_GettingSleepTime);
                    Console.WriteLine($"Present all vehicles in the garage:{Environment.NewLine}" +
                                      $"=============================== ");
                    presentLicenseNumbersInTheGarage();
                    break;
                case eMenuOptions.ChangeVehicleStatus:
                    clearScreen(k_GettingSleepTime);
                    Console.WriteLine($"Change vehicle status:{Environment.NewLine}" +
                                      $"=============================== ");
                    changeVehicleStatus();
                    break;
                case eMenuOptions.InfalteTires:
                    clearScreen(k_GettingSleepTime);
                    Console.WriteLine($"Inflate tires to max:{Environment.NewLine}" +
                                      $"=============================== ");
                    infalateTiresToMax();
                    break;
                case eMenuOptions.AddGas:
                    clearScreen(k_GettingSleepTime);
                    Console.WriteLine($"Add fuel:{Environment.NewLine}" +
                                      $"=============================== ");
                    addFuelToVehicle();
                    break;
                case eMenuOptions.ChargeBattery:
                    clearScreen(k_GettingSleepTime);
                    Console.WriteLine($"Add battery:{Environment.NewLine}" +
                                      $"=============================== ");
                    chargeYourVehicle();
                    break;
                case eMenuOptions.PresentFullDetails:
                    clearScreen(k_GettingSleepTime);
                    Console.WriteLine($"Present full vehicle details:{Environment.NewLine}" +
                                      $"=============================== ");
                    presentAllVehicleDetails();
                    break;
                case eMenuOptions.Exit:
                    io_Exit = false;
                    break;
                default:
                    throw new ArgumentException("Your choice is not from the menu");
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

        private void getVehicleAndAddToTheGarage()
        {
            string vehicleLicense = getLicenseNumberFromUser();

            if (r_Garage.Vehicles.ContainsKey(vehicleLicense))
            {
                Console.WriteLine("Vehicle license {0} is already exist in the garage!", vehicleLicense);
                r_Garage.ChangeVehicleStatus(vehicleLicense, eVehicleStatus.InRepair);
            }
            else
            {
                getNewVehicle(vehicleLicense);
                Console.WriteLine();
                Console.WriteLine("Vehicle with {0} license added to the garage successfully! ", vehicleLicense);
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
                    if (i_IsGetAllTirePressureTogether)
                    {
                        setAllTiresTogether(i_Vehicle, i_ManufacturerName);
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

        private void setAllTiresTogether(Vehicle i_Vehicle, string i_ManufacturerName)
        {
            float tireAirPressuerNumber;

            Console.Write("Please enter tires air pressure: ");
            checkIfNumber(Console.ReadLine(), out tireAirPressuerNumber);
            for (int i = 0; i < i_Vehicle.NumberOfTires; i++)
            {
                i_Vehicle.AddNewTireToTireList(i_ManufacturerName, tireAirPressuerNumber);
            }
        }

        private void setTiresSeparate(Vehicle i_Vehicle, string i_ManufacturerName)
        {
            float tireAirPressuerNumber;

            Console.WriteLine("Please enter tires air pressure: ");
            for (int i = 0; i < i_Vehicle.NumberOfTires; i++)
            {
                Console.Write("Please enter tire number {0} pressure: ", i + 1);
                checkIfNumber(Console.ReadLine(), out tireAirPressuerNumber);
                i_Vehicle.AddNewTireToTireList(i_ManufacturerName, tireAirPressuerNumber);
            }
        }

        private void presentLicenseNumbersInTheGarage()
        {
            bool notValid;
            string checkIfFilter;

            checkIfGarageIsEmpty(out notValid);
            presentAllVehiclesInTheGarage();
            while (notValid)
            {
                try
                {
                    checkIfWantToFilter(out checkIfFilter);
                    if (int.Parse(checkIfFilter) == 1)
                    {
                        eVehicleStatus chosenStatus;
                        chosenStatus = chooseFilter();
                        Console.WriteLine();
                        presentLicenseNumbersInTheGarageFiltered(chosenStatus);
                    }

                    notValid = false;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}. Please try again.", ex.Message);
                    Console.WriteLine();
                }
            }
        }

        private void checkIfWantToFilter(out string o_CheckIfFilter)
        {
            Console.WriteLine(@"Do you want to filter them by status?
(1) yes
(2) no");
            Console.Write("Your choice: ");
            o_CheckIfFilter = Console.ReadLine();
            Console.WriteLine();
            isValidChoice(o_CheckIfFilter, 1, 2);
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

        private eVehicleStatus chooseFilter()
        {
            Console.WriteLine("By which status you would like to filter by? ");

            return getVehicleStatus();
        }

        private void presentLicenseNumbersInTheGarageFiltered(eVehicleStatus i_CarStatus)
        {
            Dictionary<string, VehicleDataInGarage> newListSortedByStatus =
                                        r_Garage.GetLicenseNumbersByStatus(i_CarStatus);

            if (newListSortedByStatus.Count == 0)
            {
                throw new ArgumentException("There are no vehicles in the garage in this status," +
                                            "let's try again...");
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
            Console.Write("Your choice: ");
            string whichStatus = Console.ReadLine();

            isValidChoice(whichStatus, 1, Enum.GetValues(typeof(eVehicleStatus)).Length);

            return (eVehicleStatus)int.Parse(whichStatus);
        }

        private void changeVehicleStatus()
        {
            bool notValid;

            checkIfGarageIsEmpty(out notValid);
            if (notValid)
            {
                string vehicleLicense = getLicenseNumberFromUser();

                checkIfVehicleIsInTheGarage(ref vehicleLicense);
                while (notValid)
                {
                    try
                    {
                        Console.WriteLine("Vehicle with {0} license - current vehicle status is {1}",
                                           vehicleLicense, r_Garage.Vehicles[vehicleLicense].VehicleStatus);
                        eVehicleStatus vehicleStatus = getVehicleStatusFromTheUserForChange();

                        r_Garage.ChangeVehicleStatus(vehicleLicense, vehicleStatus);
                        Console.WriteLine();
                        Console.WriteLine("Vehicle with {0} license - vehicle status changed to {1} successfully",
                                          vehicleLicense, vehicleStatus);
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
                    io_VehicleLicense = getLicenseNumberFromUser();
                }
            }
        }

        private string getLicenseNumberFromUser()
        {
            Console.Write("Please Enter your vehicle license number: ");
            string vehicleLicense = Console.ReadLine();

            return vehicleLicense;
        }

        private eVehicleStatus getVehicleStatusFromTheUserForChange()
        {
            Console.WriteLine("Please enter the new status for the car: ");

            return getVehicleStatus();
        }

        private void infalateTiresToMax()
        {
            bool notValid;

            checkIfGarageIsEmpty(out notValid);
            if (notValid)
            {
                string vehicleLicense = getLicenseNumberFromUser();

                checkIfVehicleIsInTheGarage(ref vehicleLicense);
                while (notValid)
                {
                    try
                    {
                        r_Garage.InfaltionToMax(vehicleLicense);
                        Console.WriteLine();
                        Console.WriteLine("Vehicle with {0} license - the tires were successfully inflated", vehicleLicense);
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

        private void addFuelToVehicle()
        {
            bool notValid;

            checkIfGarageIsEmpty(out notValid);
            if (notValid)
            {
                string vehicleLicense = getLicenseNumberFromUser();

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
                        r_Garage.Vehicles[vehicleLicense].Vehicle.CalculatePrecentRemainingEnergy();
                        Console.WriteLine();
                        Console.WriteLine("Vehicle with {0} license - added {1} fuel successfully", vehicleLicense, vehicleFuelAmount);
                        Console.WriteLine("Your current amount of fuel is {0}",
                                           r_Garage.Vehicles[vehicleLicense].Vehicle.Engine.EnergyRemaining);
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
            isValidChoice(o_FuelType, 1, Enum.GetValues(typeof(eFuelType)).Length);
            Console.Write("Please enter how much fuel you want to add: ");
            string fuelAmount = Console.ReadLine();
            float vehicleFuelAmount;

            checkIfNumber(fuelAmount, out vehicleFuelAmount);

            return vehicleFuelAmount;
        }

        private void chargeYourVehicle()
        {
            bool notValid;

            checkIfGarageIsEmpty(out notValid);
            if (notValid)
            {
                string vehicleLicense = getLicenseNumberFromUser();

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
                        float minutesToCharge;
                        float hoursToCharge = userInputForCharging(out minutesToCharge);

                        r_Garage.ChargeVehicle(vehicleLicense, hoursToCharge);
                        r_Garage.Vehicles[vehicleLicense].Vehicle.CalculatePrecentRemainingEnergy();
                        Console.WriteLine();
                        Console.WriteLine("Vehicle with {0} license - added {1} battery minutes successfully.",
                                           vehicleLicense, minutesToCharge);
                        Console.WriteLine("Your current amount of battery is {0} in hours.",
                                           r_Garage.Vehicles[vehicleLicense].Vehicle.Engine.EnergyRemaining);
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

        private float userInputForCharging(out float o_MinutesToCharge)
        {
            Console.Write("Please enter the time you want to charge in minutes: ");
            string minuesAmount = Console.ReadLine();
            float minutesToCharge;

            checkIfNumber(minuesAmount, out minutesToCharge);
            o_MinutesToCharge = minutesToCharge;

            return convertMinutesToHours(minutesToCharge);
        }

        private void checkIfEngineMatchTheType<T>(string i_VehicleLicense, bool i_NotValid)
        {
            if (i_NotValid)
            {
                if (!(r_Garage.Vehicles[i_VehicleLicense].Vehicle.Engine is T))
                {
                    throw new ArgumentException("You cant do this action with your engine type.");
                }
            }
        }

        private float convertMinutesToHours(float i_Minutes)
        {
            return i_Minutes / 60f;
        }

        private void presentAllVehicleDetails()
        {
            bool notValid;

            checkIfGarageIsEmpty(out notValid);
            if (notValid)
            {
                string vehicleLicense = getLicenseNumberFromUser();

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
                foreach (var tire in vehicle.Tires)
                {
                    Console.Write(tire.TirePressure + " ");
                }

                Console.WriteLine();
                Console.WriteLine("Fuel type: {0}", vehicle.FuelType);
                Console.WriteLine("Precent Of Remaining Energy: {0}%", vehicle.PrecentOfRemainingEnergy);
                Console.WriteLine("Speciefic details for the vehicle");
                int index = 0;

                foreach (var detail in vehicle.SpecieficDetailsForVehicle)
                {
                    Console.Write("{0}: ", vehicle.SpecificData()[index]);
                    Console.WriteLine(detail);
                    index++;
                }
            }
        }
    }
}