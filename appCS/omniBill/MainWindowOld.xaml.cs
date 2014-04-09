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
using System.Diagnostics;
using omniBill.InnerComponents.LogicLayer;
using omniBill.InnerComponents.Localization;
using omniBill.InnerComponents.DataAccessLayer;
using System.Threading;
using System.Globalization;

namespace omniBill
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowOld : Window
    {   
        IHandler<UserTable> userHandler;

        public MainWindowOld()
        {
            InitializeComponent();
            changeLanguage();
            displayAboutPage();

            userHandler = new UserHandler();
        }
        #region Event handlers
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }


        private void settingsDropDownMenuUserItemClick(object sender, RoutedEventArgs e)
        {
            //Clear ContentWrapper
            cleanContentWrapper();

            //ScrollViewer create
            ScrollViewer userTabScrollView = new ScrollViewer();
            userTabScrollView.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            userTabScrollView.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            userTabScrollView.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            userTabScrollView.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            //ScrollViewer place into ContentWrapper
            Grid.SetColumn(userTabScrollView, 0);
            Grid.SetColumnSpan(userTabScrollView, 2);
            contentWrapper.Children.Add(userTabScrollView);

            //Create user GRID
            Grid userGrid = new Grid();
            userGrid.Name = "UserTabGrid";
            userGrid.Background = Brushes.White;
            userGrid.Margin = new Thickness(10);

            userTabScrollView.Content = userGrid;

            //DEFINE rows and columns
            RowDefinition[] rows = new RowDefinition[11];

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = new RowDefinition();
                rows[i].Height = new GridLength(1, GridUnitType.Star);
                userGrid.RowDefinitions.Add(rows[i]);
            }

            ColumnDefinition[] columns = {
                                             new ColumnDefinition(),
                                             new ColumnDefinition()
                                         };


            columns[0].Width = new GridLength(1, GridUnitType.Star);
            userGrid.ColumnDefinitions.Add(columns[0]);
            columns[1].Width = new GridLength(3, GridUnitType.Star);
            userGrid.ColumnDefinitions.Add(columns[1]);
            
            //Create TextBoxes and labels
            TextBox[] userTextBoxes = new TextBox[10];
            TextBlock[] userLabels = new TextBlock[10];

            for (int i = 0; i < userTextBoxes.Length; i++)
            {
                userTextBoxes[i] = new TextBox();
                userLabels[i] = new TextBlock();
            }

            int x = 0;
            //0
            userLabels[x].Text = omniLang.CompanyName;
            userTextBoxes[x++].Name = "tbCompanyName";

            userLabels[x].Text = omniLang.ContactName;
            userTextBoxes[x++].Name = "tbContactName";

            userLabels[x].Text =omniLang.Street;
            userTextBoxes[x++].Name = "tbStreet";

            userLabels[x].Text = omniLang.PostCode;
            userTextBoxes[x++].Name = "tbPostCode";

            userLabels[x].Text = omniLang.City;
            userTextBoxes[x++].Name = "tbCity";
            //5
            userLabels[x].Text = omniLang.BankName;
            userTextBoxes[x++].Name = "tbBankName";

            userLabels[x].Text = omniLang.BankAccount;
            userTextBoxes[x++].Name = "tbBankAccount";

            userLabels[x].Text = omniLang.BusinessId;
            userTextBoxes[x++].Name = "tbBusinessId";

            userLabels[x].Text = omniLang.PhoneNumber;
            userTextBoxes[x++].Name = "tbPhoneNumber";

            userLabels[x].Text = omniLang.Email;
            userTextBoxes[x++].Name = "tbEmail";

            var commonMargin = new Thickness(18, 4, 18, 4);

            // TODO for loop
            for (int i = 0; i < userTextBoxes.Length; i++)
            {
                Grid.SetRow(userLabels[i], i);
                Grid.SetColumn(userLabels[i], 0);
                userLabels[i].TextWrapping = TextWrapping.Wrap;
                userLabels[i].Margin = commonMargin;
                userLabels[i].VerticalAlignment = System.Windows.VerticalAlignment.Center;

                userGrid.Children.Add(userLabels[i]);

                Grid.SetRow(userTextBoxes[i], i);
                Grid.SetColumn(userTextBoxes[i], 1);
                userTextBoxes[i].Margin = commonMargin;
                userTextBoxes[i].MaxHeight = 30;
                userTextBoxes[i].VerticalAlignment = System.Windows.VerticalAlignment.Center;

                userGrid.Children.Add(userTextBoxes[i]);
            }

            Button button = new Button();
            button.Name = "btSaveUser";
            button.Content = "Save bitch";
            //button.Click+=button_SaveUserData;

            Grid.SetRow(button, 10);
            Grid.SetColumn(button, 1);
            button.Margin = commonMargin;
            button.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            button.MaxHeight = 30;
            button.Width = 150;

            userGrid.Children.Add(button);

            userModelToDisplay(userTextBoxes);
            
        }

        private void button_SaveUserData(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void settingsDropDownMenuAboutItemClick(object sender, RoutedEventArgs e)
        {
            displayAboutPage();
        }
        #endregion

        #region UI General functions
        private void cleanContentWrapper()
        {
            contentWrapper.Children.Clear();
        }

        private void displayAboutPage()
        {
            cleanContentWrapper();
            
            Grid myGrid = new Grid();

            myGrid.Name = "aboutpage_Grid";
            Grid.SetColumn(myGrid, 0);
            Grid.SetColumnSpan(myGrid, 2);
            contentWrapper.Children.Add(myGrid);

            /* Setting up row definitions*/
            RowDefinition[] rowArray ={
                                          new RowDefinition(),
                                          new RowDefinition(),
                                          new RowDefinition()
                                      };
            rowArray[0].Height = new GridLength(1, GridUnitType.Star);
            rowArray[1].Height = new GridLength(5, GridUnitType.Star);
            rowArray[2].Height = new GridLength(1.5, GridUnitType.Star);

            foreach (var row in rowArray) 
            {
                myGrid.RowDefinitions.Add(row);
            }
            /*END row definitions settings*/

            /*Setting up header*/
            TextBlock header = new TextBlock();
            header.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            header.Padding = new Thickness(15);
            header.TextWrapping = TextWrapping.Wrap;
            header.FontSize = 21;
            header.FontWeight = FontWeights.Bold;
            header.Text = "omniBill";
            Grid.SetRow(header, 0);
            
            myGrid.Children.Add(header);

            /*Setting up body */
            TextBlock body = new TextBlock();
            body.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            body.Padding = new Thickness(15);
            body.TextWrapping = TextWrapping.Wrap;

            String aboutPageFirstLineOfDescription = omniLang.aboutFirstLine;
            body.Inlines.Add(aboutPageFirstLineOfDescription);
            body.Inlines.Add(new LineBreak());
            String aboutPageSecondLineOfDescription = omniLang.aboutSecondLine;
            body.Inlines.Add(aboutPageSecondLineOfDescription);
            Grid.SetRow(body, 1);

            myGrid.Children.Add(body);

            /*Setting footer grid */
            Grid footerGrid = new Grid();
            footerGrid.Background = Brushes.Pink;
            footerGrid.Name = "footerAboutPage_Grid";
            Grid.SetRow(footerGrid, 2);
            myGrid.Children.Add(footerGrid);

            /*Setting up column definitions for footer grid */
            ColumnDefinition[] columns = 
            {
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition()
            };

            columns[0].Width = new GridLength(1, GridUnitType.Star);
            columns[1].Width = new GridLength(1, GridUnitType.Star);
            columns[2].Width = new GridLength(1, GridUnitType.Star);

            foreach (var column in columns) 
            {
                footerGrid.ColumnDefinitions.Add(column);
            }

            /*Setting up footer content */
            TextBlock[] footerLinks_TB =
            {
                new TextBlock(),
                new TextBlock(),
                new TextBlock()
            };

            String aboutPageLicenseInfoString = omniLang.LicenseInfo;
            String aboutPageContactUsString = omniLang.ContactUs;

            Hyperlink[] links = {
                                          new Hyperlink(new Run("Github")),
                                          new Hyperlink(new Run(aboutPageLicenseInfoString)),
                                          new Hyperlink(new Run(aboutPageContactUsString))
                                      };

            links[0].NavigateUri = new Uri("https://www.github.com/omniSpectrum");
            links[1].NavigateUri = new Uri("https://www.github.com/omniSpectrum/omniBill/blob/master/LICENSE");
            links[2].NavigateUri = new Uri("mailto:omnispectrum@outlook.com");

            for (int index = 0; index < 3; index++ )
            {
                footerLinks_TB[index].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                footerLinks_TB[index].HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                footerLinks_TB[index].Padding = new Thickness(15);
                footerLinks_TB[index].TextWrapping = TextWrapping.Wrap;

                Grid.SetColumn(footerLinks_TB[index], index);
                footerGrid.Children.Add(footerLinks_TB[index]);

                /* Setting up links*/
                links[index].RequestNavigate += Hyperlink_RequestNavigate;
                footerLinks_TB[index].Inlines.Add(links[index]);
            }
        }

        #endregion


        #region DataToModel functions

        private void userModelToDisplay(TextBox[] tbUser)
        {
            List<UserTable> userTable = userHandler.ItemList();

            if (userTable.Count > 0)
            {
                UserTable user = userTable[0];

                int i = 0;

                tbUser[i++].Text = user.companyName;
                tbUser[i++].Text = user.contactName;
                tbUser[i++].Text = user.street;
                tbUser[i++].Text = user.postCode;
                tbUser[i++].Text = user.city;
                tbUser[i++].Text = user.bankName;
                tbUser[i++].Text = user.bankAccount;
                tbUser[i++].Text = user.businessId;
                tbUser[i++].Text = user.phoneNumber;
                tbUser[i++].Text = user.email;
            }
        }

        #endregion

        #region Setting functions
        private void changeLanguage(String langCode = "en-us")
        {

            //TODO: Find out how to change language
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(langCode);

            //Change basic Menu Items
            lbInvoiceMenuItem.Text = omniLang.Invoice;
            lbCustomerMenuItem.Text = omniLang.Customer;

            lbItemMenuItem.Text = omniLang.Item;
        }
        #endregion

    }
}
