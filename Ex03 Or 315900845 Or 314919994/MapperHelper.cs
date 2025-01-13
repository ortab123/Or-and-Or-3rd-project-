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
    }
}
