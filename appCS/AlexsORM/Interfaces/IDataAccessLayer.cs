using System;
using omniBill.InnerComponents.DataAccessLayer;
using omniBill.InnerComponents.Models;

namespace omniBill.InnerComponents.Interfaces
{
    public interface IDataAccessLayer
    {
        //HELPERS
        String ConnectionString { get; set; }

        //LanguageSet returns HashMap of LanguageRecords, or Dictionary<String, LanguageRecord> where String is unique english word
        ProviderLanguage Language { get; }

        //DATA ACCESS 
        IGenericDAO<Customer> Customers { get; }
        IGenericDAO<UserTable> Users { get; }
    }
}
