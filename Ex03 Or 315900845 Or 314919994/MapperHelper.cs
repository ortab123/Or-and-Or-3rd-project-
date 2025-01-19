using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03_Or_315900845_Or_314919994
{
    public static class MapperHelper
    {
        public static readonly Dictionary<int, eLicenseType> sr_LicenseMap = new Dictionary<int, eLicenseType>
                                                                                 {
                                                                                     { 1, eLicenseType.A1 },
                                                                                     { 2, eLicenseType.A2 },
                                                                                     { 3, eLicenseType.B1 },
                                                                                     { 4, eLicenseType.B2 },
                                                                                 };

        public static readonly Dictionary<int, eDoorsNumber> sr_DoorsMap = new Dictionary<int, eDoorsNumber>
                                                                               {
                                                                                   { 1, eDoorsNumber.Two },
                                                                                   { 2, eDoorsNumber.Three },
                                                                                   { 3, eDoorsNumber.Four },
                                                                                   { 4, eDoorsNumber.Five },
                                                                               };

        public static readonly Dictionary<int, eCarColor> sr_ColorMap = new Dictionary<int, eCarColor>
                                                                            {
                                                                                { 1, eCarColor.Blue },
                                                                                { 2, eCarColor.Black },
                                                                                { 3, eCarColor.White },
                                                                                { 4, eCarColor.Gray },
                                                                            };

        public static readonly Dictionary<int, eFuelType> sr_FuelTypeMap = new Dictionary<int, eFuelType>
                                                                               {
                                                                                   { 1, eFuelType.Octan98 },
                                                                                   { 2, eFuelType.Octan96 },
                                                                                   { 3, eFuelType.Octan95 },
                                                                                   { 4, eFuelType.Soler },
                                                                               };

        public static readonly Dictionary<int, eVehicleType> sr_VehicleTypeMap = new Dictionary<int, eVehicleType>
            {
                { 1, eVehicleType.FuelMotorcycle },
                { 2, eVehicleType.ElectricMotorcycle },
                { 3, eVehicleType.FuelCar },
                { 4, eVehicleType.ElectricCar },
                { 5, eVehicleType.Truck },
                {6, eVehicleType.Other }
            };

        public static readonly Dictionary<int, eChoice> sr_ChoicesMap = new Dictionary<int, eChoice>
                                                                            {
                                                                                { 1, eChoice.InsertVehicle },
                                                                                { 2, eChoice.PrintLicenses },
                                                                                { 3, eChoice.ChangeStatus },
                                                                                { 4, eChoice.Inflate },
                                                                                { 5, eChoice.Refuel },
                                                                                { 6, eChoice.Recharge },
                                                                                { 7, eChoice.PrintDetails },
                                                                                { 8, eChoice.Exit }
                                                                            };
    }
}