using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.Order
{
    public class CartItem
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string TotalPrice { get; set; }

        public double Count { get; set; }

        public string Picture { get; set; }

        public string Slug { get; set; }

        public bool IsInStock { get; set; }

        public int DiscountRate { get; set; }

        public double Discount { get; set; }

        public double Payment { get; set; }
    }
}
