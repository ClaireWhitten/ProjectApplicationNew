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
    /// Interaction logic for SalesOrder_AddEdit.xaml
    /// </summary>
    public partial class SalesOrder_AddEdit : Window
    {

        SalesOrder SelectedSalesOrder { get; set; }

        ObservableCollection<SalesOrder> SalesOrders { get; set; }

        ProjectApplicationContext Ctx { get; set; }

        public ObservableCollection<Product> ProductsOrdered { get; set; }

        public Product SelectedProduct { get; set; }



        //Add salesorder constructor
        public SalesOrder_AddEdit(ProjectApplicationContext ctx, ObservableCollection<SalesOrder> salesOrders)
        {
            InitializeComponent();
            lblOrderId.Visibility = Visibility.Hidden;
            tbOrderId.Visibility = Visibility.Hidden;
            ProductsOrdered = new ObservableCollection<Product>();
            lvOrderedProducts.ItemsSource = ProductsOrdered;


            //select customer
            var customers = Ctx.Customers.ToList();
            cbCustomer.ItemsSource = customers;

            //Employee
            tbEmployee.Text = MainMenu.User.Employee.ToString();

            //Product Type
            var categories = Enum.GetValues(typeof(ProductCategory));
            cbProductType.ItemsSource = categories;

        }


        //generates product list based on product type selected
         private void cbProductType_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {
            
            if (cbProductType.SelectedItem != null)
            {
                //List of products of that category with 1 or more left in stock
                List<Product> productChoice = new List<Product>();
                ProductCategory category = (ProductCategory)cbProductType.SelectedItem;
                
                var groupedProductsInStock = Ctx.Products
                    .Where(p => p.ProductCategory == category)
                    .GroupBy(p => p.BarCode)
                    .ToList();

                foreach (var group in groupedProductsInStock)
                {
                    productChoice.Add(group.First());
                }
                cbProducts.ItemsSource = productChoice;
            }
            else
            {
                MessageBox.Show("You must select a product type.");
            }

         }

        private void cbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product selectedProduct = cbProducts.SelectedItem as Product;
            if(selectedProduct != null)
            {
                lblLeft.Content = ReturnQuantityInStock(selectedProduct).ToString();
            }
            
        }


        //Edit salesorder constructor
        public SalesOrder_AddEdit(ProjectApplicationContext ctx, ObservableCollection<SalesOrder> salesOrders, SalesOrder selectedSalesOrder)
        {
            Ctx = ctx;
            SelectedSalesOrder = selectedSalesOrder;
            SalesOrders = salesOrders;
            InitializeComponent();
            ProductsOrdered = new ObservableCollection<Product>();
            lvOrderedProducts.ItemsSource = ProductsOrdered;

            
            var customers = Ctx.Customers.ToList();
            cbCustomer.ItemsSource = customers;

           //TUESDAY  - fill the form with selected salesorder properties
           //TUESDAY  - event handler deleting items from the order list  - for sales and purchase orders
        }



        // adds selected products to observable collection which is shown in list view (order overview)
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SelectedProduct = cbProducts.SelectedItem as Product;
            if (SelectedProduct != null)
            {
                int numberLeftinStock = ReturnQuantityInStock(SelectedProduct);
                
                int quantity = Convert.ToInt32(UpDownQuantity.Value);

                //remove products from stock and add to order overview
                if (quantity <= numberLeftinStock)
                {

                    var orderedProducts = Ctx.Products.Where(p => p.BarCode == SelectedProduct.BarCode).Take(quantity);
                    foreach (var product in orderedProducts)
                    {
                        Ctx.Products.RemoveRange(orderedProducts);
                        Ctx.SaveChanges();
                    }

                    foreach (var product in orderedProducts)
                    {
                        ProductsOrdered.Add(product);
                    }

                    //update quantity left
                    lblLeft.Content = ReturnQuantityInStock(SelectedProduct);
                }
                else
                {
                    MessageBox.Show("Products cannot be added. Not enough of this item in stock.");
                }
                
                
                tbPrice.Text = Convert.ToString(CalculateTotal());
                 
            }
            else
            {
                MessageBox.Show("No product is selected. Select a product.");
            }
        }






        //saves details of new sales order to database
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //TUESDAY -different code depending on edit or new sales order (see similar code on other pages)
        }




















        //Methods 
        private int ReturnQuantityInStock(Product selectedProduct)
        {
            int quantity = Ctx.Products
                .Where(p => p.BarCode == selectedProduct.BarCode)
                .ToList()
                .Count();

            return quantity;
        }


        //move to utility class
        private double CalculateTotal()
        {
            double totalPrice = 0;
            foreach (var item in ProductsOrdered)
            {
                totalPrice += item.Price;
            }
            return totalPrice;
        }





    }
}
