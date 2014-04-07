//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace omniBill.InnerComponents.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class DraftInvoice
    {
        public DraftInvoice()
        {
            this.InvoiceLines = new HashSet<InvoiceLine>();
        }
    
        public int invoiceId { get; set; }
        public int userId { get; set; }
        public int customerid { get; set; }
        public System.DateTime dateT { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual UserTable UserTable { get; set; }
        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }
    }
}