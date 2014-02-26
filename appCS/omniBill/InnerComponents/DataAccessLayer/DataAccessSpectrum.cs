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
        private String dataDirectory;
        //DATA ACCESS FIELDS
        private IBaseDAO customers;

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
            connectionString = dataDirectory + "omniBillMsDb.sdf";
        }
        
        //HELPER PROPERTIES
        public String ConnectionString
        { get { return connectionString; } set { connectionString = value; } }

        //DATA ACCESS PROPERTIES
        public IBaseDAO Customers
        {
            get { return customers = (customers) ?? new CustomersMsDb(connectionString); }
        }
    }
}
