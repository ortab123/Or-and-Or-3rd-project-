using System;

namespace Ex03_Or_315900845_Or_314919994
{
    internal class Wheels
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

        public void TireInflation(int i_PressureToAdd)
        {
           Vehicle.ValidateTirePressure(i_PressureToAdd, m_MaxAirPressure);

            if (i_PressureToAdd < 0)
            {
                throw new ArgumentException("Pressure to add cannot be negative.");
            }

            if (m_CurrentAirPressure + i_PressureToAdd > m_MaxAirPressure)
            {
                throw new InvalidOperationException("Inflating tire will exceed maximum air pressure.");
            }

            m_CurrentAirPressure += i_PressureToAdd;
        }

    }
}
