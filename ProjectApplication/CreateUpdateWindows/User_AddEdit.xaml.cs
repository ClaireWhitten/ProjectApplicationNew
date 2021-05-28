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

namespace ProjectApplication
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class User_AddEdit : Window
    {
        public List<Employee> Employees { get; set; } = new List<Employee>();

        public ProjectApplicationContext Ctx { get; set; }

        public ObservableCollection<UserAccount> UserAccounts { get; set; }

        public User_AddEdit(ObservableCollection<UserAccount> userAccounts, ProjectApplicationContext context)
        {
            UserAccounts = userAccounts;

            Ctx = context;
            var employeesWithoutUserAccount = Ctx.Employees
               .Include("UserAccount")
               .Where(e => e.UserAccount == null)
               .ToList();

            InitializeComponent();
            cbEmployees.ItemsSource = employeesWithoutUserAccount;
           
            

        }

        private void btnAssignEmployee_Click(object sender, RoutedEventArgs e)
        {
            if(cbEmployees.SelectedItem is Employee)
            {
                Employee selectedEmployee = cbEmployees.SelectedItem as Employee;
                UserAccounts.Add(new UserAccount()
                {
                    UserName = tbUsername.Text,
                    Password = tbPassword.Text,
                    Role = (Role)cbRole.SelectedItem, 
                    CreatedOn = DateTime.Now,
                    Employee = selectedEmployee
                }); 
                Ctx.SaveChanges();
                this.Close();
                
            }
            else
            {
                MessageBox.Show("You must select or create an employee before you can create a new account.");
            }
        }
    }
}
