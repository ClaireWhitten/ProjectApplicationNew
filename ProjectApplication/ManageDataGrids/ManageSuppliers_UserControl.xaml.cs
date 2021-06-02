using ProjectApplication.Classes;
using ProjectApplication.CreateUpdateWindows;
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


namespace ProjectApplication
{
    /// <summary>
    /// Interaction logic for ManageSuppliers_UserControl.xaml
    /// </summary>
    public partial class ManageSuppliers_UserControl : UserControl
    {

        public ProjectApplicationContext Ctx{ get; set; }

        public ObservableCollection<Supplier> Suppliers { get; set; }

        public ManageSuppliers_UserControl(ProjectApplicationContext ctx)
        {
            Ctx = ctx;
            Ctx.Suppliers.AsQueryable().Load();
            Suppliers = Ctx.Suppliers.Local;
            InitializeComponent();
            SuppliersDataGrid.ItemsSource = Suppliers;
            
        }

        //show add form
        private void Suppliers_AddClickedEventHandler(object sender, RoutedEventArgs e)
        {
            Supplier_AddEdit addSupplierForm = new Supplier_AddEdit(Ctx, Suppliers);
            addSupplierForm.Show();
        }

        //show edit form
        private void Suppliers_EditClickedEventHandler(object sender, RoutedEventArgs e)
        {
            Supplier supplier = SuppliersDataGrid.SelectedItem as Supplier;
            Supplier_AddEdit editSupplierForm = new Supplier_AddEdit(Ctx, Suppliers, supplier);
            editSupplierForm.Show();
        }


        //delete selected item
        private void Suppliers_DeleteClickedEventHandler(object sender, RoutedEventArgs e)
        {
            Supplier supplier = SuppliersDataGrid.SelectedItem as Supplier;
           
            if (supplier != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the following supplier: {supplier.Name}?", "Confirm", MessageBoxButton.YesNo);


                if (result == MessageBoxResult.Yes)
                {
                    Suppliers.Remove(supplier);
                    Ctx.SaveChanges();
                    MessageBox.Show($"The supplier {supplier.Name} is succussfully deleted.", "Deleted");
                }
                else
                {
                    MessageBox.Show($"The supplier has not been deleted.", "Action Cancelled");
                }

            }
            else
            {
                MessageBox.Show("No supplier has been selected.");
            }

        }
    }
}
