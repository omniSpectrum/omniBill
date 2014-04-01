using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace omniBill.InnerComponents.Models
{
    public class UserTable : BaseModel
    {
        public UserTable() :base(0){ }

        public UserTable(int userId, String companyName, String contactName, String street, String postCode,
            String city, String bankName, String bankAccount, String businessId, String phoneNumber, String email)
            : base(userId)
        {
            CompanyName = companyName;
            ContactName = contactName;
            Street = street;
            PostCode = postCode;
            City = city;

            BankName = bankName;
            BankAccount = bankAccount;
            BusinessId = businessId;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public String CompanyName { get; set; }
        public String ContactName { get; set; }
        public String Street { get; set; }
        public String PostCode { get; set; }
        public String City { get; set; }
        public String BankName { get; set; }
        public String BankAccount { get; set; }
        public String BusinessId { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }

    }
}
