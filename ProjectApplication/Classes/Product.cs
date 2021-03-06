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

        public int RegisteredProductId { get; set; }

        public int? SupplierId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string BarCode { get; set; }

        public ProductCategory ProductCategory { get; set; }

        public bool Sold { get; set; }



        public Supplier Supplier { get; set; }

        public RegisteredProduct RegisteredProduct { get; set; }

        public ICollection<PurchaseOrderProduct> PurchaseOrderProducts { get; set; }

        public ICollection<SalesOrderProduct> SalesOrderProducts { get; set; }

       
    }
}
