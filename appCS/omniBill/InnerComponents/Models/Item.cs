using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace omniBill.InnerComponents.Models
{
    public class Item: BaseModel
    {
        public Item() : base(0) { }

        public Item(int itemId, string itemName, string descr, decimal price, VatGroup vatId) : base(itemId) 
        {
            ItemName = itemName;
            Descr = descr;
            Price = price;
            VatId = vatId;
        }

        public string ItemName { get; set; }
        public string Descr { get; set; }
        public decimal Price { get; set; }
        public VatGroup VatId { get; set; }
    }
}
