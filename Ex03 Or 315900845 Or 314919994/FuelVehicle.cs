using System;
using System.Runtime.CompilerServices;

namespace Ex03_Or_315900845_Or_314919994
{
    public abstract class FuelVehicle : Vehicle
    {
        protected eFuelType m_FuelType { get; set; }
        protected float m_CurrentFuelTankAmount { get; set; }
        protected float m_FuelTankCapacity { get; set; }

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

        public override float GetCurrentEnergy()
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

        public void Refuel(float i_FuelAmount, eFuelType i_FuelType)
        {
            if (i_FuelAmount < 0 || GetCurrentEnergy() + i_FuelAmount > GetMaxEnergy())
            {
                throw new ValueOutOfRangeException(0,(GetMaxEnergy() - GetCurrentEnergy()), $"The value is not in the range {0} to {(GetMaxEnergy() - GetCurrentEnergy())}.");
            }

            if (i_FuelType != GetFuelType())
            {
                throw new ArgumentException("Please use the correct type of fuel");
            }

            m_CurrentFuelTankAmount += i_FuelAmount;
            SetEnergyPercentage();
        }
    }
}