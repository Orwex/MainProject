using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem
{
    public class Customer : IPOCO, IUser
    {
        public long Customer_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string CreditCardNumber { get; set; }

        public Customer()
        {
            
        }

        public Customer(string firstName, string lastName, 
            string username, string password, string address, string phoneNo, string creditCardNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Address = address;
            PhoneNo = phoneNo;
            CreditCardNumber = creditCardNumber;
        }


        public static bool operator ==(Customer c1, Customer c2)
        {
            if (ReferenceEquals(c1, null) && ReferenceEquals(c2, null))
            {
                return true;
            }
            if (ReferenceEquals(c1, null) || ReferenceEquals(c2, null))
            {
                return false;
            }
            if (c1.Customer_ID == c2.Customer_ID)
                return true;
            return false;
        }
        public static bool operator !=(Customer c1, Customer c2)
        {
            return !(c1 == c2);
        }

        public override bool Equals(object obj)
        {
            Customer otherCustomer = obj as Customer;
            if (otherCustomer == null)
                return false;
            return (otherCustomer.Customer_ID == this.Customer_ID);
        }

        public override int GetHashCode()
        {
            return (int)this.Customer_ID;
        }
    }
}
