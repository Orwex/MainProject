using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public class LoggedInCustomerFacade : AnonymousUserFacade, ILoggedInCustomerFacade, IFacade
    {
        /// <summary>
        /// Remove a ticket from the list. Adds to the flight ID 1 ticket to the TICKES_REMAINING.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ticket"></param>
        public void CancelTicket(LoginToken<Customer> token, Ticket ticket)
        {
            if (token != null)
            {
                Flight f = _flightDAO.GetFlightByID(ticket.FlightID);
                f.Remaining_Tickets++;
                _flightDAO.Update(f);

                _ticketDAO.Remove(ticket);
            }
        }
        /// <summary>
        /// Get a list of all flights.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {
            if (token != null)
            {
                return _flightDAO.GetAll();
            }
            return null;
        }
        /// <summary>
        /// Checks if there are enough tickets in the flight and purchace a ticket.
        /// Then reduce the amount of tickets in this fligh by 1.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        /// <returns></returns>        
        public Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight)
        {            
            if (token != null)
            {
                if (!CheckCustomerIfExistInFlight(token, flight))
                {
                    if (flight.Remaining_Tickets > 0)
                    {
                        //adding a ticket to the list:
                        Ticket ticket = new Ticket(flight.FlightID, token.User.Customer_ID);
                        _ticketDAO.Add(ticket);
                        //update flight remaining tickets:
                        flight.Remaining_Tickets--;
                        _flightDAO.Update(flight);

                        return ticket;
                    }

                    else
                        throw new NoTicketsInFlightException($"The flight {flight.FlightID} " +
                            $"from {flight.Origin_Country_Code} to {flight.Destination_Country_Code} is full. " +
                            $"No charge was made.");
                }
                else
                    throw new CustomerAlreadyExistException($"Customer {token.User.Customer_ID} name: {token.User.FirstName}" +
                        $" is already exist in the tickets.");
            }
            return null;
        }

        private bool CheckCustomerIfExistInFlight(LoginToken<Customer> token, Flight flight)
        {            
            IList<Ticket> tickets = _ticketDAO.GetAll();
            foreach (Ticket t in tickets)
            {
                if ((t.CustomerID == token.User.Customer_ID) && (t.FlightID == flight.FlightID))
                    return true;
            }
            return false;
        }
    }
}
