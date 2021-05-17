using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication.Classes
{
    public class UserAccount
    {
        [ForeignKey("Employee")]
        public int UserAccountId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime CreatedOn { get; set; }

        public Role Role { get; set; }


        public virtual Employee Employee { get; set; }
        
        public string EmployeeName
        {
            get
            {
                return $"{this.Employee.FirstName} {this.Employee.LastName}";
            }
            
        }

        public string EmployeeEmail
        {
            get
            {
                return $"{this.Employee.Email}";
            }
        }
    }
}
