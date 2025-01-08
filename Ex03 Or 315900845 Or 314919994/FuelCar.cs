using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace Ex03_Or_315900845_Or_314919994
{
    internal class FuelCar : FuelVehicle
    {
        private const float k_MaxTirePressure = 34f;
        private const float k_FuelTankCapacity = 52f;
        private const eFuelType k_FuelType = eFuelType.Octan95;
        private eCarColor m_Color { get; set; }
        private eDoorsNumber m_DoorsNumber { get; set; }

        public FuelCar(
            string i_ModelName,
            string i_LicenseNumber,
            float i_CurrentFuelAmount,
            List<float> i_TirePressures,
            List<string> i_TireBrands,
            eCarColor i_Color,
            eDoorsNumber i_DoorsNumber)
            : base(i_ModelName, i_LicenseNumber, i_CurrentFuelAmount, k_FuelTankCapacity, k_FuelType)
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
            vehicleDetails.AppendLine("Fuel Car Details:");
            vehicleDetails.AppendLine("------------------");
            vehicleDetails.AppendLine($"Model Name: {m_ModelName}");
            vehicleDetails.AppendLine($"License Number: {m_LicenseNumber}");
            vehicleDetails.AppendLine($"Energy Percentage: {m_EnergyPercentage:F2}%");
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
