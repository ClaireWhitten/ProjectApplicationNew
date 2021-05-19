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
    public partial class UserControl2 : UserControl
   
    {
        public ObservableCollection<Employee> Employees { get; set; }

        public ProjectApplicationContext Ctx { get; set; }

        public UserControl2(ProjectApplicationContext context)
        {
            //Ctx = context;
            InitializeComponent();
            Ctx = new ProjectApplicationContext();

            Ctx.Employees.AsQueryable().Load();
            Employees = Ctx.Employees.Local;
            EmployeeDataGrid.ItemsSource = Employees;




           
        }
    }
}
