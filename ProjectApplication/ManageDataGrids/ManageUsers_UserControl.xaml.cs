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
using ProjectApplication.Classes;


namespace ProjectApplication
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ManageUsers_UserControl : UserControl
    {
        public ProjectApplicationContext Ctx { get; set; }

        public ObservableCollection<UserAccount> UserAccounts { get; set; }

       

        public ManageUsers_UserControl(ProjectApplicationContext ctx)
        {
            InitializeComponent();
            Ctx = ctx;
            //List<UserAccount> userAccounts = ctx.UserAccounts.ToList();
            //ObservableCollection<UserAccount> observableUserAccounts = new ObservableCollection<UserAccount>(userAccounts); //makes an observable collection from the list
            //UsersDataGrid.ItemsSource = userAccounts;
         
            Ctx.UserAccounts.AsQueryable().Load(); // Executes the query and will load the objects into the DbContext so that they are tracked by the entity framework
            // load is a method of a queryable
            UserAccounts = Ctx.UserAccounts.Local;
            UsersDataGrid.ItemsSource = UserAccounts;
            
        }




        //Users edit, delete, add event handlers

        private void Users_EditClickedEventHandler(object sender, RoutedEventArgs e)
        {

            UserAccount userAccount = UsersDataGrid.SelectedItem as UserAccount;
            User_AddEdit editUserForm = new User_AddEdit(UserAccounts, Ctx, userAccount);
            editUserForm.Show();

        }





        private void Users_AddClickedEventHandler(object sender, RoutedEventArgs e)
        {
            User_AddEdit addUserForm = new User_AddEdit(UserAccounts, Ctx);
            addUserForm.Show();
        }




        private void Users_DeleteClickedEventHandler(object sender, RoutedEventArgs e)
        {
            UserAccount user = UsersDataGrid.SelectedItem as UserAccount;
            if (user != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the following user: {user.UserName}? \n Employee '{user.Employee.EmployeeDetails}' will no longer have an user account.", "Confirm", MessageBoxButton.YesNo);


                if (result == MessageBoxResult.Yes)
                {
                    Ctx.UserAccounts.Remove(user);
                    Ctx.SaveChanges();
                    MessageBox.Show($"The user {user.UserName} is succussfully deleted.", "Deleted");
                }
                else
                {
                    MessageBox.Show($"The user has not been deleted.", "Action Cancelled");
                }

            }
            else
            {
                MessageBox.Show("No user has been selected.");
            }

        }



        private void UsersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (UsersDataGrid.SelectedItem == UsersDataGrid.Items[UsersDataGrid.Items.Count-1])
            {
                User_AddEdit userAddEdit = new User_AddEdit(UserAccounts, Ctx);
                userAddEdit.Show();
                UsersDataGrid.SelectedItem = null;
            } 
        }

     
    }
}
