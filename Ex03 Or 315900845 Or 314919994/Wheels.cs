using System;
using System.Collections.Generic;

namespace Ex03_Or_315900845_Or_314919994
{
    public class Wheels
    {
        public string m_BrandName { get; set; }
        private float m_MaxAirPressure { get; set; }
        public float m_CurrentAirPressure { get; set; }

        public Wheels(float i_MaxAirPressure, float i_CurrentAirPressure, string i_BrandName)
        {
            m_MaxAirPressure = i_MaxAirPressure;
            m_BrandName = i_BrandName;
            m_CurrentAirPressure = i_CurrentAirPressure;
        }

        public Wheels()
        {

        }

        public static void ValidateTirePressure(float i_TirePressure, float i_TireMaxPressure)
        {
            if (i_TirePressure < 0 || i_TirePressure > i_TireMaxPressure)
            {
                throw new ValueOutOfRangeException(0, i_TireMaxPressure);
            }
        } 

        public void SetTireCurrentPressure(float i_TirePressure)
        {
            m_CurrentAirPressure = i_TirePressure;
        }

        public void SetTireMaxPressure(float i_TireMaxPressure)
        {
            m_MaxAirPressure = i_TireMaxPressure;
        }

        public static float GetMaxWheelPressure(Vehicle i_Vehicle)
        {
            float maxWheelPressure;

            if (i_Vehicle is FuelMotorcycle)
            {
                maxWheelPressure = FuelMotorcycle.k_MaxTirePressure;
            }
            else if (i_Vehicle is FuelCar)
            {
                maxWheelPressure = FuelCar.k_MaxTirePressure;
            }
            else if (i_Vehicle is Truck)
            {
                maxWheelPressure = Truck.k_MaxTirePressure;
            }
            else if (i_Vehicle is ElectricMotorcycle)
            {
                maxWheelPressure = ElectricMotorcycle.k_MaxTirePressure;
            }
            else if (i_Vehicle is ElectricCar)
            {
                maxWheelPressure = ElectricCar.k_MaxTirePressure;
            }
            else
            {
                throw new ArgumentException("Unsupported vehicle type.");
            }

            return maxWheelPressure;
        }

        public static void SetAllWheelsDetails(List<Wheels> i_WheelsList, string i_Brand, 
            float i_PressureInput, Vehicle i_Vehicle)
        {
            ValidateTirePressure(i_PressureInput, GetMaxWheelPressure(i_Vehicle));
            foreach (Wheels wheel in i_WheelsList)
            {
                wheel.m_BrandName = i_Brand;
                wheel.SetTireCurrentPressure(i_PressureInput);
                wheel.SetTireMaxPressure(GetMaxWheelPressure(i_Vehicle));
            }
        }

        public void SetDetails(string i_BrandName, float i_Pressure, Vehicle i_Vehicle)
        {
            if (string.IsNullOrWhiteSpace(i_BrandName))
            {
                throw new ArgumentException("Brand name cannot be empty.");
            }

            float maxPressure = GetMaxWheelPressure(i_Vehicle);
            ValidateTirePressure(i_Pressure, maxPressure);

            m_BrandName = i_BrandName;
            m_CurrentAirPressure = i_Pressure;
            m_MaxAirPressure = maxPressure;
        }

        public void InflateToMax()
        {
            if (m_CurrentAirPressure < m_MaxAirPressure)
            {
                m_CurrentAirPressure = m_MaxAirPressure;
            }
        }


    }
}
