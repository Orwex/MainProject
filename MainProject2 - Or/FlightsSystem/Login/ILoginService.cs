using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public interface ILoginService
    {
        bool TryAdminLogin(string UserName, string Password, out LoginToken<Administrator> token);
        bool TryAirLineLogin(string UserName, string Password, out LoginToken<AirlineCompany> token);
        bool TryCustomerLogin(string UserName, string Password, out LoginToken<Customer> token);
    }
}
