using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using omniBill.InnerComponents.DataAccessLayer;

namespace omniBill.InnerComponents.LogicLayer
{
    public class UserHandler : IHandler<UserTable>
    {
        omniBillMsDbEntities db;

        public UserHandler()
        {
            db = new omniBillMsDbEntities();
        }

        public List<UserTable> ItemList()
        {
            return db.UserTables.ToList();
        }
        public UserTable ItemSingle(int id)
        {
            return db.UserTables.Find(id);
        }
        public bool CreateItem(UserTable user)
        {
            try
            {
                db.UserTables.Add(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool EditItem(UserTable user)
        {
            try
            {
                db.Entry(user).State = System.Data.EntityState.Modified;
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
                UserTable userToBeDeleted = db.UserTables.Find(id);
                userToBeDeleted.DraftInvoices.Clear();
                db.UserTables.Remove(userToBeDeleted);
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
