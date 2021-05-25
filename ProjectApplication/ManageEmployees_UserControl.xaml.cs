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
    /// Interaction logic for UserControl2.xaml
    /// </summary>
    public partial class ManageEmployees_UserControl : UserControl
   
    {
        public ObservableCollection<Employee> Employees { get; set; }

        public ProjectApplicationContext Ctx { get; set; }

        public ManageEmployees_UserControl(ProjectApplicationContext ctx)
        {
            Ctx = ctx;
            InitializeComponent();

            Ctx.Employees.AsQueryable().Load();
            Employees = Ctx.Employees.Local;
            EmployeeDataGrid.ItemsSource = Employees;
            buttonsContentControl.Content = new Buttons_UserControl(this);
        }


        private void btnEditEmployee_Click(object sender, RoutedEventArgs e)
        {
            Ctx.SaveChanges();
            
        }




        private void btnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = EmployeeDataGrid.SelectedItem as Employee;
            if (selectedEmployee != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the following user: {selectedEmployee.FirstName}?", "Confirm", MessageBoxButton.YesNo);


                if (result == MessageBoxResult.Yes)
                {
                    Employees.Remove(selectedEmployee); // removes from observable collection 
                    Ctx.SaveChanges();
                    MessageBox.Show($"The Employee {selectedEmployee.FirstName} is succussfully deleted", "Deleted");
                }
                else
                {
                    MessageBox.Show($"The Employee has not been deleted.", "Action Cancelled");
                }

            }
            else
            {
                MessageBox.Show("No Employee has been selected.");
            }


        }




        private void EmployeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is Employee)
            {
                btnDeleteEmployee.IsEnabled = true;
              
            }
            else if (EmployeeDataGrid.SelectedItem == EmployeeDataGrid.Items[EmployeeDataGrid.Items.Count - 1])
            {
                btnDeleteEmployee.IsEnabled = false;
                EmployeeDataGrid.SelectedItem = null;


            }
            else if (EmployeeDataGrid.SelectedItem == null)
            {
                btnDeleteEmployee.IsEnabled = false;
            }
        }

    }
}
