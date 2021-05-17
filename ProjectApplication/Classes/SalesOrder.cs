using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication.Classes
{
    public class SalesOrder
    {
        public int SalesOrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public int TotalPrice { get; set; }

        public int CustomerId { get; set; }

        public int EmployeeId { get; set; }

        public bool Paid { get; set; }




        public Customer Customer { get; set; }

        public Employee Employee { get; set; }

        public ICollection<SalesOrderProduct> SalesOrderProducts { get; set; }

        public virtual Invoice Invoice { get; set; }


    }
}
