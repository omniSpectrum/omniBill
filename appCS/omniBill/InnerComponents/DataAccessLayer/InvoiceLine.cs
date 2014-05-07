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
    
    public partial class InvoiceLine
    {
        private decimal? linePrice;

        public int invoiceId { get; set; }
        public int itemId { get; set; }
        public int quantity { get; set; }
        public string comment { get; set; }
        public Nullable<decimal> priceptax { 
            get 
        {            
            if(Item != null)
                linePrice =((this.Item.price * this.quantity) * (1 + (decimal)this.Item.VatGroup.percentage));

            return linePrice;            
        }
            set
            { this.linePrice = value; }
        }
    
        public virtual DraftInvoice DraftInvoice { get; set; }
        public virtual Item Item { get; set; }

        
    }
}
