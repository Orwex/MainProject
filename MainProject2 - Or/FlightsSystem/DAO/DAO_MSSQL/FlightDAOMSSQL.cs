using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public class FlightDAOMSSQL : IFlightDAO
    {
        static SqlCommand cmd = new SqlCommand();

        public void Add(Flight item)
        {
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                string format = "yyyy-MM-dd HH:mm:ss";
                cmd.CommandText = $"INSERT INTO " +
                    $"Flights (AIRLINECOMPANY_ID, ORIGIN_COUNTRY_CODE, DESTINATION_COUNTRY_CODE, DEPARTURE_TIME, LANDING_TIME, REMAINING_TICKETS) " +
                    $"Values ({item.AirLineCompany_ID}, {item.Origin_Country_Code}, {item.Destination_Country_Code}, '{item.DepartureTime.ToString(format)}', '{item.LandingTime.ToString(format)}', {item.Remaining_Tickets})";

                cmd.ExecuteNonQuery();
            }
        }

        public Flight Get(int id)
        {
            Flight resultFlight = new Flight();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT * FROM Flights WHERE ID = {id}";
                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        resultFlight = new Flight()
                        {
                            FlightID = (long)reader["ID"],
                            AirLineCompany_ID = (long)reader["AIRLINECOMPANY_ID"],
                            Origin_Country_Code = (long)reader["ORIGIN_COUNTRY_CODE"],
                            Destination_Country_Code = (long)reader["DESTINATION_COUNTRY_CODE"],
                            DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                            LandingTime = (DateTime)reader["LANDING_TIME"],
                            Remaining_Tickets = (int)reader["REMAINING_TICKETS"]
                        };
                    }
                }
            }
            return resultFlight;
        }

        public IList<Flight> GetAll()
        {
            IList<Flight> resultFlight = new List<Flight>();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT * FROM Flights";
                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Flight CurrentFlight = new Flight
                        {
                            FlightID = (long)reader["ID"],
                            AirLineCompany_ID = (long)reader["AIRLINECOMPANY_ID"],
                            Origin_Country_Code = (long)reader["ORIGIN_COUNTRY_CODE"],
                            Destination_Country_Code = (long)reader["DESTINATION_COUNTRY_CODE"],
                            DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                            LandingTime = (DateTime)reader["LANDING_TIME"],
                            Remaining_Tickets = (int)reader["REMAINING_TICKETS"]
                        };

                        resultFlight.Add(CurrentFlight);
                    }
                }
            }
            return resultFlight;
        }

        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> flightVacancy = new Dictionary<Flight, int>();
            IList<Flight> Flights = GetAll();
            foreach (Flight Flightitem in Flights)
            {
                flightVacancy.Add(Flightitem, Flightitem.Remaining_Tickets);
            }
            return flightVacancy;
        }

        public Flight GetFlightByID(long FlightID)
        {
            return Get((int)FlightID);
        }

        public IList<Flight> GetFlightsByCustomer(Customer Customer)
        {
            IList<Flight> flightsByCustomer = new List<Flight>();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandText = $"GET_FLIGHTS_BY_CUSTOMER_ID";
                cmd.Parameters.Add(new SqlParameter("@Customer_ID",Customer.Customer_ID));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Flight CurrentFlight = new Flight
                        {
                            FlightID = (int)reader["ID"],
                            AirLineCompany_ID = (int)reader["AIRLINECOMPANY_ID"],
                            Origin_Country_Code = (int)reader["ORIGIN_COUNTRY_CODE"],
                            Destination_Country_Code = (int)reader["DESTINATION_COUNTRY_CODE"],
                            DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                            LandingTime = (DateTime)reader["LANDING_TIME"],
                            Remaining_Tickets = (int)reader["REMAINING_TICKETS"]
                        };

                        flightsByCustomer.Add(CurrentFlight);
                    }
                }
            }
            return flightsByCustomer;            
        }

        public IList<Flight> GetFlightsByDepartureDate(DateTime DepartureDate)
        {
            IList<Flight> flightsByCustomer = new List<Flight>();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandText = $"GET_FLIGHTS_BY_DEPARTURE_DATE";
                string format = "yyyy-MM-dd HH:mm:ss";
                cmd.Parameters.Add(new SqlParameter("@DepartureDate", DepartureDate.ToString(format)));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Flight CurrentFlight = new Flight
                        {
                            FlightID = (long)reader["ID"],
                            AirLineCompany_ID = (long)reader["AIRLINECOMPANY_ID"],
                            Origin_Country_Code = (long)reader["ORIGIN_COUNTRY_CODE"],
                            Destination_Country_Code = (long)reader["DESTINATION_COUNTRY_CODE"],
                            DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                            LandingTime = (DateTime)reader["LANDING_TIME"],
                            Remaining_Tickets = (int)reader["REMAINING_TICKETS"]
                        };

                        flightsByCustomer.Add(CurrentFlight);
                    }
                }
            }
            return flightsByCustomer;
        }

        public IList<Flight> GetFlightsByDestinationCountry(long CountryCode)
        {
            IList<Flight> flightsByCustomer = new List<Flight>();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandText = "GET_FLIGHTS_BY_DESTINATION_COUNTRY";
                cmd.Parameters.Add(new SqlParameter("@DestCountryCode", CountryCode));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Flight CurrentFlight = new Flight
                        {
                            FlightID = (long)reader["F_ID"],
                            AirLineCompany_ID = (long)reader["AIRLINECOMPANY_ID"],
                            Origin_Country_Code = (long)reader["ORIGIN_COUNTRY_CODE"],
                            Destination_Country_Code = (long)reader["DESTINATION_COUNTRY_CODE"],
                            DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                            LandingTime = (DateTime)reader["LANDING_TIME"],
                            Remaining_Tickets = (int)reader["REMAINING_TICKETS"]
                        };

                        flightsByCustomer.Add(CurrentFlight);
                    }
                }
            }
            return flightsByCustomer;
        }

        public IList<Flight> GetFlightsByLandingDate(DateTime LandingDate)
        {
            IList<Flight> flightsByCustomer = new List<Flight>();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandText = $"GET_FLIGHTS_BY_LANDING_DATE";
                cmd.Parameters.Add(new SqlParameter("@LandingDate", LandingDate));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Flight CurrentFlight = new Flight
                        {
                            FlightID = (long)reader["ID"],
                            AirLineCompany_ID = (long)reader["AIRLINECOMPANY_ID"],
                            Origin_Country_Code = (long)reader["ORIGIN_COUNTRY_CODE"],
                            Destination_Country_Code = (long)reader["DESTINATION_COUNTRY_CODE"],
                            DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                            LandingTime = (DateTime)reader["LANDING_TIME"],
                            Remaining_Tickets = (int)reader["REMAINING_TICKETS"]
                        };

                        flightsByCustomer.Add(CurrentFlight);
                    }
                }
            }
            return flightsByCustomer;
        }

        public IList<Flight> GetFlightsByOriginCountry(long CountryCode)
        {
            IList<Flight> flightsByCustomer = new List<Flight>();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandText = $"GET_FLIGHTS_BY_ORIGIN_COUNTRY";
                cmd.Parameters.Add(new SqlParameter("@OriginCountry", CountryCode));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Flight CurrentFlight = new Flight
                        {
                            FlightID = (long)reader["F_ID"], //according to my stored procedure this column got the name F_ID for flightsID.
                            AirLineCompany_ID = (long)reader["AIRLINECOMPANY_ID"],
                            Origin_Country_Code = (long)reader["ORIGIN_COUNTRY_CODE"],
                            Destination_Country_Code = (long)reader["DESTINATION_COUNTRY_CODE"],
                            DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                            LandingTime = (DateTime)reader["LANDING_TIME"],
                            Remaining_Tickets = (int)reader["REMAINING_TICKETS"]
                        };

                        flightsByCustomer.Add(CurrentFlight);
                    }
                }
            }
            return flightsByCustomer;
        }

        public void Remove(Flight item)
        {
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"DELETE FROM Flights WHERE ID = {item.FlightID}";

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Flight item)
        {
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                string format = "yyyy-MM-dd HH:mm:ss";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"UPDATE Flights " +
                    $"SET AIRLINECOMPANY_ID = {item.AirLineCompany_ID}, ORIGIN_COUNTRY_CODE = {item.Origin_Country_Code}, " +
                    $"DESTINATION_COUNTRY_CODE = {item.Destination_Country_Code}, DEPARTURE_TIME = '{item.DepartureTime.ToString(format)}', " +
                    $"LANDING_TIME = '{item.LandingTime.ToString(format)}', REMAINING_TICKETS = {item.Remaining_Tickets} " +
                    $"WHERE ID = {item.FlightID}";

                cmd.ExecuteNonQuery();
            }
        }
    }
}
