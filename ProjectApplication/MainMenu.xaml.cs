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
using System.Windows.Shapes;
using ProjectApplication.Classes;

namespace ProjectApplication
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {

        public UserAccount User { get; set; }

        public List<string> DataSubMenuItems = new List<string>();
        public List<string> OverviewSubMenuItems = new List<string>();
        public List<string> OrdersSubMenuItems = new List<string>();

        public ProjectApplicationContext Ctx { get; set; } = new ProjectApplicationContext();


        public MainMenu(UserAccount user)
        {
            User = user;
            InitializeComponent();
           
            switch (User.Role)
            {
                case Role.Administrator:
                    DataSubMenuItems.Add("Manage Users");
                    DataSubMenuItems.Add("Manage Employees");
                    DataSubMenuItems.Add("Manage Stock");
                    DataSubMenuItems.Add("Manage Suppliers");
                    DataSubMenuItems.Add("Manage Customers");
                    OverviewSubMenuItems.Add("Employee Overview");
                    OverviewSubMenuItems.Add("Warehouse Overview");
                    OverviewSubMenuItems.Add("Supplier Overview");
                    OverviewSubMenuItems.Add("Products Overview");
                    OrdersSubMenuItems.Add("Sales");
                    OrdersSubMenuItems.Add("Purchases");
                    break;
                case Role.SalesEmployee:
                    DataSubMenuItems.Add("Manage Customers");

                    OrdersSubMenuItems.Add("Sales");
                    OrdersSubMenuItems.Add("Purchases");

                    break;
                case Role.WarehouseEmployee:
                    DataSubMenuItems.Add("Manage Stock");
                    OverviewSubMenuItems.Add("Warehouse Overview");
                    OverviewSubMenuItems.Add("Products Overview");
                    OrdersSubMenuItems.Add("Sales Orders");
                    OrdersSubMenuItems.Add("Purchase Orders");
                    break;
            }

            DataSubMenu.ItemsSource = DataSubMenuItems;
            OverviewSubMenu.ItemsSource = OverviewSubMenuItems;
            OrdersSubmenu.ItemsSource = OrdersSubMenuItems;

        }

       

        private void MenuItem_Click(object sender, RoutedEventArgs e) 
        {
            MenuItem menuItem = e.OriginalSource as MenuItem;
            string selected = menuItem.Header.ToString();
            
            switch (selected) 
            {
                case "Manage Users":
                    this.MainContentControl.Content = new ManageUsers_UserControl(Ctx);
                    break;
                case "Manage Employees":
                    this.MainContentControl.Content = new ManageEmployees_UserControl(Ctx);
                    break;
                case "Manage Suppliers":
                    this.MainContentControl.Content = new ManageSuppliers_UserControl(Ctx);
                    break;
                case "Manage Stock":
                    this.MainContentControl.Content = new ManageStock_UserControl(Ctx);
                    break;
                case "Manage Customers":
                    this.MainContentControl.Content = new ManageCustomers_UserControl(Ctx);
                    break;
            }
        }
    }
}
