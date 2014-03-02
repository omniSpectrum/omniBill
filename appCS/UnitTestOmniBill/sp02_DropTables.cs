using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using omniBill;
using omniBill.InnerComponents.DataAccessLayer;
using omniBill.InnerComponents.Interfaces;
using omniBill.InnerComponents.Models;

namespace UnitTestOmniBill
{
    [TestClass]
    public class sp02_DropTables
    {
        [TestMethod]
        public void GoDropTables()
        {
            StoredProcedureRepo sq = new StoredProcedureRepo();
            sq.DropTables();
        }
    }
}
