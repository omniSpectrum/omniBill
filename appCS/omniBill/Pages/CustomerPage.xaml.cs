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
using omniBill.InnerComponents.Localization;

namespace omniBill.pages
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        IHandler<Customer> customerHandler;

        public CustomerPage()
        {
            InitializeComponent();
            customerHandler = new CustomerHandler();
            Utils.customerPage = this;
        }

        private void customerListGrid_Loaded(object sender, RoutedEventArgs e)
        {
            customerListGrid.ItemsSource = customerHandler.ItemList();

            /*
             * public int customerId { get; set; }
                public string customerName { get; set; }
                public string street { get; set; }
                public string postCode { get; set; }
                public string city { get; set; }
                public string phoneNumber { get; set; }
                public string email { get; set; }
             *  ICollection<DraftInvoice> DraftInvoices { get; set; }
             */

            customerListGrid.Columns[0].Visibility = Visibility.Hidden;
            customerListGrid.Columns[1].Header = "Customer Name"; // TODO add Customer Name to omniLang
            customerListGrid.Columns[2].Header = omniLang.Street;
            customerListGrid.Columns[3].Header = omniLang.PostCode;
            customerListGrid.Columns[4].Header = omniLang.City;
            customerListGrid.Columns[5].Header = omniLang.PhoneNumber;
            customerListGrid.Columns[6].Header = omniLang.Email;
            customerListGrid.Columns[7].Visibility = Visibility.Hidden;
        }

        private void NewCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO Create
        }

        private void customerListGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Customer currentCustomer = (Customer)customerListGrid.SelectedItem;
            showSidePanel(currentCustomer);
        }

        private void showSidePanel(Customer customer)
        {
            Grid.SetColumnSpan(listView, 1);
            sidePanelFrame.Visibility = System.Windows.Visibility.Visible;
            sidePanelFrame.Navigate(new CustomerStuffPage(customer));
        }

    }
}
