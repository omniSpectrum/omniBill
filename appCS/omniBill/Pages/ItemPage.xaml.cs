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
using omniBill.InnerComponents.Localization;

namespace omniBill.pages
{
    /// <summary>
    /// Interaction logic for ItemPage.xaml
    /// </summary>
    public partial class ItemPage : Page
    {
        IHandler<Item> itemHandler;
        // private ItemStuffPage mypage;

        public ItemPage()
        {
            InitializeComponent();
            itemHandler = new ItemHandler();
            Utils.itemPage = this;
        }

        private void itemListGrid_Loaded(object sender, RoutedEventArgs e)
        {
            refreshTable();
        }
        private void itemListGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // TODO Show/Hide panel

        private void newItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void refreshTable()
        {
            itemListGrid.ItemsSource = itemHandler.ItemList();

            /*
             *  public int itemId { get; set; }
                public string itemName { get; set; }
                public string descr { get; set; }
                public decimal price { get; set; }
                public int vatId { get; set; }
    
                public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }
                public virtual VatGroup VatGroup { get; set; }
             */

            itemListGrid.Columns[0].Visibility = Visibility.Hidden;
            itemListGrid.Columns[1].Header = "Item Name";
            itemListGrid.Columns[2].Header = "Description";
            itemListGrid.Columns[3].Header = "Price";
            itemListGrid.Columns[4].Visibility = Visibility.Hidden;
            itemListGrid.Columns[5].Visibility = Visibility.Hidden;
            itemListGrid.Columns[6].Visibility = Visibility.Hidden;
            
            var c = new DataGridTextColumn();
            c.Header = "Vat Rate";
            c.Binding = new Binding("VatGroup.percentage");
            c.Binding.StringFormat = "#.0 %";
            itemListGrid.Columns.Add(c);
            

            // !!!! TODO ADD TO DICTIONARY!!!!
        }
    }
}
