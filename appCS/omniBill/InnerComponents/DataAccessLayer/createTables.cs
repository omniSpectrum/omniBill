using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace omniBill.InnerComponents.DataAccessLayer
{
    public class createTables
    {
        private const string connectionString = "";
        private string dbDir = "Data Source=C:\\Users\\a1203248\\Desktop\\omniBill\\appCS\\omniBill\\Data\\omniBillMsDb.sdf";

        public void accessDB()
        {
            using (SqlCeConnection sn = new SqlCeConnection(dbDir))
            {
                sn.Open();
                SqlCeCommand cmd = sn.CreateCommand();

                cmd.CommandText =
                    @"CREATE TABLE Customer
                    (
	                    customerId		INTEGER			IDENTITY(1,1)		PRIMARY KEY,
	                    customerName	NVARCHAR(100)	NOT NULL,
	                    street			NVARCHAR(250)	NOT NULL,
	                    postCode		NVARCHAR(25)		NOT NULL,
	                    city			NVARCHAR(30)	NOT NULL,
	                    phoneNumber		NVARCHAR(15)			NULL,
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
    }
}
