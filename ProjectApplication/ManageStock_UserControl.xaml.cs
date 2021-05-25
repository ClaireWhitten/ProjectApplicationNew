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

        public ManageStock_UserControl(ProjectApplicationContext ctx)
        {
            Ctx = ctx;

            Ctx.Products.Include("Supplier").Load();
            Products = Ctx.Products.Local;


            InitializeComponent();
            StockDataGrid.ItemsSource = Products;
            buttonsContentControl.Content = new Buttons_UserControl(Products, StockDataGrid, Ctx);

        }
    }
}
