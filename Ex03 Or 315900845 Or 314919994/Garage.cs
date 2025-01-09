using System.Collections.Generic;

namespace Ex03_Or_315900845_Or_314919994
{
    public class Garage
    {
        private static readonly Garage sr_Instance = new Garage();
        private readonly List<VehicleInGarage> m_VehiclesInGarage;

        public Garage()
        {
            m_VehiclesInGarage = new List<VehicleInGarage>();
        }

        public static Garage GetInstance()
        {
            return sr_Instance;
        }

        public static string FindVehicleInGarage(ref Garage io_Garage, string i_VehicleLicenseNumber)
        {
            
            string isFoundMessage = null;
            int amount = io_Garage.m_VehiclesInGarage.Count;

            for(int i = 0; i < amount; i++)
            {
                if(i_VehicleLicenseNumber == io_Garage.m_VehiclesInGarage[i].m_Vehicle.m_LicenseNumber)
                {
                    isFoundMessage = "Vehicle already inside the Garage, changing status.";
                    io_Garage.m_VehiclesInGarage[i].m_Status = eVehicleStatus.InRepair;
                }
            }

            return isFoundMessage;
        }

        public void InsertNewVehicleToGarage(ref Garage io_Garage, string i_VehicleLicenseNumber)
        {

        }
     
        
    }
}
