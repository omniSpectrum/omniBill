using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using omniBill;
using omniBill.InnerComponents.DataAccessLayer;
using omniBill.InnerComponents.Interfaces;
using omniBill.InnerComponents.Models;

namespace UnitTestOmniBill
{
    [TestClass]
    public class DataAcessDBTests
    {
        [TestMethod]
        public void TestConnectionString()
        {
            IDataAccessLayer db = new DataAccessSpectrum();

            Customer myCustomer = new Customer(11, "Nokia", "Espoo");

            db.Customers.Create(myCustomer);

            List<BaseModel> myList = db.Customers.GetAll();

            foreach (Customer singleCustomer in myList)
            {
                String here = singleCustomer.CompanyName;
            }
        }
    }
}
