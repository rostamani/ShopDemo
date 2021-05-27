using System;
using System.Collections.Generic;
using System.Text;
using ShopManagement.Application.Contracts.Order;

namespace _01_ShopQuery.Contracts.Cart
{
    public interface ICartQuery
    {
        ShopManagement.Application.Contracts.Order.Cart ComputeCart(List<CartItem> cartItems);
    }
}
