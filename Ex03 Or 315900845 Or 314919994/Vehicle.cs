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

        protected Vehicle()
        {
        }

        public void SetVehicleModel(string i_Model) 
        {
            m_ModelName = i_Model;
        }

        public void SetEnergyPercentage(float i_EnergyPercentage)
        {
            m_EnergyPercentage = i_EnergyPercentage;
        }

        private float getEnergyPercentage()
        {
            return (GetCurrentEnergy() / GetMaxEnergy()) * 100;
        }

        protected abstract float GetCurrentEnergy();

        public abstract float GetMaxEnergy();

        public void SetWheels(List<Wheels> i_Wheels)
        {
            m_Wheels = i_Wheels;
        }

        public void InflateAllWheelsToMax()
        {
            foreach (Wheels wheel in m_Wheels)
            {
                wheel.InflateToMax();
            }
        }

        public abstract StringBuilder PrintVehicleDetails();

    }
}