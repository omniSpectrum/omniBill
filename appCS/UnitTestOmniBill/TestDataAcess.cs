﻿using System;
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
                "C:\\Users\\a1203246\\Desktop\\omniBill\\appCS\\omniBill" + "\\bin\\Debug"+ "\\Data\\omniBillMsDb.sdf";

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
    }
}
