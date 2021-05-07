using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_ShopQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryWithProductsViewComponent:ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;

        public ProductCategoryWithProductsViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var productCategoriesWithProducts = _productCategoryQuery.GetProductCategoriesWithProducts();
            return View(productCategoriesWithProducts);
        }
    }
}
