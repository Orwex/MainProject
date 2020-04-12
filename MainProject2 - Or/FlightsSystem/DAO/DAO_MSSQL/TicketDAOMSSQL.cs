using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public class TicketDAOMSSQL : ITicketDAO
    {
        static SqlCommand cmd = new SqlCommand();
                
        public void Add(Ticket item)
        {
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"INSERT INTO Tickets (FLIGHT_ID, CUSTOMER_ID) Values ({item.FlightID}, {item.CustomerID})";

                cmd.ExecuteNonQuery();
            }
        }

        public Ticket Get(int id)
        {
            Ticket resultTicket = new Ticket();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT * FROM Tickets WHERE ID = {id}";
                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        resultTicket = new Ticket()
                        {
                            TicketID = (int)reader["ID"],
                            FlightID = (int)reader["FLIGHT_ID"],
                            CustomerID = (int)reader["CUSTOMER_ID"]
                        };
                    }
                }
            }
            return resultTicket;
        }

        public IList<Ticket> GetAll()
        {
            IList<Ticket> resultTicket = new List<Ticket>();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT * FROM Tickets";
                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Ticket CurrentTicket = new Ticket()
                        {
                            TicketID = (long)reader["ID"],
                            FlightID = (long)reader["FLIGHT_ID"],
                            CustomerID = (long)reader["CUSTOMER_ID"]
                        };

                        resultTicket.Add(CurrentTicket);
                    }
                }
            }
            return resultTicket;
        }

        public void Remove(Ticket item)
        {
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"DELETE FROM Tickets WHERE ID = {item.TicketID}";

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Ticket item)
        {
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"UPDATE Tickets SET FLIGHT_ID = {item.FlightID}, CUSTOMER_ID = {item.CustomerID} WHERE ID =  {item.TicketID}";

                cmd.ExecuteNonQuery();
            }
        }

        
    }
}
