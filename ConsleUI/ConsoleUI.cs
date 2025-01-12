using System;
using System.Collections.Generic;
using ConsoleUI;
using Ex03_Or_315900845_Or_314919994;


namespace ConsleUI
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine($"Welcome to our Garage!{Environment.NewLine}Please choose an option:{Environment.NewLine}1. Insert a car "
                              + $"{Environment.NewLine}2. Print license numbers{Environment.NewLine}3. Change vehicle status"
                              + $"{Environment.NewLine}4. Inflate wheels{Environment.NewLine}5. Refuel{Environment.NewLine}6. Recharge"
                              + $"{Environment.NewLine}7. Print vehicle details");
            string customerChoice = Console.ReadLine();

            Garage garage = new Garage();

            customerChoice = getValidChoice(customerChoice);

            handleCustomerChoice(customerChoice, ref garage);
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

        private static void handleCustomerChoice(string i_CustomerChoice, ref Garage io_Garage)
        {
            switch (stringToChoice(i_CustomerChoice))
            {
                case eChoice.Insert:
                    handleInsertion(ref io_Garage);
                    break;
                case eChoice.PrintLicenses:
                    handlePrintLicenses(ref io_Garage);
                    break;
                case eChoice.ChangeStatus:
                    handleChangeStatus(ref io_Garage);
                    break;
                case eChoice.Inflate:
                    handleInflate(ref io_Garage);
                    break;
                case eChoice.Refuel:
                    handleRefuel(ref io_Garage);
                    break;
                case eChoice.Recharge:
                    handleRecharge(ref io_Garage);
                    break;
                case eChoice.PrintDetails:
                    handlePrintDetails(ref io_Garage);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private static void handleInsertion(ref Garage io_Garage)
        {
            Console.WriteLine("Enter license number:");
            string licenseNumber = Console.ReadLine();

            while (!validateLicenseNumber(licenseNumber))
            {
                Console.WriteLine("Invalid license number, please try again:");
                licenseNumber = Console.ReadLine();
            }

            while (true)
            {
                try
                {
                    if (io_Garage.FindVehicleInGarage(licenseNumber))
                    {
                        Console.WriteLine("Found the vehicle in the garage, changing it's status.");
                        io_Garage.UpdateVehicleStatus(licenseNumber, eVehicleStatus.InRepair);
                    }
                    else
                    {
                        Console.WriteLine($"Please choose type of vehicle:{Environment.NewLine}1. FuelMotorcycle "
                                          + $"{Environment.NewLine}2. ElectricMotorcycle{Environment.NewLine}3. FuelCar"
                                          + $"{Environment.NewLine}4. ElectricCar{Environment.NewLine}5. Truck");
                        string vehicleTypeString = Console.ReadLine();

                        Vehicle vehicle = handleVehicleType(vehicleTypeString, licenseNumber);
                        string ownerPhoneNumber = getOwnerPhoneNumber();
                        string ownerName = getOwnerName();
                        VehicleInGarage vehicleInGarage = new VehicleInGarage(vehicle, ownerName, ownerPhoneNumber);

                        io_Garage.AddVehicle(vehicleInGarage);
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
                    return ownerName; // Valid name
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

        private static void handlePrintLicenses(ref Garage io_Garage)
        {

        }

        private static void handleChangeStatus(ref Garage io_Garage)
        {

        }

        private static void handleInflate(ref Garage io_Garage)
        {

        }

        private static void handleRefuel(ref Garage io_Garage)
        {

        }

        private static void handleRecharge(ref Garage io_Garage)
        {

        }

        private static void handlePrintDetails(ref Garage io_Garage)
        {

        }

        private static Vehicle handleVehicleType(string i_VehicleTypeString, string i_LicenseNumber)
        {
            Vehicle vehicle = null;
            switch (stringToVehicleType(i_VehicleTypeString))
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

        private static bool validateNumber(string i_ChosenNumber)
        {
            bool isValidated = false;
            if (int.TryParse(i_ChosenNumber, out int result))
            {
                if (result < 8 && result > 0)
                {
                    isValidated = true;
                }
            }

            return isValidated;
        }

        private static FuelMotorcycle fuelMotorcycleAdding(string i_LicenseNumber)
        {
            FuelMotorcycle fuelMotorcycle = new FuelMotorcycle();

            fuelMotorcycle.SetLicenseType(getLicenseType());
            setMotorcycleEngine(fuelMotorcycle);

            fuelMotorcycle.SetFuelType(fuelMotorcycle.GetFuelType());
            fuelMotorcycle.SetMaxFuelAmount(FuelMotorcycle.k_FuelTankCapacity);
            setFuelVehicleCurrentAmount(fuelMotorcycle);

            fuelMotorcycle.m_LicenseNumber = i_LicenseNumber;
            fuelMotorcycle.SetVehicleModel(getVehicleModel("Fuel motorcycle"));
            List<Wheels> wheelsList = new List<Wheels>(2);
            getWheelsDetails(ref wheelsList, fuelMotorcycle);

            return fuelMotorcycle;
        }

        private static ElectricMotorcycle electricMotorcycleAdding(string i_LicenseNumber)
        {
            ElectricMotorcycle electricMotorcycle = new ElectricMotorcycle();

            electricMotorcycle.SetLicenseType(getLicenseType());
            setMotorcycleEngine(electricMotorcycle);

            electricMotorcycle.SetMaxBatteryAmount(ElectricMotorcycle.k_BatteryTimeCapacity);
            setBatteryTimeLeft(electricMotorcycle);

            electricMotorcycle.m_LicenseNumber = i_LicenseNumber;
            electricMotorcycle.SetVehicleModel(getVehicleModel("Fuel motorcycle"));
            List<Wheels> wheelsList = new List<Wheels>(2);
            getWheelsDetails(ref wheelsList, electricMotorcycle);

            return electricMotorcycle;
        }

        private static FuelCar fuelCarAdding(string i_LicenseNumber)
        {
            FuelCar fuelCar = new FuelCar();

            fuelCar.SetCarColor(getCarColor());
            fuelCar.SetCarDoorsAmount(getDoorsNumber());

            fuelCar.SetFuelType(fuelCar.GetFuelType());
            fuelCar.SetMaxFuelAmount(FuelMotorcycle.k_FuelTankCapacity);
            setFuelVehicleCurrentAmount(fuelCar);

            fuelCar.m_LicenseNumber = i_LicenseNumber;
            fuelCar.SetVehicleModel(getVehicleModel("Fuel motorcycle"));
            List<Wheels> wheelsList = new List<Wheels>(5);
            getWheelsDetails(ref wheelsList, fuelCar);

            return fuelCar;
        }

        private static ElectricCar electricCarAdding(string i_LicenseNumber)
        {
            ElectricCar electricCar = new ElectricCar();

            electricCar.SetCarColor(getCarColor());
            electricCar.SetCarDoorsAmount(getDoorsNumber());

            electricCar.SetMaxBatteryAmount(ElectricMotorcycle.k_BatteryTimeCapacity);
            setBatteryTimeLeft(electricCar);

            electricCar.m_LicenseNumber = i_LicenseNumber;
            electricCar.SetVehicleModel(getVehicleModel("Fuel motorcycle"));
            List<Wheels> wheelsList = new List<Wheels>(5);
            getWheelsDetails(ref wheelsList, electricCar);

            return electricCar;
        }

        private static Truck truckAdding(string i_LicenseNumber)
        {
            Truck truck = new Truck();

            truck.SetCargoVolume(getCargoVolume());
            truck.SetRefrigeration(getRefrigeration());

            truck.SetFuelType(truck.GetFuelType());
            truck.SetMaxFuelAmount(FuelMotorcycle.k_FuelTankCapacity);
            setFuelVehicleCurrentAmount(truck);

            truck.m_LicenseNumber = i_LicenseNumber;
            truck.SetVehicleModel(getVehicleModel("Fuel motorcycle"));
            List<Wheels> wheelsList = new List<Wheels>(14);
            getWheelsDetails(ref wheelsList, truck);

            return truck;
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

        private static eLicenseType stringToLicenseType(string i_LicenseTypeString)
        {
            var licenseTypesMap = new Dictionary<string, eLicenseType>
                                      {
                                          { "1", eLicenseType.A1 },
                                          { "2", eLicenseType.A2 },
                                          { "3", eLicenseType.B1 },
                                          { "4", eLicenseType.B2 },
                                      };

            licenseTypesMap.TryGetValue(i_LicenseTypeString, out var choice);

            return choice;
        }

        private static eLicenseType getLicenseType()
        {
            Console.WriteLine($"Please choose type of license:{Environment.NewLine}1. A1"
                              + $"{Environment.NewLine}2. A2{Environment.NewLine}3. B1"
                              + $"{Environment.NewLine}4. B2");
            string licenseTypeString = Console.ReadLine();
            eLicenseType licenseType;

            while (true)
            {

                while (!int.TryParse(licenseTypeString, out int value) || value > 4 || value < 1)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 0 and 4.");
                    licenseTypeString = Console.ReadLine();
                }

                try
                {
                    licenseType = stringToLicenseType(licenseTypeString);
                    return licenseType;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static eCarColor stringToCarColor(string i_ColorString)
        {
            var carColorsMap = new Dictionary<string, eCarColor>
                                   {
                                       { "1", eCarColor.Blue },
                                       { "2", eCarColor.Black },
                                       { "3", eCarColor.White },
                                       { "4", eCarColor.Gray },
                                   };

            carColorsMap.TryGetValue(i_ColorString, out var choice);

            return choice;
        }

        private static eCarColor getCarColor()
        {
            Console.WriteLine($"Please choose a car color:{Environment.NewLine}1. Blue"
                              + $"{Environment.NewLine}2. Black{Environment.NewLine}3. White"
                              + $"{Environment.NewLine}4. Gray");
            string colorString = Console.ReadLine();
            eCarColor carColor;

            while (true)
            {
                while (!int.TryParse(colorString, out int value) || value > 4 || value < 1)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                    colorString = Console.ReadLine();
                }

                try
                {
                    carColor = stringToCarColor(colorString);
                    return carColor;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static eDoorsNumber stringToDoorsNumber(string i_DoorsString)
        {
            var doorsNumberMap = new Dictionary<string, eDoorsNumber>
                                     {
                                         { "1", eDoorsNumber.Two },
                                         { "2", eDoorsNumber.Three },
                                         { "3", eDoorsNumber.Four },
                                         { "4", eDoorsNumber.Five },
                                     };

            doorsNumberMap.TryGetValue(i_DoorsString, out var choice);

            return choice;
        }

        private static eDoorsNumber getDoorsNumber()
        {
            Console.WriteLine($"Please choose the number of doors:{Environment.NewLine}1. Two"
                              + $"{Environment.NewLine}2. Three{Environment.NewLine}3. Four"
                              + $"{Environment.NewLine}4. Five");
            string doorsNumberString = Console.ReadLine();
            eDoorsNumber doorsNumber;

            while (true)
            {
                while (!int.TryParse(doorsNumberString, out int value) || value < 1 || value > 4)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                    doorsNumberString = Console.ReadLine();
                }

                try
                {
                    doorsNumber = stringToDoorsNumber(doorsNumberString);
                    return doorsNumber;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
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

                    if (i_FuelVehicle is FuelMotorcycle fuelMotorcycle)
                    {
                        fuelTankCapacity = FuelMotorcycle.k_FuelTankCapacity;
                    }
                    else if (i_FuelVehicle is FuelCar fuelCar)
                    {
                        fuelTankCapacity = FuelCar.k_FuelTankCapacity;
                    }
                    else if(i_FuelVehicle is Truck truck)
                    {
                        fuelTankCapacity = Truck.k_FuelTankCapacity;
                    }
                    else
                    {
                        throw new ArgumentException("Unsupported vehicle type. Only fuel vehicles are supported.");
                    }

                    FuelVehicle.ValidateFuelAmount(currentFuelAmount, fuelTankCapacity);
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
                    Console.WriteLine("Invalid format. Please enter a valid integer for engine volume.");
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

        private static void getWheelsDetails(ref List<Wheels> io_WheelsList, Vehicle i_Vehicle)
        {
            Console.WriteLine("Are all your wheels the same brand and tire pressure? (y/n):");
            char choice = char.Parse(Console.ReadLine().ToUpper());

            while (choice != 'Y' && choice != 'N')
            {
                Console.WriteLine("Invalid choice. Please enter 'y' or 'n'.");
                choice = char.Parse(Console.ReadLine().ToUpper());
            }
            try
            {

                if (choice == 'Y')
                {
                    Console.WriteLine("Enter tire brand:");
                    string brand = Console.ReadLine();
                    Console.WriteLine("Enter tire pressure:");
                    float pressureInput = float.Parse(Console.ReadLine());
                    Wheels.SetAllWheelsDetails(io_WheelsList, brand, pressureInput, i_Vehicle);
                }
                else
                {
                    for (int i = 0; i < io_WheelsList.Count; i++)
                    {
                        Console.WriteLine($"Enter tire brand for wheel {i + 1}:");
                        string brand = Console.ReadLine();

                        Console.WriteLine($"Enter tire pressure for wheel {i + 1}:");
                        float pressure = float.Parse(Console.ReadLine());

                        io_WheelsList[i].SetDetails(brand, pressure, i_Vehicle);
                    }
                }
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

        private static void setBatteryTimeLeft(ElectricVehicle i_ElectricVehicle)
        {
            try
            {
                Console.WriteLine("Please enter how many hours left");
                float hoursLeft = float.Parse(Console.ReadLine());
                ElectricVehicle.ValidateBatteryTime(hoursLeft, i_ElectricVehicle.GetMaxEnergy());
                i_ElectricVehicle.SetCurrentEnergy(hoursLeft);
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


