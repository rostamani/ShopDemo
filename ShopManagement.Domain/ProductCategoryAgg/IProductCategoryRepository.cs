using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Domain;
using Microsoft.Extensions.Logging.Abstractions;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository:IRepository<long,ProductCategory>
    {
        
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);

        EditProductCategory GetDetails(long id);

        List<SelectProductCategory> GetCategories();

        string GetSlugBy(long id);

    }
}
