using System;
using System.Collections.Generic;
using System.Text;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class CustomerDiscountSearchModel
    {
        public long ProductId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

    }
}
