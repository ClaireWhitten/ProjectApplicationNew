using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication.Classes
{
    public class PurchaseOrder
    {
        public int PurchaseOrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public double TotalPrice { get; set; }

        public int SupplierId { get; set; }

        public int EmployeeId { get; set; }

        public bool Paid { get; set; }



        public OrderStatus OrderStatus { get; set; }

        public Employee Employee { get; set; }

        public Supplier Supplier { get; set; }

        public ICollection<PurchaseOrderProduct> PurchaseOrderProducts { get; set; }
    }


}

