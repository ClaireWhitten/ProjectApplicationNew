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
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Drawing;
using Microsoft.Win32;
using System.Diagnostics;
using Syncfusion.Pdf.Grid;
using System.Data;

namespace ProjectApplication.OrdersListViews
{
    /// <summary>
    /// Interaction logic for PurchaseOrders_UserControl.xaml
    /// </summary>
    public partial class PurchaseOrders_UserControl : UserControl
    {

        public ProjectApplicationContext Ctx { get; set; }

        ObservableCollection<PurchaseOrder> PurchaseOrders { get; set; }

        PurchaseOrder SelectedPurchaseOrder { get; set; }


        public PurchaseOrders_UserControl(ProjectApplicationContext ctx)
        {
            Ctx = ctx;
            InitializeComponent();
            getPurchaseOrderData();
           
        }


        //checkbox and text box inputs changed event handler
        private void inputChanged(object sender, RoutedEventArgs e)
        {
            //save changes to database
            Ctx.SaveChanges();
        }

      

        //new purchase order event handler
        private void btnNewPurchaseOrder_Click(object sender, RoutedEventArgs e)
        {
            PurchaseOrder_AddEdit purchaseOrderForm = new PurchaseOrder_AddEdit(Ctx);
            purchaseOrderForm.Show();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            
            string searchBy = tbSearch.Text;

            var searchResults  = Ctx.PurchaseOrders
                .Where(po=> po.PurchaseOrderId.ToString() == searchBy || po.Supplier.Name == searchBy)
                .Include("Supplier")
                .Include("Employee")
                .Include(po => po.PurchaseOrderProducts.Select(p => p.Product)).ToList();


            ObservableCollection<PurchaseOrder> myCollection = new ObservableCollection<PurchaseOrder>(searchResults);
            PurchaseOrders = myCollection;
            lvPurchaseOrders.ItemsSource = PurchaseOrders;
            
        }


        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text.Trim().Length == 0)
            {
                getPurchaseOrderData();
            }
        }

        private void getPurchaseOrderData()
        {
            // query purchase orders including the supplier, employee and the products in the purchase order (via the purchaseorderproducts icollection property)
            Ctx.PurchaseOrders
               .Include("Supplier")
               .Include("Employee")
               .Include(po => po.PurchaseOrderProducts.Select(p => p.Product)).Load(); //load - does same as toList() - executes query
                                                                                       //Ctx.PurchaseOrders.AsQueryable().Load();
           

            PurchaseOrders = Ctx.PurchaseOrders.Local;
            lvPurchaseOrders.ItemsSource = PurchaseOrders;

        }
       
    }
}
