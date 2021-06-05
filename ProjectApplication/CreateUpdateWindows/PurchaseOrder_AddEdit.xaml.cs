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
        public ProjectApplicationContext Ctx { get; set; }

        public Supplier SelectedSupplier { get; set; }

        public Product SelectedProduct { get; set; }

        public  Employee SelectedEmployee { get; set; }

        public ObservableCollection<Product> ProductsOrdered { get; set; }

        public PurchaseOrder SelectedPurchaseOrder { get; set; }

        public ObservableCollection<PurchaseOrder> PurchaseOrders { get; set; }

       



        //constructor new purchase order
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

        //constructor edit purchase order


        public PurchaseOrder_AddEdit(ProjectApplicationContext ctx, PurchaseOrder selectedPurchaseOrder, ObservableCollection<PurchaseOrder> purchaseOrders)
        {
            Ctx = ctx;
            SelectedPurchaseOrder = selectedPurchaseOrder;
            PurchaseOrders = purchaseOrders;

            
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
                for (int i = 0; i <quantity; i++)
                {
                    ProductsOrdered.Add(new Product()
                    {
                        BarCode = SelectedProduct.BarCode,
                        Name = SelectedProduct.Name,
                        Description = SelectedProduct.Description,
                        Price = SelectedProduct.Price,
                        Supplier = SelectedSupplier
                    });
                }
                tbPrice.Text = Convert.ToString(CalculateTotal());
            }
            else
            {
                MessageBox.Show("You must select a product");
            }
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SelectedEmployee = cbEmployee.SelectedItem as Employee;

            

            if (SelectedEmployee != null)
            {
                // create the purchase order
                PurchaseOrder newPurchaseOrder = new PurchaseOrder()
                {
                    OrderDate = (DateTime)dpOrderDate.SelectedDate,
                    TotalPrice = Convert.ToDouble(tbPrice.Text),
                    Supplier = SelectedSupplier,
                    Employee = SelectedEmployee,
                    Paid = (bool)chkPaid.IsChecked,
                    Arrived = false,
                    Problem = false,
                    Archived = false
                };
                Ctx.PurchaseOrders.Add(newPurchaseOrder);
               
                //loop over each product in the products Ordered list and add them to the database
                foreach (var item in ProductsOrdered)
                {
                    //Method 1 (in comment) adding products to products table, 

                    //Ctx.Products.Add(item);

                    //Method 2  - adding all via the purchaseOrderProducts icollection property of the purchase order (the icollection needs to be initiated first. Have done so in the constructor.)
                    newPurchaseOrder.PurchaseOrderProducts.Add(new PurchaseOrderProduct()
                    {
                        PurchaseOrder = newPurchaseOrder,
                        Product = item
                    });


                    /*PurchaseOrderProduct purchaseOrderProduct = new PurchaseOrderProduct()
                    {
                        PurchaseOrderId = newPurchaseOrder.PurchaseOrderId,
                        ProductId = item.ProductId
                    };
                    
                    Ctx.PurchaseOrderProducts.Add(purchaseOrderProduct);*/
                    Ctx.SaveChanges();
                }
                MessageBox.Show("The purchase order has been saved in the system.");
                this.Close();


            }
            else
            {
                MessageBox.Show("You must select an employee before saving the order.");
            }
            
        }




        //methods
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
                
       
