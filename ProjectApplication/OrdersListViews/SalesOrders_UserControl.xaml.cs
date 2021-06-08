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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectApplication.Classes;
using ProjectApplication.CreateUpdateWindows;
using System.Data.Entity;
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
    /// Interaction logic for SalesOrders_UserControl.xaml
    /// </summary>
    public partial class SalesOrders_UserControl : UserControl
    {

        public ProjectApplicationContext Ctx { get; set; }

        ObservableCollection<SalesOrder> SalesOrders { get; set; }

        SalesOrder SelectedSalesOrder { get; set; }




        public SalesOrders_UserControl(ProjectApplicationContext ctx)
        {
            Ctx = ctx;
            InitializeComponent();
            Ctx.SalesOrders
                .Include("Customer")
                .Include("Employee")
                .Include(so => so.SalesOrderProducts.Select(p => p.Product)).Load();

            SalesOrders = Ctx.SalesOrders.Local;
            lvSalesOrders.ItemsSource = SalesOrders;
        }

        //add sale
        private void btnNewSalesOrder_Click(object sender, RoutedEventArgs e)
        {
            SalesOrder_AddEdit addSaleForm = new SalesOrder_AddEdit(Ctx, SalesOrders);
            addSaleForm.Show();
        }


        //edit sale
        private void btnEditSalesOrder_Click(object sender, RoutedEventArgs e)
        {
           
            if(SalesOrderSelected())
            {
                SalesOrder_AddEdit editSaleForm = new SalesOrder_AddEdit(Ctx, SalesOrders, SelectedSalesOrder);
                editSaleForm.Show();
            }
            else
            {
                MessageNotSelected();
            }
           
        }


        //cancel sale
        private void btnCancelSalesOrder_Click(object sender, RoutedEventArgs e)
        {
            if (SalesOrderSelected())
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the following order: {SelectedSalesOrder.SalesOrderId}?", "Confirm", MessageBoxButton.YesNo);


                if (result == MessageBoxResult.Yes)
                {

                    var salesOrderProducts = Ctx.SalesOrderProducts
                    .Where(so => so.SalesOrderId == SelectedSalesOrder.SalesOrderId).ToList();


                    foreach (var item in salesOrderProducts)
                    {
                        item.Product.Sold = false;
                    }

                    Ctx.SalesOrderProducts.RemoveRange(salesOrderProducts);
                    Ctx.SalesOrders.Remove(SelectedSalesOrder);

                    Ctx.SaveChanges();
                    MessageBox.Show($"Sales order {SelectedSalesOrder.SalesOrderId} is succussfully cancelled.", "Deleted");
                }
                else
                {
                    MessageBox.Show($"The order has not been deleted.", "Action Cancelled");
                }

            }
            else
            {
                MessageNotSelected();
            }
        }


        private void inputChanged(object sender, RoutedEventArgs e)
        {
            //save changes to database
            Ctx.SaveChanges();
        }







        //invoice
        private void btnGenerateInvoice_Click(object sender, RoutedEventArgs e)
        {
            SelectedSalesOrder = lvSalesOrders.SelectedItem as SalesOrder;

            if (SelectedSalesOrder != null)
            {
                Invoice invoice = new Invoice()
                {
                    SalesOrder = SelectedSalesOrder,
                    Date = DateTime.Now,
                    Paid = SelectedSalesOrder.Paid
                };


                //create pdf invoice
                using (PdfDocument document = new PdfDocument())
                {
                    PdfPage page = document.Pages.Add();

                    PdfGraphics graphics = page.Graphics;

                    PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);

                    PdfImage image = PdfImage.FromFile("../../Images/coolblue_banner.jpg");

                    graphics.DrawImage(image, 0, 0, 300, 100);

                    graphics.DrawString("Order Invoice", font, PdfBrushes.Black, new PointF(0, 100));

                    graphics.DrawString("Reference Number: " + SelectedSalesOrder.SalesOrderId.ToString(), font, PdfBrushes.Black, new PointF(0, 120));

                    graphics.DrawString("Date: " + SelectedSalesOrder.OrderDate.ToString(), font, PdfBrushes.Black, new PointF(0, 140));

                    graphics.DrawString("Total: £" + SelectedSalesOrder.TotalPrice.ToString(), font, PdfBrushes.Black, new PointF(0, 160));
                    graphics.DrawString("BTW: £" + UtilityClass.BTW(SelectedSalesOrder.TotalPrice).ToString(), font, PdfBrushes.Black, new PointF(100, 160));
                    graphics.DrawString("Total (inc BTW): £" + UtilityClass.PriceWithBTW(SelectedSalesOrder.TotalPrice).ToString(), font, PdfBrushes.Black, new PointF(200, 160));

                    PdfGrid pdfGrid = new PdfGrid();

                    DataTable datatable = new DataTable();

                    datatable.Columns.Add("Product");
                    datatable.Columns.Add("Description");
                    datatable.Columns.Add("Quantity");
                    datatable.Columns.Add("Price");


                    //grouped products for invoice
                    var groupedProducts = SelectedSalesOrder.SalesOrderProducts.GroupBy(product => product.Product.Name);

                    //create 1 row for each group of products
                    foreach (var group in groupedProducts)
                    {
                        datatable.Rows.Add(new Object[] { group.Key, group.First().Product.Description, group.Count().ToString(), (group.First().Product.Price * group.Count()).ToString() });
                    }

                    pdfGrid.DataSource = datatable;
                    pdfGrid.Draw(page, new PointF(0, 200));



                    //allow user to select where to save document-  savefiledialog
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Files(*.pdf)|*.pdf";
                    saveFileDialog.AddExtension = true;
                    saveFileDialog.DefaultExt = ".pdf";

                    if (saveFileDialog.ShowDialog() == true && saveFileDialog.CheckPathExists)
                    {
                        document.Save(saveFileDialog.FileName);
                        document.Close();

                        MessageBoxResult result = MessageBox.Show("Do you want to view the invoice?", "Invoice Created", MessageBoxButton.YesNo, MessageBoxImage.Information);
                        if (result == MessageBoxResult.Yes)
                        {
                            Process process = new Process();
                            process.StartInfo.FileName = saveFileDialog.FileName;
                            process.Start();
                        }
                    }


                }

            }
            else
            {
                MessageNotSelected();
            }

        }




  





        //Methods
        private bool SalesOrderSelected()
        {
            SelectedSalesOrder = lvSalesOrders.SelectedItem as SalesOrder;
            if (SelectedSalesOrder != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void MessageNotSelected()
        {
            MessageBox.Show("No sales order has been selected. Select an order.");
        }
        
    }
}
