using ProjectApplication.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectApplication.Overview
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Overview_UserControl : UserControl
    {
        public ProjectApplicationContext Ctx { get; set; }

        public Overview_UserControl(ProjectApplicationContext ctx)
        {
            
            Ctx = ctx;
            InitializeComponent();

            DateTime date = DateTime.Now;
            lblDateTime.Content = date.ToString("HH:mm");

            lblWelcome.Content = $"Welcome {MainMenu.User.Employee.FirstName} {MainMenu.User.Employee.LastName} \n Employee Number: {MainMenu.User.Employee.EmployeeId}";

           
            
            //new employees
            DateTime today = DateTime.Now;
            DateTime onemonthago = today.AddDays(-30);
            var newEmployees = Ctx.Employees.Where(e => e.StartDate > onemonthago).ToList();
            lbnewStaff.ItemsSource = newEmployees;


            //top sellers
            var topSellers = Ctx.Employees
                .Include("SalesOrders")
                .OrderByDescending(e => e.SalesOrders.Count)
                .Select(e => new
                {
                    Name = e.FirstName + e.LastName,
                    NumberofSales = e.SalesOrders.Count
                })
                .ToList();
            /* foreach (var item in topSellers)
             {
                 MessageBox.Show(item.Name + item.NumberofSales.ToString());
             }*/

            lbTopSellers.ItemsSource = topSellers;

        }
    }
}
