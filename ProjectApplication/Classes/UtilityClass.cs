using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication.Classes
{
    public static class UtilityClass
    {

        // methods

        //methods for invoice
        public static double PriceWithBTW(double totalPrice)
        {
            return totalPrice * 1.21;
        }

        public  static double BTW(double totalPrice)
        {
            return totalPrice * 0.21;
        }




    }
}
