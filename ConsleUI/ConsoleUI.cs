using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
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

            Ex03_Or_315900845_Or_314919994.Garage garage = new Garage();

            while (!validateNumber(customerChoice))
            {
                Console.WriteLine("Invalid choice please try again.");
                customerChoice = Console.ReadLine();
            }

            switch (stringToChoice(customerChoice))
            {
                case eChoice.Insert:
                    // handleInsertion
                    string licenseNumber = Console.ReadLine();

                    while (!validateLicenseNumber(licenseNumber))
                    {
                        Console.WriteLine("Invalid license number please try again.");
                        licenseNumber = Console.ReadLine();
                    }

                    string foundVehicleInGarage = Ex03_Or_315900845_Or_314919994.Garage.FindVehicleInGarage(ref garage, licenseNumber);

                    if (foundVehicleInGarage == null)
                    {
                        Console.WriteLine($"Please choose type of vehicle:{Environment.NewLine}1. FuelMotorcycle "
                                          + $"{Environment.NewLine}2. ElectricMotorcycle{Environment.NewLine}3. FuelCar"
                                          + $"{Environment.NewLine}4. ElectricCar{Environment.NewLine}5. Truck");
                        string vehicleTypeString = Console.ReadLine();

                        switch (stringToVehicleType(vehicleTypeString))
                        {
                            case eVehicleType.FuelMotorcycle:

                                // Add Fuel Motorcycle function
                                addFuelMotorCycle(licenseNumber); // הערך שחוזר מכאן צריך להכנס לתוך הרשימה של המוסך


                                break;
                            case eVehicleType.ElectricMotorcycle:
                                break;
                            case eVehicleType.FuelCar:
                                break;
                            case eVehicleType.ElectricCar:
                                break;
                            case eVehicleType.Truck:
                                break;
                        }
                    }
                    break;
                case eChoice.PrintLicenses:
                    break;
                case eChoice.ChangeStatus:
                    break;
                case eChoice.Inflate:
                    break;
                case eChoice.Refuel:
                    break;
                case eChoice.Recharge:
                    break;
                case eChoice.PrintDetails:
                    break;
            }
        }

        static string GetValidatedInput(string prompt, Func<string, bool> validationFunc)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();

            while (!validationFunc(input))
            {
                Console.WriteLine("Invalid input, please try again.");
                input = Console.ReadLine();
            }

            return input;
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

        private static FuelMotorcycle addFuelMotorCycle(string i_LicenseNumber)
        {
            eLicenseType licenseType = GetLicenseType();
            int engineVolume = GetValidatedInteger("Please enter engine volume (cc):");
            string fuelMotorcycleModel = getVehicleModel("Fuel motorcycle");
            float currentFuelAmount = GetValidatedFloat("Please enter current fuel amount:");
            List<string> wheelsBrandsList = new List<string>();
            List<float> wheelsPressureList = new List<float>();
            int numberOfWheels = 2;
            GetWheelsDetails(ref wheelsBrandsList, ref wheelsPressureList, numberOfWheels);

            FuelMotorcycle fuelMotorcycle = new FuelMotorcycle(
                                                fuelMotorcycleModel,
                                                i_LicenseNumber,
                                                currentFuelAmount,
                                                wheelsPressureList,
                                                wheelsBrandsList,
                                                licenseType,
                                                engineVolume)
                                                { };
            return fuelMotorcycle;
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

        private static eLicenseType? stringToLicenseType(string i_LicenseTypeString)
        {
            var licenseTypesMap = new Dictionary<string, eLicenseType>
                               {
                                   { "1", eLicenseType.A1 },
                                   { "2", eLicenseType.A2 },
                                   { "3", eLicenseType.B1 },
                                   { "4", eLicenseType.B2},
                               };

            return licenseTypesMap.TryGetValue(i_LicenseTypeString, out var choice) ? choice : (eLicenseType?)null;
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

        static void GetWheelsDetails(ref List<string> io_Brands, ref List<float> io_Pressures, int i_NumberOfWheels)
        {
            Console.WriteLine("Are all your wheels the same brand and tire pressure? (y/n):");
            char choice = char.Parse(Console.ReadLine().ToUpper());

            while (choice != 'Y' && choice != 'N')
            {
                Console.WriteLine("Invalid choice. Please enter 'y' or 'n'.");
                choice = char.Parse(Console.ReadLine().ToUpper());
            }

            if (choice == 'Y')
            {
                Console.WriteLine("Enter tire brand:");
                string brand = Console.ReadLine();

                float pressure = GetValidatedFloat("Enter tire pressure:");

                for (int i = 0; i < i_NumberOfWheels; i++)
                {
                    io_Brands.Add(brand);
                    io_Pressures.Add(pressure);
                }
            }
            else
            {
                for (int i = 0; i < i_NumberOfWheels; i++)
                {
                    Console.WriteLine($"Enter tire brand for wheel {i + 1}:");
                    io_Brands.Add(Console.ReadLine());
                    io_Pressures.Add(GetValidatedFloat($"Enter tire pressure for wheel {i + 1}:"));
                }
            }
        }
        
        private static string getVehicleModel(string prompt)
        {
            Console.WriteLine($"Please enter {prompt}'s model");
            string vehicleModel = Console.ReadLine();
            
            return vehicleModel;
        }

        static int GetValidatedInteger(string prompt)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            int value;

            while (!int.TryParse(input, out value) || value <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a positive number.");
                input = Console.ReadLine();
            }

            return value;
        }

        static float GetValidatedFloat(string prompt)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            float value;

            while (!float.TryParse(input, out value) || value < 0)
            {
                Console.WriteLine("Invalid input. Please enter a positive number.");
                input = Console.ReadLine();
            }

            return value;
        }

        static string GetValidNumberInput(string prompt, int min, int max)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            int value;

            while (!int.TryParse(input, out value) || value < min || value > max)
            {
                Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
                input = Console.ReadLine();
            }

            return input;
        }

        static eLicenseType GetLicenseType()
        {
            Console.WriteLine($"Please choose type of license:{Environment.NewLine}1. A1"
                              + $"{Environment.NewLine}2. A2{Environment.NewLine}3. B1"
                              + $"{Environment.NewLine}4. B2");
            string input;
            eLicenseType? licenseType = null;

            while (licenseType == null)
            {
                input = GetValidNumberInput("Enter your choice (1-4):", 1, 4);
                licenseType = stringToLicenseType(input);

                if (licenseType == null)
                {
                    Console.WriteLine("Invalid choice, please try again.");
                }
            }

            return licenseType.Value;
        }
    }
}


