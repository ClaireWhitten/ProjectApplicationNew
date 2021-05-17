using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication.Classes
{
    [Flags] // add flags attribute to declare that the enum type declares bit fields (powers of two) - allows you to combine enum types (e.g. give 2 roles)
    public enum Role
    {
        Administrator = 0,
        SalesEmployee = 1,
        WarehouseEmployee = 2
    }
}
