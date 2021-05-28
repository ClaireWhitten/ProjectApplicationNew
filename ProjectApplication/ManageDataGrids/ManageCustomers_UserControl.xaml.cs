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
    /// Interaction logic for ManageCustomers_UserControl.xaml
    /// </summary>
    public partial class ManageCustomers_UserControl : UserControl
    {

        public ProjectApplicationContext Ctx { get; set; }

        public ObservableCollection<Customer> Customers { get; set; }

        public ManageCustomers_UserControl(ProjectApplicationContext ctx)
        {
            
            Ctx = ctx;

            Ctx.Customers.Load();

            Customers = Ctx.Customers.Local;

            InitializeComponent();


            CustomerDataGrid.ItemsSource = Customers;


        }

        private void CustomerDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            MessageBox.Show("You must save the changes to the selected customer before selecting another.");
            
        }
    }
}
