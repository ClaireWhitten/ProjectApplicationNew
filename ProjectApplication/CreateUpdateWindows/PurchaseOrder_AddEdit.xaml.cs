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
    /// Interaction logic for PurchaseOrder_AddEdit.xaml
    /// </summary>
    public partial class PurchaseOrder_AddEdit : Window
    {
        ProjectApplicationContext Ctx { get; set; }

        Supplier SelectedSupplier { get; set; }

        Product SelectedProduct { get; set; }

        ObservableCollection<Product> ProductsOrdered { get; set; }


        public PurchaseOrder_AddEdit(ProjectApplicationContext ctx)
        {
            Ctx = ctx;
            ProductsOrdered = new ObservableCollection<Product>();
            InitializeComponent();
            tbOrderId.Visibility = Visibility.Hidden;
            lvOrderedProducts.ItemsSource = ProductsOrdered;

            //List warehouse employees and administrators
            var purchaseEmployees = Ctx.Employees.Where(e => e.Role == Role.Administrator || e.Role == Role.WarehouseEmployee).ToList();
            cbEmployee.ItemsSource = purchaseEmployees;

            //List existing suppliers 
            var currentSuppliers = Ctx.Suppliers.ToList();
            cbSupplier.ItemsSource = currentSuppliers;
        }



        private void cbSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedSupplier = cbSupplier.SelectedItem as Supplier;
            if (SelectedSupplier != null)
            {
                int supplierId = SelectedSupplier.SupplierId;
                //List products from that supplier
                var SupplierProducts = Ctx.Products.Include("Supplier") //set to list of registered products 
                    .Where(p => p.Supplier.SupplierId == supplierId)
                    .ToList();
                cbProducts.ItemsSource = SupplierProducts;
            }
            else
            {
                MessageBox.Show("You must select a supplier.");
            }

        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SelectedProduct = cbProducts.SelectedItem as Product;
            int quantity = Convert.ToInt32(UpDownQuantity.Value);
            if (SelectedProduct != null)
            {
                for (int i = 0; i < quantity; i++)
                {
                    ProductsOrdered.Add(new Product()
                    {
                        BarCode = SelectedProduct.BarCode,
                        Name = SelectedProduct.Name,
                        Description = SelectedProduct.Description,
                        Price = SelectedProduct.Price,
                        Supplier = SelectedSupplier,
                    });
                }
            }
            else
            {
                MessageBox.Show("You must select a product");
            }
        }
    }
}
                
       
