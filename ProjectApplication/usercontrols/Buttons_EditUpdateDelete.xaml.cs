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

namespace ProjectApplication.usercontrols
{
    /// <summary>
    /// Interaction logic for Buttons_EditUpdateDelete.xaml
    /// </summary>
    public partial class Buttons_EditUpdateDelete : UserControl
    {
        //add a new routed event property for each button 
        public static readonly RoutedEvent EditClickedEvent = // Name of routed event
        EventManager.RegisterRoutedEvent("EditClickedEvent", RoutingStrategy.Bubble,
        typeof(RoutedEventHandler), typeof(Buttons_EditUpdateDelete)); //name of usercontrol/class

        public static readonly RoutedEvent AddClickedEvent = // Name of routed event
        EventManager.RegisterRoutedEvent("AddClickedEvent", RoutingStrategy.Bubble,
        typeof(RoutedEventHandler), typeof(Buttons_EditUpdateDelete)); //name of usercontrol/class


        public static readonly RoutedEvent DeleteClickedEvent = // Name of routed event
        EventManager.RegisterRoutedEvent("DeleteClickedEvent", RoutingStrategy.Bubble,
        typeof(RoutedEventHandler), typeof(Buttons_EditUpdateDelete)); //name of usercontrol/class


        // Add event handlers for each routed event
        public event RoutedEventHandler EditClicked
        {// In the XAML of the main window, you can attach a handler to your custom event 'EditClickedEvent' by using this 'EditClicked' method
            // e.g.: EditClicked="Customers_EditClickedEventHandler" 
            add { AddHandler(EditClickedEvent, value); }
            remove { RemoveHandler(EditClickedEvent, value); }
        }

        public event RoutedEventHandler AddClicked
        {
            add { AddHandler(AddClickedEvent, value); }
            remove { RemoveHandler(AddClickedEvent, value); }
        }

        public event RoutedEventHandler DeleteClicked
        {
            add { AddHandler(DeleteClickedEvent, value); }
            remove { RemoveHandler(DeleteClickedEvent, value); }
        }

        //constructor
        public Buttons_EditUpdateDelete()
        {
            InitializeComponent();

        }


        // Buttons handlers:

        private void btnEdit_Click_1(object sender, RoutedEventArgs e)
        {
            // Raise the custom routed event, this fires the event from the ManageCustomers_usercontrol
            RaiseEvent(new RoutedEventArgs(Buttons_EditUpdateDelete.EditClickedEvent));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(Buttons_EditUpdateDelete.AddClickedEvent));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(Buttons_EditUpdateDelete.DeleteClickedEvent));
        }

       
    }
}
