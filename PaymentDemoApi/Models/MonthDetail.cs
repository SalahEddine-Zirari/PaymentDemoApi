using System;
using System.Collections.Generic;

namespace PaymentDemoApi.Models
{
    public partial class MonthDetail
    {

        public int TransactionId { get; set; }
        public int? CoOwnerId { get; set; }
        public int MonthNum { get; set; }
        public string IsPaid { get; set; } = null!;
        public decimal? AmmountPaid { get; set; }

        
        public virtual CoOwner? CoOwner { get; set; }
        

    }
}
