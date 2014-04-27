using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using omniBill.InnerComponents.DataAccessLayer;

namespace omniBill.InnerComponents.LogicLayer
{
    public class InvoiceHandler : IHandler<DraftInvoice>
    {
        omniBillMsDbEntities db;

        public InvoiceHandler()
        {
            db = new omniBillMsDbEntities();
        }

        public List<DraftInvoice> ItemList() 
        {
            return db.DraftInvoices.ToList();
        }
        public DraftInvoice ItemSingle(int id)
        {
            return db.DraftInvoices.Find(id);
        }
        public bool CreateItem(DraftInvoice invoice) { throw new NotImplementedException(); }
        public bool EditItem(DraftInvoice invoice) { throw new NotImplementedException(); }
        public bool DeleteItem(int id) { throw new NotImplementedException(); }
    }
}
