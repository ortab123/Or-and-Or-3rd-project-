using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03_Or_315900845_Or_314919994
{
    public class Other : Vehicle
    {
        private bool m_IsElectric;
        private string m_TypeName;
        public int m_NumberOfWheels { get; private set; }
        public string m_EnergyType { get; private set; }
        public float m_MaxEnergyCapacity { get; private set; }
        public float m_CurrentEnergy { get; private set; }

        public List<Wheels> WheelsList { get; private set; } = new List<Wheels>();

        public Other() { }

        //public void InitializeNewVehicle(
        //    string i_ModelName,
        //    string i_LicenseNumber,
        //    int i_NumberOfWheels,
        //    string i_EnergyType,
        //    float i_MaxEnergyCapacity,
        //    float i_CurrentEnergy,
        //    List<Wheels> i_Wheels)
        //{
        //    m_ModelName = i_ModelName;
        //    m_LicenseNumber = i_LicenseNumber;
        //    m_NumberOfWheels = i_NumberOfWheels;
        //    m_EnergyType = i_EnergyType;
        //    m_MaxEnergyCapacity = i_MaxEnergyCapacity;
        //    m_CurrentEnergy = i_CurrentEnergy;
        //    WheelsList = i_Wheels;
        //    m_EnergyPercentage = getEnergyPercentage();
        //}

        public void SetTypeName(string i_TypeName)
        {
            m_TypeName = i_TypeName;
        }

        public string GetTypeName()
        {
            return m_TypeName;
        }

        public void SetElectricEngine()
        {
            m_IsElectric = true;
            // כאן ניתן להוסיף לוגיקה נוספת לאתחול מנוע חשמלי
        }

        public void SetFuelEngine()
        {
            m_IsElectric = false;
            // כאן ניתן להוסיף לוגיקה נוספת לאתחול מנוע דלק
        }

        public override int GetWheelsNumber()
        {
            return m_NumberOfWheels;
        }

        public override float GetMaxTirePressure()
        {
            if (WheelsList.Any())
            {
                return Wheels.GetMaxWheelPressure(this);
            }
            else
            {
                throw new InvalidOperationException("No wheels are defined for this vehicle.");
            }
        }

        public override float GetCurrentEnergy()
        {
            return m_CurrentEnergy;
        }

        public override float GetMaxEnergy()
        {
            return m_MaxEnergyCapacity;
        }

        public void AddWheel(Wheels i_Wheel)
        {
            if (WheelsList.Count >= m_NumberOfWheels)
            {
                throw new InvalidOperationException("Cannot add more wheels than the defined number of wheels.");
            }
            WheelsList.Add(i_Wheel);
        }

        public void InflateAllWheelsToMax()
        {
            foreach (Wheels wheel in WheelsList)
            {
                wheel.InflateToMax();
            }
        }

        public override StringBuilder PrintVehicleDetails()
        {
            StringBuilder details = new StringBuilder();
            details.AppendLine($"Model Name: {m_ModelName}");
            details.AppendLine($"License Number: {m_LicenseNumber}");
            details.AppendLine($"Number of Wheels: {m_NumberOfWheels}");
            details.AppendLine($"Energy Type: {m_EnergyType}");
            details.AppendLine($"Max Energy Capacity: {m_MaxEnergyCapacity}");
            details.AppendLine($"Current Energy: {m_CurrentEnergy}");
            details.AppendLine($"Energy Percentage: {m_EnergyPercentage * 100}%");
            details.AppendLine("Wheels Details:");
            foreach (var wheel in WheelsList)
            {
                details.AppendLine($"- Manufacturer: {wheel.m_ManufacturerName}, Current Pressure: {wheel.m_CurrentAirPressure}, Max Pressure: {Wheels.GetMaxWheelPressure(this)}");
            }
            return details;
        }
    }


}
