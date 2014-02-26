CREATE PROCEDURE sp_01_createTables
AS

CREATE TABLE Customer
(
	customerId		INTEGER			IDENTITY(1,1)		PRIMARY KEY,
	customerName	VARCHAR(100)	NOT NULL,
	street			VARCHAR(250)	NOT NULL,
	postCode		VARCHAR(20)		NOT NULL,
	city			VARCHAR(100)	NOT NULL,
	phoneNumber		BIGINT			NOT NULL,
	email			VARCHAR(50)		NOT NULL
);

CREATE TABLE DraftInvoice
(
	invoiceId		INTEGER			IDENTITY(1,1)		PRIMARY KEY,		
	ownerNumber		INTEGER			NOT NULL			default 1,
	customerid		INTEGER			NOT NULL,
	dateT			DATETIME		NOT NULL

	CONSTRAINT		FK_customerNumber		FOREIGN KEY(customerId)	REFERENCES Customer(customerId)
);

CREATE TABLE InvoiceLine
(
	invoiceId		INTEGER			NOT NULL,
	itemNumber	    INTEGER			NOT NULL,
	quantity		SMALLINT		NOT NULL,
	comment			VARCHAR(1000)


	CONSTRAINT		FK_invoiceNumber		FOREIGN KEY(invoiceId)	REFERENCES DraftInvoice(invoiceId),
	CONSTRAINT		FK_itemNumber			FOREIGN KEY(itemId)	REFERENCES Item(itemId),
	CONSTRAINT		PK_InvoiceLine			PRIMARY KEY(invoiceId, itemId)
);

CREATE TABLE VatGroup
(
	vatId			VARCHAR(50)		IDENTITY(1,1)			PRIMARY KEY,
	percentage		MONEY			NOT NULL
);

CREATE TABLE Item
(
	itemId			INTEGER			IDENTITY(1,1)			NOT NULL,
	itemName		INTEGER			NOT NULL,
	descr		    VARCHAR(2000)	NOT NULL,
	price			MONEY			NOT NULL,
	vatId			VARCHAR(20)		NOT NULL

	CONSTRAINT		PK_Item		PRIMARY KEY(itemId, vatId),
	CONSTRAINT		FK_VatGroupCode		FOREIGN KEY(vatId)		REFERENCES		VatGroup(vatid)
);