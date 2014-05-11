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
using omniBill.InnerComponents.DataAccessLayer;
using omniBill.InnerComponents.LogicLayer;

namespace omniBill.pages
{
    /// <summary>
    /// Interaction logic for InvoicePage.xaml
    /// </summary>
    public partial class InvoicePage : Page
    {
        IHandler<DraftInvoice> invoiceHandler;
        private InvoiceStuffPage mypage;
        omniBillMsDbEntities db;

        public InvoicePage(String command = "")
        {
            InitializeComponent();
            invoiceHandler = new InvoiceHandler();
            db = new omniBillMsDbEntities();
            Utils.invoicePage = this;

            if (command == "New")
            {
                showSidePanel(new DraftInvoice());
            }
        }

        private void invoiceListGrid_Loaded(object sender, RoutedEventArgs e)
        {
            refreshTable();
        }
        private void invoiceListGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DraftInvoice currentInvoice = (DraftInvoice)invoiceListGrid.SelectedItem;
            if (currentInvoice != null)
                showSidePanel(currentInvoice);
        }

        private void showSidePanel(DraftInvoice invoice)
        {
            Grid.SetColumnSpan(listView, 1);
            btSave.Visibility = System.Windows.Visibility.Visible;
            btDelete.Visibility = System.Windows.Visibility.Visible;
            btCancel.Visibility = Visibility.Visible;
            sidePanelFrame.Visibility = System.Windows.Visibility.Visible;
            sidePanelFrame.Navigate(mypage = new InvoiceStuffPage(invoice));
        }
        public void hideSidePanel()
        {
            btSave.Visibility = System.Windows.Visibility.Hidden;
            btDelete.Visibility = System.Windows.Visibility.Hidden;
            btCancel.Visibility = System.Windows.Visibility.Hidden;
            sidePanelFrame.Visibility = System.Windows.Visibility.Hidden;
            Grid.SetColumnSpan(listView, 2);

            invoiceListGrid.SelectedIndex = -1;
        }

        //Buttons Clicks
        private void btNew_Click(object sender, RoutedEventArgs e)
        {
            int customers = db.Customers.Count();

            if (customers > 0)
            {
                var newInvoice = new DraftInvoice();
                newInvoice.dateT = DateTime.Now;
                newInvoice.dueDate = DateTime.Now;

                Customer c = db.Customers.FirstOrDefault();
                UserTable u = db.UserTables.FirstOrDefault();

                newInvoice.customerid = c.customerId;
                newInvoice.userId = u.userId;

                using (var mine = new omniBillMsDbEntities())
                {
                    mine.DraftInvoices.Add(newInvoice);
                    mine.SaveChanges();
                }

                showSidePanel(newInvoice);
            }
            else
            {
                MessageBox.Show("Unable to create invoice Without customers");
            }
        }
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            DraftInvoice invoice = mypage.displayToModel();
            invoiceHandler.EditItem(invoice);
            refreshTable();
            hideSidePanel();
        }
        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            DraftInvoice invoice = mypage.displayToModel();
            invoiceHandler.DeleteItem(invoice.invoiceId);
            refreshTable();
            hideSidePanel();
        }
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            hideSidePanel();
        }

        private void refreshTable()
        {
            invoiceListGrid.ItemsSource = invoiceHandler.ItemList();

            /*
                public int invoiceId { get; set; }
                public int userId { get; set; }
                public int customerid { get; set; }
                public System.DateTime dateT { get; set; }
    
                public virtual Customer Customer { get; set; }
                public virtual UserTable UserTable { get; set; }
                public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }
             */            
        }
    }
}
