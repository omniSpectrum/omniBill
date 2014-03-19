using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using omniBill.InnerComponents.Models;
using omniBill.InnerComponents.Interfaces;
using omniBill.InnerComponents.DataAccessLayer;

namespace omniBill.InnerComponents.LogicLayer
{
    public class UserHandler : IHandler<UserTable>
    {
        IDataAccessLayer db;

        public UserHandler()
        {
            db = new DataAccessSpectrum();
        }

        public List<UserTable> ItemList()
        {
            return db.Users.FindAll();
        }
        public UserTable ItemSingle(int id)
        {
            return db.Users.FindById(id);
        }
        public bool CreateItem(UserTable user)
        {
            try
            {
                db.Users.Create(user);
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
                db.Users.Edit(user);
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
                db.Users.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
