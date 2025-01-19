using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03_Or_315900845_Or_314919994
{
    public class Other : Vehicle
    {
        public bool m_IsElectric;

        public eFuelType? m_FuelType { get; private set; }
        public float m_MaxEnergyCapacity { get; private set; }
        public float m_CurrentEnergy { get; private set; }

        private string m_TypeName;
        public int m_NumberOfWheels { get; private set; }

        public override eVehicleType GetEVehicleType()
        {
            return eVehicleType.Other;
        }

        public void SetTypeName(string i_TypeName)
        {
            m_TypeName = i_TypeName;
        }

        public void SetIsElectricEngine(bool i_IsElectric)
        {
            m_IsElectric = i_IsElectric;
        }

        public override int GetWheelsNumber()
        {
            return m_NumberOfWheels;
        }

        public override float GetMaxTirePressure()
        {
            if (m_Wheels.Any())
            {
                return Wheels.GetMaxWheelPressure(this);
            }
            else
            {
                throw new InvalidOperationException("No wheels are defined for this vehicle.");
            }
        }

        public override float GetCurrentEnergy()
        {
            return m_CurrentEnergy;
        }

        public void SetCurrentEnergy(float i_CurrentEnergy)
        {
            m_CurrentEnergy = i_CurrentEnergy;
        }

        public override float GetMaxEnergy()
        {
            return m_MaxEnergyCapacity;
        }

        public void SetMaxEnergy(float i_MaxEnergy)
        {
            m_MaxEnergyCapacity = i_MaxEnergy;
        }

        public void SetFuelType(eFuelType i_FuelType)
        {
            m_FuelType = i_FuelType;
        }

        public eFuelType? GetFuelType()
        {
            return m_FuelType;
        }

        public void Refuel(float i_FuelAmount, eFuelType i_FuelType)
        {
            if (i_FuelAmount < 0 || GetCurrentEnergy() + i_FuelAmount > GetMaxEnergy())
            {
                throw new ValueOutOfRangeException(0, (GetMaxEnergy() - GetCurrentEnergy()), $"The value is not in the range {0} to {(GetMaxEnergy() - GetCurrentEnergy())}.");
            }

            if (i_FuelType != GetFuelType())
            {
                throw new ArgumentException("Please use the correct type of fuel");
            }

            m_CurrentEnergy += i_FuelAmount;
            SetEnergyPercentage();
        }

        public void Recharge(float i_Hours)
        {
            if (i_Hours < 0 || GetCurrentEnergy() + i_Hours > GetMaxEnergy())
            {
                throw new ValueOutOfRangeException(0, (GetMaxEnergy() - GetCurrentEnergy()), $"The value is not in the range {0} to {(GetMaxEnergy() - GetCurrentEnergy())}.");
            }

            m_CurrentEnergy += i_Hours;
            SetEnergyPercentage();
        }

        public sealed override StringBuilder PrintVehicleDetails()
        {
            string tiresDetails = "Tires:" + Environment.NewLine;
            for (int i = 0; i < m_Wheels.Count; i++)
            {
                tiresDetails += $"   - Tire {i + 1}: {m_Wheels[i].m_CurrentAirPressure} psi, {m_Wheels[i].m_ManufacturerName}" + Environment.NewLine;
            }

            StringBuilder vehicleDetails = new StringBuilder();
            vehicleDetails.AppendLine($"{m_TypeName} Details:");
            vehicleDetails.AppendLine("----------------------");
            vehicleDetails.AppendLine($"Model Name: {m_ModelName}");
            vehicleDetails.AppendLine($"License Number: {m_LicenseNumber}");
            vehicleDetails.AppendLine($"Energy Percentage: {m_EnergyPercentage * 100:F2}%");
            vehicleDetails.AppendLine($"Electric: {m_IsElectric}");
            if (m_IsElectric)
            {
                vehicleDetails.AppendLine($"Battery Capacity: {m_MaxEnergyCapacity:F1} H");
                vehicleDetails.AppendLine($"Current Battery: {m_CurrentEnergy:F1} H");
            }
            else
            {
                vehicleDetails.AppendLine($"Fuel type: {m_FuelType}");
                vehicleDetails.AppendLine($"Fuel Capacity: {m_MaxEnergyCapacity:F1} L");
                vehicleDetails.AppendLine($"Current Fuel Left: {m_CurrentEnergy:F1} L");
            }

            vehicleDetails.AppendLine(tiresDetails);

            return vehicleDetails;
        }
    }


}
