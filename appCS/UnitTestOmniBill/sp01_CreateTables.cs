using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using omniBill;
using omniBill.InnerComponents.DataAccessLayer;
using omniBill.InnerComponents.Interfaces;
using omniBill.InnerComponents.Models;

namespace UnitTestOmniBill.InvokeStoredProc
{
    [TestClass]
    public class sp01_CreateTables
    {
        [TestMethod]
        public void GoCreateTables()
        {
            StoredProcedureRepo sq = new StoredProcedureRepo();
            sq.CreateTables();
        }
    }
}
