using ProjectApplication.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ProjectApplication
{
    /// <summary>
    /// Interaction logic for Buttons_UserControl.xaml
    /// </summary>
    public partial class Buttons_UserControl : UserControl
    {
        public DataGrid DataGrid { get; set; }

        public ProjectApplicationContext Ctx { get; set; }



        public Buttons_UserControl(Object itemsource, DataGrid dataGrid, ProjectApplicationContext ctx)
        {
            InitializeComponent();
            Ctx = ctx;
            this.DataContext = itemsource; //set datacontext of button control to the observable collection 
            DataGrid = dataGrid;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            switch (DataGrid.Name)
            {
                case "CustomerDataGrid":
                    ObservableCollection<Customer> customers = this.DataContext as ObservableCollection<Customer>; // convert observable collection to correct type
                    Customer selectedCustomer = DataGrid.SelectedItem as Customer; // convert selected item in datagrid
                    customers.Remove(selectedCustomer);
                    break;
                case "EmployeeDataGrid":
                    ObservableCollection<Employee> employees = this.DataContext as ObservableCollection<Employee>; // convert observable collection to correct type
                    Employee selectedEmployee = DataGrid.SelectedItem as Employee; // convert selected item in datagrid
                    employees.Remove(selectedEmployee);
                    break;
                case "StockDataGrid":
                    ObservableCollection<Product> stock = this.DataContext as ObservableCollection<Product>; // convert observable collection to correct type
                    Product selectedProduct = DataGrid.SelectedItem as Product; // convert selected item in datagrid
                    stock.Remove(selectedProduct);
                    break;
                case "SuppliersDataGrid":
                    ObservableCollection<Supplier> suppliers = this.DataContext as ObservableCollection<Supplier>; // convert observable collection to correct type
                    Supplier selectedSupplier = DataGrid.SelectedItem as Supplier; // convert selected item in datagrid
                    suppliers.Remove(selectedSupplier);
                    break;
                case "UsersDataGrid":
                    ObservableCollection<UserAccount> users = this.DataContext as ObservableCollection<UserAccount>; // convert observable collection to correct type
                    UserAccount selectedUser = DataGrid.SelectedItem as UserAccount; // convert selected item in datagrid
                    users.Remove(selectedUser);
                    break;

            }
            Ctx.SaveChanges();

        }
    }
}
