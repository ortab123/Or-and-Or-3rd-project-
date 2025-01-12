﻿using System;
using System.Runtime.CompilerServices;

namespace Ex03_Or_315900845_Or_314919994
{
    // Maybe check for option to do it fully abstract:
    public abstract class FuelVehicle : Vehicle
    {
        protected eFuelType m_FuelType { get; set; }
        protected float m_CurrentFuelTankAmount { get; set; }
        protected float m_FuelTankCapacity { get; set; }

        protected FuelVehicle(
            string i_ModelName,
            string i_LicenseNumber,
            float i_CurrentFuelAmount,
            float i_FuelTankCapacity,
            eFuelType i_FuelType)
            : base(i_ModelName, i_LicenseNumber)
        {

            ValidateFuelAmount(i_CurrentFuelAmount, i_FuelTankCapacity);
            m_CurrentFuelTankAmount = i_CurrentFuelAmount;
            m_FuelTankCapacity = i_FuelTankCapacity;
            m_FuelType = i_FuelType;
        }

        protected FuelVehicle()
        {

        }

        public void SetCurrentFuelAmount(float i_FuelCurrentAmount)
        {
            m_CurrentFuelTankAmount = i_FuelCurrentAmount;
        }

        public void SetMaxFuelAmount(float i_MaxFuelAmount)
        {
            m_FuelTankCapacity = i_MaxFuelAmount;
        }

        public void SetFuelType(eFuelType i_FuelType)
        {
            m_FuelType = i_FuelType;
        }

        public static void ValidateFuelAmount(float i_FuelCurrentAmount, float i_FuelTankCapacity)
        {
            if (i_FuelCurrentAmount < 0 || i_FuelCurrentAmount > i_FuelTankCapacity)
            {
                throw new ValueOutOfRangeException(0, i_FuelTankCapacity, $"Fuel current amount ({i_FuelCurrentAmount} L)" +
                    $" is not in range {i_FuelCurrentAmount} - {i_FuelTankCapacity}.");
            }
        }

        protected override float GetCurrentEnergy()
        {
            return m_CurrentFuelTankAmount;
        }

        public override float GetMaxEnergy()
        {
            return m_FuelTankCapacity;
        }

        public eFuelType GetFuelType()
        {
            return m_FuelType;
        }

        public void Refuel(int i_FuelAmount, eFuelType i_FuelType)
        {
            if (i_FuelAmount < 0 || GetCurrentEnergy() + i_FuelAmount > GetMaxEnergy())
            {
                throw new ValueOutOfRangeException(0,(GetMaxEnergy() -GetCurrentEnergy()), $"The value is not in the range {0} to {(GetMaxEnergy() - GetCurrentEnergy())}.");
            }

            if (i_FuelType != GetFuelType())
            {
                throw new ArgumentException("Please use the correct type of fuel");
            }

            m_CurrentFuelTankAmount += i_FuelAmount;
        }
    }

}