using System;
using System.Collections.Generic;
using System.Text;
using _01_ShopQuery.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.Order;

namespace _01_ShopQuery.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestArrivals();
        ProductQueryModel GetDetails(string slug);
        List<ProductQueryModel> Search(string value);
        List<CartItem> CheckCartItemStatus(List<CartItem> cartItems);
    }
}
