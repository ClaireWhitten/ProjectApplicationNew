using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication.Classes
{
    [Flags] // add flags attribute to declare that the enum type declares bit fields (powers of two) - allows you to combine enum types 
    public enum OrderStatus
    {
        Confirmed = 0,
        Processing = 1,
        Packing = 2,
        Sent = 4,
        Delivered = 8


    }
}
