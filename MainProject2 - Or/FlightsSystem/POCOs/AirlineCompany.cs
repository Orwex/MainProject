using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public class AirlineCompany : IPOCO, IUser
    {
        public long Airline_ID { get; set; }
        public string AirLineName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long CountryCode { get; set; }

        public AirlineCompany()
        {

        }

        public AirlineCompany(string airLineName, string userName, string password, long countryCode)
        {
            AirLineName = airLineName;
            UserName = userName;
            Password = password;
            CountryCode = countryCode;
        }

        public static bool operator ==(AirlineCompany AL1, AirlineCompany AL2)
        {
            if (ReferenceEquals(AL1, null) && ReferenceEquals(AL2, null))
            {
                return true;
            }
            if (ReferenceEquals(AL1, null) || ReferenceEquals(AL2, null))
            {
                return false;
            }
            if (AL1.Airline_ID == AL2.Airline_ID)
                return true;
            return false;
        }
        public static bool operator !=(AirlineCompany AL1, AirlineCompany AL2)
        {
            return !(AL1 == AL2);
        }

        public override bool Equals(object obj)
        {
            AirlineCompany otherAirlineCompany = obj as AirlineCompany;
            if (otherAirlineCompany == null)
                return false;
            return (otherAirlineCompany.Airline_ID == this.Airline_ID);
        }

        public override int GetHashCode()
        {
            return (int)this.Airline_ID;
        }
    }
}
