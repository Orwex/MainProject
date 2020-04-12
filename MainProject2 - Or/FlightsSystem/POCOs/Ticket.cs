using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public class Ticket : IPOCO
    {
        public long TicketID { get; set; }
        public long FlightID { get; set; }
        public long CustomerID { get; set; }

        public Ticket()
        {

        }

        public Ticket(long flightID, long customerID)
        {
            FlightID = flightID;
            CustomerID = customerID;
        }


        public static bool operator ==(Ticket t1, Ticket t2)
        {
            if (ReferenceEquals(t1, null) && ReferenceEquals(t2, null))
            {
                return true;
            }
            if (ReferenceEquals(t1, null) || ReferenceEquals(t2, null))
            {
                return false;
            }
            if (t1.TicketID == t2.TicketID)
                return true;
            return false;
        }
        public static bool operator !=(Ticket t1, Ticket t2)
        {
            return !(t1 == t2);
        }

        public override bool Equals(object obj)
        {
            Ticket otherTicket = obj as Ticket;
            if (otherTicket == null)
                return false;
            return (otherTicket.TicketID == this.TicketID);
        }

        public override int GetHashCode()
        {
            return (int)this.TicketID;
        }
    }
}
