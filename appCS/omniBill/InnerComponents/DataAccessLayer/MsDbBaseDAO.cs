using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using omniBill.InnerComponents.Models;

namespace omniBill.InnerComponents.DataAccessLayer
{
    /// <summary>
    /// Class specificly for Microsoft SQL database
    /// </summary>
    public abstract class MsDbBaseDAO : IBaseDAO
    {
        ///Will contain generic SELECT, INSERT, UPDATE and DELETE

        public MsDbBaseDAO()
        {

        }

        public virtual List<BaseModel> GetAll()
        { 
            throw new NotImplementedException(); 
        }
        public virtual BaseModel GetById(int key)
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
