using System;
using System.Collections.Generic;

namespace Ex03_Or_315900845_Or_314919994
{
    public class Wheels
    {
        public string m_ManufacturerName { get; set; }
        private float m_MaxAirPressure { get; set; }
        public float m_CurrentAirPressure { get; set; }

        public Wheels(float i_MaxAirPressure, float i_CurrentAirPressure, string i_BrandName)
        {
            m_MaxAirPressure = i_MaxAirPressure;
            m_ManufacturerName = i_BrandName;
            m_CurrentAirPressure = i_CurrentAirPressure;
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

        public void InflateToMax()
        {
            if (m_CurrentAirPressure < m_MaxAirPressure)
            {
                m_CurrentAirPressure = m_MaxAirPressure;
            }
        }
    }
}
