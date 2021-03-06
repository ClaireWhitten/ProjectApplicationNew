using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication.Classes
{
    public class Customer
    {
        public int CustomerId { get; set; }
       
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string PostCode { get; set; }

        public string City { get; set; }


        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime CustomerSince { get; set; }

        public ICollection<SalesOrder> SalesOrders { get; set; }

        public override string ToString()
        {
            return $"{this.CustomerId}: {this.FirstName} {this.LastName}";
        }


    }
}
