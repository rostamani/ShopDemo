using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using _01_ShopQuery.Contracts.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly ICartQuery _cartQuery;

        public CheckoutModel(ICartQuery cartQuery)
        {
            _cartQuery = cartQuery;
        }

        public Cart Cart { get; set; }

        public void OnGet()
        {
            Cart=new Cart();
            var cookie = Request.Cookies["cart-items"];
            if (cookie!=null)
            {
                var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(cookie);
                cartItems.ForEach(x =>
                {
                    var stringPrice = x.Price.Replace("٬", "");
                    var price = double.Parse(stringPrice);
                    x.TotalPrice = (price * x.Count).ToMoney();
                });

                Cart = _cartQuery.ComputeCart(cartItems);
            }
        }
    }
}
