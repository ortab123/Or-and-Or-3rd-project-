using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03_Or_315900845_Or_314919994
{
    public class FuelMotorcycle : FuelVehicle
    {
        public const float k_MaxTirePressure = 32f;
        public const float k_FuelTankCapacity = 6.2f;
        private const eFuelType k_FuelType = eFuelType.Octan98;
        private eLicenseType m_LicenseType { get; set; }
        private int m_EngineVolume { get; set; }

        public FuelMotorcycle()
        {
            setFuelType();
        }

        private void setFuelType()
        {
            m_FuelType = k_FuelType;
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
            vehicleDetails.AppendLine("Fuel Motorcycle Details:");
            vehicleDetails.AppendLine("------------------");
            vehicleDetails.AppendLine($"Model Name: {m_ModelName}");
            vehicleDetails.AppendLine($"License Number: {m_LicenseNumber}");
            vehicleDetails.AppendLine($"Energy Percentage: {m_EnergyPercentage * 100:F2}%");
            vehicleDetails.AppendLine($"Fuel Capacity: {k_FuelTankCapacity:F1} L");
            vehicleDetails.AppendLine($"Current Fuel Left: {m_CurrentFuelTankAmount:F1} L");
            vehicleDetails.AppendLine($"Fuel Type: {m_FuelType}");
            vehicleDetails.AppendLine($"License Type: {m_LicenseType}");
            vehicleDetails.AppendLine($"Engine Volume: {m_EngineVolume}");
            vehicleDetails.AppendLine(tiresDetails);

            return vehicleDetails;
        }
    }
}