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

            MessageBox.Show("fired!");

            //Ctx.PurchaseOrders.AsQueryable().Load();

            // query purchase orders including the supplier, employee and the products in the purchase order (via the purchaseorderproducts icollection property)
            Ctx.PurchaseOrders
                .Include("Supplier")
                .Include("Employee")
                .Include(po => po.PurchaseOrderProducts.Select(p => p.Product)).Load(); //load - does same as toList() - executes query

            PurchaseOrders = Ctx.PurchaseOrders.Local;
            lvPurchaseOrders.ItemsSource = PurchaseOrders;
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


        //generate pdf invoice - using synfusion library
        private void btnGenerateInvoice_Click(object sender, RoutedEventArgs e)
        {
            SelectedPurchaseOrder = lvPurchaseOrders.SelectedItem as PurchaseOrder;

            if (SelectedPurchaseOrder != null)
            {
                Invoice invoice = new Invoice()
                {
                    PurchaseOrder = SelectedPurchaseOrder,
                    Date = DateTime.Now,
                    Paid = SelectedPurchaseOrder.Paid
                };

                // pdf 
                using (PdfDocument document = new PdfDocument())
                {
                    PdfPage page = document.Pages.Add();

                    PdfGraphics graphics = page.Graphics;

                    PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);

                    graphics.DrawString("Invoice", font, PdfBrushes.Black, new PointF(0, 0));

                    document.Save("Invoice.pdf");

                    System.Diagnostics.Process.Start("Invoice.pdf");
                }

            }
            else
            {
                MessageBox.Show("No purchase order selected. To generate an invoice select an order.");
            }
        }


        //methods for invoice
        private double PriceWithBTW(double totalPrice)
        {
            return totalPrice * 1.21;
        }

        private double BTW(double totalPrice)
        {
            return totalPrice * 0.21;
        }






    }
}
