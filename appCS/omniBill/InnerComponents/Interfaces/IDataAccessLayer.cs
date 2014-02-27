using System;
using omniBill.InnerComponents.DataAccessLayer;
using omniBill.InnerComponents.Models;

namespace omniBill.InnerComponents.Interfaces
{
    public interface IDataAccessLayer
    {
        //HELPERS
        String ConnectionString { get; set; }

        //DATA ACCESS 
        IGenericDAO<Customer> Customers { get; }
    }
}
