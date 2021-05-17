using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication.Classes
{
    public class Product
    {
        public int ProductId { get; set; }

        public int? SupplierId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }



        public Supplier Supplier { get; set; }

        public ICollection<PurchaseOrderProduct> PurchaseOrderProducts { get; set; }

        public ICollection<SalesOrderProduct> SalesOrderProducts { get; set; }
    }
}
