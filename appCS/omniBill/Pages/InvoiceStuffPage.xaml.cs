using System;
using System.Collections.Generic;
using System.Data;
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
        DraftInvoice myInvoice;
        omniBillMsDbEntities db;

        public InvoiceStuffPage(DraftInvoice invoice)
        {
            InitializeComponent();

            db = new omniBillMsDbEntities();

            invoiceHeaderGrid.DataContext = myInvoice = invoice;
            refreshTable(myInvoice.invoiceId);
            cbBind(myInvoice);
        }

        #region Header Events
        public DraftInvoice displayToModel()
        {
            var inv = (DraftInvoice)invoiceHeaderGrid.DataContext;
            inv.customerid = ((Customer)cbCustomer.SelectedItem).customerId;
            inv.InvoiceLines = (ICollection<InvoiceLine>)invoiceLinesGrid.ItemsSource;
            return inv;
        }

        private void cbBind(DraftInvoice invoice)
        {
            cbCustomer.ItemsSource = db.Customers.ToList();
            cbCustomer.DisplayMemberPath = "customerName";
            cbCustomer.SelectedValuePath = "customerId";
            cbCustomer.SelectedValue = invoice.customerid;

            if (invoice.customerid == 0) //NEW Invoice, in Order to Avoid NULL Foreign Key
            { cbCustomer.SelectedIndex = 0; }
        }

        private void invoiceLinesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InvoiceLine currentLine = (InvoiceLine)invoiceLinesGrid.SelectedItem;
            if (currentLine != null)
                showLineEditor(currentLine);
        }

        private void showLineEditor(InvoiceLine currentLine)
        {
            //Check if there are availible items
            List<Item> itemsLeft = checkAvailibleItems(myInvoice.invoiceId);

            if (itemsLeft.Count > 0)
            {
                Grid.SetColumnSpan(invoiceHeaderGrid, 1);
                lineEditorGrid.Visibility = Visibility.Visible;

                lineEditorGrid.DataContext = currentLine;
                cbItemNameBind(currentLine, itemsLeft);
            }
            else
            {
                MessageBox.Show("You have no Items availible, or you already used all availible");
            }            
        }
        private void hideLineEditor()
        {
            Grid.SetColumnSpan(invoiceHeaderGrid, 2);
            lineEditorGrid.Visibility = Visibility.Hidden;
        }

        private void refreshTable(int invoiceId)
        {
            invoiceLinesGrid.ItemsSource = db.InvoiceLines.Where(x => x.invoiceId == invoiceId).ToList();
        }
        #endregion

        #region LineButtons
        private void NewLineButton_Click(object sender, RoutedEventArgs e)
        {
            showLineEditor(new InvoiceLine());
        }
        #endregion

        #region LineEditor Events
        private void lineCloseButton_Click(object sender, RoutedEventArgs e)
        {
            hideLineEditor();
        }

        private void cbItemNameBind(InvoiceLine line, List<Item> avalible)
        {
            if(line.Item != null)
                avalible.Add(line.Item);

            cbItemName.ItemsSource = avalible;

            cbItemName.SelectedValue = line.itemId;
            cbItemName.SelectedValuePath = "itemId";
            cbItemName.DisplayMemberPath = "itemName";

            if (line.itemId == 0) //NEW line, in Order to Avoid NULL Foreign Key
            { cbItemName.SelectedIndex = 0; }
        }

        private List<Item> checkAvailibleItems(int invoiceId)
        {
            List<Item> allItems = db.Items.ToList();
            List<Item> inUseItems = db.InvoiceLines.Where(x => x.invoiceId == invoiceId)
                                    .Select(z => z.Item).ToList();

            List<Item> availibleItems = allItems.Except(inUseItems).ToList();

            return availibleItems;
        }
        #endregion
    }
}
