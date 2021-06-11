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
using System.Windows.Shapes;
using ProjectApplication.Classes;

namespace ProjectApplication.CreateUpdateWindows
{
    /// <summary>
    /// Interaction logic for Customer_AddEdit.xaml
    /// </summary>
    public partial class Customer_AddEdit : Window
    {

        public Customer SelectedCustomer { get; set; }

        public ProjectApplicationContext Ctx { get; set; }

        public ObservableCollection<Customer> Customers { get; set; }

     
        //add employee form
        public Customer_AddEdit(ProjectApplicationContext ctx, ObservableCollection<Customer> customers)
        {
            Ctx = ctx;
            Customers = customers;
            InitializeComponent();
            tbCustomerId.Visibility = Visibility.Hidden;
            lblCustomerId.Visibility = Visibility.Hidden;
        }

        //edit employee form
        public Customer_AddEdit(ProjectApplicationContext ctx, ObservableCollection<Customer> customers, Customer selectedCustomer)
        {
            Ctx = ctx;
            Customers = customers;
            SelectedCustomer = selectedCustomer;
            this.DataContext = selectedCustomer;
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            if (SelectedCustomer == null)
            {
                Customer newCustomer = new Customer()
                {
                    FirstName = tbFirstName.Text,
                    LastName = tbLastName.Text,
                    CustomerSince = (DateTime)dpCustomerSince.SelectedDate,
                    PhoneNumber = tbPhoneNumber.Text,
                    Street = tbStreet.Text,
                    PostCode = tbPostcode.Text,
                    Number = tbNumber.Text,
                    Email = tbEmail.Text,
                    City = tbCity.Text
                };
                Ctx.Customers.Add(newCustomer);
                Ctx.SaveChanges();
                this.Close();
            }
            else
            {
                
                SelectedCustomer.FirstName = tbFirstName.Text;
                SelectedCustomer.LastName = tbLastName.Text;
                SelectedCustomer.CustomerSince = (DateTime)dpCustomerSince.SelectedDate;
                SelectedCustomer.PhoneNumber = tbPhoneNumber.Text;
                SelectedCustomer.Street = tbStreet.Text;
                SelectedCustomer.PostCode = tbPostcode.Text;
                SelectedCustomer.Number = tbNumber.Text;
                SelectedCustomer.Email = tbEmail.Text;
                SelectedCustomer.City = tbCity.Text;

                CollectionViewSource.GetDefaultView(Customers).Refresh();
                Ctx.SaveChanges();
                this.Close();

            }
            
        }
    }
}
