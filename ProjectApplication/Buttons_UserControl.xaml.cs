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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectApplication
{
    /// <summary>
    /// Interaction logic for Buttons_UserControl.xaml
    /// </summary>
    public partial class Buttons_UserControl : UserControl
    {
        public DataGrid DataGrid { get; set; }
       
        public Buttons_UserControl(object itemsource, DataGrid dataGrid)
        {
            
            InitializeComponent();
            this.DataContext = itemsource;
            this.DataGrid = dataGrid;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Type type = DataGrid.SelectedItem.GetType();
            UtilityClass<T>.Delete(DataGrid.SelectedItem);
            
            MessageBox.Show("Delete");

            MessageBox.Show(this.DataContext.ToString());
        
            
            
        }
    }
}
