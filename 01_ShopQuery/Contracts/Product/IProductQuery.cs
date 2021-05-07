using System;
using System.Collections.Generic;
using System.Text;
using _01_ShopQuery.Contracts.ProductCategory;

namespace _01_ShopQuery.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestArrivals();
    }
}
