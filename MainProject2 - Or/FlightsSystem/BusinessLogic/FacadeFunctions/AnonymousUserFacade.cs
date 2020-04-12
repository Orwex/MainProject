using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public class AnonymousUserFacade : FacadeBase, IAnonymousUserFacade, IFacade
    {
        /// <summary>
        /// Gives back a list of all of the airline companies.
        /// </summary>
        /// <returns></returns>
        public IList<AirlineCompany> GetAllAirLineCompanies()
        {
            return _airlineDAO.GetAll();
        }
        
        /// <summary>
        /// gives back all flights in a list.
        /// </summary>
        /// <returns></returns>
        public IList<Flight> GetAllFlights()
        {
            return _flightDAO.GetAll();
        }
        
        /// <summary>
        /// gives back all flights and how many tickets left in Dictionary.
        /// </summary>
        /// <returns></returns>
        public Dictionary<Flight, int> GetAllFlightVacancy()
        {
            return _flightDAO.GetAllFlightsVacancy();
        }
        
        /// <summary>
        /// find a flight according to it's Id number.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Flight GetFlightByID(int Id)
        {
            return _flightDAO.GetFlightByID(Id);
        }
        
        /// <summary>
        /// Gives back a list of all flights that departure in a specific date.
        /// </summary>
        /// <param name="departureDate"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByDepartureDate(DateTime departureDate)
        {
            return _flightDAO.GetFlightsByDepartureDate(departureDate);
        }
        
        /// <summary>
        /// find all flight to a certain country, returns a list.
        /// </summary>
        /// <param name="CountryCode"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByDestinationCountry(int CountryCode)
        {
            return _flightDAO.GetFlightsByDestinationCountry(CountryCode);
        }

        /// <summary>
        /// Gives back a list of all flights that land in a specific date.
        /// </summary>
        /// <param name="landingDate"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            return _flightDAO.GetFlightsByLandingDate(landingDate);
        }
        
        /// <summary>
        /// Gives back a list of all flights that departure from a specific country.
        /// </summary>
        /// <param name="CountryCode"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByOriginCountry(int CountryCode)
        {
            return _flightDAO.GetFlightsByOriginCountry(CountryCode);
        }
    }
}
