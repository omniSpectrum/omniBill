using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using omniBill.InnerComponents.Models;

namespace omniBill.InnerComponents.DataAccessLayer
{
    public class CustomersSqlCE : IGenericDAO<Customer>
    {
        private BaseProviderSqlCE myData;

        public CustomersSqlCE(String connectionString)
        {
            myData = new BaseProviderSqlCE(connectionString, "Customer");
        }

        public List<Customer> FindAll()
        {
            SqlCeDataReader reader = myData.SelectAll();
            List<Customer> customerList = new List<Customer>();

            while (reader.Read())
            {
                /* customerId   INT
                 * companyName  NVARCHAR
                 * street       NVARCHAR
                 * postCode     NVARCHAR
                 * city         NVARCHAR
                 * phoneNumber  NVARCHAR
                 * email        NVARCHAR
                 */

                int id = reader.GetInt32(0);
                String companyName = reader.GetString(1);
                String street = reader.GetString(2);
                String postCode = reader.GetString(3); 
                String city = reader.GetString(4); 
                String phoneNumber = reader.GetString(5);
                String email = reader.GetString(6);
            }

            return customerList;
        }
        public Customer FindById(int key)
        {
            throw new NotImplementedException();
        }
        public void Create(Customer model)
        {
            throw new NotImplementedException();
        }
        public void Edit(Customer model)
        {
            throw new NotImplementedException();
        }
        public void Delete(int key)
        {
            throw new NotImplementedException();
        }
    }
}
