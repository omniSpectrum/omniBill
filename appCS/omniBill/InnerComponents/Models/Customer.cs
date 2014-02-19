using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace omniBill.InnerComponents.Models
{
    public class Customer : BaseModel
    {
        private String companyName;
        private String address;

        public Customer(int key, String companyName, String address)
            : base(key)
        {
            this.companyName = companyName;
            this.address = address;
        }

        public String CompanyName
        { get { return companyName; } set { companyName = value; } }

        public String Address
        { get { return address; } set { address = value; } }
    }
}
