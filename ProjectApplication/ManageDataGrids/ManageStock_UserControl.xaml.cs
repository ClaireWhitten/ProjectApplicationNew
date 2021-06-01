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
using ProjectApplication.Classes;

namespace ProjectApplication
{
    /// <summary>
    /// Interaction logic for ManageProducts_UserControl.xaml
    /// </summary>
    public partial class ManageStock_UserControl : UserControl
    {

        public  ProjectApplicationContext Ctx { get; set; }

        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        public Product SelectedProduct { get; set; }

        public ManageStock_UserControl(ProjectApplicationContext ctx)
        {
            Ctx = ctx;
            Ctx.Products.Include("Supplier").Load();
            Products = Ctx.Products.Local;
            InitializeComponent();
            StockDataGrid.ItemsSource = Products;
            
        }

        private void Stock_AddClickedEventHandler(object sender, RoutedEventArgs e)
        {
            Product products = StockDataGrid.SelectedItem as Product;
            Product_AddEdit addProductForm = new Product_AddEdit(Ctx, Products);
            addProductForm.Show();
        }


        private void Stock_EditClickedEventHandler(object sender, RoutedEventArgs e)
        {
            Product product = StockDataGrid.SelectedItem as Product;
            Product_AddEdit editProductForm = new Product_AddEdit(Ctx, Products, product);
            editProductForm.Show();
        }


        private void Stock_DeleteClickedEventHandler(object sender, RoutedEventArgs e)
        {
            Product product = StockDataGrid.SelectedItem as Product;

            if (product != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the following product: {product.Name}?", "Confirm", MessageBoxButton.YesNo);


                if (result == MessageBoxResult.Yes)
                {
                    Products.Remove(product);
                    Ctx.SaveChanges();
                    MessageBox.Show($"The product {product.Name} is succussfully deleted.", "Deleted");
                }
                else
                {
                    MessageBox.Show($"The product has not been deleted.", "Action Cancelled");
                }

            }
            else
            {
                MessageBox.Show("No product has been selected.");
            }

        }
    }
}
