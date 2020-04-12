using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public class CountryDAOMSSQL : ICountryDAO
    {
        public static SqlCommand cmd = new SqlCommand();

        public void Add(Country item)
        {
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"INSERT INTO Countries (COUNTRY_NAME) Values ({item.CountryName})";

                cmd.ExecuteNonQuery();
            }
        }

        public Country Get(int id)
        {
            Country resultCountry = new Country();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT * FROM Countries WHERE ID = {id}";
                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        resultCountry = new Country()
                        {
                            CountyID = (int)reader["ID"],
                            CountryName = (string)reader["COUNTRY_NAME"]
                        };
                    }
                }
            }
            return resultCountry;
        }

        public IList<Country> GetAll()
        {
            IList<Country> resultCountry = new List<Country>();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT * FROM Countries";
                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Country CurrentCountry = new Country
                        {
                            CountyID = (int)reader["ID"],
                            CountryName = (string)reader["COUNTRY_NAME"]
                        };

                        resultCountry.Add(CurrentCountry);
                    }
                }
            }
            return resultCountry;
        }

        public void Remove(Country item)
        {
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"DELETE FROM Countries WHERE COUNTRY_NAME='{item.CountryName}'";

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Country item)
        {
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"UPDATE Countries SET COUNTRY_NAME = '{item.CountryName}' " +
                    $"WHERE ID = {item.CountyID}";

                cmd.ExecuteNonQuery();
            }
        }        
    }
}
