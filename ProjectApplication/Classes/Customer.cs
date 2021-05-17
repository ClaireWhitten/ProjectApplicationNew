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

        public int Number { get; set; }

        public int PostCode { get; set; }

        public string Region { get; set; }


        public int PhoneNumber { get; set; }

        public string Email { get; set; }

        public ICollection<SalesOrder> SalesOrders { get; set; }


    }
}
