using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication.Classes
{
    public class RegisteredProduct
    {
        public int RegisteredProductId { get; set; }

        public int? SupplierId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string BarCode { get; set; }

        public ProductCategory ProductCategory { get; set; }


        public Supplier Supplier { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
