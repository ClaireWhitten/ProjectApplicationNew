using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication.Classes
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public string Street {get; set; }

        public string Number { get; set; }

        public string PostCode { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public double Salary { get; set; }

        public DateTime StartDate { get; set; }

        public Role Role { get; set; }




        public virtual UserAccount UserAccount { get; set; }

        public ICollection<PurchaseOrder> PurchaseOrders { get; set; } //only sellers

        public ICollection<SalesOrder> SalesOrders { get; set; } // only sellers

       
        public string EmployeeDetails
        {
            get
            {
                return $"Name: {this.FirstName} {this.LastName}, Employee ID: {this.EmployeeId}, DOB: {this.DOB}, Role:{this.Role}";
            }
        }

        public override string ToString()
        {
            return $"Name:  Employee ID: {this.EmployeeId}, {this.FirstName} {this.LastName}, Role:{this.Role}";
        }


    }
}
