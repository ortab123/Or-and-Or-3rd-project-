﻿using System;

namespace Ex03_Or_315900845_Or_314919994
{
    // Maybe check for option to do it fully abstract:
    public abstract class ElectricVehicle : Vehicle
    {
        protected float m_CurrentBatteryTimeLeft { get; private set; }
        protected float m_BatteryTimeCapacity { get; set; }

        protected ElectricVehicle(
            string i_ModelName,
            string i_LicenseNumber,
            float i_BatteryCurrentAmount,
            float i_BatteryTimeCapacity)
            : base(i_ModelName, i_LicenseNumber)
        {

            ValidateBatteryTime(i_BatteryCurrentAmount, i_BatteryTimeCapacity);
            m_BatteryTimeCapacity = i_BatteryTimeCapacity;
            m_CurrentBatteryTimeLeft = i_BatteryCurrentAmount;
        }

        protected ElectricVehicle()
        {

        }

        public static void ValidateBatteryTime(float i_BatteryCurrentAmount, float i_BatteryTimeCapacity)
        {
            if (i_BatteryCurrentAmount < 0 || i_BatteryCurrentAmount > i_BatteryTimeCapacity)
            {
                throw new ValueOutOfRangeException(0, i_BatteryTimeCapacity, $"Battery current amount ({i_BatteryCurrentAmount} H)" +
                    $" is not in range. {i_BatteryCurrentAmount} - {i_BatteryTimeCapacity}");
            }
        }

        public void SetCurrentEnergy(float i_Hours)
        {
            m_CurrentBatteryTimeLeft = i_Hours;
        }

        public void SetMaxBatteryAmount(float i_BatteryTimeCapacity)
        {
            m_BatteryTimeCapacity = i_BatteryTimeCapacity;
        }

        protected override float GetCurrentEnergy()
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
        }

    }
}
