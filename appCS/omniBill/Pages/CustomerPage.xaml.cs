using omniBill.InnerComponents.DataAccessLayer;
using omniBill.InnerComponents.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace omniBill.pages
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        MainWindow mainWindow;
        IHandler<Customer> customerHandler;

        public CustomerPage(MainWindow mainWindow)
        {
            InitializeComponent();
            customerHandler = new CustomerHandler();
            this.mainWindow = mainWindow;
        }

        private void customerListGrid_Loaded(object sender, RoutedEventArgs e)
        {
            customerListGrid.ItemsSource = customerHandler.ItemList();

            //TODO Data Annotations for Customer object
        }

        private void NewCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO Create
        }

    }
}
