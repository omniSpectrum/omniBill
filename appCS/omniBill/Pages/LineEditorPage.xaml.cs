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

namespace omniBill.pages
{
    /// <summary>
    /// Interaction logic for LineEditorPage.xaml
    /// </summary>
    public partial class LineEditorPage : Page
    {
        InvoiceStuffPage parent;
        omniBillMsDbEntities db;

        public LineEditorPage(InvoiceLine line, List<Item> itemsLeft, InvoiceStuffPage parent)
        {
            InitializeComponent();

            this.parent = parent;
            db = new omniBillMsDbEntities();

            refreshLineEditor(line);
            cbItemNameBind(line, itemsLeft);
        }

        private void lineCloseButton_Click(object sender, RoutedEventArgs e)
        {
            parent.hideLineEditor();
        }

        private void cbItemNameBind(InvoiceLine line, List<Item> avalible)
        {
            cbItemName.SelectionChanged -= cbItemName_SelectionChanged;

            if (line.Item != null)
            {
                avalible.Insert(0, line.Item);
                cbItemName.IsEnabled = false;
            }

            cbItemName.ItemsSource = avalible;

            cbItemName.SelectedValue = line.itemId;
            cbItemName.SelectedValuePath = "itemId";
            cbItemName.DisplayMemberPath = "itemName";

            if (line.itemId == 0) //NEW line, in Order to Avoid NULL Foreign Key
            { cbItemName.SelectedIndex = 0; }

            cbItemName.SelectionChanged += cbItemName_SelectionChanged;
        }
        private void cbItemName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Item x = (Item)cbItemName.SelectedItem;
            InvoiceLine currentLine = (InvoiceLine)lineEditorGrid.DataContext;

            currentLine.itemId = x.itemId;
            currentLine.Item = x;

            refreshLineEditor(currentLine);
        }
        private void tbQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(tbQuantity.Text, out n);

            if (isNumeric)
            {
                InvoiceLine currentLine = (InvoiceLine)lineEditorGrid.DataContext;

                currentLine.quantity = n;
                refreshLineEditor(currentLine);
                tbQuantity.CaretIndex = tbQuantity.Text.Length;
            }
        }

        private void refreshLineEditor(InvoiceLine currentLine)
        {
            tbQuantity.TextChanged -= tbQuantity_TextChanged;

            lineEditorGrid.DataContext = null;
            lineEditorGrid.DataContext = currentLine;

            tbQuantity.TextChanged += tbQuantity_TextChanged;
        }

        public InvoiceLine displayToModel()
        {
            var i = (InvoiceLine)lineEditorGrid.DataContext;
            i.itemId = ((Item)cbItemName.SelectedItem).itemId;
            return i;
        }
    }
}
