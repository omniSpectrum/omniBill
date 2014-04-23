using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using omniBill.InnerComponents.DataAccessLayer;

namespace omniBill.InnerComponents.LogicLayer
{
    public class VatHandler : IHandler<VatGroup>
    {
        omniBillMsDbEntities db;

        public VatHandler()
        {
            db = new omniBillMsDbEntities();
        }

        public List<VatGroup> ItemList() 
        {
            return db.VatGroups.ToList();
        }
        public VatGroup ItemSingle(int id) 
        {
            throw new NotImplementedException();
        }
        public bool CreateItem(VatGroup vatGroup) 
        {
            throw new NotImplementedException();        
        }
        public bool EditItem(VatGroup vatGroup) 
        {
            throw new NotImplementedException();
        }
        public bool DeleteItem(int id) 
        {
            throw new NotImplementedException();
        }

    }
}
