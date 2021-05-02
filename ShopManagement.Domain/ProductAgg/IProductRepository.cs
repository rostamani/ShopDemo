using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductAgg;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository:IRepository<long,Product>
    {
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        List<SelectProductViewModel> GetProducts();
        EditProduct GetDetails(long id);
    }
}
