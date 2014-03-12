using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace omniBill.InnerComponents.Models
{
    public class VatGroup: BaseModel
    {
        public VatGroup(int vatId, double percentage) : base(vatId)
        {
            Percentage = percentage;
        }

        public double Percentage { get; set; }
    }
}
