using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public class FlyingCenterSystem
    {
        private static FlyingCenterSystem _MyFlyingCenterInstance;
        private static object key = new object();

        protected FlyingCenterSystem()
        {
            new Task(() =>
            {
                Move3HoursOldTickets();
                Move3HoursOldFlights();
                
                Thread.Sleep(FlightCenterConfig.OneDayInterval);
            });
        }

        public static FlyingCenterSystem GetFlyingCenterSystemInstance()
        {
            if (_MyFlyingCenterInstance == null)
            {
                lock (key)
                {
                    if (_MyFlyingCenterInstance == null)
                    {
                        _MyFlyingCenterInstance = new FlyingCenterSystem();
                    }
                }
            }
            return _MyFlyingCenterInstance;
        }

        public ILoginToken Login(string userName, string Password)
        {
            LoginService loginService = new LoginService();
            
            if (loginService.TryAdminLogin(userName, Password, out LoginToken<Administrator> AdminToken))
            {
                return AdminToken;
            }
            else if (loginService.TryAirLineLogin(userName, Password, out LoginToken<AirlineCompany> AirlineCompanyToken))
            {                
                return AirlineCompanyToken;
            }
            else if (loginService.TryCustomerLogin(userName, Password, out LoginToken<Customer> CustomerToken))
            {
                return CustomerToken; 
            }
            else
                return null;            
        }

        public IFacade GetFacade(ILoginToken loginToken)
        {
            string a = "";
            if (loginToken != null)
                a = loginToken.GetType().GenericTypeArguments[0].Name;
            if (a == "Administrator")
            {
                return new LoggedInAdministratorFacade();
            }
            else if (a == "AirlineCompany")
            {
                return new LoggedInAirlineFacade();
            }
            else if (a == "Customer")
            {
                return new LoggedInCustomerFacade();
            }
            else // IloginToken is null - > user is Anonymous
                return new AnonymousUserFacade();            
        }


        private void Move3HoursOldFlights()
        {
            //find the time 3 hours ago:            
            DateTime x3HrsAgo = DateTime.Now.Subtract(DateTime.Now.AddHours(3) - DateTime.Now);
            string threeHoursAgo = x3HrsAgo.ToString("yyyy-MM-dd HH:mm:ss");

            SqlCommand cmd = new SqlCommand();

            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandText = "MOVE_3HOURS_OLD_FLIGHTS";
                cmd.Parameters.Add(new SqlParameter("@ThreeHoursAgo", threeHoursAgo));               

                cmd.ExecuteNonQuery();
            }
        }

        private void Move3HoursOldTickets()
        {
            //find the time 3 hours ago:            
            DateTime x3HrsAgo = DateTime.Now.Subtract(DateTime.Now.AddHours(3) - DateTime.Now);
            string threeHoursAgo = x3HrsAgo.ToString("yyyy-MM-dd HH:mm:ss");

            SqlCommand cmd = new SqlCommand();

            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandText = "MOVE_3HOURS_OLD_TICKETS";
                cmd.Parameters.Add(new SqlParameter("@ThreeHoursAgo", threeHoursAgo));

                cmd.ExecuteNonQuery();
            }            
        }
    }
}
