using omniBill.InnerComponents.DataAccessLayer;
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
using omniBill.InnerComponents.LogicLayer;

namespace omniBill.pages
{
    /// <summary>
    /// Interaction logic for CustomerStuffPage.xaml
    /// </summary>
    public partial class CustomerStuffPage : Page
    {

        public CustomerStuffPage(Customer customer)
        {
            InitializeComponent();
            // TODO Add @CustomerName to omniLang 
            mainCustomerStuffGrid.DataContext = customer;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Utils.customerPage.hideSidePanel();
        }

        public Customer displayToModel() {

            return (Customer) mainCustomerStuffGrid.DataContext;
        }
    }
}
