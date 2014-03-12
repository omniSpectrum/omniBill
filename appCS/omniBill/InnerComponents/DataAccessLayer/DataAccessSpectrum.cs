using System;
using omniBill.InnerComponents.Interfaces;
using omniBill.InnerComponents.Models;

namespace omniBill.InnerComponents.DataAccessLayer
{
    /*
     * Facade class which serves as Adaptor 
     * and gives entry point to DAO objects
     * 
     * This wrapper contains objects to manipulate 
     * different databases, existing systems as well as XML
     */

    public enum DataStorage { MSSql, SqLite, Legacy}

    public class DataAccessSpectrum : IDataAccessLayer
    {
        //HELPER FIELDS
        private DataStorage storageInUse;
        private String connectionString;       
        private String dataDirectory;
        //DATA ACCESS FIELDS
        private IGenericDAO<Customer> customers;
        private IGenericDAO<UserTable> users;

        //CONSTRUCTORS
        public DataAccessSpectrum(DataStorage storageToUse, String connectString)
        {
            //by initial idea connection strings and Storage to use should be stored in and retrived from Setting 
            this.storageInUse = storageToUse;
            this.connectionString = connectString;           
            this.dataDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\Data\\";
        }
        public DataAccessSpectrum() : this(DataStorage.MSSql, null) 
        {
            dataDirectory = dataDirectory.Replace("\\UnitTestOmniBill", "\\omniBill");
            connectionString = String.Format("Data Source={0}omniBillMsDb.sdf", dataDirectory);
        }
        
        //HELPER PROPERTIES
        public String ConnectionString
        { get { return connectionString; } set { connectionString = value; } }

        //DATA ACCESS PROPERTIES
        public IGenericDAO<Customer> Customers
        {
            get { return customers = (customers) ?? new GenericProviderSqlCE<Customer>(connectionString, "customerId"); }
        }
        public IGenericDAO<UserTable> Users
        {
            get { return users = (users) ?? new GenericProviderSqlCE<UserTable>(connectionString, "userId"); }
        }
    }
}
