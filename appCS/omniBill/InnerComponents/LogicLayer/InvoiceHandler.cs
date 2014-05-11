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
        public bool CreateItem(DraftInvoice invoice) 
        {
            try
            {
                db.DraftInvoices.Add(invoice);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            } 
        }
        public bool EditItem(DraftInvoice invoice) 
        {
            try
            {
                db.Entry(invoice).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteItem(int id) 
        {
            try
            {
                DraftInvoice invoiceToBeDeleted = db.DraftInvoices.Find(id);
                db.DraftInvoices.Remove(invoiceToBeDeleted);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
