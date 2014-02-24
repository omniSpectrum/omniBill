using System;
using System.Collections.Generic;
using omniBill.InnerComponents.Models;

namespace omniBill.InnerComponents.DataAccessLayer
{
    public interface IBaseDAO
    {
        List<BaseModel> GetAll();
        BaseModel GetById(int key);
        void Create(BaseModel model);
        void Edit(BaseModel model);
        void Delete(int key);
    }
}
