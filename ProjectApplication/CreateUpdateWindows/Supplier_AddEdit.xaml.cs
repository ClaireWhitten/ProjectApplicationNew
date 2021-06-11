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
using System.Windows.Shapes;

namespace ProjectApplication.CreateUpdateWindows
{
    /// <summary>
    /// Interaction logic for Supplier_AddEdit.xaml
    /// </summary>
    public partial class Supplier_AddEdit : Window
    {

        public ProjectApplicationContext Ctx { get; set; }

        public ObservableCollection<Supplier> Suppliers { get; set;}

        public Supplier SelectedSupplier { get; set; }

        //add contructor
        public Supplier_AddEdit(ProjectApplicationContext ctx, ObservableCollection<Supplier> suppliers)
        {
            Ctx = ctx;
            Suppliers = suppliers;
            InitializeComponent();
            this.Title = "Add Supplier";
            tbSupplierId.Visibility = Visibility.Hidden;
            lblSupplierId.Visibility = Visibility.Hidden;
        }


        //edit constructor
        public Supplier_AddEdit(ProjectApplicationContext ctx, ObservableCollection<Supplier> suppliers, Supplier selectedSupplier)
        {
            Ctx = ctx;
            SelectedSupplier = selectedSupplier;
            Suppliers = suppliers;
            InitializeComponent();
            this.Title = "Edit Supplier";
            this.DataContext = selectedSupplier;
            dpCustomerSince.IsEnabled = false;
            tbSupplierId.IsEnabled = false;


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSupplier == null)
            {

                Supplier newSupplier = new Supplier()
                {
                    Name = tbName.Text,
                    Street = tbStreet.Text,
                    Number = tbNumber.Text,
                    PostCode = tbPostcode.Text,
                    City = tbCity.Text,
                    Country = tbCountry.Text,
                    PhoneNumber = tbPhoneNumber.Text,
                    Email = tbEmail.Text,
                    SupplierSince = DateTime.Now
                };

                Ctx.Suppliers.Add(newSupplier);
                Ctx.SaveChanges();
                this.Close();

            }
            else
            {

                SelectedSupplier.Name = tbName.Text;
                SelectedSupplier.Street = tbStreet.Text;
                SelectedSupplier.Number = tbNumber.Text;
                SelectedSupplier.PostCode = tbPostcode.Text;
                SelectedSupplier.City = tbCity.Text;
                SelectedSupplier.Country = tbCountry.Text;
                SelectedSupplier.PhoneNumber = tbPhoneNumber.Text;
                SelectedSupplier.Email = tbEmail.Text;
                



                CollectionViewSource.GetDefaultView(Suppliers).Refresh();
                Ctx.SaveChanges();
                this.Close();

            }
        }
    }

}


