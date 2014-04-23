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
        private ItemStuffPage mypage;

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
            Item currentItem = (Item)itemListGrid.SelectedItem;
            if (currentItem != null)
                showSidePanel(currentItem);
        }

        private void showSidePanel(Item item)
        {
            Grid.SetColumnSpan(listView, 1);
            btSave.Visibility = System.Windows.Visibility.Visible;
            if (item.itemId != 0)
            {
                btDelete.Visibility = System.Windows.Visibility.Visible;
            }
            sidePanelFrame.Visibility = System.Windows.Visibility.Visible;
            sidePanelFrame.Navigate(mypage = new ItemStuffPage(item));
        }

        public void hideSidePanel()
        {
            btSave.Visibility = System.Windows.Visibility.Hidden;
            btDelete.Visibility = System.Windows.Visibility.Hidden;
            sidePanelFrame.Visibility = System.Windows.Visibility.Hidden;
            Grid.SetColumnSpan(listView, 2);
        }

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
            itemListGrid.Columns[1].Header = omniLang.ItemName;
            itemListGrid.Columns[2].Header = omniLang.Description;
            itemListGrid.Columns[3].Header = omniLang.Price;
            itemListGrid.Columns[4].Visibility = Visibility.Hidden;
            itemListGrid.Columns[5].Visibility = Visibility.Hidden;
            itemListGrid.Columns[6].Visibility = Visibility.Hidden;
            
            var c = new DataGridTextColumn();
            c.Header = omniLang.VatRate;
            c.Binding = new Binding("VatGroup.percentage");
            c.Binding.StringFormat = "#.0 %";
            itemListGrid.Columns.Add(c);
            
            // !!!! TODO ADD TO DICTIONARY!!!!
        }
    }
}
