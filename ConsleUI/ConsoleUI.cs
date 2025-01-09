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
            while (!validateNumber(customerChoice))
            {
                Console.WriteLine("Invalid choice please try again.");
                customerChoice = Console.ReadLine();
            }

            Ex03_Or_315900845_Or_314919994.Garage garage = new Garage();

            switch (stringToChoice(customerChoice))
            {
                case eChoice.Insert:
                    string licenseNumber = Console.ReadLine();

                    while (!validateLicenseNumber(licenseNumber))
                    {
                        Console.WriteLine("Invalid license number please try again.");
                        licenseNumber = Console.ReadLine();
                    }

                    string foundVehicleInGarage = Ex03_Or_315900845_Or_314919994.Garage.FindVehicleInGarage(ref garage, licenseNumber);

                    if(foundVehicleInGarage == null)
                    {
                        Console.WriteLine($"Please choose type of vehicle:{Environment.NewLine}1. FuelMotorcycle "
                                          + $"{Environment.NewLine}2. ElectricMotorcycle{Environment.NewLine}3. FuelCar"
                                          + $"{Environment.NewLine}4. ElectricCar{Environment.NewLine}5. Truck");
                        string vehicleTypeString = Console.ReadLine();

                        switch(stringToVehicleType(vehicleTypeString))
                        {
                            case eVehicleType.FuelMotorcycle:
                                Console.WriteLine($"Please choose type of license:{Environment.NewLine}1. A1"
                                                  + $"{Environment.NewLine}2. A2{Environment.NewLine}3. B1"
                                                  + $"{Environment.NewLine}4. B2");
                                string licenseTypeString = Console.ReadLine();
                                eLicenseType licenseType;

                                switch (stringToLicenseType(licenseTypeString))
                                {
                                    case eLicenseType.A1:
                                        licenseType = eLicenseType.A1;
                                        break;
                                    case eLicenseType.A2:
                                        licenseType = eLicenseType.A2;
                                        break;
                                    case eLicenseType.B1:
                                        licenseType = eLicenseType.B1;
                                        break;
                                    case eLicenseType.B2:
                                        licenseType = eLicenseType.B2;
                                        break;
                                }

                                Console.WriteLine("Please enter engine volume (cc):");
                                string engineVolumeString = Console.ReadLine();
                                int engineVolume;

                                while (!int.TryParse(engineVolumeString, out engineVolume) || engineVolume < 0)
                                {
                                    Console.WriteLine("Invalid volume, try again:");
                                    engineVolumeString = Console.ReadLine();
                                }

                                Console.WriteLine("Please enter MotorcycleModel:");
                                string fuelMotorcycleModel = Console.ReadLine();

                                Console.WriteLine("Please enter current fuel amount:");
                                string currentFuelString = Console.ReadLine();
                                float currentFuelAmount;

                                while (!float.TryParse(currentFuelString, out currentFuelAmount)
                                      || currentFuelAmount < 0)
                                {
                                    Console.WriteLine("Invalid fuel amount, try again:");
                                    currentFuelString = Console.ReadLine();
                                }

                                List<string> wheelsBrandsList = new List<string>();
                                List<float> wheelsPressureList = new List<float>();
                                int numberOfWheels = 2;

                                Console.WriteLine("Are all your wheels has the same brand and tire pressure (y/n) ?");
                                char allTheSame = char.Parse(Console.ReadLine().ToUpper());

                                while(allTheSame != 'N' || allTheSame != 'Y')
                                {
                                    Console.WriteLine("Invalid choice, try again:");
                                    allTheSame = char.Parse(Console.ReadLine().ToUpper());
                                }

                                if(allTheSame == 'Y')
                                {
                                    Console.WriteLine("Please enter tire brand:");
                                    string oneBrandWheels = Console.ReadLine();

                                    Console.WriteLine("Please enter tire pressure for wheels:");
                                    float wheelPressure;

                                    while (!float.TryParse(Console.ReadLine(), out wheelPressure)
                                           || wheelPressure < 0)
                                    {
                                        Console.WriteLine("Invalid pressure amount, please enter a positive number:");
                                    }

                                    for(int i = 0; i < numberOfWheels; i++)
                                    {
                                       wheelsBrandsList.Add(oneBrandWheels);
                                       wheelsPressureList.Add(wheelPressure);
                                    }
                                }
                                else
                                {
                                    for(int i = 0; i < numberOfWheels; i++)
                                    {
                                        Console.WriteLine($"Please enter tire brand for wheel {i + 1}: ");
                                        wheelsBrandsList.Add(Console.ReadLine());
                                        Console.WriteLine($"Please enter tire pressure for wheel {i + 1}: ");
                                        float wheelPressure;

                                        while(!float.TryParse(Console.ReadLine(), out wheelPressure)
                                              || wheelPressure < 0)
                                        {
                                            Console.WriteLine("Invalid pressure amount, please enter a positive number:");
                                        }

                                        wheelsPressureList.Add(wheelPressure);
                                    }
                                }

                                FuelMotorcycle fuelMotorcycle = new FuelMotorcycle(
                                                                    fuelMotorcycleModel,
                                                                    licenseNumber,
                                                                    currentFuelAmount,
                                                                    wheelsPressureList,
                                                                    wheelsBrandsList,
                                                                    licenseType,
                                                                    engineVolume) { };
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

    }
}


