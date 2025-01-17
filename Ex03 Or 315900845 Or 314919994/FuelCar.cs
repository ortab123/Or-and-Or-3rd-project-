using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace Ex03_Or_315900845_Or_314919994
{
    public class FuelCar : FuelVehicle
    {
        public const float k_MaxTirePressure = 34f;
        public const float k_FuelTankCapacity = 52f;
        private const eFuelType k_FuelType = eFuelType.Octan95;
        private const int k_WheelsNumber = 5;

        private eCarColor m_Color { get; set; }
        private eDoorsNumber m_DoorsNumber { get; set; }

        public FuelCar()
        {
            m_FuelType = k_FuelType;
            m_FuelTankCapacity = k_FuelTankCapacity;
        }

        public override int GetWheelsNumber()
        {
            return k_WheelsNumber;
        }

        public override float GetMaxTirePressure()
        {
            return k_MaxTirePressure;
        }

        public void SetCarColor(eCarColor i_CarColor)
        {
            m_Color = i_CarColor;
        }

        public void SetCarDoorsAmount(eDoorsNumber i_DoorsNumber)
        {
            m_DoorsNumber = i_DoorsNumber;
        }

        public sealed override StringBuilder PrintVehicleDetails()
        {
            string tiresDetails = "Tires:" + Environment.NewLine;
            for (int i = 0; i < m_Wheels.Count; i++)
            {
                tiresDetails += $"   - Tire {i + 1}: {m_Wheels[i].m_CurrentAirPressure} psi, {m_Wheels[i].m_ManufacturerName}" + Environment.NewLine;
            }

            StringBuilder vehicleDetails = new StringBuilder();
            vehicleDetails.AppendLine("Fuel Car Details:");
            vehicleDetails.AppendLine("------------------");
            vehicleDetails.AppendLine($"Model Name: {m_ModelName}");
            vehicleDetails.AppendLine($"License Number: {m_LicenseNumber}");
            vehicleDetails.AppendLine($"Energy Percentage: {m_EnergyPercentage * 100:F2}%");
            vehicleDetails.AppendLine($"Fuel Capacity: {m_FuelTankCapacity:F1} L");
            vehicleDetails.AppendLine($"Current Fuel Left: {m_CurrentFuelTankAmount:F1} L");
            vehicleDetails.AppendLine($"Fuel Type: {m_FuelType}");
            vehicleDetails.AppendLine($"Color: {m_Color}");
            vehicleDetails.AppendLine($"Doors: {m_DoorsNumber}");
            vehicleDetails.AppendLine(tiresDetails);

            return vehicleDetails;
        }
    }
}
