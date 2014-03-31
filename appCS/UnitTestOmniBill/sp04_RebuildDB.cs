using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using omniBill;
using omniBill.InnerComponents.DataAccessLayer;
using omniBill.InnerComponents.Interfaces;
using omniBill.InnerComponents.Models;

namespace UnitTestOmniBill
{
    [TestClass]
    public class sp04_RebuildDB
    {
        [TestMethod]
        public void GoRebuildDb()
        {
            StoredProcedureRepo sq = new StoredProcedureRepo();

            sq.DropTables();
            sq.CreateTables();
            //sq.InsertData();
        }
    }
}