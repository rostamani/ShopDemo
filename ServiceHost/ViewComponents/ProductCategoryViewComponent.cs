using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_ShopQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryViewComponent:ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;

        public ProductCategoryViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }
        public IViewComponentResult Invoke()
        {
            return View(_productCategoryQuery.GetProductCategories());
        }
    }
}
