using System;
using System.Collections.Generic;
using System.Linq;
using Ex03_Or_315900845_Or_314919994;
using Ex03_Or_315900845_Or_314919994.GarageLogic;


namespace ConsleUI
{
    class Program
    {
        public static void Main()
        {
            Garage garage = new Garage();
            while (true)
            {
                Console.WriteLine(
                    $"{Environment.NewLine}========Or's Garage========{Environment.NewLine}"
                    + $"Welcome to our Garage!{Environment.NewLine}"
                    + $"Please choose an option:{Environment.NewLine}1. Insert a car "
                    + $"{Environment.NewLine}2. Print license numbers{Environment.NewLine}3. Change vehicle status"
                    + $"{Environment.NewLine}4. Inflate wheels{Environment.NewLine}5. Refuel"
                    + $"{Environment.NewLine}6. Recharge" + $"{Environment.NewLine}7. Print vehicle details"
                    + $"{Environment.NewLine}8. Exit"
                    + $"{Environment.NewLine}==========================={Environment.NewLine}");

                eChoice customerChoice = getChoiceFromUser();

                if (customerChoice == eChoice.Exit)
                {
                    break;
                }

                handleCustomerChoice(customerChoice, garage);
            }
        }

        private static eChoice getChoiceFromUser()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && MapperHelper.sr_ChoicesMap.ContainsKey(choice))
                {
                    return MapperHelper.sr_ChoicesMap[choice];
                }

                Console.WriteLine("Invalid input. Please enter a number corresponding to a valid choice.");
            }
        }

        private static void handleCustomerChoice(eChoice i_CustomerChoice, Garage i_Garage)
        {
            switch (i_CustomerChoice)
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
                case eChoice.Exit:
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

            if (i_Garage.FindVehicleInGarage(licenseNumber))
            {
                Console.WriteLine("Vehicle already exists in the garage. Changing its status to 'InRepair'.");
                i_Garage.UpdateVehicleStatus(licenseNumber, eVehicleStatus.InRepair);
                return;
            }

            eVehicleType vehicleType = getVehicleType();

            Dictionary<string, object> vehicleDetails = collectVehicleDetails(vehicleType);
            Dictionary<string, object> wheelDetails = collectWheelDetails(vehicleType);
            vehicleDetails = vehicleDetails.Concat(wheelDetails).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            Vehicle newVehicle = VehicleFactory.CreateVehicle(vehicleType, licenseNumber, vehicleDetails);

            string ownerName = getOwnerName();
            string ownerPhoneNumber = getOwnerPhoneNumber();

            VehicleInGarage vehicleInGarage = new VehicleInGarage(newVehicle, ownerName, ownerPhoneNumber);
            i_Garage.AddVehicle(vehicleInGarage);

            Console.WriteLine("Vehicle successfully added to the garage.");
        }

        private static int getValidatedIntInput(string i_FieldName)
        {
            while (true)
            {
                Console.WriteLine($"Enter {i_FieldName}:");
                if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
                {
                    return value;
                }

                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }

        private static bool getValidatedYesNoInput()
        {
            while (true)
            {
                string input = Console.ReadLine()?.Trim().ToUpper();

                if (input == "Y")
                {
                    return true;
                }
                else if (input == "N")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'y' for yes or 'n' for no.");
                }
            }
        }

        private static float getValidatedFloatInputWithRange(string i_FieldName, float min, float max)
        {
            while (true)
            {
                Console.WriteLine($"Enter {i_FieldName} (between {min} and {max}):");
                string input = Console.ReadLine();

                if (float.TryParse(input, out float value))
                {
                    if (value >= min && value <= max)
                    {
                        return value;
                    }
                    Console.WriteLine($"Invalid input. {i_FieldName} must be between {min} and {max}.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
        }

        private static Dictionary<string, object> collectWheelDetails(eVehicleType i_VehicleType)
        {
            Dictionary<string, object> wheelDetails = new Dictionary<string, object>();

            Console.WriteLine("Are all of your wheels have the same brand pressure? (y/n):");
            bool allSame = getValidatedYesNoInput();
            float maxPressure = 0;
            switch (i_VehicleType)
            {
                case eVehicleType.ElectricCar:
                case eVehicleType.FuelCar:
                    maxPressure = 34f;
                    break;
                case eVehicleType.ElectricMotorcycle:
                case eVehicleType.FuelMotorcycle:
                    maxPressure = 32f;
                    break;
                case eVehicleType.Truck:
                    maxPressure = 29f;
                    break;
            }

            if (allSame)
            {
                Console.WriteLine("Enter wheel manufacturer:");
                string manufacturer = Console.ReadLine();
                float currentPressure = getValidatedFloatInputWithRange("current tire pressure", 0, maxPressure);

                wheelDetails["UniformWheels"] = true;
                wheelDetails["WheelManufacturer"] = manufacturer;
                wheelDetails["WheelCurrentPressure"] = currentPressure;
                wheelDetails["WheelMaxPressure"] = maxPressure;
            }
            else
            {
                int wheelsCount = 0;

                switch (i_VehicleType)
                {
                    case eVehicleType.FuelMotorcycle:
                    case eVehicleType.ElectricMotorcycle:
                        wheelsCount = 2;
                        break;
                    case eVehicleType.FuelCar:
                    case eVehicleType.ElectricCar:
                        wheelsCount = 5;
                        break;
                    case eVehicleType.Truck:
                        wheelsCount = 14;
                        break;
                }

                wheelDetails["UniformWheels"] = false;
                wheelDetails["WheelMaxPressure"] = maxPressure;
                List<Dictionary<string, object>> individualWheels = new List<Dictionary<string, object>>();

                for (int i = 0; i < wheelsCount; i++)
                {
                    Console.WriteLine($"Wheel {i + 1}:");

                    Console.WriteLine("Enter wheel manufacturer:");
                    string manufacturer = Console.ReadLine();

                    float currentPressure = getValidatedFloatInputWithRange("current tire pressure", 0, maxPressure);

                    individualWheels.Add(new Dictionary<string, object>
                                             {
                                                 { "WheelManufacturer", manufacturer },
                                                 { "WheelCurrentPressure", currentPressure },
                                                 { "WheelMaxPressure", maxPressure }
                                             });
                }

                wheelDetails["IndividualWheels"] = individualWheels;
            }

            return wheelDetails;
        }

        private static Dictionary<string, object> collectVehicleDetails(eVehicleType i_VehicleType)
        {
            Dictionary<string, object> vehicleDetails = new Dictionary<string, object>();

            if (i_VehicleType == eVehicleType.FuelMotorcycle || i_VehicleType == eVehicleType.ElectricMotorcycle)
            {
                if (i_VehicleType == eVehicleType.FuelMotorcycle)
                {
                    vehicleDetails["FuelType"] = new FuelMotorcycle().GetFuelType();
                    vehicleDetails["MaxFuelAmount"] = new FuelMotorcycle().GetMaxEnergy();
                    float currentFuelAmount = getValidatedFloatInputWithRange(
                        "amount of current fuel", 0, 6.2f);
                    vehicleDetails["CurrentFuelAmount"] = currentFuelAmount;
                }
                else
                {
                    float currentBatteryAmount = getValidatedFloatInputWithRange(
                        "amount battery time left in hours", 0, 2.9f);
                    vehicleDetails["CurrentBatteryAmount"] = currentBatteryAmount;
                    vehicleDetails["MaxBatteryAmount"] = new ElectricMotorcycle().GetMaxEnergy();
                }

                int engineVolume = getValidatedIntInput("engine volume (cc)");
                vehicleDetails["EngineVolume"] = engineVolume;
                eLicenseType licenseType = getLicenseType();
                vehicleDetails["LicenseType"] = licenseType;
            }
            else if (i_VehicleType == eVehicleType.FuelCar || i_VehicleType == eVehicleType.ElectricCar)
            {
                if (i_VehicleType == eVehicleType.FuelCar)
                {
                    vehicleDetails["FuelType"] = new FuelCar().GetFuelType();
                    vehicleDetails["MaxFuelAmount"] = new FuelCar().GetMaxEnergy();
                    float currentFuelAmount = getValidatedFloatInputWithRange(
                        "amount of current fuel", 0, 52f);
                    vehicleDetails["CurrentFuelAmount"] = currentFuelAmount;
                }
                else
                {
                    float currentBatteryAmount = getValidatedFloatInputWithRange(
                        "amount battery time left in hours", 0, 5.4f);
                    vehicleDetails["CurrentBatteryAmount"] = currentBatteryAmount;
                    vehicleDetails["MaxBatteryAmount"] = new ElectricMotorcycle().GetMaxEnergy();
                }

                eCarColor carColor = getCarColor();
                eDoorsNumber doorsNumber = getDoorsNumber();
                vehicleDetails["CarColor"] = carColor;
                vehicleDetails["DoorsNumber"] = doorsNumber;
            }
            else
            {
                vehicleDetails["FuelType"] = new Truck().GetFuelType();
                vehicleDetails["MaxFuelAmount"] = new Truck().GetMaxEnergy();
                float currentFuelAmount = getValidatedFloatInputWithRange(
                    "amount of current fuel", 0, 125f);
                vehicleDetails["CurrentFuelAmount"] = currentFuelAmount;
                Console.WriteLine("Enter cargo volume (in cubic meters):");
                float cargoVolume = float.Parse(Console.ReadLine());
                Console.WriteLine("Does the truck have refrigeration? (y/n):");
                bool refrigeration = getValidatedYesNoInput();
                vehicleDetails["CargoVolume"] = cargoVolume;
                vehicleDetails["Refrigeration"] = refrigeration;
            }

            Console.WriteLine("Please enter your car model:");
            string model = Console.ReadLine();
            vehicleDetails["Model"] = model;

            return vehicleDetails;
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

                Console.WriteLine("Invalid name. Please enter a name containing only letters and spaces.");
            }
        }

        private static bool isValidOwnerName(string i_OwnerName)
        {
            bool isValid = !string.IsNullOrWhiteSpace(i_OwnerName);

            if (isValid)
            {
                foreach (char c in i_OwnerName)
                {
                    if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            return isValid;
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

                Console.WriteLine("Invalid phone number. Please enter exactly 10 digits.");
            }
        }

        private static bool isValidPhoneNumber(string i_PhoneNumber)
        {
            return i_PhoneNumber.Length == 10 && long.TryParse(i_PhoneNumber, out _);
        }

        private static void handlePrintLicenses(Garage i_Garage)
        {
            eVehicleStatus status = getVehicleStatus();
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
                Console.WriteLine(
                    $"No vehicle with the license number '{licenseNumber}' was found in the garage."
                    + " Please double-check the license number.");
            }
            else
            {
                try
                {
                    eVehicleStatus status = getVehicleStatus();
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
                Console.WriteLine(
                    $"No vehicle with the license number '{licenseNumber}' was found in the garage."
                    + " Please double-check the license number.");
            }
            else
            {
                bool isInflated = i_Garage.InflateVehicleWheels(licenseNumber);

                if (isInflated)
                {
                    Console.WriteLine(
                        "Success! " + $"All wheels of the vehicle with license number '{licenseNumber}' have been"
                                    + " inflated to their maximum pressure.");
                }
                else
                {
                    Console.WriteLine(
                        $"Failed to inflate wheels for the vehicle with license number '{licenseNumber}'.");
                }
            }
        }

        private static void handleRefuel(Garage i_Garage)
        {
            string licenseNumber = getValidatedLicenseNumber();

            if (!i_Garage.FindVehicleInGarage(licenseNumber))
            {
                Console.WriteLine(
                    $"No vehicle with the license number '{licenseNumber}' was found in the garage."
                    + " Please double-check the license number.");
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
                Console.WriteLine(
                    $"No vehicle with the license number '{licenseNumber}' was found in the garage."
                    + " Please double-check the license number.");
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
                    Console.WriteLine(
                        $"The vehicle with license number '{licenseNumber}' does not support recharging.");
                }
            }
        }

        private static void handlePrintDetails(Garage i_Garage)
        {
            string licenseNumber = getValidatedLicenseNumber();
            if (!i_Garage.FindVehicleInGarage(licenseNumber))
            {
                Console.WriteLine(
                    $"No vehicle with the license number '{licenseNumber}' was found in the garage."
                    + " Please double-check the license number.");
            }
            else
            {
                Vehicle vehicle = i_Garage.GetVehicleFromGarage(licenseNumber);
                Console.WriteLine(vehicle.PrintVehicleDetails());
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

        private static eVehicleStatus getVehicleStatus()
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