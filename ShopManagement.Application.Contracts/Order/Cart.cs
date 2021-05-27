using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.Order
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; }
        public double TotalPrice { get; set; }
        public double Discount { get; set; }
        public double Payment { get; set; }

        public Cart()
        {
            CartItems=new List<CartItem>();
        }

        public void Add(CartItem cartItem)
        {
            CartItems.Add(cartItem);
            TotalPrice = TotalPrice + double.Parse(cartItem.TotalPrice.Replace("٬", ""));
            Discount = Discount + cartItem.Discount;
            Payment = Payment + cartItem.Payment;
        }
    }
}
