using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Domain;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public class CustomerDiscount:EntityBase
    {
        public long ProductId { get; private set; }
        public int DiscountRate { get; private set; }
        public string Reason { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public bool IsRemoved { get; private set; }

        public CustomerDiscount(long productId, int discountRate, string reason, DateTime startDate, DateTime endDate)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            Reason = reason;
            StartDate = startDate;
            EndDate = endDate;
            IsRemoved = false;
        }

        public void Edit(long productId, int discountRate, string reason, DateTime startDate, DateTime endDate)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            Reason = reason;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }


    }
}
