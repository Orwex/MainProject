using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public class Country : IPOCO
    {
        public long CountyID { get; set; }
        public string CountryName { get; set; }

        public Country()
        {

        }

        public Country(string countryName)
        {
            CountryName = countryName;
        }
                
        public static bool operator ==(Country Co1, Country Co2)
        {
            if (ReferenceEquals(Co1, null) && ReferenceEquals(Co2, null))
            {
                return true;
            }
            if (ReferenceEquals(Co1, null) || ReferenceEquals(Co2, null))
            {
                return false;
            }
            if (Co1.CountyID == Co2.CountyID)
                return true;
            return false;
        }
        public static bool operator !=(Country Co1, Country Co2)
        {
            return !(Co1 == Co2);
        }

        public override bool Equals(object obj)
        {
            Country otherCountry = obj as Country;
            if (otherCountry == null)
                return false;
            return (otherCountry.CountyID == this.CountyID);
        }

        public override int GetHashCode()
        {
            return (int)this.CountyID;
        }
    }
}
