using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Ex03_Or_315900845_Or_314919994
{
    namespace GarageLogic
    {
        public static class VehicleFactory
        {
            public static Vehicle CreateVehicle(eVehicleType i_VehicleType, string i_LicenseNumber, Dictionary<string, object> i_AdditionalParameters)
            {
                Vehicle vehicle = null;

                if (i_VehicleType == eVehicleType.FuelMotorcycle)
                {
                    vehicle = new FuelMotorcycle();
                }
                else if (i_VehicleType == eVehicleType.ElectricMotorcycle)
                {
                    vehicle = new ElectricMotorcycle();
                }
                else if (i_VehicleType == eVehicleType.FuelCar)
                {
                    vehicle = new FuelCar();
                }
                else if (i_VehicleType == eVehicleType.ElectricCar)
                {
                    vehicle = new ElectricCar();
                }
                else if (i_VehicleType == eVehicleType.Truck)
                {
                    vehicle = new Truck();
                }
                else if(i_VehicleType == eVehicleType.Other)
                {
                    vehicle = new Other();
                }
                else
                {
                    throw new ArgumentException("Invalid vehicle type.");
                }

                initializeVehicle(vehicle, i_LicenseNumber, i_AdditionalParameters);
                initializeWheels(vehicle, i_AdditionalParameters);

                return vehicle;
            }
            
            private static void initializeVehicle(Vehicle i_Vehicle, string i_LicenseNumber, Dictionary<string, object> i_AdditionalParameters)
            {
                i_Vehicle.m_LicenseNumber = i_LicenseNumber;

                if (i_Vehicle is FuelVehicle fuelVehicle)
                {
                    initializeFuelVehicle(fuelVehicle, i_AdditionalParameters);
                }
                else if (i_Vehicle is ElectricVehicle electricVehicle)
                {
                    initializeBatteryVehicle(electricVehicle, i_AdditionalParameters);
                }

                if (i_Vehicle is FuelMotorcycle || i_Vehicle is ElectricMotorcycle)
                {
                    int engineVolume = (int)i_AdditionalParameters["EngineVolume"];
                    eLicenseType licenseType = (eLicenseType)i_AdditionalParameters["LicenseType"];

                    if (i_Vehicle is FuelMotorcycle fuelMotorcycle)
                    {
                        fuelMotorcycle.SetEngineVolume(engineVolume);
                        fuelMotorcycle.SetLicenseType(licenseType);
                    }
                    else if (i_Vehicle is ElectricMotorcycle electricMotorcycle)
                    {
                        electricMotorcycle.SetEngineVolume(engineVolume);
                        electricMotorcycle.SetLicenseType(licenseType);
                    }
                }

                if (i_Vehicle is FuelCar || i_Vehicle is ElectricCar)
                {
                    eCarColor carColor = (eCarColor)i_AdditionalParameters["CarColor"];
                    eDoorsNumber doorsNumber = (eDoorsNumber)i_AdditionalParameters["DoorsNumber"];
                    if (i_Vehicle is FuelCar fuelCar)
                    {
                        fuelCar.SetCarColor(carColor);
                        fuelCar.SetCarDoorsAmount(doorsNumber);
                    }
                    else if (i_Vehicle is ElectricCar electricCar)
                    {
                        electricCar.SetCarColor(carColor);
                        electricCar.SetCarDoorsAmount(doorsNumber);
                    }
                }

                if (i_Vehicle is Truck truck)
                {
                    float cargoVolume = (float)i_AdditionalParameters["CargoVolume"];
                    bool refrigeration = (bool)i_AdditionalParameters["Refrigeration"];
                    truck.SetCargoVolume(cargoVolume);
                    truck.SetRefrigeration(refrigeration);
                }

                i_Vehicle.SetEnergyPercentage();
            }

            private static void initializeWheels(Vehicle i_Vehicle, Dictionary<string, object> i_AdditionalParameters)
            {
                try
                {
                    int wheelsCount = i_Vehicle.GetWheelsNumber();
                    float maxPressure = i_Vehicle.GetMaxTirePressure();

                    List<Wheels> wheelsList = new List<Wheels>();

                    if ((bool)i_AdditionalParameters["UniformWheels"])
                    {
                        string manufacturer = validateParameter<string>(i_AdditionalParameters, "WheelManufacturer");
                        float currentPressure = validateParameter<float>(i_AdditionalParameters, "WheelCurrentPressure");

                        if (currentPressure > maxPressure)
                        {
                            throw new ValueOutOfRangeException(0, maxPressure, "Current pressure");
                        }

                        for (int i = 0; i < wheelsCount; i++)
                        {
                            Wheels wheel = new Wheels(maxPressure, currentPressure, manufacturer);
                            wheelsList.Add(wheel);
                        }
                    }
                    else
                    {
                        List<Dictionary<string, object>> individualWheels = validateParameter<List<Dictionary<string, object>>>(i_AdditionalParameters, "IndividualWheels");

                        for (int i = 0; i < wheelsCount; i++)
                        {
                            string manufacturer = (string)individualWheels[i]["WheelManufacturer"];
                            float currentPressure = (float)individualWheels[i]["WheelCurrentPressure"];

                            if (currentPressure > maxPressure)
                            {
                                throw new ValueOutOfRangeException(0, maxPressure, $"Current pressure for wheel {i + 1}");
                            }

                            Wheels wheel = new Wheels(maxPressure, currentPressure, manufacturer);
                            wheelsList.Add(wheel);
                        }
                    }

                    i_Vehicle.SetWheels(wheelsList);
                }
                catch (KeyNotFoundException ex)
                {
                    throw new ArgumentException("A required parameter is missing in the additional parameters dictionary.", ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred while initializing wheels: {ex.Message}");
                    throw;
                }
            }

            public static Vehicle InitializeNewVehicle(
            string i_VehicleTypeName,
            int i_WheelCount,
            bool i_IsElectric,
            string i_ModelName,
            string i_LicenseNumber,
            float i_EnergyPercentage,
            List<Wheels> i_Wheels)
            {
                Vehicle newVehicle;

                // יצירת רכב מסוג 'Other'
                newVehicle = new Other()
                {
                    m_ModelName = i_ModelName,
                    m_LicenseNumber = i_LicenseNumber,
                    m_EnergyPercentage = i_EnergyPercentage
                };

                // הגדרת הגלגלים
                if (i_Wheels.Count != i_WheelCount)
                {
                    throw new ArgumentException("The number of wheels provided does not match the specified wheel count.");
                }
                newVehicle.SetWheels(i_Wheels);

                // הגדרת סוג האנרגיה
                if (i_IsElectric)
                {
                    ((Other)newVehicle).SetElectricEngine();
                }
                else
                {
                    ((Other)newVehicle).SetFuelEngine();
                }

                return newVehicle;
            }


            private static T validateParameter<T>(Dictionary<string, object> i_Parameters, string i_Key)
            {
                if (!i_Parameters.ContainsKey(i_Key))
                {
                    throw new KeyNotFoundException($"The parameter '{i_Key}' is missing.");
                }

                return (T)i_Parameters[i_Key];
            }

            private static void initializeFuelVehicle(FuelVehicle i_Vehicle, Dictionary<string, object> i_AdditionalParameters)
            {
                i_Vehicle.SetFuelType((eFuelType)i_AdditionalParameters["FuelType"]);
                i_Vehicle.SetMaxFuelAmount((float)i_AdditionalParameters["MaxFuelAmount"]);
                i_Vehicle.SetCurrentFuelAmount((float)i_AdditionalParameters["CurrentFuelAmount"]);
            }

            private static void initializeBatteryVehicle(ElectricVehicle i_Vehicle, Dictionary<string, object> i_AdditionalParameters)
            {
                i_Vehicle.SetMaxBatteryAmount((float)i_AdditionalParameters["MaxBatteryAmount"]);
                i_Vehicle.SetCurrentEnergy((float)i_AdditionalParameters["CurrentBatteryAmount"]);
            }

        }
    }

}
