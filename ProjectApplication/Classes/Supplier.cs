﻿using System;
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

        public string Number { get; set; }

        public string PostCode { get; set; }

        public string City { get; set; }
        public string Country { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime SupplierSince { get; set; }

        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }

        public ICollection<Product> Products { get; set; }



    }
}
