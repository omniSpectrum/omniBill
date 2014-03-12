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

namespace omniBill
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IDataAccessLayer db = new DataAccessSpectrum(); // TODO Remove later when Handler is implemented
        Dictionary<String, LanguageRecord> langSet; //Need to be here for until app is closed
        LanguageInUse currentLanguage = LanguageInUse.Finnish; // Finnish is default

        public MainWindow()
        {
            InitializeComponent();
        }

        #region UI functions
        #endregion

        #region DataToModel functions
        #endregion

        #region Setting functions
        private void changeLanguage()
        {
            //FindOut language in Use from settings
            // currentLanguage = Settings.LanguageToUse;

            //Get Language Set
            langSet = db.Language.FindAll();

            //Change basic Menu Items
            lbInvoiceMenuItem.Text = langSet["Invoice"].Finnish;
        }
        #endregion
    }
}
