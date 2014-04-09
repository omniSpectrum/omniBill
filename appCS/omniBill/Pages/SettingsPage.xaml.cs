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
using omniBill.Properties;
using omniBill;
using omniBill.InnerComponents.Localization;

namespace omniBill.pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private MainWindow mainWindow;

        public SettingsPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void cbLanguages_Loaded(object sender, RoutedEventArgs e)
        {
            int currentLang = Settings.Default.LanguageInUse;
            cbLanguages.SelectedIndex = currentLang;
        }

        private void cbLanguages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int userSelection = cbLanguages.SelectedIndex;

            Settings.Default.LanguageInUse = userSelection;

            Settings.Default.Save();

            mainWindow.changeLanguage((omniBill.MainWindow.omniLanguages) userSelection);

            lbLangs.Text = omniLang.Language;

        }



    }
}
