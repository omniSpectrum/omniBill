using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using omniBill.InnerComponents.Models;
using System.Text;

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

                String[][] propertyMap = MapProperties((dynamic)model);
                int PROPERTY_NAME_SubArrayIndex = 0;
                int PROPERTY_VALUE_SubArrayIndex = 1;

                StringBuilder myBuilder = new StringBuilder("INSERT INTO " + tableName + "(");

                //column names
                myBuilder.Append(string.Join(", ", propertyMap[PROPERTY_NAME_SubArrayIndex], 
                    1, propertyMap[PROPERTY_NAME_SubArrayIndex].Length - 1));

                myBuilder.Append(") VALUES(");

                //row values
                myBuilder.Append(string.Join(", ", propertyMap[PROPERTY_VALUE_SubArrayIndex], 
                    1, propertyMap[PROPERTY_VALUE_SubArrayIndex].Length - 1));

                myBuilder.Append(")");

                command.CommandText = myBuilder.ToString();
                
                command.ExecuteNonQuery();
            }
        }

        public virtual void Edit(M model)
        {
            using (SqlCeConnection scn = new SqlCeConnection(connectionString))
            {
                scn.Open();
                SqlCeCommand command = scn.CreateCommand();

                String[][] propertyMap = MapProperties((dynamic)model);
                int PROPERTY_NAME = 0;
                int PROPERTY_VALUE = 1;

                StringBuilder myBuilder = new StringBuilder("UPDATE " + tableName + " SET ");

                /*
                 * UPDATE Customer 
                 * SET propertyName1 = propertyValue1, propertyName2 = propertyValue2, ....
                 * WHERE customerId = 12
                 */
                String[] propertyPair = new string[propertyMap[PROPERTY_NAME].Length - 1];

                for (int i = 1; i < propertyMap[PROPERTY_NAME].Length; i++)
                {
                    propertyPair[i - 1] = String.Format("{0} = {1}", propertyMap[PROPERTY_NAME][i], propertyMap[PROPERTY_VALUE][i]);
                }

                myBuilder.Append(string.Join(", ", propertyPair));
                myBuilder.Append(String.Format(" WHERE {0} = {1}", propertyMap[PROPERTY_NAME][0], propertyMap[PROPERTY_VALUE][0]));

                command.CommandText = myBuilder.ToString();
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
        private String[][] MapProperties(Customer customer)
        {
            /* customerId   INT
            * companyName  NVARCHAR
            * street       NVARCHAR
            * postCode     NVARCHAR
            * city         NVARCHAR
            * phoneNumber  NVARCHAR
            * email        NVARCHAR
            */

            String[][] myProperties = 
            {
                //property NAMES
                new string[]{keyName, "customerName", "street", "postCode", "city", "phoneNumber", "email"},
                //property VALUES
                new string[]{String.Format("{0}", customer.Key), String.Format("\'{0}\'", customer.CompanyName), 
                    String.Format("\'{0}\'", customer.Street), String.Format("\'{0}\'", customer.PostCode),
                    String.Format("\'{0}\'", customer.City), String.Format("\'{0}\'", customer.PhoneNumber),
                    String.Format("\'{0}\'", customer.Email)}
            };

            return myProperties;
        }

        private void MapObject(SqlCeDataReader reader, UserTable user)
        {
            user.Key = reader.GetInt32(0);

            user.CompanyName = reader.IsDBNull(1) ? null : reader.GetString(1); 
            user.ContactName = reader.GetString(2);
            user.Street = reader.IsDBNull(3) ? null : reader.GetString(3); 

            user.PostCode = reader.IsDBNull(4) ? null : reader.GetString(4);
            user.City = reader.IsDBNull(5) ? null : reader.GetString(5);
            user.BankName = reader.IsDBNull(6) ? null : reader.GetString(6);

            user.BankAccount = reader.IsDBNull(7) ? null : reader.GetString(7);
            user.BusinessId = reader.IsDBNull(8) ? null : reader.GetString(8);
            user.PhoneNumber = reader.IsDBNull(9) ? null : reader.GetString(9);

            user.Email = reader.GetString(10);
        }
        private String[][] MapProperties(UserTable user)
        {
            /* userId INTEGER
	         * companyName		NVARCHAR(50)
	         * contactName		NVARCHAR(50)
	         * street			NVARCHAR(250)
             * postCode         NVARCHAR(25)
             * city             NVARCHAR(75)
	         * bankName		    NVARCHAR(300)
	         * bankAccount		NVARCHAR(250)
	         * businessId		NVARCHAR(10)
	         * phoneNumber		NVARCHAR(100)
	         * email			NVARCHAR(150)
             */

            String[][] myProperties =
            {
                //property NAMES
                new string[]
                {
                    keyName, "companyName", "contactName", "street", "postCode", "city",
                    "bankName", "bankAccount", "businessId", "phoneNumber", "email"
                },

                //property VALUES
                new string[]
                {
                    String.Format("{0}", user.Key), String.Format("\'{0}\'", user.CompanyName), String.Format("\'{0}\'", user.ContactName), 
                    String.Format("\'{0}\'", user.Street), String.Format("\'{0}\'", user.PostCode),String.Format("\'{0}\'", user.City),
                    String.Format("\'{0}\'", user.BankName), String.Format("\'{0}\'", user.BankAccount), 
                    String.Format("\'{0}\'", user.BusinessId), String.Format("\'{0}\'", user.PhoneNumber), String.Format("\'{0}\'", user.Email)
                }
            };

            return myProperties;
        }
        #endregion
    }
}
