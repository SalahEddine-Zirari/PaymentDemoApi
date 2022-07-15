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
        private decimal? _amountPaid;
        public decimal? AmountPaid { get => _amountPaid; 
            set
            {
                if (value<0)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _amountPaid = value;



            }
        }

        public virtual CoOwner? CoOwner { get; set; }
    }
}
