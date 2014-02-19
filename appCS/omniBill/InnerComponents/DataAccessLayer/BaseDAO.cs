using System;
using System.Collections.Generic;
using omniBill.InnerComponents.Models;

namespace omniBill.InnerComponents.DataAccessLayer
{
    public abstract class BaseDAO
    {
        public abstract List<BaseModel> GetAll();
        public abstract BaseModel GetById(int key);
        public abstract void Create(BaseModel model);
        public abstract void Edit(BaseModel model);
        public abstract void Delete(int key);
    }
}
