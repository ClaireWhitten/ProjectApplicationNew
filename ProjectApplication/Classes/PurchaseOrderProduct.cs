using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication.Classes
{
    public class PurchaseOrderProduct
    {
        [Key]
        [Column(Order = 1)]
        public int PurchaseOrderId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }
    }
}
