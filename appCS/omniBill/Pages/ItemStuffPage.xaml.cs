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
    /// Interaction logic for ItemStuffPage.xaml
    /// </summary>
    public partial class ItemStuffPage : Page
    {
        IHandler<VatGroup> vatHandler;

        public ItemStuffPage(Item item)
        {
            InitializeComponent();

            vatHandler = new VatHandler();
            mainItemStuffGrid.DataContext = item;
            cbBind();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Utils.itemPage.hideSidePanel();
        }

        public Item displayToModel()
        {
            return (Item)mainItemStuffGrid.DataContext;
        }

        private void cbBind()
        {
            
        }
    }
}
