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
using System.Windows.Shapes;

namespace ProjectApplication
{
    /// <summary>
    /// Interaction logic for EditDeleteUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        public UserAccount  SelectedUser { get; set; }

        public EditUser(UserAccount selectedUser)
        {
            SelectedUser = selectedUser;
            InitializeComponent();
            tbUserName.Text = selectedUser.UserName;
            tbUserPassword.Text = selectedUser.Password;
            tbUserRole.Text = selectedUser.Role.ToString();
            

        }
    }
}
