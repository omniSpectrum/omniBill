﻿using System;
using System.Data.SqlServerCe;

namespace omniBill.InnerComponents.DataAccessLayer
{
    /*TRANSACTION EXAMPLE
     * The Idea is that if some statement fails 
     * We don't need to drop tables manually,
     * but the transaction will be rolled back */
    //    SqlConnection = null;
    //try
    //{
    //        connection = new SqlConnection(connStr);
    //        connection.Open();
    //        SqlTransaction trans = connection.BeginTransaction();
    //        String queryString = "INSERT SOME STUFF";
    //        SqlCommand command = new SqlCommand(queryString, connection);
    //        command.Transaction = trans;
    //        command.ExecuteNonQuery();
    //        queryString = "INSERT SOME OTHER STUFF";
    //        command = new SqlCommand(queryString, connection);
    //        command.Transaction = trans;
    //        command.ExecuteNonQuery();
    //        trans.Commit();
    //}
    //catch (Exception ex)
    //{
    //        trans.Rollback();
    //        errMsg.Text = ex.Message;
    //}
    //finally
    //{
    //        if(connection != null)
    //                connection.Close();
    //}

    public class StoredProcedureRepo
    {
        private string connectionString = (new DataAccessSpectrum()).ConnectionString;

        public void CreateTables()
        {
            using (SqlCeConnection sn = new SqlCeConnection(connectionString))
            {
                sn.Open();        
                SqlCeCommand cmd = sn.CreateCommand();

                cmd.CommandText =
                    @"CREATE TABLE Customer
                    (
	                    customerId		INTEGER			IDENTITY(1,1)		PRIMARY KEY,
	                    customerName	NVARCHAR(100)	NOT NULL,
	                    street			NVARCHAR(250)	NOT NULL,
	                    postCode		NVARCHAR(25)	NOT NULL,
	                    city			NVARCHAR(30)	NOT NULL,
	                    phoneNumber		NVARCHAR(15)	NULL,
	                    email			NVARCHAR(50)	NOT NULL
                    );";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    @"CREATE TABLE UserTable
                    (
	                    userId			INTEGER			IDENTITY(1,1)		PRIMARY KEY,
	                    companyName		NVARCHAR(50)	NOT NULL,
	                    contactName		NVARCHAR(50)	NOT NULL,
	                    street			NVARCHAR(250)	NOT NULL,
	                    bankName		NVARCHAR(300)	NOT NULL,
	                    bankAccount		NVARCHAR(250)	NOT NULL,
	                    businessId		NVARCHAR(10)		NULL,
	                    phoneNumber		NVARCHAR(100)		NULL,
	                    email			NVARCHAR(150)	NOT	NULL
                    );";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    @"CREATE TABLE VatGroup
                    (
	                    vatId			INTEGER		    IDENTITY(1,1)			PRIMARY KEY,
	                    percentage		FLOAT			NOT NULL
                    )";
                cmd.ExecuteNonQuery();
                cmd.CommandText =
                    @"
                  CREATE TABLE Item
                    (
	                    itemId			INTEGER			IDENTITY(1,1)			PRIMARY KEY,
	                    itemName		NVARCHAR(100)			NOT NULL,
	                    descr		    NVARCHAR(2000)	NOT NULL,
	                    price			MONEY			NOT NULL,
	                    vatId			INTEGER		NOT NULL              REFERENCES        VatGroup(vatId)
                    )";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    @"CREATE TABLE DraftInvoice
                    (
	                    invoiceId		INTEGER			IDENTITY(1,1)		PRIMARY KEY,		
	                    userId			INTEGER			NOT NULL			REFERENCES UserTable(userId),
	                    customerid		INTEGER			NOT NULL            REFERENCES Customer(customerId),
	                    dateT			DATETIME		NOT NULL
                    )";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    @"CREATE TABLE InvoiceLine
                    (
	                    invoiceId		INTEGER			NOT NULL REFERENCES DraftInvoice(invoiceId),
	                    itemId  	    INTEGER			NOT NULL REFERENCES Item(itemId),
	                    quantity		INTEGER 		NOT NULL,
	                    comment			NVARCHAR(1000),

                    	PRIMARY KEY(invoiceId, itemId)
                    )";
                cmd.ExecuteNonQuery();
                
                sn.Close();
            }
        }

        public void DropTables()
        {
            using(SqlCeConnection db = new SqlCeConnection(connectionString))
            {
                db.Open();
                SqlCeCommand command = db.CreateCommand();

                String[] tablesToDrop = 
                    new String[] { "InvoiceLine", "DraftInvoice", "Item", "VatGroup", "UserTable", "Customer" };

                foreach (var table in tablesToDrop)
                {
                    command.CommandText = 
                        String.Format("drop table if exists {0}", table);
                    command.ExecuteNonQuery();
                }
                
                db.Close();
            }
        }

        public void InsertData()
        {
            
        }
    }
}
