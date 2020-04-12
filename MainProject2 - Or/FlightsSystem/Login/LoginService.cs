using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public class LoginService : ILoginService
    {
        private AirlineDAOMSSQL _airlineDAO;
        private CustomerDAOMSSQL _customerDAO;

        public LoginService()
        {
            _airlineDAO = new AirlineDAOMSSQL();
            _customerDAO = new CustomerDAOMSSQL();
        }
        public bool TryAdminLogin(string UserName, string Password, out LoginToken<Administrator> token)
        {
            if (UserName == FlightCenterConfig.ADMIN_NAME && Password == FlightCenterConfig.ADMIN_PASSWORD)
            {
                token = new LoginToken<Administrator>();
                token.User = new Administrator();
                return true;
            }

            token = null;
            return false;
        }

        public bool TryAirLineLogin(string UserName, string Password, out LoginToken<AirlineCompany> token)
        {
            AirlineCompany airlineCompany = _airlineDAO.GetAirlineByUsername(UserName);
            if (airlineCompany != null)
            {
                if ((UserName == airlineCompany.UserName) && (Password == airlineCompany.Password))
                {
                    token = new LoginToken<AirlineCompany>()
                    {
                        User = airlineCompany
                    };
                    return true;
                }
                //else if (Password != airlineCompany.Password)
                //{
                //    throw new WrongPasswordException($"{Password} is wrong !");
                //}
            }
            token = null;
            return false;
        }
        
        public bool TryCustomerLogin(string UserName, string Password, out LoginToken<Customer> token)
        {
            Customer customer = _customerDAO.GetCustomerByUserName(UserName);
            if (customer != null)
            {
                if ((UserName == customer.Username) && (Password == customer.Password))
                {
                    token = new LoginToken<Customer>()
                    {
                        User = customer
                    };
                    return true;
                }
                //else if (Password != customer.Password)
                //{
                //    throw new WrongPasswordException($"{Password} is wrong !");
                //}
            }
            token = null;
            return false;
        }
    }
}
