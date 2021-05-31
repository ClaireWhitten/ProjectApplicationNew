using ProjectApplication.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using ProjectApplication.CreateUpdateWindows;

namespace ProjectApplication
{
    /// <summary>
    /// Interaction logic for ManageCustomers_UserControl.xaml
    /// </summary>
    public partial class ManageCustomers_UserControl : UserControl
    {

        public ProjectApplicationContext Ctx { get; set; }

        public ObservableCollection<Customer> Customers { get; set; }

        public ManageCustomers_UserControl(ProjectApplicationContext ctx)
        {
            
            Ctx = ctx;

            Ctx.Customers.Load();

            Customers = Ctx.Customers.Local;

            InitializeComponent();


            CustomerDataGrid.ItemsSource = Customers;


        }

     

        //Event handlers for Edit/Add/Delete Customers
        private void Customers_EditClickedEventHandler(object sender,RoutedEventArgs e)
        {
            
            Customer customer = CustomerDataGrid.SelectedItem as Customer;
            Customer_AddEdit editCustomerForm = new Customer_AddEdit(Ctx, Customers, customer);
            editCustomerForm.Show();

        }





        private void Customers_AddClickedEventHandler(object sender, RoutedEventArgs e)
        {
            Customer_AddEdit addCustomerForm = new Customer_AddEdit(Ctx, Customers);
            addCustomerForm.Show();
        }




        private void Customers_DeleteClickedEventHandler(object sender, RoutedEventArgs e)
        {
            Customer customer = CustomerDataGrid.SelectedItem as Customer; // can this be null?
            if (customer != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the following user: {customer.FirstName}?", "Confirm", MessageBoxButton.YesNo);


                if (result == MessageBoxResult.Yes)
                {
                    Customers.Remove(customer); // removes from observable collection 
                    Ctx.SaveChanges();
                    MessageBox.Show($"The customer {customer.FirstName} is succussfully deleted", "Deleted");
                }
                else
                {
                    MessageBox.Show($"The customer has not been deleted.", "Action Cancelled");
                }

            }
            else
            {
                MessageBox.Show("No customer has been selected.");
            }

        }


    }
}
