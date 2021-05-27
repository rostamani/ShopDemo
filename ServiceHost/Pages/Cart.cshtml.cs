using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using _01_ShopQuery.Contracts.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems { get; set; }
        private const string CookieName = "cart-items";

        private readonly IProductQuery _productQuery;

        public CartModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public void OnGet()
        {
            CartItems=new List<CartItem>();
            var cookie = Request.Cookies[CookieName];
            if (cookie!=null)
            {
                CartItems = JsonConvert.DeserializeObject<List<CartItem>>(cookie);
                CartItems.ForEach(x =>
                {
                    var stringPrice = x.Price.Replace("٬", "");
                    var price = double.Parse(stringPrice);
                    x.TotalPrice = (price * x.Count).ToMoney();
                });

                _productQuery.CheckCartItemStatus(CartItems);
            }
            
        }

        public IActionResult OnGetRemoveCartItem(long id)
        {
            var cookie = Request.Cookies[CookieName];
            CartItems = JsonConvert.DeserializeObject<List<CartItem>>(cookie);

            var cartItemToBeRemoved = CartItems.FirstOrDefault(x => x.Id == id);
            if (cartItemToBeRemoved!=null)
            {
                CartItems.Remove(cartItemToBeRemoved);
                Response.Cookies.Delete(CookieName);
                var cookieOptions = new CookieOptions() { Expires = DateTime.Now.AddDays(1) };
                Response.Cookies.Append(CookieName,JsonConvert.SerializeObject(CartItems),cookieOptions);
            }

            return RedirectToPage("Cart");

        }

        public IActionResult OnGetGoToCheckout()
        {
            var cookie = Request.Cookies[CookieName];
            if (cookie!=null)
            {
                CartItems = JsonConvert.DeserializeObject<List<CartItem>>(cookie);
                _productQuery.CheckCartItemStatus(CartItems);
                if (!CartItems.Any(x=>x.IsInStock==false))
                {
                    return RedirectToPage("Checkout");
                }
            }
            CartItems.ForEach(x =>
            {
                var stringPrice = x.Price.Replace("٬", "");
                var price = double.Parse(stringPrice);
                x.TotalPrice = (price * x.Count).ToMoney();
            });
            return RedirectToPage("Cart");
        }
    }
}
