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
        IHandler<Customer> customerHandler;
        IHandler<Item> itemHandler;
        List<Item> items;
        omniBillMsDbEntities db;

        public InvoiceStuffPage(DraftInvoice invoice)
        {
            InitializeComponent();

            customerHandler = new CustomerHandler();
            itemHandler = new ItemHandler();
            db = new omniBillMsDbEntities();

            mainInvoiceStuffGrid.DataContext = invoice;
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
            var undef = invoiceLinesGrid.SelectedItem;

            if (undef is InvoiceLine)
            {
                InvoiceLine currentLine = (InvoiceLine)undef;

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

        private void invoiceLinesGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                InvoiceLine currentRow = e.Row.DataContext as InvoiceLine;

                InvoiceLine myLine = db.InvoiceLines.Find(currentRow.invoiceId, currentRow.itemId);

                db.Entry(myLine).State = System.Data.EntityState.Modified;
                db.SaveChanges();

            }
        }

        private void invoiceLinesGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if ((MessageBox.Show("Are you sure you want to delete these items?", "Please confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
                {
                    var grid = (DataGrid)sender;
                    foreach (var row in grid.SelectedItems)
                    {
                        InvoiceLine lineToFind = (InvoiceLine)row;
                        InvoiceLine linetoDelete = db.InvoiceLines.Find(lineToFind.invoiceId, lineToFind.itemId);
                        db.InvoiceLines.Remove(linetoDelete);
                        db.SaveChanges();
                    }
                }
            }
        }

        ///*Works on Key press Enter Or loosing focus*/
        //private void invoiceLinesGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        //{
        //    // The whole grid
        //    var x = (DataGrid)sender;

        //}
    }
}
