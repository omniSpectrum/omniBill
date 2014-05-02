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

        public InvoicePage()
        {
            InitializeComponent();
            invoiceHandler = new InvoiceHandler();
            Utils.invoicePage = this;
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
            if (invoice.invoiceId != 0)
            {
                btDelete.Visibility = System.Windows.Visibility.Visible;
            }
            sidePanelFrame.Visibility = System.Windows.Visibility.Visible;
            sidePanelFrame.Navigate(mypage = new InvoiceStuffPage(invoice));
        }
        public void hideSidePanel()
        {
            btSave.Visibility = System.Windows.Visibility.Hidden;
            btDelete.Visibility = System.Windows.Visibility.Hidden;
            sidePanelFrame.Visibility = System.Windows.Visibility.Hidden;
            Grid.SetColumnSpan(listView, 2);
        }

        //TODO Buttons Clicks

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
