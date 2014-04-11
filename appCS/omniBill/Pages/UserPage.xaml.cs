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

                tbCompanyName.Text = user.companyName;
                tbContactName.Text = user.contactName;
                tbStreet.Text = user.street;
                tbPostCode.Text = user.postCode;
                tbCity.Text = user.city;
                tbBankName.Text = user.bankName;
                tbBankAccount.Text = user.bankAccount;
                tbBusinessId.Text = user.businessId;
                tbPhoneNumber.Text = user.phoneNumber;
                tbEmail.Text = user.email;

                tbId.Text = user.userId.ToString();
            }
        }

        // Event handler Save to save record to DB
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO Validation of user input + UserHandler.EditItem returns predicate

            int id = int.Parse(tbId.Text);

            UserTable user = userHandler.ItemSingle(id);

            user.companyName = tbCompanyName.Text;
            user.contactName = tbContactName.Text;
            user.street = tbStreet.Text;
            user.postCode = tbPostCode.Text;
            user.city = tbCity.Text;
            user.bankName = tbBankName.Text;
            user.bankAccount = tbBankAccount.Text;
            user.businessId = tbBusinessId.Text;
            user.phoneNumber = tbPhoneNumber.Text;
            user.email = tbEmail.Text;

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
