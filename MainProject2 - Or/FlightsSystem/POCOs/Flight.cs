using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public class Flight : IPOCO
    {
        public long FlightID { get; set; }
        public long AirLineCompany_ID { get; set; }
        public long Origin_Country_Code { get; set; }
        public long Destination_Country_Code { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime LandingTime { get; set; }
        public int Remaining_Tickets { get; set; }

        public Flight()
        {
            
        }

        public Flight(long airLineCompany_ID, long origin_Country_Code, 
            long destination_Country_Code, DateTime departureTime, DateTime landingTime, int remaining_Tickets)
        {
            AirLineCompany_ID = airLineCompany_ID;
            Origin_Country_Code = origin_Country_Code;
            Destination_Country_Code = destination_Country_Code;
            DepartureTime = departureTime;
            LandingTime = landingTime;
            Remaining_Tickets = remaining_Tickets;
        }


        public static bool operator ==(Flight f1, Flight f2)
        {
            if (ReferenceEquals(f1, null) && ReferenceEquals(f2, null))
            {
                return true;
            }
            if (ReferenceEquals(f1, null) || ReferenceEquals(f2, null))
            {
                return false;
            }
            if (f1.FlightID == f2.FlightID)
                return true;
            return false;
        }
        public static bool operator !=(Flight f1, Flight f2)
        {
            return !(f1 == f2);
        }

        public override bool Equals(object obj)
        {
            Flight otherFlight = obj as Flight;
            if (otherFlight == null)
                return false;
            return (otherFlight.FlightID == this.FlightID);
        }

        public override int GetHashCode()
        {
            return (int)this.FlightID;
        }
    }
}
