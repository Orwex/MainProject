using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    interface ILoggedInAdministratorFacade
    {
        void CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airlineCompany);
        void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany airlineCompany);
        void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airlineCompany);

        void CreateNewCustomer(LoginToken<Administrator> token, Customer customer);
        void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer);
        void RemoveCustomer(LoginToken<Administrator> token, Customer customer);
    }
}
