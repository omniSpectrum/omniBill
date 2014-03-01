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
    public class GenericProviderSqlCE<M> : IGenericDAO<M> where M : new()
    {
        private String connectionString;
        public String tableName;

        public GenericProviderSqlCE(String connectionString)
        {
            this.connectionString = connectionString;
            this.tableName = typeof(M).Name.ToString();
        }

        #region CRUD functions
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
                    dynamic obj = new M();
                    MapObject(result, obj);
                    genericList.Add(obj);                    
                }

                scn.Close();
                return genericList;
            }
        }
        public virtual M FindById(int key)
        {
            using (SqlCeConnection scn = new SqlCeConnection(connectionString))
            {
                scn.Open();
                SqlCeCommand command = scn.CreateCommand();

                String keyProperty = String.Format("{0}Id", tableName.ToLower()); 

                command.CommandText =
                    String.Format("SELECT * FROM {0} WHERE {1} = {2}", tableName, keyProperty, key);
                SqlCeDataReader result = command.ExecuteReader();

                dynamic genericModel = null;

                while (result.Read())
                {
                    genericModel = new M();
                    MapObject(result, genericModel);
                }

                scn.Close();
                return genericModel;
            }
        }
        public virtual void Create(M model)
        {
            using (SqlCeConnection scn = new SqlCeConnection(connectionString))
            {
                scn.Open();
                SqlCeCommand command = scn.CreateCommand();

                var here = typeof(M).GetProperties();

                Dictionary<String, String> valueSet = new Dictionary<string, string>();

                foreach (var item in here)
                {
                    valueSet.Add(item.Name.ToString(), item.GetValue(model, null).ToString());                 
                }

                command.CommandText =
                    String.Format("INSERT INTO {0} VALUES()", tableName);

                /////////////////////Doesn't work
                
                command.ExecuteNonQuery();
            }
        }
        public virtual void Edit(M model)
        { 
            throw new NotImplementedException(); 
        }
        public virtual void Delete(int key)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region MappingHelpers
        private void MapObject(SqlCeDataReader reader, Customer customer)
        {
            /* customerId   INT
                * companyName  NVARCHAR
                * street       NVARCHAR
                * postCode     NVARCHAR
                * city         NVARCHAR
                * phoneNumber  NVARCHAR
                * email        NVARCHAR
                */

            customer.Key = reader.GetInt32(0);
            customer.CompanyName = reader.GetString(1);
            customer.Street = reader.GetString(2);
            customer.PostCode = reader.GetString(3);
            customer.City = reader.GetString(4);
            customer.PhoneNumber = reader.GetString(5);
            customer.Email = reader.GetString(6);
        }
        #endregion
    }
}
