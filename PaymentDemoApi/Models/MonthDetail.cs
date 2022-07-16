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
        private decimal? _ammountPaid;
        public decimal? AmmountPaid{ get => _ammountPaid; 
            set
            {
                if (value<0) 
                    throw new ArgumentOutOfRangeException(nameof(value));

                _ammountPaid = value;



            }
        }

        public virtual CoOwner? CoOwner { get; set; }
    }
}
