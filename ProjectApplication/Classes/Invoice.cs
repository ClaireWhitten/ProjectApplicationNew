using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication.Classes
{
    public class Invoice
    {
        [ForeignKey("SalesOrder")]
        public int InvoiceId { get; set; }

        public DateTime Date { get; set; }

        public bool Paid { get; set; }


        public virtual SalesOrder SalesOrder { get; set; }


    }
}
