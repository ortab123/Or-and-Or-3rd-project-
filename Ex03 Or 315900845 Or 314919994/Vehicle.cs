﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03_Or_315900845_Or_314919994
{
    public abstract class Vehicle
    {
        public string m_ModelName { get; set; }
        public string m_LicenseNumber { get; set; }
        public float m_EnergyPercentage { get; set; }
        protected List<Wheels> m_Wheels { get; set; } = new List<Wheels>();

        public void SetEnergyPercentage()
        {
            m_EnergyPercentage = GetEnergyPercentage();
        }

        protected float GetEnergyPercentage()
        {
            return GetCurrentEnergy() / GetMaxEnergy();
        }

        public abstract int GetWheelsNumber();

        public abstract float GetMaxTirePressure();

        public abstract float GetCurrentEnergy();

        public abstract float GetMaxEnergy();

        public void SetWheels(List<Wheels> i_Wheels)
        {
            m_Wheels = i_Wheels;
        }

        public void SetVehicleModel(string i_Model)
        {
            m_ModelName = i_Model;
        }

        public void InflateAllWheelsToMax()
        {
            foreach (Wheels wheel in m_Wheels)
            {
                wheel.InflateToMax();
            }
        }

        public abstract eVehicleType GetEVehicleType();

        public abstract StringBuilder PrintVehicleDetails();

    }
}