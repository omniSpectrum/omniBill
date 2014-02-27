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
    public class BaseProviderSqlCE
    {
        private String connectionString;
        private String table;

        public BaseProviderSqlCE(String connectionString, String table)
        {
            this.connectionString = connectionString;
            this.table = table;
        }

        public virtual SqlCeDataReader SelectAll()
        {
            using (SqlCeConnection scn = new SqlCeConnection(connectionString))
            {
                scn.Open();
                SqlCeCommand command = scn.CreateCommand();

                command.CommandText = String.Format("SELECT * FROM {0}", table);
                SqlCeDataReader result = command.ExecuteReader();

                scn.Close();
                return result;
            }
        }
        public virtual BaseModel SelectOne(int key)
        { 
            throw new NotImplementedException();
        }
        public virtual void Insert(BaseModel model)
        { 
            throw new NotImplementedException();
        }
        public virtual void Update(BaseModel model)
        { 
            throw new NotImplementedException(); 
        }
        public virtual void Delete(int key)
        {
            throw new NotImplementedException();
        }
    }
}
