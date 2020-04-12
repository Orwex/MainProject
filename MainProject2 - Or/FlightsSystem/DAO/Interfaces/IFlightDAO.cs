using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    interface IFlightDAO :IBasicDB<Flight>
    {
        Dictionary<Flight,int> GetAllFlightsVacancy();
        Flight GetFlightByID(long FlightID);
        IList<Flight> GetFlightsByCustomer(Customer Customer);
        IList<Flight> GetFlightsByDepartureDate(DateTime DepartureDate);
        IList<Flight> GetFlightsByDestinationCountry(long CountryCode);
        IList<Flight> GetFlightsByLandingDate(DateTime LandingDate);
        IList<Flight> GetFlightsByOriginCountry(long CountryCode);
    }
}
