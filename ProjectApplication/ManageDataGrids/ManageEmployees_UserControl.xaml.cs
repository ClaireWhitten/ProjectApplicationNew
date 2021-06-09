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
            GetEmployeeData();
            
            
        }


        //Event handlers for Edit/Add/Delete Employees
        private void Employees_EditClickedEventHandler(object sender, RoutedEventArgs e)
        {

            Employee employee = EmployeeDataGrid.SelectedItem as Employee;
            Employee_AddEdit editEmployeeForm = new Employee_AddEdit(Ctx, employee, Employees);
            editEmployeeForm.Show();

        }





        private void Employees_AddClickedEventHandler(object sender, RoutedEventArgs e)
        {
            Employee_AddEdit addEmployeeForm = new Employee_AddEdit(Ctx, Employees);
            addEmployeeForm.Show();
        }




        private void Employees_DeleteClickedEventHandler(object sender, RoutedEventArgs e)
        {
            Employee employee = EmployeeDataGrid.SelectedItem as Employee; 
            if (employee != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the following employee: {employee.FirstName}? \n The user account for this employee will also be deleted.", "Confirm", MessageBoxButton.YesNo);


                if (result == MessageBoxResult.Yes)
                {
                    Ctx.Employees.Remove(employee);
                    //Employees.Remove(employee); // removes from observable collection 
                    Ctx.SaveChanges();
                    MessageBox.Show($"The employee {employee.FirstName} is succussfully deleted. The user account for this user no longer exists.", "Deleted");
                }
                else
                {
                    MessageBox.Show($"The employee has not been deleted.", "Action Cancelled");
                }

            }
            else
            {
                MessageBox.Show("No employee has been selected.");
            }

        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text.Trim().Length == 0)
            {
                GetEmployeeData();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchBy = tbSearch.Text;

            var searchResults = Ctx.Employees
                .Where(c => c.FirstName == searchBy || c.LastName == searchBy || c.EmployeeId.ToString() == searchBy)
                .ToList();


            ObservableCollection<Employee> myCollection = new ObservableCollection<Employee>(searchResults);
            Employees = myCollection;
            EmployeeDataGrid.ItemsSource = Employees;
        }

        private void GetEmployeeData()
        {
            Ctx.Employees.AsQueryable().Load();
            Employees = Ctx.Employees.Local;
            EmployeeDataGrid.ItemsSource = Employees;
        }
    }
}
