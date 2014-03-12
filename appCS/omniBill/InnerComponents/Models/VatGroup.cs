using System;

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
