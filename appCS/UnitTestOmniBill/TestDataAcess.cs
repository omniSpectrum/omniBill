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
    public class TestDataAcess
    {
        [TestMethod]
        public void TestConnectionString()
        {
            String HARD_CODED_WORKING_DIRECTORY = 
                "C:\\Users\\a1203248\\Desktop\\omniBill\\appCS\\omniBill" + "\\bin\\Debug"+ "\\Data\\omniBillMsDb.sdf";

            IDataAccessLayer my = new DataAccessSpectrum();

            string expect = "Data Source=" + HARD_CODED_WORKING_DIRECTORY;

            Assert.AreEqual(expect, my.ConnectionString);
        }

        [TestMethod]
        public void TestCustomerDAO()
        {
            IDataAccessLayer db = new DataAccessSpectrum();

            //INSERT TEST
            Customer initialCustomer = new Customer(0, "Nokia", "katu", "00000", "Espoo", "156456", "dfd@mail.fi");
            db.Customers.Create(initialCustomer);

            List<Customer> myList = db.Customers.FindAll();
            Customer retrievedCustomer = myList[myList.Count - 1];

            string expected = "Nokia";
            string result = retrievedCustomer.CompanyName;

            Assert.AreEqual(expected, result);

            //UPDATE TEST
            retrievedCustomer.CompanyName = "Sunny";
            db.Customers.Edit(retrievedCustomer);
            
            Customer findOne = db.Customers.FindById(retrievedCustomer.Key);
            Assert.AreEqual(retrievedCustomer.Key, findOne.Key);
            Assert.AreEqual(retrievedCustomer.CompanyName, findOne.CompanyName);

            //DELETE TEST
            db.Customers.Delete(retrievedCustomer.Key);
            Customer findTwo = db.Customers.FindById(retrievedCustomer.Key);
            Assert.AreEqual(null, findTwo);
        }

        // TODO UserTable tests
        public void TestUserDao()
        {
            IDataAccessLayer db = new DataAccessSpectrum();
            //INSERT TEST
            UserTable initialUser = new UserTable(0, "omniSpectrum", "Daniel", "Random", "598", 
                "Espoo", "Nordea", "123455", "A32432", "2344242", "dana@gmail.com");

            db.Users.Create(initialUser);

            List<UserTable> myUserList = db.Users.FindAll();
            UserTable myUser = myUserList[myUserList.Count - 1];

            string expected = "Daniel";
            string actual = myUser.ContactName;

            Assert.AreEqual(expected, actual);

            //UPDATE TEST
            myUser.ContactName = "Niko";
            db.Users.Edit(myUser);

            const string expectedName = "Niko";
            var retrievedUser = db.Users.FindById(myUser.Key);

            Assert.AreEqual(expectedName, retrievedUser.ContactName);

            //DELETE TEST
            db.Users.Delete(myUser.Key);
            Assert.IsNull(db.Users.FindById(myUser.Key));
        }
        // TODO Item tests
    }
}
