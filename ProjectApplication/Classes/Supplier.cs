using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication.Classes
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public int Number { get; set; }

        public int PostCode { get; set; }

        public string Region { get; set; }
        public string Country { get; set; }

        public int PhoneNumber { get; set; }

        public string Email { get; set; } 

        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }

        public ICollection<Product> Products { get; set; }



    }
}
