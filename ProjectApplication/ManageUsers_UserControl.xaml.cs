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
            MessageBox.Show("fired");
            UserAccounts = Ctx.UserAccounts.Local;
            UsersDataGrid.ItemsSource = UserAccounts;
            
        }

      

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            Ctx.SaveChanges();
            /*if (UsersDataGrid.SelectedItem is UserAccount)
            {
                UserAccount selectedUseraccount = UsersDataGrid.SelectedItem as UserAccount;
                MessageBox.Show(selectedUseraccount.UserName);
                EditUser editUser = new EditUser(selectedUseraccount);
                editUser.Show();
            }*/

        }




        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            UserAccount selectedUser = UsersDataGrid.SelectedItem as UserAccount;
            
            if (selectedUser != null)
            {
                MessageBoxResult result  = MessageBox.Show($"Are you sure you want to delete the following user: {selectedUser.UserName}?", "Confirm", MessageBoxButton.YesNo);
                
                
                if (result == MessageBoxResult.Yes)
                {
                    UserAccounts.Remove(selectedUser); // removes from observable collection 
                    Ctx.SaveChanges();
                    MessageBox.Show($"The user {selectedUser.UserName} is succussfully deleted","Deleted");
                } else
                {
                    MessageBox.Show($"The user has not been deleted.", "Action Cancelled");
                }

            } else
            {
                MessageBox.Show("No user has been selected.");
            }
            
            
        }

        private void UsersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is UserAccount)
            {
                btnDelete.IsEnabled = true;
                //MessageBox.Show(UsersDataGrid.SelectedItem.ToString());
            } else if (UsersDataGrid.SelectedItem == UsersDataGrid.Items[UsersDataGrid.Items.Count-1])
            {
                btnDelete.IsEnabled = false;
                AddUser addUser = new AddUser(UserAccounts, Ctx);
                addUser.Show();
                UsersDataGrid.SelectedItem = null;
               
                
            } else if (UsersDataGrid.SelectedItem == null)
            {
                btnDelete.IsEnabled = false;
            }
        }

        private void UsersDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //((DataGrid)sender).BorderBrush = Brushes.Yellow;

            UserAccount selectedUser = (UserAccount)UsersDataGrid.SelectedItem;
            DataGridRow row = UsersDataGrid.ItemContainerGenerator.ContainerFromItem(selectedUser) as DataGridRow;
            row.Background = Brushes.YellowGreen;

        }

        private void UsersDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            MessageBox.Show("You must save the changes you've made to this row or they will be lost.");
        }
    }
}
