using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Ex03_Or_315900845_Or_314919994
{
    public class Garage
    {
        private static readonly Garage sr_Instance = new Garage();
        private readonly List<VehicleInGarage> r_VehiclesInGarage;

        public Garage()
        {
            r_VehiclesInGarage = new List<VehicleInGarage>();
        }

        public static Garage GetInstance()
        {
            return sr_Instance;
        }

        public void AddVehicle(VehicleInGarage i_VehicleInGarage)
        {
            if (!FindVehicleInGarage(i_VehicleInGarage.m_Vehicle.m_LicenseNumber))
            {
                r_VehiclesInGarage.Add(i_VehicleInGarage);
            }
            else
            {
                throw new ArgumentException("A vehicle with this license number already exists in the garage.");
            }
        }

        public bool FindVehicleInGarage(string i_LicenseNumber)
        {
            bool isFound = false;

            foreach (VehicleInGarage vehicleInGarage in r_VehiclesInGarage)
            {
                if (vehicleInGarage.m_Vehicle.m_LicenseNumber == i_LicenseNumber)
                {
                    isFound = true;
                }
            }

            return isFound;
        }

        public void UpdateVehicleStatus(string i_LicenseNumber, eVehicleStatus i_NewStatus)
        {
            foreach (VehicleInGarage vehicle in r_VehiclesInGarage)
            {
                if (vehicle.m_Vehicle.m_LicenseNumber == i_LicenseNumber)
                {
                    vehicle.m_Status = i_NewStatus;
                    return;
                }
            }

            throw new ArgumentException("Vehicle with the given license number was not found in the garage.");
        }

        public List<string> FindVehiclesByStatus(eVehicleStatus i_Status)
        {
            List<string> licenseNumbers = new List<string>();
            foreach(VehicleInGarage vehicleInGarage in r_VehiclesInGarage)
            {
                if (vehicleInGarage.m_Status == i_Status)
                {
                    licenseNumbers.Add(vehicleInGarage.m_Vehicle.m_LicenseNumber);
                }
            }

            return licenseNumbers;
        }

        public bool InflateVehicleWheels(string i_LicenseNumber)
        {
            bool isInflated = false;
            foreach (VehicleInGarage vehicleInGarage in r_VehiclesInGarage)
            {
                if (vehicleInGarage.m_Vehicle.m_LicenseNumber == i_LicenseNumber)
                {
                    vehicleInGarage.m_Vehicle.InflateAllWheelsToMax();
                    isInflated = true ;
                }
            }

            return isInflated;
        }


    }
}
