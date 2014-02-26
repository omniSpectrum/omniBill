using System;

namespace omniBill.InnerComponents.Models
{
    public class Customer : BaseModel
    {
        public Customer() :base(0){ }

        public Customer(int customerId, String companyName, String street, String postCode, String city, String phoneNumber, String email)
            : base(customerId)
        {
            CompanyName = companyName;
            Street = street;
            PostCode = postCode;
            City = city;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public String CompanyName { get; set; }
        public String Street { get; set; }
        public String PostCode { get; set; }
        public String City { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
    }
}
