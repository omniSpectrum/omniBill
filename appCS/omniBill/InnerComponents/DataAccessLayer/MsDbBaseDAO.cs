using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using omniBill.InnerComponents.Models;

namespace omniBill.InnerComponents.DataAccessLayer
{
    /// <summary>
    /// Class specificly for Microsoft SQL database
    /// Contains generic SELECT, INSERT, UPDATE and DELETE
    /// </summary>
    public abstract class MsDbBaseDAO : IBaseDAO
    {
        String connectionString;

        public MsDbBaseDAO(String connectionString)
        {
            this.connectionString = connectionString;
        }

        public virtual List<BaseModel> FindAll()
        {
            using (SqlCeConnection scn = new SqlCeConnection(connectionString))
            {
                scn.Open();
                SqlCeCommand command = scn.CreateCommand();

                command.CommandText = "SELECT * FROM table";
                SqlCeDataReader result = command.ExecuteReader();

                scn.Close();
                return null;
            }
        }
        public virtual BaseModel FindById(int key)
        { 
            throw new NotImplementedException();
        }
        public virtual void Create(BaseModel model)
        { 
            throw new NotImplementedException();
        }
        public virtual void Edit(BaseModel model)
        { 
            throw new NotImplementedException(); 
        }
        public virtual void Delete(int key)
        {
            throw new NotImplementedException();
        }
    }
}
