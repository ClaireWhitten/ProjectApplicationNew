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
using System.Data.Entity;

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

            lblWelcome.Content = $"Welcome {MainMenu.User.Employee.FirstName} {MainMenu.User.Employee.LastName} \n Employee Number: {MainMenu.User.Employee.EmployeeId}!  ";

           
            
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


            //current stock
            var currentStock = Ctx.Products
                .GroupBy(p => p.Name)
                .Select(p => new
                {
                    Name = p.Key + " " + p.FirstOrDefault().Name,
                    NumberInStock = p.Count()
                })
                .ToList();
            lvStock.ItemsSource = currentStock;


            //sales order  total
            DateTime oneweekago = today.AddDays(-7);
            var weeklySales = Ctx.SalesOrders.Where(s => s.OrderDate > oneweekago).ToList();
            double totalSales = 0;

            foreach (var sale in weeklySales)
            {
                totalSales += sale.TotalPrice;
            }
            tbSalesNumber.Text = $"{weeklySales.Count()}  £{totalSales.ToString()}";

            //purchase order  total
            var weeklyPurchases = Ctx.PurchaseOrders.Where(p => p.OrderDate > oneweekago).ToList();
            double totalPurchases = 0;
            foreach (var purchase in weeklyPurchases)
            {
                totalPurchases += purchase.TotalPrice;
            }

            tbPurchases.Text = $"{weeklyPurchases.Count()}  £{totalPurchases.ToString()}";



            // profit 
            double profit = totalSales - totalPurchases;
            tbProfit.Text = $" £{profit}";


            //Most popular products 
            var salesOrders = Ctx.SalesOrders
                .Include(so => so.SalesOrderProducts.Select(p => p.Product))
                .ToList();

            var productGroups = Ctx.Products
                .GroupBy(p => p.BarCode)
                .ToList();

            List<Product> products = new List<Product>();

            foreach (var group in productGroups)
            {
                products.Add(group.First());
                MessageBox.Show(group.First().BarCode);
                
            }


            int occurences = 0;
            Dictionary<Product, int> productOccurences = new Dictionary<Product, int>();

            for (int i = 0; i < products.Count(); i++)
            {
               
                foreach (var order in salesOrders)
                {
                    if (order.SalesOrderProducts.ToList().Exists(p => p.Product.BarCode == products[i].BarCode))
                    {
                        
                        occurences++;
                    }
                }

                productOccurences.Add(products[i], occurences);
                MessageBox.Show(occurences.ToString());
                occurences = 0;
            }

            List<KeyValuePair<Product, int>> sortedList = productOccurences.ToList();
            sortedList.Sort((x, y) => x.Value.CompareTo(y.Value));
            sortedList.Reverse();
            lvPopularProducts.ItemsSource = sortedList;

         

            



            /*method 
            private double calculateTotal(List<T>)
            {

            }*/
        }
    }
}
