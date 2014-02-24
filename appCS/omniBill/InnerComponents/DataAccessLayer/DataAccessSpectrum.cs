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
        private DataStorage storageInUse;
        private String connectionString;       
        private String appDirectory;
        //DATA ACCESS FIELDS
        private IBaseDAO customers;

        //CONSTRUCTORS
        public DataAccessSpectrum(DataStorage storageToUse, String connectString)
        {
            //by initial idea connection strings and Storage to use should be stored in and retrived from Setting 
            this.storageInUse = storageToUse;
            this.connectionString = connectString;           
            this.appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }
        public DataAccessSpectrum() : this(DataStorage.MSSql, null) { }
        
        //HELPER PROPERTIES
        public String ConnectionString
        { get { return connectionString; } set { connectionString = value; } }

        //DATA ACCESS PROPERTIES
        public IBaseDAO Customers
        { 
            get { return Customers = (customers) ?? new CustomersMsDb(); }
            set { customers = value; }
        }
    }
}
