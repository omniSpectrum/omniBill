using System;
using System.Collections.Generic;
using omniBill.InnerComponents.Models;

namespace omniBill.InnerComponents.DataAccessLayer
{
    public class CustomersMsDb : MsDbBaseDAO
    {
        public CustomersMsDb(String connectionString)
            : base(connectionString)
        {

        }

        public override List<BaseModel> FindAll()
        {


            throw new NotImplementedException();
        }
        public override BaseModel FindById(int key)
        {
            throw new NotImplementedException();
        }
        public override void Create(BaseModel model)
        {
            throw new NotImplementedException();
        }
        public override void Edit(BaseModel model)
        {
            throw new NotImplementedException();
        }
        public override void Delete(int key)
        {
            throw new NotImplementedException();
        }
    }
}
