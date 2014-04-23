using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using omniBill.InnerComponents.DataAccessLayer;

namespace omniBill.InnerComponents.LogicLayer
{
    public class ItemHandler : IHandler<Item>
    {
        omniBillMsDbEntities db;

        public ItemHandler()
        {
            db = new omniBillMsDbEntities();
        }

        public List<Item> ItemList()
        {
            return db.Items.ToList();
        }
        public Item ItemSingle(int id)
        {
            return db.Items.Find(id);
        }
        public bool CreateItem(Item item)
        {
            try
            {
                db.Items.Add(item);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool EditItem(Item item)
        {
            try
            {
                db.Entry(item).State = System.Data.EntityState.Modified;
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
                Item itemToBeDeleted = db.Items.Find(id);
                db.Items.Remove(itemToBeDeleted);
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
