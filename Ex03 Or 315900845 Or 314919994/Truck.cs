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

        public Truck()
        {
            setFuelType();
        }

        private void setFuelType()
        {
            m_FuelType = k_FuelType;
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
                tiresDetails += $"   - Tire {i + 1}: {m_Wheels[i].m_CurrentAirPressure} psi, {m_Wheels[i].m_ManufacturerName}" + Environment.NewLine;
            }

            StringBuilder vehicleDetails = new StringBuilder();
            vehicleDetails.AppendLine("Truck Details:");
            vehicleDetails.AppendLine("------------------");
            vehicleDetails.AppendLine($"Model Name: {m_ModelName}");
            vehicleDetails.AppendLine($"License Number: {m_LicenseNumber}");
            vehicleDetails.AppendLine($"Energy Percentage: {m_EnergyPercentage * 100:F2}%");
            vehicleDetails.AppendLine($"Fuel Capacity: {k_FuelTankCapacity:F1} L");
            vehicleDetails.AppendLine($"Current Fuel Left: {m_CurrentFuelTankAmount:F1} L");
            vehicleDetails.AppendLine($"Fuel Type: {m_FuelType}");
            vehicleDetails.AppendLine($"Cargo Volume: {m_CargoVolume}");
            vehicleDetails.AppendLine($"Refrigerated: {(m_Refrigeration == true ? "yes" : "no")}");
            vehicleDetails.Append(tiresDetails);

            return vehicleDetails;
        }
    }
}
