using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03_Or_315900845_Or_314919994
{
    public class ElectricMotorcycle : ElectricVehicle
    {
        public const float k_MaxTirePressure = 32f;
        public const float k_BatteryTimeCapacity = 2.9F;
        private const int k_WheelsNumber = 2;

        private eLicenseType m_LicenseType { get; set; }
        private int m_EngineVolume { get; set; }

        public ElectricMotorcycle()
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

        public void SetLicenseType(eLicenseType i_LicenseType)
        {
            m_LicenseType = i_LicenseType;
        }

        public void SetEngineVolume(int i_EngineVolume)
        {
            if (i_EngineVolume <= 0)
            {
                throw new ArgumentException("Engine volume must be positive.");
            }

            m_EngineVolume = i_EngineVolume;
        }

        public sealed override StringBuilder PrintVehicleDetails()
        {
            string tiresDetails = "Tires:" + Environment.NewLine;
            for (int i = 0; i < m_Wheels.Count; i++)
            {
                tiresDetails += $"   - Tire {i + 1}: {m_Wheels[i].m_CurrentAirPressure} psi, {m_Wheels[i].m_ManufacturerName}" + Environment.NewLine;
            }

            StringBuilder vehicleDetails = new StringBuilder();
            vehicleDetails.AppendLine("Electric Motorcycle Details:");
            vehicleDetails.AppendLine("----------------------");
            vehicleDetails.AppendLine($"Model Name: {m_ModelName}");
            vehicleDetails.AppendLine($"License Number: {m_LicenseNumber}");
            vehicleDetails.AppendLine($"Energy Percentage: {m_EnergyPercentage * 100:F2}%");
            vehicleDetails.AppendLine($"Battery Capacity: {m_BatteryTimeCapacity:F1} H");
            vehicleDetails.AppendLine($"Current Battery: {m_CurrentBatteryTimeLeft:F1} H");
            vehicleDetails.AppendLine($"License Type: {m_LicenseType}");
            vehicleDetails.AppendLine($"Engine Volume: {m_EngineVolume}");
            vehicleDetails.AppendLine(tiresDetails);

            return vehicleDetails;
        }
    }
}
