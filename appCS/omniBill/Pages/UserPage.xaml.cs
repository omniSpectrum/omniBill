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
using omniBill.InnerComponents.LogicLayer;
using omniBill.InnerComponents.DataAccessLayer;

namespace omniBill.pages
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        IHandler<UserTable> userHandler;

        public UserPage()
        {
            InitializeComponent();

            this.userHandler = new UserHandler();           
            fetchUserData();
        }

        //method called in constructor to fetch record from DB
        private void fetchUserData()
        {
            List<UserTable> userList = userHandler.ItemList();

            if (userList.Count > 0)
            {
                UserTable user = userList[0];

                userPageGrid.DataContext = user;
            }
        }

        // Event handler Save to save record to DB
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO Validation of user input + UserHandler.EditItem returns predicate

            UserTable user = (UserTable) userPageGrid.DataContext;


            userHandler.EditItem(user);

            //TODO POP-up saved succesfully
        }

        // Event handler for Cancel for Redirecting to About page
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Frame.Navigate(new About page());
            Utils.mainWindow.navigation(new AboutPage());          
        }
    }
}
