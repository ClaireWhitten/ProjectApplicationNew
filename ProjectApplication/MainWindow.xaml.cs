using ProjectApplication.Classes;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            ProjectApplicationContext ctx = new ProjectApplicationContext();
           ctx.Employees.Add(new Employee()
            {
                FirstName = "Claire",
                LastName = "Whitten",
                DOB = new DateTime(1993,03,04),
                Street = "Cherry Street",
                Number = 8,
                PostCode = 2000,
                PhoneNumber = 045622345,
                Email = "claire_whitten@gmail.com",
                Salary = 24000,
                StartDate = new DateTime(2020, 05, 14),
                Role = Role.Administrator
            });


            ctx.Employees.Add(new Employee()
            {
                FirstName = "Kristof",
                LastName = "Bruggeman",
                DOB = new DateTime(1987, 04, 11),
                Street = "Cherry Street",
                Number = 8,
                PostCode = 2000,
                PhoneNumber = 045622345,
                Email = "k_bruggeman@gmail.com",
                Salary = 24000,
                StartDate = new DateTime(2020, 05, 14),
                Role = Role.SalesEmployee,
                UserAccount = new UserAccount()
                {
                    UserName = "KBrugg5",
                    Password="123",
                    CreatedOn = DateTime.Now,
                    Role = Role.SalesEmployee
                }
                
            });

            ctx.Employees.Add(new Employee()
            {
                FirstName = "Callum",
                LastName = "Whitten",
                DOB = new DateTime(1999, 04, 11),
                Street = "Cherry Street",
                Number = 8,
                PostCode = 2000,
                PhoneNumber = 045622345,
                Email = "c_whit@gmail.com",
                Salary = 51000,
                StartDate = new DateTime(2020, 05, 14),
                Role = Role.WarehouseEmployee,
            });

            ctx.SaveChanges();
            var employee = ctx.Employees.FirstOrDefault(e => e.Role == Role.Administrator);


            employee.UserAccount = new UserAccount()
            {
                UserName = "Claire",
                Password = "12345",
                CreatedOn = DateTime.Now,
                Role = Role.Administrator
            };



            ctx.SaveChanges();





            InitializeComponent();
            MessageBox.Show("Ended");
        }

       

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            string username = tbUserName.Text;
            string password = tbPassword.Text;
            ProjectApplicationContext ctx = new ProjectApplicationContext();
            UserAccount user = ctx.UserAccounts.FirstOrDefault(u => u.UserName == username);
            if (user != null)
            {
                if (user.Password == password)
                {
                    MainMenu mainMenu = new MainMenu(user);
                    mainMenu.Show();
                }
                else
                {
                    MessageBox.Show("Incorrect password for this username.");
                    tbPassword.Clear();
                }
            }
            else
            {
                MessageBox.Show("Username does not exist.");
                tbUserName.Clear();
                tbPassword.Clear();
            }
        }
    }
}
