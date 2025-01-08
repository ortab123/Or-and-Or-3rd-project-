using System.Collections.Generic;

namespace Ex03_Or_315900845_Or_314919994
{
    public class Garage
    {
        private static readonly Garage sr_Instance = new Garage();
        private readonly List<VehicleInGarage> m_VehiclesInGarage;

        private Garage()
        {
            m_VehiclesInGarage = new List<VehicleInGarage>();
        }

        public static Garage GetInstance()
        {
            return sr_Instance;
        }

        public bool FindVehicleInGarage(Garage io_Garage, string i_VehicleLicenseNumber, out int o_Index)
        {
            o_Index = -1;
            bool isFound = false;
            int amount = io_Garage.m_VehiclesInGarage.Count;
            for(int i = 0; i < amount; i++)
            {
                if(i_VehicleLicenseNumber == io_Garage.m_VehiclesInGarage[i].m_Vehicle.m_LicenseNumber)
                {
                    isFound = true;
                    o_Index = i;
                }
            }
            return isFound;
        }

        public void InsertNewVehicleToGarage(ref Garage io_Garage, string i_VehicleLicenseNumber)
        {
            if(FindVehicleInGarage(io_Garage, i_VehicleLicenseNumber, out int index))
            {
                io_Garage.m_VehiclesInGarage[index].m_Status = eVehicleStatus.InRepair;
                //למסור הודעה מתאימה שהרכב במאגר
            }
            else
            {
                
            }
        }

        
    }
}
