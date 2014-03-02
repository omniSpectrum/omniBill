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
        private String tableName;
        private String keyName;

        public GenericProviderSqlCE(String connectionString, String keyName)
        {
            this.connectionString = connectionString;
            this.tableName = typeof(M).Name.ToString();
            this.keyName = keyName;
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

        public virtual M FindById(int keyValue)
        {
            using (SqlCeConnection scn = new SqlCeConnection(connectionString))
            {
                scn.Open();
                SqlCeCommand command = scn.CreateCommand();

                command.CommandText =
                    String.Format("SELECT * FROM {0} WHERE {1} = {2}", tableName, keyName, keyValue);
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

                String querySuffix = MapInsertSuffix((dynamic)model);

                command.CommandText =
                    String.Format("INSERT INTO {0}{1}", tableName, querySuffix);
                
                command.ExecuteNonQuery();
            }
        }

        public virtual void Edit(M model)
        {
            using (SqlCeConnection scn = new SqlCeConnection(connectionString))
            {
                scn.Open();
                SqlCeCommand command = scn.CreateCommand();

                String querySuffix = MapUpdateSuffix((dynamic)model);

                command.CommandText =
                    String.Format("UPDATE {0} SET {1}", tableName, querySuffix);

                command.ExecuteNonQuery();
            }
        }

        public virtual void Delete(int keyValue)
        {
            using (SqlCeConnection scn = new SqlCeConnection(connectionString))
            {
                scn.Open();
                SqlCeCommand command = scn.CreateCommand();

                command.CommandText =
                    String.Format("DELETE FROM {0} WHERE {1}={2}", tableName, keyName, keyValue);

                command.ExecuteNonQuery();
            }
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
        private String MapInsertSuffix(Customer customer)
        {
            String suffix = String.Format("(customerName, street, postCode, city, phoneNumber, email) VALUES"
                                        + "(\'{0}\', \'{1}\', \'{2}\', \'{3}\', \'{4}\', \'{5}\')",
                                        customer.CompanyName, customer.Street, customer.PostCode,
                                        customer.City, customer.PhoneNumber, customer.Email);
            return suffix;
        }
        private String MapUpdateSuffix(Customer customer)
        {
            String suffix = String.Format("customerName=\'{0}\', street=\'{1}\', postCode=\'{2}\', "
                                        +"city=\'{3}\', phoneNumber=\'{4}\', email=\'{5}\' "
                                        +"WHERE {6}={7}",
                                        customer.CompanyName, customer.Street, customer.PostCode,
                                        customer.City, customer.PhoneNumber, customer.Email,
                                        keyName, customer.Key);

            return suffix;
        }
        #endregion
    }
}
