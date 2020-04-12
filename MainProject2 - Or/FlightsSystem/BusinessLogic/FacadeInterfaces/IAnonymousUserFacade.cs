using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    interface IAnonymousUserFacade
    {
        IList<Flight> GetAllFlights();
        IList<AirlineCompany> GetAllAirLineCompanies();
        Dictionary<Flight, int> GetAllFlightVacancy();
        Flight GetFlightByID(int Id);
        IList<Flight> GetFlightsByOriginCountry(int CountryCode);
        IList<Flight> GetFlightsByDestinationCountry(int CountryCode);
        IList<Flight> GetFlightsByDepartureDate(DateTime departureDate);
        IList<Flight> GetFlightsByLandingDate(DateTime landingDate);
    }
}
