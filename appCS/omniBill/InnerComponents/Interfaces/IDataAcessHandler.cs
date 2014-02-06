using System;
using omniBill.InnerComponents.Models;

namespace omniBill.InnerComponents.Interfaces
{
    public interface IDataAcessHandler
    {
        GenericModel[] SelectAll(String table);

        GenericModel SelectSingle(String table, int primaryKey);

        GenericModel[] SelectWhere(String table, String column, String equalityCondition);

        GenericModel[] SearchContains(String table, String query);

        bool Insert(String table, GenericModel model);

        bool Delete(String table, int primaryKey);

        bool CheckForChilds(GenericModel model);
    }
}
