using ProjectApplication.Classes;
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


namespace ProjectApplication
{
    /// <summary>
    /// Interaction logic for ManageSuppliers_UserControl.xaml
    /// </summary>
    public partial class ManageSuppliers_UserControl : UserControl
    {

        public ProjectApplicationContext Ctx{ get; set; }

        public ObservableCollection<Supplier> Suppliers { get; set; }

        public ManageSuppliers_UserControl(ProjectApplicationContext ctx)
        {
            Ctx = ctx;
            Ctx.Suppliers.AsQueryable().Load();
            Suppliers = Ctx.Suppliers.Local;
            InitializeComponent();
            DataGridSuppliers.ItemsSource = Suppliers;

        }

        private void DataGridSuppliers_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            MessageBox.Show("You are editing a cell.");// save button appears
        }

        private void DataGridSuppliers_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            MessageBox.Show("You are finished editing this row"); // must save the row before being able to click another row(selection changed)
        }

        private void DataGridSuppliers_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            MessageBox.Show("End of cell edit"); // check valid input  - colour if not 
        }

        private void DataGridSuppliers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("different row selected");
        }





        // create event listeners and functions for all usercontrols - to add,edit and delete
    }
}
