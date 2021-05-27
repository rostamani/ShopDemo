using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using _01_ShopQuery.Contracts.Cart;
using DiscountManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Order;

namespace _01_ShopQuery.Query
{
    public class CartQuery:ICartQuery
    {
        private readonly DiscountContext _discountContext;
        private readonly IAuthHelper _authHelper;

        public CartQuery(DiscountContext discountContext, IAuthHelper authHelper)
        {
            _discountContext = discountContext;
            _authHelper = authHelper;
        }
        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var result=new Cart();
            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => x.IsRemoved == false && x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {ProductId = x.ProductId, DiscountRate = x.DiscountRate})
                .AsNoTracking().ToList();

            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                .Where(x => x.IsRemoved == false)
                .Select(x => new {DiscountRate = x.DiscountRate, ProductId = x.ProductId})
                .AsNoTracking().ToList();

            var currentUserRole = _authHelper.GetCurrentUserRole();

            if (currentUserRole==Roles.Colleague)
            {
                foreach (var cartItem in cartItems)
                {
                    var discount = colleagueDiscounts.FirstOrDefault(x => x.ProductId == cartItem.Id);
                    if (discount!=null)
                    {
                        cartItem.DiscountRate = discount.DiscountRate;
                    }

                    cartItem.Discount = double.Parse(cartItem.Price.Replace("٬", "")) * cartItem.DiscountRate / 100;
                    cartItem.Payment = double.Parse(cartItem.TotalPrice.Replace("٬", "")) - cartItem.Discount;
                    result.Add(cartItem);
                }
            }
            else
            {
                foreach (var cartItem in cartItems)
                {
                    var discount = customerDiscounts.FirstOrDefault(x => x.ProductId == cartItem.Id);
                    if (discount != null)
                    {
                        cartItem.DiscountRate = discount.DiscountRate;
                    }
                    cartItem.Discount = double.Parse(cartItem.Price.Replace("٬", "")) * cartItem.DiscountRate / 100;
                    cartItem.Payment = double.Parse(cartItem.TotalPrice.Replace("٬", "")) - cartItem.Discount;

                    result.Add(cartItem);
                }
            }

            return result;

        }
    }
}
