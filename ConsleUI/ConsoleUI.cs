﻿using System;
using System.Collections.Generic;
using ConsoleUI;
using Ex03_Or_315900845_Or_314919994;


namespace ConsleUI
{
    class Program
    {
        public static void Main()
        {
            Garage garage = new Garage();
            while (true)
            {
                Console.WriteLine($"{Environment.NewLine}========Or's Garage========{Environment.NewLine}"
                                  + $"Welcome to our Garage!{Environment.NewLine}"
                                  + $"Please choose an option:{Environment.NewLine}1. Insert a car "
                                  + $"{Environment.NewLine}2. Print license numbers{Environment.NewLine}3. Change vehicle status"
                                  + $"{Environment.NewLine}4. Inflate wheels{Environment.NewLine}5. Refuel"
                                  + $"{Environment.NewLine}6. Recharge"
                                  + $"{Environment.NewLine}7. Print vehicle details" + $"{Environment.NewLine}8. Exit"
                                  + $"{Environment.NewLine}==========================={Environment.NewLine}");

                string customerChoice = Console.ReadLine();
                customerChoice = getValidChoice(customerChoice);

                if (customerChoice == "8")
                {
                    break;
                }

                handleCustomerChoice(customerChoice, garage);
            }
        }

        private static bool validateNumber(string i_ChosenNumber)
        {
            bool isValidated = false;
            if (int.TryParse(i_ChosenNumber, out int result))
            {
                if (result < 9 && result > 0)
                {
                    isValidated = true;
                }
            }

            return isValidated;
        }

        private static string getValidChoice(string i_CustomerChoice)
        {
            while (!validateNumber(i_CustomerChoice))
            {
                Console.WriteLine("Invalid choice, please try again.");
                i_CustomerChoice = Console.ReadLine();
            }

            return i_CustomerChoice;
        }

        private static void handleCustomerChoice(string i_CustomerChoice, Garage i_Garage)
        {
            switch (stringToChoice(i_CustomerChoice))
            {
                case eChoice.Insert:
                    handleInsertion(i_Garage);
                    break;
                case eChoice.PrintLicenses:
                    handlePrintLicenses(i_Garage);
                    break;
                case eChoice.ChangeStatus:
                    handleChangeStatus(i_Garage);
                    break;
                case eChoice.Inflate:
                    handleInflate(i_Garage);
                    break;
                case eChoice.Refuel:
                    handleRefuel(i_Garage);
                    break;
                case eChoice.Recharge:
                    handleRecharge(i_Garage);
                    break;
                case eChoice.PrintDetails:
                    handlePrintDetails(i_Garage);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private static string getValidatedLicenseNumber()
        {
            Console.WriteLine("Enter license number:");
            string licenseNumber = Console.ReadLine();

            while (!validateLicenseNumber(licenseNumber))
            {
                Console.WriteLine("Invalid license number, please try again:");
                licenseNumber = Console.ReadLine();
            }

            return licenseNumber;
        }

        private static void handleInsertion(Garage i_Garage)
        {
            string licenseNumber = getValidatedLicenseNumber();

            while (true)
            {
                try
                {
                    if (i_Garage.FindVehicleInGarage(licenseNumber))
                    {
                        Console.WriteLine("Found the vehicle in the garage, changing its status.");
                        i_Garage.UpdateVehicleStatus(licenseNumber, eVehicleStatus.InRepair);
                    }
                    else
                    {
                        eVehicleType vehicleType = getVehicleType();

                        Vehicle vehicle = handleVehicleType(vehicleType, licenseNumber);
                        string ownerPhoneNumber = getOwnerPhoneNumber();
                        string ownerName = getOwnerName();
                        VehicleInGarage vehicleInGarage = new VehicleInGarage(vehicle, ownerName, ownerPhoneNumber);

                        i_Garage.AddVehicle(vehicleInGarage);
                        Console.WriteLine("The vehicle has been successfully added to the garage.");
                    }

                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
        }

        private static string getOwnerName()
        {
            while (true)
            {
                Console.WriteLine("Please enter the owner's name:");
                string ownerName = Console.ReadLine();

                if (isValidOwnerName(ownerName))
                {
                    return ownerName;
                }
                else
                {
                    Console.WriteLine("Invalid name. Please enter a name containing only letters and spaces.");
                }
            }
        }

        private static bool isValidOwnerName(string i_OwnerName)
        {
            if (string.IsNullOrWhiteSpace(i_OwnerName))
            {
                return false;
            }

            foreach (char c in i_OwnerName)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    return false;
                }
            }

            return true;
        }

        private static string getOwnerPhoneNumber()
        {
            while (true)
            {
                Console.WriteLine("Please enter the owner's phone number (10 digits):");
                string phoneNumber = Console.ReadLine();

                if (isValidPhoneNumber(phoneNumber))
                {
                    return phoneNumber;
                }
                else
                {
                    Console.WriteLine("Invalid phone number. Please enter exactly 10 digits.");
                }
            }
        }

        private static bool isValidPhoneNumber(string i_PhoneNumber)
        {
            return i_PhoneNumber.Length == 10 && long.TryParse(i_PhoneNumber, out _);
        }

        private static void handlePrintLicenses(Garage i_Garage)
        {
            eVehicleStatus status = GetVehicleStatus();
            List<string> vehiclesLicenseNumbers = i_Garage.FindVehiclesByStatus(status);
            Console.WriteLine($"Printing license numbers by the status {status}:");
            if (vehiclesLicenseNumbers.Count > 0)
            {
                foreach (string licenseNumber in vehiclesLicenseNumbers)
                {
                    Console.WriteLine(licenseNumber);
                }
            }
            else
            {
                Console.WriteLine($"There are no vehicles with status {status} in the garage. :(");
            }
        }

        private static void handleChangeStatus(Garage i_Garage)
        {
            string licenseNumber = getValidatedLicenseNumber();

            if (!i_Garage.FindVehicleInGarage(licenseNumber))
            {
                Console.WriteLine($"No vehicle with the license number '{licenseNumber}' was found in the garage." +
                                  " Please double-check the license number.");
            }
            else
            {
                try
                {
                    eVehicleStatus status = GetVehicleStatus();
                    i_Garage.UpdateVehicleStatus(licenseNumber, status);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void handleInflate(Garage i_Garage)
        {
            string licenseNumber = getValidatedLicenseNumber();

            if (!i_Garage.FindVehicleInGarage(licenseNumber))
            {
                Console.WriteLine($"No vehicle with the license number '{licenseNumber}' was found in the garage." +
                    " Please double-check the license number.");
            }
            else
            {
                bool isInflated = i_Garage.InflateVehicleWheels(licenseNumber);

                if (isInflated)
                {
                    Console.WriteLine("Success! " +
                        $"All wheels of the vehicle with license number '{licenseNumber}' have been" +
                        " inflated to their maximum pressure.");
                }
                else
                {
                    Console.WriteLine($"Failed to inflate wheels for the vehicle with license number '{licenseNumber}'.");
                }
            }
        }

        private static void handleRefuel(Garage i_Garage)
        {
            string licenseNumber = getValidatedLicenseNumber();

            if (!i_Garage.FindVehicleInGarage(licenseNumber))
            {
                Console.WriteLine($"No vehicle with the license number '{licenseNumber}' was found in the garage." +
                                  " Please double-check the license number.");
            }
            else
            {
                Vehicle vehicle = i_Garage.GetVehicleFromGarage(licenseNumber);

                if (vehicle is FuelVehicle fuelVehicle)
                {
                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("Please enter fuel amount (liters) you want to refuel:");
                            float fuelAmount = float.Parse(Console.ReadLine());
                            eFuelType fuelType = getFuelType();
                            fuelVehicle.Refuel(fuelAmount, fuelType);
                            Console.WriteLine("Vehicle refueled successfully!");
                            break;
                        }
                        catch (ValueOutOfRangeException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"The vehicle with license number '{licenseNumber}' does not support refueling.");
                }
            }
        }

        private static void handleRecharge(Garage i_Garage)
        {
            string licenseNumber = getValidatedLicenseNumber();

            if (!i_Garage.FindVehicleInGarage(licenseNumber))
            {
                Console.WriteLine($"No vehicle with the license number '{licenseNumber}' was found in the garage." +
                                  " Please double-check the license number.");
            }
            else
            {
                Vehicle vehicle = i_Garage.GetVehicleFromGarage(licenseNumber);

                if (vehicle is ElectricVehicle electricVehicle)
                {
                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("Please enter minutes you want to charge:");
                            float minutesToCharge = float.Parse(Console.ReadLine());
                            electricVehicle.Recharge(minutesToCharge / 60);
                            Console.WriteLine("Vehicle recharged successfully!");
                            break;
                        }
                        catch (ValueOutOfRangeException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"The vehicle with license number '{licenseNumber}' does not support recharging.");
                }
            }
        }
   
        private static void handlePrintDetails(Garage i_Garage)
        {
            string licenseNumber = getValidatedLicenseNumber();
            if (!i_Garage.FindVehicleInGarage(licenseNumber))
            {
                Console.WriteLine($"No vehicle with the license number '{licenseNumber}' was found in the garage." +
                                  " Please double-check the license number.");
            }
            else
            {
                Vehicle vehicle = i_Garage.GetVehicleFromGarage(licenseNumber);
                Console.WriteLine(vehicle.PrintVehicleDetails());
            }
        }

        private static Vehicle handleVehicleType(eVehicleType i_VehicleType, string i_LicenseNumber)
        {
            Vehicle vehicle = null;
            switch (i_VehicleType)
            {
                case eVehicleType.FuelMotorcycle:
                    vehicle = fuelMotorcycleAdding(i_LicenseNumber);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    vehicle = electricMotorcycleAdding(i_LicenseNumber);
                    break;
                case eVehicleType.FuelCar:
                    vehicle = fuelCarAdding(i_LicenseNumber);
                    break;
                case eVehicleType.ElectricCar:
                    vehicle = electricCarAdding(i_LicenseNumber);
                    break;
                case eVehicleType.Truck:
                    vehicle = truckAdding(i_LicenseNumber);
                    break;
                default:
                    Console.WriteLine("Invalid vehicle type.");
                    break;
            }

            return vehicle;
        }

        private static FuelMotorcycle fuelMotorcycleAdding(string i_LicenseNumber)
        {
            FuelMotorcycle fuelMotorcycle = new FuelMotorcycle();

            fuelMotorcycle.SetLicenseType(getLicenseType());
            setMotorcycleEngine(fuelMotorcycle);

            initializeFuelVehicle(fuelMotorcycle);

            initializeVehicleDetails(fuelMotorcycle, i_LicenseNumber, "Fuel Motorcycle", 2);

            return fuelMotorcycle;
        }

        private static ElectricMotorcycle electricMotorcycleAdding(string i_LicenseNumber)
        {
            ElectricMotorcycle electricMotorcycle = new ElectricMotorcycle();

            electricMotorcycle.SetLicenseType(getLicenseType());
            setMotorcycleEngine(electricMotorcycle);

            initializeBatteryVehicle(electricMotorcycle);

            initializeVehicleDetails(electricMotorcycle, i_LicenseNumber, "Electric Motorcycle", 2);

            return electricMotorcycle;
        }

        private static FuelCar fuelCarAdding(string i_LicenseNumber)
        {
            FuelCar fuelCar = new FuelCar();

            fuelCar.SetCarColor(getCarColor());
            fuelCar.SetCarDoorsAmount(getDoorsNumber());

            initializeFuelVehicle(fuelCar);

            initializeVehicleDetails(fuelCar, i_LicenseNumber, "Fuel Car", 5);

            return fuelCar;
        }

        private static ElectricCar electricCarAdding(string i_LicenseNumber)
        {
            ElectricCar electricCar = new ElectricCar();

            electricCar.SetCarColor(getCarColor());
            electricCar.SetCarDoorsAmount(getDoorsNumber());

            initializeBatteryVehicle(electricCar);

            initializeVehicleDetails(electricCar, i_LicenseNumber, "Electric Car", 5);

            return electricCar;
        }

        private static Truck truckAdding(string i_LicenseNumber)
        {
            Truck truck = new Truck();

            truck.SetCargoVolume(getCargoVolume());
            truck.SetRefrigeration(getRefrigeration());

            initializeFuelVehicle(truck);

            initializeVehicleDetails(truck, i_LicenseNumber, "Truck", 14);

            return truck;
        }

        private static void initializeFuelVehicle(FuelVehicle i_Vehicle)
        {
            i_Vehicle.SetFuelType(i_Vehicle.GetFuelType());
            i_Vehicle.SetMaxFuelAmount(i_Vehicle.GetMaxEnergy());
            setFuelVehicleCurrentAmount(i_Vehicle);
        }

        private static void initializeBatteryVehicle(ElectricVehicle i_Vehicle)
        {
            i_Vehicle.SetMaxBatteryAmount(i_Vehicle.GetMaxEnergy());
            setBatteryTimeLeft(i_Vehicle);
        }

        private static void initializeVehicleDetails(Vehicle i_Vehicle, string i_LicenseNumber,
            string i_Prompt, int i_WheelsCount)
        {
            i_Vehicle.m_LicenseNumber = i_LicenseNumber;
            i_Vehicle.SetVehicleModel(getVehicleModel(i_Prompt));
            List<Wheels> wheelsList = new List<Wheels>();
            for (int i = 0; i < i_WheelsCount; i++)
            {
                wheelsList.Add(new Wheels());
            }

            i_Vehicle.SetWheels(getWheelsDetails(wheelsList, i_Vehicle));
        }

        private static void setMotorcycleEngine(Vehicle i_Motorcycle)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter engine volume (cc):");
                    int engineVolume = int.Parse(Console.ReadLine());

                    if (i_Motorcycle is FuelMotorcycle fuelMotorcycle)
                    {
                        fuelMotorcycle.SetEngineVolume(engineVolume);
                    }
                    else if (i_Motorcycle is ElectricMotorcycle electricMotorcycle)
                    {
                        electricMotorcycle.SetEngineVolume(engineVolume);
                    }
                    else
                    {
                        throw new ArgumentException("Unsupported vehicle type. Only motorcycles are supported.");
                    }

                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Invalid format. Please enter a valid integer for engine volume.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
        }

        private static eLicenseType getLicenseType()
        {
            while (true)
            {
                Console.WriteLine("Please choose the type of license:");
                foreach (var license in MapperHelper.sr_LicenseMap)
                {
                    Console.WriteLine($"{license.Key}. {license.Value}");
                }

                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && MapperHelper.sr_LicenseMap.ContainsKey(choice))
                {
                    return MapperHelper.sr_LicenseMap[choice];
                }

                Console.WriteLine("Invalid input. Please enter a number corresponding to a license type.");
            }
        }

        private static eCarColor getCarColor()
        {
            while (true)
            {
                Console.WriteLine("Please choose a car color:");
                foreach (var color in MapperHelper.sr_ColorMap)
                {
                    Console.WriteLine($"{color.Key}. {color.Value}");
                }

                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && MapperHelper.sr_ColorMap.ContainsKey(choice))
                {
                    return MapperHelper.sr_ColorMap[choice];
                }

                Console.WriteLine("Invalid input. Please enter a number corresponding to a car color.");
            }
        }

        private static eDoorsNumber getDoorsNumber()
        {
            while (true)
            {
                Console.WriteLine("Please choose the number of doors:");
                foreach (var door in MapperHelper.sr_DoorsMap)
                {
                    Console.WriteLine($"{door.Key}. {door.Value}");
                }

                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && MapperHelper.sr_DoorsMap.ContainsKey(choice))
                {
                    return MapperHelper.sr_DoorsMap[choice];
                }

                Console.WriteLine("Invalid input. Please enter a number corresponding to a door count.");
            }
        }

        private static eFuelType getFuelType()
        {
            while (true)
            {
                Console.WriteLine("Please choose the type of fuel:");
                foreach (var fuel in MapperHelper.sr_FuelTypeMap)
                {
                    Console.WriteLine($"{fuel.Key}. {fuel.Value}");
                }

                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && MapperHelper.sr_FuelTypeMap.ContainsKey(choice))
                {
                    return MapperHelper.sr_FuelTypeMap[choice];
                }

                Console.WriteLine("Invalid input. Please enter a number corresponding to a fuel type.");
            }
        }

        private static eVehicleType getVehicleType()
        {
            while (true)
            {
                Console.WriteLine("Please choose the type of vehicle:");
                foreach (var vehicleType in MapperHelper.sr_VehicleTypeMap)
                {
                    Console.WriteLine($"{vehicleType.Key}. {vehicleType.Value}");
                }

                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && MapperHelper.sr_VehicleTypeMap.ContainsKey(choice))
                {
                    return MapperHelper.sr_VehicleTypeMap[choice];
                }

                Console.WriteLine("Invalid input. Please enter a number corresponding to a vehicle type.");
            }
        }

        private static void setFuelVehicleCurrentAmount(FuelVehicle i_FuelVehicle)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter current fuel amount (liters):");
                    string currentFuelInput = Console.ReadLine();
                    float currentFuelAmount = float.Parse(currentFuelInput);
                    float fuelTankCapacity;

                    if (i_FuelVehicle is FuelMotorcycle)
                    {
                        fuelTankCapacity = FuelMotorcycle.k_FuelTankCapacity;
                    }
                    else if (i_FuelVehicle is FuelCar)
                    {
                        fuelTankCapacity = FuelCar.k_FuelTankCapacity;
                    }
                    else if(i_FuelVehicle is Truck)
                    {
                        fuelTankCapacity = Truck.k_FuelTankCapacity;
                    }
                    else
                    {
                        throw new ArgumentException("Unsupported vehicle type. Only fuel vehicles are supported.");
                    }

                    FuelVehicle.ValidateFuelAmount(currentFuelAmount, fuelTankCapacity);
                    i_FuelVehicle.SetEnergyPercentage(currentFuelAmount / fuelTankCapacity);
                    i_FuelVehicle.SetCurrentFuelAmount(currentFuelAmount);
                    break;
                }
                catch(ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Invalid format. Please enter a valid float for fuel amount.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
        }

        private static string getVehicleModel(string i_Prompt)
        {
            Console.WriteLine($"Please enter {i_Prompt}'s model");
            string vehicleModel = Console.ReadLine();

            return vehicleModel;
        }

        private static List<Wheels> getWheelsDetails(List<Wheels> i_WheelsList, Vehicle i_Vehicle)
        {
            Console.WriteLine("Are all your wheels the same brand and tire pressure? (y/n):");
            char choice = char.Parse(Console.ReadLine().ToUpper());

            while (choice != 'Y' && choice != 'N')
            {
                Console.WriteLine("Invalid choice. Please enter 'y' or 'n'.");
                choice = char.Parse(Console.ReadLine().ToUpper());
            }

            while (true)
            {
                try
                {
                    if (choice == 'Y')
                    {
                        Console.WriteLine("Enter tire brand:");
                        string brand = Console.ReadLine();
                        Console.WriteLine("Enter tire pressure:");
                        float pressureInput = float.Parse(Console.ReadLine());
                        Wheels.SetAllWheelsDetails(i_WheelsList, brand, pressureInput, i_Vehicle);
                    }
                    else
                    {
                        string brand = "unset";
                        for (int i = 0; i < i_WheelsList.Count;)
                        {
                            while (true)
                            {
                                if (brand == "unset")
                                {
                                    Console.WriteLine($"Enter tire brand for wheel {i + 1}:");
                                    brand = Console.ReadLine();
                                }
                                try
                                {

                                    Console.WriteLine($"Enter tire pressure for wheel {i + 1}:");
                                    float pressure = float.Parse(Console.ReadLine());

                                    i_WheelsList[i].SetDetails(brand, pressure, i_Vehicle);
                                    brand = "unset";
                                    i++;
                                }
                                catch (ValueOutOfRangeException ex)
                                {
                                    Console.WriteLine($"{ex.Message}");
                                }
                                catch (FormatException ex)
                                {
                                    Console.WriteLine("Invalid input. pressure.");
                                }

                                break;
                            }
                        }
                    }

                    return i_WheelsList;
                }
                catch (Exception ex) when (ex is ValueOutOfRangeException || ex is ArgumentException)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter numeric values for tire pressure.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
        }

        private static void setBatteryTimeLeft(ElectricVehicle i_ElectricVehicle)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter how many hours of usage are left with the current battery charge:");
                    float hoursLeft = float.Parse(Console.ReadLine());
                    ElectricVehicle.ValidateBatteryTime(hoursLeft, i_ElectricVehicle.GetMaxEnergy());
                    i_ElectricVehicle.SetEnergyPercentage(hoursLeft / i_ElectricVehicle.GetMaxEnergy());
                    i_ElectricVehicle.SetCurrentEnergy(hoursLeft);
                    break;
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Invalid format. Please enter a valid integer for engine volume.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
        }

        private static bool getRefrigeration()
        {
            Console.WriteLine("Can this truck transfer with refrigeration? (y/n):");
            char choice = char.Parse(Console.ReadLine().ToUpper());

            while (choice != 'Y' && choice != 'N')
            {
                Console.WriteLine("Invalid choice. Please enter 'y' or 'n'.");
                choice = char.Parse(Console.ReadLine().ToUpper());
            }

            return choice == 'Y';
        }

        private static float getCargoVolume()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter cargo volume (cc):");
                    string input = Console.ReadLine();

                    if (!float.TryParse(input, out float cargoVolumeInput) || cargoVolumeInput <= 0)
                    {
                        throw new ArgumentException("Cargo volume must be a positive number.");
                    }

                    return cargoVolumeInput;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
        }

        public static eVehicleStatus GetVehicleStatus()
        {
            while (true)
            {
                Console.WriteLine("Please choose a vehicle status:");
                foreach (var status in VehicleInGarage.sr_StatusMap)
                {
                    Console.WriteLine($"{status.Key}. {status.Value}");
                }

                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && VehicleInGarage.sr_StatusMap.ContainsKey(choice))
                {
                    return VehicleInGarage.sr_StatusMap[choice];
                }

                Console.WriteLine("Invalid input. Please enter a number corresponding to a vehicle status.");
            }
        }

        private static eChoice? stringToChoice(string i_CustomerChoice)
        {
            var choicesMap = new Dictionary<string, eChoice>
                                 {
                                     { "1", eChoice.Insert },
                                     { "2", eChoice.PrintLicenses },
                                     { "3", eChoice.ChangeStatus },
                                     { "4", eChoice.Inflate },
                                     { "5", eChoice.Refuel },
                                     { "6", eChoice.Recharge },
                                     { "7", eChoice.PrintDetails }
                                 };

            return choicesMap.TryGetValue(i_CustomerChoice, out var choice) ? choice : (eChoice?)null;
        }

        private static eVehicleType? stringToVehicleType(string i_VehicleTypeString)
        {
            var vehicleTypesMap = new Dictionary<string, eVehicleType>
                                 {
                                     { "1", eVehicleType.FuelMotorcycle },
                                     { "2", eVehicleType.ElectricMotorcycle },
                                     { "3", eVehicleType.FuelCar },
                                     { "4", eVehicleType.ElectricCar },
                                     { "5", eVehicleType.Truck },

                                 };

            return vehicleTypesMap.TryGetValue(i_VehicleTypeString, out var choice) ? choice : (eVehicleType?)null;
        }

        private static bool validateLicenseNumber(string i_LicenseNumber)
        {
            bool validatedNumber = false;

            if (!string.IsNullOrEmpty(i_LicenseNumber) && (i_LicenseNumber.Length == 8 || i_LicenseNumber.Length == 7))
            {
                validatedNumber = true;
                foreach (char c in i_LicenseNumber)
                {
                    if (!char.IsDigit(c))
                    {
                        validatedNumber = false;
                        break;
                    }
                }
            }

            return validatedNumber;
        }

    }
}


