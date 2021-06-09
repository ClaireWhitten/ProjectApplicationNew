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
using System.Data.Entity;
using System.ComponentModel;

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
            Ctx = ctx;
            SalesOrders = salesOrders;
            InitializeComponent();
            lblOrderId.Visibility = Visibility.Hidden;
            tbOrderId.Visibility = Visibility.Hidden;
            ProductsOrdered = new ObservableCollection<Product>();
            lvOrderedProducts.ItemsSource = ProductsOrdered;


            //select customer
            List<Customer> customers = Ctx.Customers.ToList();
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
                    .Where(p => p.ProductCategory == category && p.Sold == false)
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
                lblLeft.Content = ReturnQuantityInStock(selectedProduct).ToString() + " left in stock";
            }
            
        }


        //Edit salesorder constructor
        public SalesOrder_AddEdit(ProjectApplicationContext ctx, ObservableCollection<SalesOrder> salesOrders, SalesOrder selectedSalesOrder)
        {
            Ctx = ctx;
            SelectedSalesOrder = selectedSalesOrder;
            SalesOrders = salesOrders;
            InitializeComponent();

            //Set existing properties
            this.DataContext = selectedSalesOrder;
            
            //product overview
            ProductsOrdered = new ObservableCollection<Product>();
            lvOrderedProducts.ItemsSource = ProductsOrdered;

            foreach (var item in SelectedSalesOrder.SalesOrderProducts)
            {
                ProductsOrdered.Add(item.Product);
            }
            tbPrice.Text = Convert.ToString(CalculateTotal());


            //order id
            tbOrderId.Text = SelectedSalesOrder.SalesOrderId.ToString();

            //Customer
            var customers = Ctx.Customers.ToList();
            cbCustomer.ItemsSource = customers;
            cbCustomer.SelectedItem = SelectedSalesOrder.Customer;


            //Employee
            tbEmployee.Text = selectedSalesOrder.Employee.ToString();

            //Product Type
            var categories = Enum.GetValues(typeof(ProductCategory));
            cbProductType.ItemsSource = categories;
      
        }



        // adds selected products to observable collection which is shown in list view (order overview)
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            SelectedProduct = cbProducts.SelectedItem as Product;
            if (SelectedProduct != null)
            {
                int numberLeftinStock = ReturnQuantityInStock(SelectedProduct);
                
                int quantity = Convert.ToInt32(UpDownQuantity.Value);

                //mark products as sold in databse and add to order overview
                if (quantity <= numberLeftinStock)
                {

                    var orderedProducts = Ctx.Products.Where(p => p.BarCode == SelectedProduct.BarCode && p.Sold == false).Take(quantity).ToList();
                    foreach (var product in orderedProducts)
                    {
                        product.Sold = true;
                        Ctx.SaveChanges();
                    }

                    foreach (var product in orderedProducts)
                    {
                        ProductsOrdered.Add(product);
                    }

                    //update quantity left
                    lblLeft.Content = ReturnQuantityInStock(SelectedProduct).ToString() + " left in stock";
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



        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            lvOrderedProducts.SelectedItem = ((Button)sender).DataContext;
            MessageBox.Show(lvOrderedProducts.SelectedIndex.ToString() + lvOrderedProducts.SelectedItem);

            Product selectedProduct = lvOrderedProducts.SelectedItem as Product;
            ProductsOrdered.RemoveAt(lvOrderedProducts.SelectedIndex);
            selectedProduct.Sold = false;
            tbPrice.Text = Convert.ToString(CalculateTotal());
            Ctx.SaveChanges();
        }




        //saves details of new sales order to database
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.Closing -= new System.ComponentModel.CancelEventHandler(SalesOrder_Closing);

            //add new salesorder to database
            if (SelectedSalesOrder == null)
            {
                if (cbCustomer.SelectedItem is Customer && dpOrderDate.SelectedDate != null && ProductsOrdered.Count != 0)
                {
                    SalesOrder newSalesOrder = new SalesOrder()
                    {
                        OrderDate = (DateTime)dpOrderDate.SelectedDate,
                        TotalPrice = Convert.ToDouble(tbPrice.Text),
                        Customer = cbCustomer.SelectedItem as Customer,
                        Employee = MainMenu.User.Employee,
                        Paid = (bool)chkPaid.IsChecked,
                        OrderStatus = OrderStatus.Confirmed,
                        Problem = false,
                        Active = true,

                    };

                    SalesOrders.Add(newSalesOrder);
                    Ctx.SaveChanges();

                    //method 1
                    foreach (var product in ProductsOrdered)
                    {
                        SalesOrderProduct salesOrderProduct = new SalesOrderProduct()
                        {
                            SalesOrderId = newSalesOrder.SalesOrderId,
                            ProductId = product.ProductId
                        };
                        Ctx.SalesOrderProducts.Add(salesOrderProduct);
                    }
                    Ctx.SaveChanges();

                    

                    /*method 2
                    foreach (var item in ProductsOrdered)
                    {
                        newSalesOrder.SalesOrderProducts.Add(new SalesOrderProduct()
                        {
                            SalesOrder = newSalesOrder,
                            Product = item
                        }); 
                    }*/


                    this.Close();
                    this.Closing += new System.ComponentModel.CancelEventHandler(SalesOrder_Closing);

                }
                else
                {
                    MessageBox.Show("Not all fields are completed.");
                }

            }
            else //update selectedSales order in database
            {
                SelectedSalesOrder.TotalPrice = Convert.ToDouble(tbPrice.Text);
                SelectedSalesOrder.Paid = (bool)chkPaid.IsChecked;

                //Update the products in the order
                
                var salesOrderProducts = Ctx.SalesOrderProducts
                .Where(so => so.SalesOrderId == SelectedSalesOrder.SalesOrderId).ToList();

                Ctx.SalesOrderProducts.RemoveRange(salesOrderProducts);
                Ctx.SaveChanges();

                foreach (var product in ProductsOrdered)
                {
                    SalesOrderProduct salesOrderProduct = new SalesOrderProduct()
                    {
                        SalesOrderId = SelectedSalesOrder.SalesOrderId,
                        ProductId = product.ProductId
                    };
                    Ctx.SalesOrderProducts.Add(salesOrderProduct);
                }
                Ctx.SaveChanges();

                CollectionViewSource.GetDefaultView(SalesOrders).Refresh();
                Ctx.SaveChanges();
        
            }


            this.Close();
            this.Closing += new System.ComponentModel.CancelEventHandler(SalesOrder_Closing);
        }

        private void SalesOrder_Closing(object sender, CancelEventArgs e)
        {
            Button btnClicked = sender as Button;
            if (btnClicked == null)
            {

                if (ProductsOrdered.Count != 0)
                {
                    MessageBoxResult result = MessageBox.Show($"The current order has not been saved. Are you sure you want to close this window?", "Confirm", MessageBoxButton.YesNo);


                    if (result == MessageBoxResult.Yes)
                    {
                        foreach (var product in ProductsOrdered)
                        {

                            product.Sold = false;
                            Ctx.SaveChanges();
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                    }

                }

            }
           
        }



















        //Methods 
        private int ReturnQuantityInStock(Product selectedProduct)
        {
            int quantity = Ctx.Products
                .Where(p => p.BarCode == selectedProduct.BarCode && p.Sold == false)
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
