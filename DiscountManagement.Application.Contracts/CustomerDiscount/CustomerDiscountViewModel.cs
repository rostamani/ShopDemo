using System;
using System.Collections.Generic;
using System.Text;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class CustomerDiscountViewModel
    {
        public long Id { get; set; }
        public string Reason { get; set; }
        public int DiscountRate { get; set; }
        public string FaStartDate { get; set; }
        public string FaEndDate { get; set; }
        public DateTime GrStartDate { get; set; }
        public DateTime GrEndDate { get; set; }
        public bool IsRemoved { get; set; }
        public string Product { get; set; }
        public long ProductId { get; set; }

    }
}
