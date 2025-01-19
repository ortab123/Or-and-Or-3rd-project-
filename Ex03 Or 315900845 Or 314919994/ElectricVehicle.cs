 using System;

namespace Ex03_Or_315900845_Or_314919994
{
    public abstract class ElectricVehicle : Vehicle
    {
        protected float m_CurrentBatteryTimeLeft { get; private set; }
        protected float m_BatteryTimeCapacity { get; set; }

        public void SetCurrentEnergy(float i_Hours)
        {
            m_CurrentBatteryTimeLeft = i_Hours;
        }

        public void SetMaxBatteryAmount(float i_BatteryTimeCapacity)
        {
            m_BatteryTimeCapacity = i_BatteryTimeCapacity;
        }

        public override float GetCurrentEnergy()
        {
            return m_CurrentBatteryTimeLeft;
        }

        public override float GetMaxEnergy()
        {
            return m_BatteryTimeCapacity;
        }

        public void Recharge(float i_Hours)
        {
            if (i_Hours < 0 || GetCurrentEnergy() + i_Hours > GetMaxEnergy())
            {
                throw new ValueOutOfRangeException(0, (GetMaxEnergy() - GetCurrentEnergy()), $"The value is not in the range {0} to {(GetMaxEnergy() - GetCurrentEnergy())}.");
            }

            m_CurrentBatteryTimeLeft += i_Hours;
            SetEnergyPercentage();
        }
    }
}
