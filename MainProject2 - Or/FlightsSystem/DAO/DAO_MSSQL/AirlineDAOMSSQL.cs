using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public class AirlineDAOMSSQL : IAirLineDAO
    {
        public static SqlCommand cmd = new SqlCommand();
                
        public void Add(AirlineCompany item)
        {
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"INSERT INTO AirlineCompanies " +
                    $"(AIRLINE_NAME, USER_NAME, PASSWORD, COUNTRY_CODE) " +
                    $"Values ('{item.AirLineName}', '{item.UserName}', '{item.Password}', {item.CountryCode})";

                cmd.ExecuteNonQuery();
            }
        }

        public AirlineCompany Get(int id)
        {
            AirlineCompany resultAirlineCompany = new AirlineCompany();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT * FROM AirlineCompanies WHERE ID = {id}";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        resultAirlineCompany = new AirlineCompany()
                        {
                            Airline_ID = (int)reader["ID"],
                            AirLineName = (string)reader["AIRLINE_NAME"],
                            UserName = (string)reader["USER_NAME"],
                            Password = (string)reader["PASSWORD"],
                            CountryCode = (int)reader["COUNTRY_CODE"]
                        };
                    }
                }
            }
            return resultAirlineCompany;
        }

        public AirlineCompany GetAirlineByUsername(string UserName)
        {
            AirlineCompany resultAirlineCompany = new AirlineCompany();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT * FROM AirlineCompanies WHERE USER_NAME = '{UserName}'";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        resultAirlineCompany = new AirlineCompany()
                        {
                            Airline_ID = (long)reader["ID"],
                            AirLineName = (string)reader["AIRLINE_NAME"],
                            UserName = (string)reader["USER_NAME"],
                            Password = (string)reader["PASSWORD"],
                            CountryCode = (long)reader["COUNTRY_CODE"]
                        };
                    }
                }
            }
            return resultAirlineCompany;
        }

        public IList<AirlineCompany> GetAllAirLinesByCountry(int CountryId)
        {
            IList<AirlineCompany> resultAirlineCompany = new List<AirlineCompany>();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT * FROM AirlineCompanies WHERE COUNTRY_CODE = {CountryId}";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AirlineCompany CurrentresultAirlineCompany = new AirlineCompany()
                        {
                            Airline_ID = (int)reader["ID"],
                            AirLineName = (string)reader["AIRLINE_NAME"],
                            UserName = (string)reader["USER_NAME"],
                            Password = (string)reader["PASSWORD"],
                            CountryCode = (int)reader["COUNTRY_CODE"]
                        };

                        resultAirlineCompany.Add(CurrentresultAirlineCompany);
                    }
                }
            }
            return resultAirlineCompany;
        }

        public void Remove(AirlineCompany item)
        {
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"DELETE FROM AirlineCompanies WHERE ID = {item.Airline_ID} ";

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(AirlineCompany item)
        {
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"UPDATE AirlineCompanies " +
                    $"SET AIRLINE_NAME = '{item.AirLineName}', USER_NAME = '{item.UserName}', " +
                    $"PASSWORD = '{item.Password}', COUNTRY_CODE = '{item.CountryCode}' " +
                    $"WHERE ID =  {item.Airline_ID}";

                cmd.ExecuteNonQuery();
            }
        }

        public IList<AirlineCompany> GetAll()
        {
            IList<AirlineCompany> resultAirlineCompany = new List<AirlineCompany>();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT * FROM AirlineCompanies";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AirlineCompany CurrentresultAirlineCompany = new AirlineCompany()
                        {
                            Airline_ID = (long)reader["ID"],
                            AirLineName = (string)reader["AIRLINE_NAME"],
                            UserName = (string)reader["USER_NAME"],
                            Password = (string)reader["PASSWORD"],
                            CountryCode = (long)reader["COUNTRY_CODE"]
                        };

                        resultAirlineCompany.Add(CurrentresultAirlineCompany);
                    }
                }
            }
            return resultAirlineCompany;
        }

        public IList<Ticket> GetAllTicketsOfTheAirline(long AirlineId)
        {
            IList<Ticket> TicketsOfTheAirline = new List<Ticket>();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandText = $"GET_ALL_TICKETS_OF_THE_AIRLINE";
                cmd.Parameters.Add(new SqlParameter("@AirlineId", AirlineId));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Ticket CurrentTicket = new Ticket
                        {
                            TicketID = (long)reader["TIKTS_ID"],
                            FlightID = (long)reader["FL_ID"],
                            CustomerID = (long)reader["CUSTOMER_ID"]
                        };

                        TicketsOfTheAirline.Add(CurrentTicket);
                    }
                }
            }
            return TicketsOfTheAirline;
        }
    }
}
