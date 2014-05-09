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
        DraftInvoice myInvoice;
        omniBillMsDbEntities db;

        public InvoiceStuffPage(DraftInvoice invoice)
        {
            InitializeComponent();

            customerHandler = new CustomerHandler();
            itemHandler = new ItemHandler();
            db = new omniBillMsDbEntities();

            mainInvoiceStuffGrid.DataContext = myInvoice = invoice;
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
            // Invoice line if exists
            var undef = invoiceLinesGrid.SelectedItem;
            // Selected Product/Item
            var z = ((IList<Item>)e.AddedItems)[0];

            // Editting
            if (undef is InvoiceLine)
            {
                InvoiceLine currentLine = (InvoiceLine)undef;

                ComboBox combo = sender as ComboBox;
                int x = (int)combo.SelectedValue;

                Item item = items.Find(i => i.itemId == x);
                currentLine.itemId = x;
                currentLine.Item = item;

                refreshLineDataGrid();
            }
            else
            {
                InvoiceLine line = new InvoiceLine();
                line.itemId = z.itemId;
                line.invoiceId = myInvoice.invoiceId;

                saveInvoiceLine(line);
            }
        }

        private void invoiceLinesGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                InvoiceLine currentRow = e.Row.DataContext as InvoiceLine;

                saveInvoiceLine(currentRow);
            }
        }

        private void saveInvoiceLine(InvoiceLine currentRow)
        {
            InvoiceLine myLine = db.InvoiceLines.Find(currentRow.invoiceId, currentRow.itemId);

            //Adding new Line
            if (myLine == null)
            {
                Item defaultItem = db.Items.FirstOrDefault();
                if (defaultItem != null)
                {
                    currentRow.itemId = defaultItem.itemId;
                    currentRow.invoiceId = ((DraftInvoice)mainInvoiceStuffGrid.DataContext).invoiceId;

                    db.InvoiceLines.Add(currentRow);
                }
            }
            //Updating
            else
            {
                db.Entry(myLine).State = System.Data.EntityState.Modified;
            }

            db.SaveChanges();

            refreshLineDataGrid();
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
                        InvoiceLine lineToFind = row as InvoiceLine;
                        InvoiceLine linetoDelete = db.InvoiceLines.Find(lineToFind.invoiceId, lineToFind.itemId);
                        db.InvoiceLines.Remove(linetoDelete);
                    }

                    db.SaveChanges();
                    refreshLineDataGrid();
                }
            }
        }

        private void refreshLineDataGrid() 
        {
            var temp = invoiceLinesGrid.ItemsSource;
            invoiceLinesGrid.ItemsSource = null;
            invoiceLinesGrid.ItemsSource = temp;
        }
    }
}
