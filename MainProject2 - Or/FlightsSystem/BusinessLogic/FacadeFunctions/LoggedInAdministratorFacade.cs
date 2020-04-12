using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public class LoggedInAdministratorFacade : AnonymousUserFacade, ILoggedInAdministratorFacade, IFacade
    {
        /// <summary>
        /// Creates a new airline company and adds it to the table in the DB.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airlineCompany"></param>
        public void CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airlineCompany)
        {
            if (token != null)
            {
                _airlineDAO.Add(airlineCompany);
            }
        }

        /// <summary>
        /// Creates a new customer and adds it to the table in the DB.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        public void CreateNewCustomer(LoginToken<Administrator> token, Customer customer)
        {
            if (token != null)
            {
                _customerDAO.Add(customer);
            }
        }

        /// <summary>
        /// Deletes an airline company in the table.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airlineCompany"></param>
        public void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airlineCompany)
        {
            if (token != null)
            {
                _airlineDAO.Remove(airlineCompany);
            }
        }

        /// <summary>
        /// Deletes a customer from the table.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        public void RemoveCustomer(LoginToken<Administrator> token, Customer customer)
        {
            if (token != null)
            {
                _customerDAO.Remove(customer);
            }
        }

        /// <summary>
        /// updates an airline details.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airlineCompany"></param>
        public void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany airlineCompany)
        {
            if (token != null)
            {
                _airlineDAO.Update(airlineCompany);
            }
        }

        /// <summary>
        /// update a customer details.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        public void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer)
        {
            if (token != null)
            {
                _customerDAO.Update(customer);
            }
        }
    }
}
