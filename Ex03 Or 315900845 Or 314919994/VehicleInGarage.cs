using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03_Or_315900845_Or_314919994
{
    public class VehicleInGarage
    {
        public Vehicle m_Vehicle { get; private set; }
        public string m_OwnerName { get; private set; }
        public string m_OwnerPhoneNumber { get; private set; }
        public eVehicleStatus m_Status { get; set; }

        public static readonly Dictionary<int, eVehicleStatus> sr_StatusMap = new Dictionary<int, eVehicleStatus>
                                                                               {
                                                                                   { 1, eVehicleStatus.InRepair },
                                                                                   { 2, eVehicleStatus.Fixed },
                                                                                   { 3, eVehicleStatus.Paid },
                                                                               };

        public VehicleInGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_Vehicle = i_Vehicle;
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_Status = eVehicleStatus.InRepair;
        }

        public static bool IsValidOwnerName(string i_OwnerName)
        {
            bool isValid = !string.IsNullOrWhiteSpace(i_OwnerName);

            if (isValid)
            {
                foreach (char c in i_OwnerName)
                {
                    if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            return isValid;
        }

        public static bool IsValidPhoneNumber(string i_PhoneNumber)
        {
            return i_PhoneNumber.Length == 10 && long.TryParse(i_PhoneNumber, out _);
        }

        public static bool ValidateLicenseNumber(string i_LicenseNumber)
        {
            bool validatedNumber = false;

            if (!string.IsNullOrEmpty(i_LicenseNumber) && (i_LicenseNumber.Length == 8 || i_LicenseNumber.Length == 7))
            {
                validatedNumber = true;
                foreach (char c in i_LicenseNumber)
                {
                    if (!char.IsDigit(c))
                    {
                        validatedNumber = false;
                        break;
                    }
                }
            }

            return validatedNumber;
        }
    }
}
