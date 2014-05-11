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
        LineEditorPage childPage;

        //Constructor
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

            try
            {
                inv.dateT = DateTime.Parse(tbIssuedDate.Text);
                inv.dueDate = DateTime.Parse(tbDueDate.Text);
            }
            catch (Exception ex) { }
            
            inv.customerid = ((Customer)cbCustomer.SelectedItem).customerId;

            return inv;
        }

        // Bind for Customer ComboBox
        private void cbBind(DraftInvoice invoice)
        {
            cbCustomer.ItemsSource = db.Customers.ToList();
            cbCustomer.DisplayMemberPath = "customerName";
            cbCustomer.SelectedValuePath = "customerId";
            cbCustomer.SelectedValue = invoice.customerid;

            if (invoice.customerid == 0) //NEW Invoice, in Order to Avoid NULL Foreign Key
            { cbCustomer.SelectedIndex = 0; }
        }

        // Lines Grid Event
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

            if (itemsLeft.Count > 0 || currentLine.itemId != 0)
            {
                Grid.SetColumnSpan(invoiceHeaderGrid, 1);
                lineEditorFrame.Visibility = Visibility.Visible;
                btSaveLine.Visibility = Visibility.Visible;
                if (currentLine.invoiceId != 0)
                {
                    btDeleteLine.Visibility = System.Windows.Visibility.Visible;
                }

                lineEditorFrame.Navigate(childPage = new LineEditorPage(currentLine, itemsLeft, this));
            }
            else
            {
                MessageBox.Show("You have no Items availible, or you already used all availible");
            }            
        }
        public void hideLineEditor()
        {
            Grid.SetColumnSpan(invoiceHeaderGrid, 2);
            lineEditorFrame.Visibility = Visibility.Hidden;
            btSaveLine.Visibility = Visibility.Hidden;
            btDeleteLine.Visibility = Visibility.Hidden;

            invoiceLinesGrid.SelectedIndex = -1;
        }

        private void refreshTable(int invoiceId)
        {
            invoiceLinesGrid.ItemsSource = db.InvoiceLines.Where(x => x.invoiceId == invoiceId).ToList();
        }
        #endregion

        #region LineButtons
        private void NewLineButton_Click(object sender, RoutedEventArgs e)
        {
            invoiceLinesGrid.SelectedIndex = -1;
            showLineEditor(new InvoiceLine());
        }
        private void SaveLineButton_Click(object sender, RoutedEventArgs e)
        {
            InvoiceLine line = childPage.displayToModel();
            bool x = line.invoiceId == 0;
 
            if(x)
            {
                line.invoiceId = myInvoice.invoiceId;
                db.InvoiceLines.Add(line);                
            }
            else
            {
                db.Entry(line).State = System.Data.EntityState.Modified;
            }

            db.SaveChanges();
            refreshTable(myInvoice.invoiceId);
            hideLineEditor();
        }
        private void btDeleteLine_Click(object sender, RoutedEventArgs e)
        {
            InvoiceLine line = childPage.displayToModel();

            InvoiceLine lineToBeDeleted = db.InvoiceLines.Find(line.invoiceId,line.itemId);
            db.InvoiceLines.Remove(lineToBeDeleted);
            db.SaveChanges();

            refreshTable(myInvoice.invoiceId);
            hideLineEditor();
        }
        #endregion   
 
        private List<Item> checkAvailibleItems(int invoiceId)
        {
            List<Item> allItems = db.Items.ToList();
            List<Item> inUseItems = db.InvoiceLines.Where(x => x.invoiceId == invoiceId)
                                    .Select(z => z.Item).ToList();

            List<Item> availibleItems = allItems.Except(inUseItems).ToList();

            return availibleItems;
        }
    }
}
