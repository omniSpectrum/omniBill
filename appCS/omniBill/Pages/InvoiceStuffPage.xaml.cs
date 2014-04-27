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
using omniBill.InnerComponents.DataAccessLayer;
using omniBill.InnerComponents.LogicLayer;

namespace omniBill.pages
{
    /// <summary>
    /// Interaction logic for InvoiceStuffPage.xaml
    /// </summary>
    public partial class InvoiceStuffPage : Page
    {
        IHandler<Customer> customerHandler;

        public InvoiceStuffPage(DraftInvoice invoice)
        {
            InitializeComponent();

            customerHandler = new CustomerHandler();
            invoiceHeaderGrid.DataContext = invoice;
            invoiceLinesGrid.DataContext = invoice.InvoiceLines;
            cbBind(invoice);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Utils.invoicePage.hideSidePanel();
        }
        public DraftInvoice displayToModel()
        {
            var inv = (DraftInvoice)invoiceHeaderGrid.DataContext;
            inv.customerid = ((Customer)cbCustomer.SelectedItem).customerId;
            return inv;
        }
        private void cbBind(DraftInvoice invoice)
        {
            cbCustomer.ItemsSource = customerHandler.ItemList();
            cbCustomer.DisplayMemberPath = "customerName";
            cbCustomer.SelectedValuePath = "customerId";
            cbCustomer.SelectedValue = invoice.customerid;

            if (invoice.customerid == 0) //NEW Invoice, in Order to Avoid NULL Foreign Key
            { cbCustomer.SelectedIndex = 0; }
        }
    }
}
