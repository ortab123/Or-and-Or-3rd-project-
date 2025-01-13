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

        protected Vehicle()
        {

        }

        public void SetVehicleModel(string i_Model) 
        {
            m_ModelName = i_Model;
        }

        private float getEnergyPercentage()
        {
            return (GetCurrentEnergy() / GetMaxEnergy()) * 100;
        }

        protected abstract float GetCurrentEnergy();

        public abstract float GetMaxEnergy();

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