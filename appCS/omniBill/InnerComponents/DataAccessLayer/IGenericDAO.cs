using System;
using System.Collections.Generic;
using omniBill.InnerComponents.Models;

namespace omniBill.InnerComponents.DataAccessLayer
{
    public interface IGenericDAO<M>
    {
        List<M> FindAll();
        M FindById(int key);
        void Create(M model);
        void Edit(M model);
        void Delete(int key);
    }
}
