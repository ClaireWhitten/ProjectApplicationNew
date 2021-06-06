using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectApplication.Classes;
using ProjectApplication.CreateUpdateWindows;

namespace ProjectApplication
{
    /// <summary>
    /// Interaction logic for ManageProducts_UserControl.xaml
    /// </summary>
    public partial class ManageProducts_UserControl : UserControl
    {

        public  ProjectApplicationContext Ctx { get; set; }

        public ObservableCollection<RegisteredProduct> RegisteredProducts { get; set; } = new ObservableCollection<RegisteredProduct>();

        public RegisteredProduct SelectedProduct { get; set; }

        public ManageProducts_UserControl(ProjectApplicationContext ctx)
        {
            Ctx = ctx;
            Ctx.RegisteredProducts.Include("Supplier").Load();
            RegisteredProducts = Ctx.RegisteredProducts.Local;
            InitializeComponent();
            ProductDataGrid.ItemsSource = RegisteredProducts;
            
        }

        private void Product_AddClickedEventHandler(object sender, RoutedEventArgs e)
        {
            Product_AddEdit addProductForm = new Product_AddEdit(Ctx, RegisteredProducts);
            addProductForm.Show();
        }


        private void Product_EditClickedEventHandler(object sender, RoutedEventArgs e)
        {
            RegisteredProduct product = ProductDataGrid.SelectedItem as RegisteredProduct;
            Product_AddEdit editProductForm = new Product_AddEdit(Ctx, RegisteredProducts, product);
            editProductForm.Show();
        }


        private void Product_DeleteClickedEventHandler(object sender, RoutedEventArgs e)
        {
            RegisteredProduct product = ProductDataGrid.SelectedItem as RegisteredProduct;

            if (product != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the following product: {product.Name}?", "Confirm", MessageBoxButton.YesNo);


                if (result == MessageBoxResult.Yes)
                {
                    RegisteredProducts.Remove(product);
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

        //generate product template
        private void btnGenerateTemplate_Click(object sender, RoutedEventArgs e)
        {
            string[] templateProperties = {"Product Name:","Product Description:","BarCode:","Price:"};

                                 //returns the path of                this special folder https://docs.microsoft.com/en-us/dotnet/api/system.environment.specialfolder?view=net-5.0
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(docPath, "ProductTemplate.txt")))
            {
                foreach (var line in templateProperties)
                {
                    outputFile.WriteLine(line);
                }
            }
        }
    }
}
