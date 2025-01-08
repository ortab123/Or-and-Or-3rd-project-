using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03_Or_315900845_Or_314919994
{
    internal class ElectricMotorcycle : ElectricVehicle
    {
        private const float k_MaxTirePressure = 32f;
        private const float k_BatteryTimeCapacity = 2.9F;

        private eLicenseType m_LicenseType { get; set; }
        private int m_EngineVolume { get; set; }

        public ElectricMotorcycle(
            string i_ModelName,
            string i_LicenseNumber,
            float i_BatteryCurrentAmount,
            List<float> i_TirePressures,
            List<string> i_TireBrands,
            eLicenseType i_LicenseType,
            int i_EngineVolume)
            : base(i_ModelName, i_LicenseNumber, i_BatteryCurrentAmount, k_BatteryTimeCapacity)
        {
            // NEED TO CHECK! ValueOutOfRange?
            if (i_EngineVolume <= 0)
            {
                throw new ArgumentException("Engine volume must be positive.");
            }

            if (i_TirePressures.Count != 2 || i_TireBrands.Count != 2)
            {
                throw new ArgumentException("Motorcycles must have exactly 2.");
            }

            m_LicenseType = i_LicenseType;
            m_EngineVolume = i_EngineVolume;
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
            vehicleDetails.AppendLine("Electric Motorcycle Details:");
            vehicleDetails.AppendLine("----------------------");
            vehicleDetails.AppendLine($"Model Name: {m_ModelName}");
            vehicleDetails.AppendLine($"License Number: {m_LicenseNumber}");
            vehicleDetails.AppendLine($"Energy Percentage: {m_EnergyPercentage:F2}%");
            vehicleDetails.AppendLine($"Battery Capacity: {m_BatteryTimeCapacity:F1} H");
            vehicleDetails.AppendLine($"Current Battery: {m_CurrentBatteryTimeLeft:F1} H");
            vehicleDetails.AppendLine($"License Type: {m_LicenseType}");
            vehicleDetails.AppendLine($"Engine Volume: {m_EngineVolume}");
            vehicleDetails.AppendLine(tiresDetails);

            return vehicleDetails;
        }
    }
}
