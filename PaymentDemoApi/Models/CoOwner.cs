using System;
using System.Collections.Generic;

namespace PaymentDemoApi.Models
{
    public partial class CoOwner
    {
        public CoOwner()
        {
            MonthDetails = new HashSet<MonthDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal? Balance { get; set; }
        private decimal _monthlyFee;
        public decimal MonthlyFee
        {
            //check if this works
            get => _monthlyFee; 
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value)); 
                else
                    _monthlyFee = value;
            }
        }
        

        public virtual ICollection<MonthDetail> MonthDetails { get; set; }
    }
}
