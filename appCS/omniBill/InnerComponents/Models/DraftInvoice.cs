using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace omniBill.InnerComponents.Models
{
    public class DraftInvoice: BaseModel
    {
        public DraftInvoice(): base(0){}

        //TODO add llist of invoice lines to constructor
        public DraftInvoice(int invoiceId, UserTable userId, Customer customerId, DateTime dateT) : base(invoiceId) { }

        //TODO UserId Constructor
    }
}
