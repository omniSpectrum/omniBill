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
using omniBill.InnerComponents.Models;
using omniBill.InnerComponents.Interfaces;
using omniBill.InnerComponents.DataAccessLayer;
using System.Diagnostics;
using omniBill.InnerComponents.LogicLayer;

namespace omniBill
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IDataAccessLayer db = new DataAccessSpectrum(); // TODO Remove later when Handler is implemented
        Dictionary<String, LanguageRecord> langSet; //Need to be in memory for until app is closed
        int currentLanguage; // Finnish is default
        IHandler<UserTable> userHandler;

        public MainWindow()
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
            cleanContentWrapper();

            Grid userGrid = new Grid();
            userGrid.Name = "UserTabGrid";
            Grid.SetColumn(userGrid, 0);
            Grid.SetColumnSpan(userGrid, 2);
            userGrid.Background = Brushes.White;
            userGrid.Margin = new Thickness(10);

            contentWrapper.Children.Add(userGrid);

            RowDefinition[] rows = new RowDefinition[10];

            for (int i = 0; i < 10; i++) 
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
            columns[1].Width = new GridLength(2, GridUnitType.Star);
            userGrid.ColumnDefinitions.Add(columns[1]); 


            //TODO put textboxes in array

            TextBlock lbCompanyName = new TextBlock();
            // TODO lbCompanyName = fetch from lang.xml
            lbCompanyName.Text = "Company Name";
            //lbCompanyName.Margin = new Thickness(10);
            Grid.SetRow(lbCompanyName, 0);
            Grid.SetColumn(lbCompanyName, 0);
            userGrid.Children.Add(lbCompanyName);


            TextBox tbCompanyName = new TextBox();
            //TODO tbCompanyName.text = fetch single user from db
            //tbCompanyName.Margin = new Thickness(10);
            Grid.SetRow(tbCompanyName, 0);
            Grid.SetColumn(tbCompanyName, 1);
            userGrid.Children.Add(tbCompanyName);
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

            String aboutPageFirstLineOfDescription = 
                langSet["omniBill is a simple standalone, lightweight and modular invoicing desktop app designed by omniSpectrum."][currentLanguage];
            body.Inlines.Add(aboutPageFirstLineOfDescription);
            body.Inlines.Add(new LineBreak());
            String aboutPageSecondLineOfDescription = langSet["omniBill will save you, just like Jesus."][currentLanguage];
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

            String aboutPageLicenseInfoString = langSet["License Information"][currentLanguage];
            String aboutPageContactUsString = langSet["Contact Us"][currentLanguage];

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
        #endregion

        #region Setting functions
        private void changeLanguage()
        {
            //FindOut language in Use from settings
            // currentLanguage = Settings.LanguageToUse;
            currentLanguage = (int)LanguageInUse.Portugese;

            //Get Language Set         
            langSet = db.Language.FindAll();

            //Change basic Menu Items
            lbInvoiceMenuItem.Text = langSet["Invoice"][currentLanguage];
            lbCustomerMenuItem.Text = langSet["Customer"][currentLanguage];

            lbItemMenuItem.Text = langSet["Item"][currentLanguage];
        }
        #endregion




        
    }
}
