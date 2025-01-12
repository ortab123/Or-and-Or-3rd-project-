using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03_Or_315900845_Or_314919994
{
    internal class VehicleInGarage
    {
        public Vehicle m_Vehicle { get; private set; }
        public string m_OwnerName { get; private set; }
        public string m_OwnerPhoneNumber { get; private set; }
        public eVehicleStatus m_Status { get; set; }

        public VehicleInGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_Vehicle = i_Vehicle;
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_Status = eVehicleStatus.InRepair;
        }

        public VehicleInGarage()
        {

        }
    }
}
