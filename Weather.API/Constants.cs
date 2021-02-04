using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.API
{
    public class Constants
    {
        public static List<Location> Locations
        {
            get
            {
                return new List<Location>
                {
                    new Location {Country = "India", State ="Kerala", City="Kochi", CityId=1273874},
                    new Location {Country = "India", State ="Kerala", City="Thiruvananthapuram", CityId=1254163},
                    new Location {Country = "India", State ="Himachal Pradesh", City="Manali", CityId=1263967},
                };
            }
        }
    }
}
