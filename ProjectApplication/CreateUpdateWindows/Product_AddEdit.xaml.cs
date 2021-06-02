using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using ProjectApplication.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for Product_AddEdit.xaml
    /// </summary>
    public partial class Product_AddEdit : Window
    {
        public ProjectApplicationContext Ctx { get; set; }

        public ObservableCollection<Product> Products { get; set; }

        public Product SelectedProduct { get; set; }

        //add constructor
        public Product_AddEdit(ProjectApplicationContext ctx, ObservableCollection<Product> products)
        {
            Ctx = ctx;
            Products = products;
            InitializeComponent();
            this.Title = "Add Product";
            cbSupplier.ItemsSource = Ctx.Suppliers.ToList();
            tbQuantity.IsEnabled = false;
            tbProductId.IsEnabled = false;
        }

        //edit constructor
        public Product_AddEdit(ProjectApplicationContext ctx, ObservableCollection<Product> products, Product selectedProduct)
        {
            Ctx = ctx;
            Products = products;
            SelectedProduct = selectedProduct;
            InitializeComponent();
            this.Title = "Edit Product";

            var productAndSupplier = Ctx.Products.Include("Supplier").Where(p => p.ProductId == selectedProduct.ProductId).ToList();
            this.DataContext = productAndSupplier;

            var suppliers = Ctx.Suppliers.ToList();
            cbSupplier.DataContext = suppliers;
            cbSupplier.ItemsSource = suppliers;
            Supplier selectedSupplier = null;
            foreach (var item in suppliers)
            {
                if (item.SupplierId == productAndSupplier[0].Supplier.SupplierId)
                {
                    selectedSupplier = item;
                }
                    
            }
            cbSupplier.SelectedItem = selectedSupplier;
            

            int numberOfProducts = Ctx.Products.Where(p => p.BarCode == selectedProduct.BarCode).ToList().Count();
            tbQuantity.Text = numberOfProducts.ToString();


            tbQuantity.IsEnabled = false;
            tbProductId.IsEnabled = false;
            cbSupplier.IsEnabled = false;

        }

        private void btnSave_Click_1(object sender, RoutedEventArgs e)
        {
            Supplier supplier = cbSupplier.SelectedItem as Supplier;
            if (SelectedProduct == null)
            {

                Product newProduct = new Product()
                {
                    Name = tbName.Text,
                    Description = tbDescription.Text,
                    BarCode = tbBarcode.Text,
                    Price = Convert.ToDouble(tbPrice.Text),
                    Supplier = supplier
                };

                Ctx.Products.Add(newProduct);
                Ctx.SaveChanges();
                this.Close();
            }
            else
            {

                SelectedProduct.Name = tbName.Text;
                SelectedProduct.Description = tbDescription.Text;
                SelectedProduct.BarCode = tbBarcode.Text;
                SelectedProduct.ProductId = Convert.ToInt32(tbProductId.Text);
                SelectedProduct.Price = Convert.ToInt32(tbPrice.Text);
                SelectedProduct.Supplier = supplier;


                CollectionViewSource.GetDefaultView(Products).Refresh();
                Ctx.SaveChanges();
                this.Close();
            }
        }

        private void btnuploadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                MessageBox.Show(File.ReadAllText(openFileDialog.FileName));
            }

            string fileContent = File.ReadAllText(openFileDialog.FileName);

            //???
            JObject data = JObject.Parse(fileContent);
            MessageBox.Show(data.ToString());


        }
    }
}
