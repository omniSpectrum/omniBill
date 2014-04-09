using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using omniBill.InnerComponents.DataAccessLayer;

namespace omniBill.InnerComponents.LogicLayer
{
    public class CustomerHandler : IHandler<Customer>
    {
        omniBillMsDbEntities db;

        public CustomerHandler()
        {
            db = new omniBillMsDbEntities();
        }

        public List<Customer> ItemList()
        {
            return db.Customers.ToList();
        }
        public Customer ItemSingle(int id)
        {
            return db.Customers.Find(id);
        }
        public bool CreateItem(Customer customer)
        {
            try
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool EditItem(Customer customer)
        {
            try
            {
                db.Entry(customer).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteItem(int id)
        {
            try
            {
                Customer customerToBeDeleted = db.Customers.Find(id);
                db.Customers.Remove(customerToBeDeleted);
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
