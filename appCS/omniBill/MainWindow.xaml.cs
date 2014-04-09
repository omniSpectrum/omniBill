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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Threading;
using System.Globalization;
using omniBill.pages;
using omniBill.InnerComponents.Localization;
namespace omniBill
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        readonly String[] langCodes = { "en-us", "fi-fi", "ru-ru", "pt-br" };
        public enum omniLanguages { english, finnish, russian, portuguese }
        
        public MainWindow()
        {
            InitializeComponent();
            changeLanguage(omniLanguages.finnish); // TODO later change to | changeLanguage((omniLanguages)Resource.LangToUse) |
            navigation(new UserPage(this));
        }

        private void settingsDropDownButton_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void dropDownAboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            navigation(new AboutPage());
        }
        private void dropDownUserMenuItem_Click(object sender, RoutedEventArgs e)
        {
            navigation(new UserPage(this));
        }

        public void navigation(Page destinationPage)
        {
            ContentFrame.Navigate(destinationPage);
        }

        public void changeLanguage(omniLanguages langToUse = omniLanguages.english) {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(langCodes[(int)langToUse]);

            mmInvoice.Header = omniLang.Invoice;
            mmCustomer.Header = omniLang.Customer;
            mmItem.Header = omniLang.Item;

            ddmUser.Header = omniLang.User;
            ddmAbout.Header = omniLang.About;
            ddmSettings.Header = omniLang.Settings;
        }

        
    }
}
