using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using omniBill.InnerComponents.Models;

namespace omniBill.InnerComponents.DataAccessLayer
{
    /// <summary>
    /// Class specificly for Microsoft SQL CE database
    /// Contains generic SELECT, INSERT, UPDATE and DELETE
    /// </summary>
    public class GenericProviderSqlCE<M> : IGenericDAO<M>
    {
        private String connectionString;
        public String tableName;

        public GenericProviderSqlCE(String connectionString)
        {
            this.connectionString = connectionString;
            this.tableName = typeof(M).Name.ToString();
        }

        public virtual List<M> FindAll()
        {
            using (SqlCeConnection scn = new SqlCeConnection(connectionString))
            {
                scn.Open();
                SqlCeCommand command = scn.CreateCommand();

                command.CommandText = String.Format("SELECT * FROM {0}", tableName);
                SqlCeDataReader result = command.ExecuteReader();

                List<M> genericList = new List<M>();

                while (result.Read())
                { 
                    ///Logic for mapping             

                    dynamic obj = null;
                    //MapObject(result);
                    if (tableName == "Customer")
                         obj = MapObject(result);

                    genericList.Add(obj);
                }

                scn.Close();
                return genericList;
            }
        }
        public virtual M FindById(int key)
        { 
            throw new NotImplementedException();
        }
        public virtual void Create(M model)
        { 
            throw new NotImplementedException();
        }
        public virtual void Edit(M model)
        { 
            throw new NotImplementedException(); 
        }
        public virtual void Delete(int key)
        {
            throw new NotImplementedException();
        }

        public Customer MapObject(SqlCeDataReader reader)
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

            Customer customer = new Customer(id, companyName, street, postCode, city, phoneNumber, email);

            return customer;
        }
    }
}
