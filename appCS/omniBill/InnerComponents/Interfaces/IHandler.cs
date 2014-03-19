using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace omniBill.InnerComponents.Interfaces
{
    public interface IHandler<M>
    {
        List<M> ItemList();
        M ItemSingle(int key);
        bool CreateItem(M model);
        bool EditItem(M model);
        bool DeleteItem(int key);
    }
}
