﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03_Or_315900845_Or_314919994
{
    internal class ElectricCar : ElectricVehicle
    {
        private const float k_MaxTirePressure = 34f;
        private const float k_BatteryTimeCapacity = 5.4f;
        private eCarColor m_Color { get; set; }
        private eDoorsNumber m_DoorsNumber { get; set; }

        public ElectricCar(
            string i_ModelName,
            string i_LicenseNumber,
            float i_BatteryCurrentAmount,
            List<float> i_TirePressures,
            List<string> i_TireBrands,
            eCarColor i_Color,
            eDoorsNumber i_DoorsNumber)
            : base(i_ModelName, i_LicenseNumber, i_BatteryCurrentAmount, k_BatteryTimeCapacity)
        {
            if (i_TirePressures.Count != 5 || i_TireBrands.Count != 5)
            {
                throw new ArgumentException("Cars must have exactly 5 tires (4 main + 1 reserve).");
            }

            m_Color = i_Color;
            m_DoorsNumber = i_DoorsNumber;
            InitializeWheels(i_TirePressures, i_TireBrands, k_MaxTirePressure);
        }

        public sealed override StringBuilder PrintVehicleDetails()
        {
            string tiresDetails = "Tires:" + Environment.NewLine;
            for (int i = 0; i < m_Wheels.Count; i++)
            {
                tiresDetails += $"   - Tire {i + 1}: {m_Wheels[i].m_CurrentAirPressure} psi, {m_Wheels[i].m_BrandName}" + Environment.NewLine;
            }

            StringBuilder vehicleDetails = new StringBuilder();
            vehicleDetails.AppendLine("Electric Car Details:");
            vehicleDetails.AppendLine("----------------------");
            vehicleDetails.AppendLine($"Model Name: {m_ModelName}");
            vehicleDetails.AppendLine($"License Number: {m_LicenseNumber}");
            vehicleDetails.AppendLine($"Energy Percentage: {m_EnergyPercentage:F2}%");
            vehicleDetails.AppendLine($"Battery Capacity: {m_BatteryTimeCapacity:F1} H");
            vehicleDetails.AppendLine($"Current Battery: {m_CurrentBatteryTimeLeft:F1} H");
            vehicleDetails.AppendLine($"Color: {m_Color}");
            vehicleDetails.AppendLine($"Doors: {m_DoorsNumber}");
            vehicleDetails.AppendLine(tiresDetails);

            return vehicleDetails;
        }
    }
}
