using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public static class FlightCenterConfig
    {
        public const string ConnectionString = @"Data Source=.;Initial Catalog=FlightsData;Integrated Security=True";

        public const string ADMIN_NAME = "admin";
        public const string ADMIN_PASSWORD = "9999";

        public static int OneDayInterval = 1000 * 60 * 60 * 24; // 24 hours
    }
}
