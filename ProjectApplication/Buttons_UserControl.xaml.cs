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
                    Customer selectedCustomer = DataGrid.SelectedItem as Customer; // convert selected item in datagrid
                    if (selectedCustomer != null)
                    {
                        ObservableCollection<Customer> customers = this.DataContext as ObservableCollection<Customer>; // convert observable collection to correct type
                        MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete this customer?", "Confirm", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            customers.Remove(selectedCustomer);
                        }
                    } else
                    {
                        MessageBox.Show("No customer has been selected.");
                    }
                    break;


                case "EmployeeDataGrid":
                    Employee selectedEmployee = DataGrid.SelectedItem as Employee; // convert selected item in datagrid
                    if (selectedEmployee != null)
                    {
                        ObservableCollection<Employee> employees = this.DataContext as ObservableCollection<Employee>; // convert observable collection to correct type
                        MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete this Employee?", "Confirm", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            employees.Remove(selectedEmployee);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Employee has been selected.");
                    }
                    break;

                case "StockDataGrid":
                    Product selectedProduct = DataGrid.SelectedItem as Product; // convert selected item in datagrid
                    if (selectedProduct != null)
                    {
                        ObservableCollection<Product> stock = this.DataContext as ObservableCollection<Product>; // convert observable collection to correct type
                        MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete this Product?", "Confirm", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            stock.Remove(selectedProduct);
                        }
                    }
                    break;
                case "SuppliersDataGrid":
                    Supplier selectedSupplier = DataGrid.SelectedItem as Supplier; // convert selected item in datagrid
                    if (selectedSupplier!= null)
                    {
                        ObservableCollection<Supplier> suppliers = this.DataContext as ObservableCollection<Supplier>; // convert observable collection to correct type
                        MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete this Supplier?", "Confirm", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            suppliers.Remove(selectedSupplier);
                        }
                    }
                    break;

                case "UsersDataGrid":
                    UserAccount selectedUser = DataGrid.SelectedItem as UserAccount; // convert selected item in datagrid
                    if (selectedUser != null)
                    {
                        ObservableCollection<UserAccount> users = this.DataContext as ObservableCollection<UserAccount>; // convert observable collection to correct type
                        MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete this User?", "Confirm", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            users.Remove(selectedUser);
                        }
                    }

                    break;
            }
                
            Ctx.SaveChanges();

        }




    }
}
