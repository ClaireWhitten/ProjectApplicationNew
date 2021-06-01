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
    /// Interaction logic for Supplier_AddEdit.xaml
    /// </summary>
    public partial class Supplier_AddEdit : Window
    {

        ProjectApplicationContext Ctx { get; set; }

        ObservableCollection<Supplier> Suppliers { get; set; }

        Supplier SelectedSupplier { get; set; }

        //add contructor
        public Supplier_AddEdit(ProjectApplicationContext ctx, ObservableCollection<Supplier> suppliers)
        {
            InitializeComponent();
        }


        //edit constructor
        public Supplier_AddEdit(ProjectApplicationContext ctx, ObservableCollection<Supplier> suppliers, Supplier selectedSupplier)
        {
            Ctx = ctx;
            SelectedSupplier = selectedSupplier;
            Suppliers = suppliers;
            InitializeComponent();
            this.DataContext = Suppliers;
            
        }
    }
}
