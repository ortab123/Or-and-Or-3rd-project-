using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03_Or_315900845_Or_314919994
{
    public class ElectricCar : ElectricVehicle
    {
        public const float k_MaxTirePressure = 34f;
        public const float k_BatteryTimeCapacity = 5.4f;
        private const int k_WheelsNumber = 5;
        private eCarColor m_Color { get; set; }
        private eDoorsNumber m_DoorsNumber { get; set; }

        public ElectricCar()
        {
            m_BatteryTimeCapacity = k_BatteryTimeCapacity;
        }

        public override int GetWheelsNumber()
        {
            return k_WheelsNumber;
        }

        public override float GetMaxTirePressure()
        {
            return k_MaxTirePressure;
        }

        public override float GetMaxEnergy()
        {
            return k_BatteryTimeCapacity;
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
            vehicleDetails.AppendLine("Electric Car Details:");
            vehicleDetails.AppendLine("----------------------");
            vehicleDetails.AppendLine($"Model Name: {m_ModelName}");
            vehicleDetails.AppendLine($"License Number: {m_LicenseNumber}");
            vehicleDetails.AppendLine($"Energy Percentage: {m_EnergyPercentage * 100:F2}%");
            vehicleDetails.AppendLine($"Battery Capacity: {m_BatteryTimeCapacity:F1} H");
            vehicleDetails.AppendLine($"Current Battery: {m_CurrentBatteryTimeLeft:F1} H");
            vehicleDetails.AppendLine($"Color: {m_Color}");
            vehicleDetails.AppendLine($"Doors: {m_DoorsNumber}");
            vehicleDetails.AppendLine(tiresDetails);

            return vehicleDetails;
        }
    }
}
