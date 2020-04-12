using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public class LoggedInAirlineFacade : AnonymousUserFacade, ILoggedInAirlineFacade, IFacade
    {
        /// <summary>
        /// Canceling a flight and when cancelling a flight, we should cancel the tickets first. if exist. 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        public void CancelFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token!= null)
            {                              
                IList<Ticket> ticketsList = _ticketDAO.GetAll();
                foreach (Ticket t in ticketsList)
                {
                    if (t.FlightID == flight.FlightID)
                        _ticketDAO.Remove(t);
                }
                _flightDAO.Remove(flight);
            }
        }
        /// <summary>
        /// Change the password of the Airline User.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        public void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword)
        {
            if (token != null)
            {
                if (token.User.Password == oldPassword)
                {
                    token.User.Password = newPassword;
                    _airlineDAO = new AirlineDAOMSSQL();
                    _airlineDAO.Update(token.User);
                }
                else
                    throw new WrongPasswordException($"Old Password {oldPassword} is wrong");
            }
        }

        /// <summary>
        /// Creates a flight.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        public void CreateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token != null)
            {
                _flightDAO.Add(flight);                
            }
        }
        /// <summary>
        /// Gives a list of all flights and details.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Flight> GetAllFlights(LoginToken<AirlineCompany> token)
        {
            if (token != null)
            {
                return _flightDAO.GetAll();
            }
            return null;
        }
        /// <summary>
        ///  Gives a list of all tickets of a specific airline.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Ticket> GetAllTickets(LoginToken<AirlineCompany> token)
        {
            if (token != null)
            {
                return _airlineDAO.GetAllTicketsOfTheAirline(token.User.Airline_ID); 
            }
            return null;
        }

        /// <summary>
        /// Changes the details of the airline company.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airlineCompany"></param>
        public void ModifyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airlineCompany)
        {
            if (token != null)
            {
                _airlineDAO.Update(airlineCompany);
            }
        }
        /// <summary>
        /// Changes the details of the flight.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        public void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token != null)
            {
                _flightDAO.Update(flight);                
            }
        }
    }
}
