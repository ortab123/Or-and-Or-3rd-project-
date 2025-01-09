using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03_Or_315900845_Or_314919994
{
    public abstract class Vehicle
    {
        protected string m_ModelName { get; set; }
        public string m_LicenseNumber { get; set; }
        protected float m_EnergyPercentage { get; set; }
        protected List<Wheels> m_Wheels { get; set; } = new List<Wheels>();

        protected Vehicle(string i_ModelName, string i_LicenseNumber)
        {
            m_ModelName = i_ModelName;
            m_LicenseNumber = i_LicenseNumber;
            m_EnergyPercentage = getEnergyPercentage();
        }

        public static void ValidateTirePressure(float i_TirePressure, float i_TireMaxPressure)
        {
            if (i_TirePressure < 0 || i_TirePressure > i_TireMaxPressure)
            {
                throw new ValueOutOfRangeException(0, i_TireMaxPressure);
            }
        }

        protected void InitializeWheels(List<float> i_TirePressures, List<string> i_TireBrands, float i_MaxTirePressure)
        {
            if (i_TirePressures.Count != i_TireBrands.Count)
            {
                throw new ArgumentException("Tire pressures and brands must have the same number of elements.");
            }

            for (int i = 0; i < i_TirePressures.Count; i++)
            {
                ValidateTirePressure(i_TirePressures[i], i_MaxTirePressure);
                m_Wheels.Add(new Wheels(i_MaxTirePressure, i_TirePressures[i], i_TireBrands[i]));
            }
        }

        private float getEnergyPercentage()
        {
            return (GetCurrentEnergy() / GetMaxEnergy()) * 100;
        }

        protected abstract float GetCurrentEnergy();

        protected abstract float GetMaxEnergy();

        public abstract StringBuilder PrintVehicleDetails();
    }
}