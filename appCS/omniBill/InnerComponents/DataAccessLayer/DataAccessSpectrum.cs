using System;
using omniBill.InnerComponents.Interfaces;

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
        private String connectionString;
        private DataStorage storageInUse;
        //DATA ACCESS FIELDS
        private BaseDAO customers;

        public DataAccessSpectrum(String connectString, DataStorage storageToUse)
        {
            //by initial idea connection strings should be stored in and retrived from Setting 
            this.connectionString = connectString;
            storageInUse = storageToUse;
        }
        public DataAccessSpectrum() : this(null, DataStorage.MSSql) { }
        
        //HELPER PROPERTIES
        public String ConnectionString
        { get { return connectionString; } set { connectionString = value; } }

        //DATA ACCESS PROPERTIES
        public BaseDAO Customers
        { 
            get { return Customers = (customers) ?? new CustomersMSSql(); }
            set { customers = value; }
        }
    }
}
