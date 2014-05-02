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
        IHandler<Item> itemHandler;
        List<Item> items;

        public InvoiceStuffPage(DraftInvoice invoice)
        {
            InitializeComponent();

            customerHandler = new CustomerHandler();
            itemHandler = new ItemHandler();
            invoiceHeaderGrid.DataContext = invoice;
            invoiceLinesGrid.ItemsSource = invoice.InvoiceLines.ToList();
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


        private void cbTest_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            combo.ItemsSource = items = itemHandler.ItemList();
            combo.SelectionChanged += cbTest_SelectionChanged;
        }

        private void cbTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InvoiceLine currentLine = (InvoiceLine)invoiceLinesGrid.SelectedItem;

            ComboBox combo = sender as ComboBox;
            int x = (int)combo.SelectedValue;

            Item item = items.Find(i => i.itemId == x);
            currentLine.itemId = x;
            currentLine.Item = item;

            var temp = invoiceLinesGrid.ItemsSource;
            invoiceLinesGrid.ItemsSource = null;
            invoiceLinesGrid.ItemsSource = temp;
        }
    }
}
