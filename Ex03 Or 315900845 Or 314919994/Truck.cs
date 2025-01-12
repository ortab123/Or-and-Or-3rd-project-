using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text;

namespace Ex03_Or_315900845_Or_314919994
{
    public class Truck : FuelVehicle
    {
        public const float k_MaxTirePressure = 29f;
        public const float k_FuelTankCapacity = 125f;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private bool m_Refrigeration { set; get; }
        private float m_CargoVolume { set; get; }

        public Truck(
            string i_ModelName,
            string i_LicenseNumber,
            float i_CurrentFuelAmount,
            List<float> i_TirePressures,
            List<string> i_TireBrands,
            bool i_Refrigeration,
            float i_CargoVolume)
            : base(i_ModelName, i_LicenseNumber, i_CurrentFuelAmount, k_FuelTankCapacity, k_FuelType)
        {
            if (i_TirePressures.Count != 14 || i_TireBrands.Count != 14)
            {
                throw new ArgumentException("Trucks must have exactly 14 tires.");
            }

            m_Refrigeration = i_Refrigeration;
            m_CargoVolume = i_CargoVolume;
            InitializeWheels(i_TirePressures, i_TireBrands, k_MaxTirePressure);
        }

        public Truck()
        {

        }

        public void SetCargoVolume(float i_CargoVolume)
        {
            if (i_CargoVolume <= 0)
            {
                throw new ArgumentException("Engine volume must be positive.");
            }

            m_CargoVolume = i_CargoVolume;
        }

        public void SetRefrigeration(bool i_Refrigation)
        {
            m_Refrigeration = i_Refrigation;
        }

        public sealed override StringBuilder PrintVehicleDetails()
        {
            string tiresDetails = "Tires:" + Environment.NewLine;
            for (int i = 0; i < m_Wheels.Count; i++)
            {
                tiresDetails += $"   - Tire {i + 1}: {m_Wheels[i].m_CurrentAirPressure} psi, {m_Wheels[i].m_BrandName}" + Environment.NewLine;
            }

            StringBuilder vehicleDetails = new StringBuilder();
            vehicleDetails.AppendLine("Truck Details:");
            vehicleDetails.AppendLine("------------------");
            vehicleDetails.AppendLine($"Model Name: {m_ModelName}");
            vehicleDetails.AppendLine($"License Number: {m_LicenseNumber}");
            vehicleDetails.AppendLine($"Energy Percentage: {m_EnergyPercentage:F2}%");
            vehicleDetails.AppendLine($"Fuel Capacity: {m_FuelTankCapacity:F1} L");
            vehicleDetails.AppendLine($"Current Fuel Left: {m_CurrentFuelTankAmount:F1} L");
            vehicleDetails.AppendLine($"Fuel Type: {m_FuelType}");
            vehicleDetails.AppendLine($"Cargo Volume: {m_CargoVolume}");
            vehicleDetails.AppendLine($"Refrigerated: {(m_Refrigeration == true ? "yes" : "no")}");
            vehicleDetails.Append(tiresDetails);

            return vehicleDetails;
        }
    }
}
