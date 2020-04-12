using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public class CustomerDAOMSSQL : ICustomerDAO
    {
        static SqlCommand cmd = new SqlCommand();

        public void Add(Customer item)
        {
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"INSERT INTO Customers (FIRST_NAME, LAST_NAME, USER_NAME,PASSWORD, ADDRESS, PHONE_NO, CREDIT_CARD_NUMBER) "
                    + $" Values ('{item.FirstName}', '{item.LastName}', '{item.Username}', '{item.Password}', '{item.Address}', '{item.PhoneNo}', {item.CreditCardNumber})";

                cmd.ExecuteNonQuery();
            }
        }

        public Customer Get(int id)
        {
            Customer resultCountry = new Customer();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT * FROM Customers WHERE ID = {id}";
                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        resultCountry = new Customer()
                        {
                            Customer_ID = (long)reader["ID"],
                            FirstName = (string)reader["FIRST_NAME"],
                            LastName = (string)reader["LAST_NAME"],
                            Username = (string)reader["USER_NAME"],
                            Password = (string)reader["PASSWORD"],
                            Address = (string)reader["ADDRESS"],
                            PhoneNo = (string)reader["PHONE_NO"],
                            CreditCardNumber = (string)reader["CREDIT_CARD_NUMBER"]
                        };
                    }
                }
            }
            return resultCountry;
        }
                
        public Customer GetCustomerByUserName(string UserName)
        {
            Customer resultCountry = new Customer();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT * FROM Customers WHERE USER_NAME = '{UserName}'";
                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        resultCountry = new Customer()
                        {
                            Customer_ID = (long)reader["ID"],
                            FirstName = (string)reader["FIRST_NAME"],
                            LastName = (string)reader["LAST_NAME"],
                            Username = (string)reader["USER_NAME"],
                            Password = (string)reader["PASSWORD"],
                            Address = (string)reader["ADDRESS"],
                            PhoneNo = (string)reader["PHONE_NO"],
                            CreditCardNumber = (string)reader["CREDIT_CARD_NUMBER"]
                        };
                    }
                }
            }
            return resultCountry;
        }

        public void Remove(Customer item)
        {
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"DELETE FROM Customers WHERE ID='{item.Customer_ID}'";

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Customer item)
        {
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"UPDATE Customers SET " +
                    $"FIRST_NAME = '{item.FirstName}', LAST_NAME= '{item.FirstName}', " +
                    $"USER_NAME= '{item.Username}',PASSWORD= '{item.Password}', ADDRESS= '{item.Address}', " +
                    $"PHONE_NO= '{item.PhoneNo}', CREDIT_CARD_NUMBER= '{item.CreditCardNumber}' " +
                    $"WHERE ID = {item.Customer_ID}";

                cmd.ExecuteNonQuery();
            }
        }

        public IList<Customer> GetAll()
        {
            IList<Customer> resultCustomer = new List<Customer>();
            using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT * FROM Customers";
                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer CurrentCustomer = new Customer
                        {
                            Customer_ID = (long)reader["ID"],
                            FirstName = (string)reader["FIRST_NAME"],
                            LastName = (string)reader["LAST_NAME"],
                            Username = (string)reader["USER_NAME"],
                            Password = (string)reader["PASSWORD"],
                            Address = (string)reader["ADDRESS"],
                            PhoneNo = (string)reader["PHONE_NO"],
                            CreditCardNumber = (string)reader["CREDIT_CARD_NUMBER"]
                        };

                        resultCustomer.Add(CurrentCustomer);
                    }
                }
            }
            return resultCustomer;
        }
    }
}
