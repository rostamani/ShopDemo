using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductAgg
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);
        OperationResult InStuck(long id);
        OperationResult NotInStuck(long id);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        EditProduct GetDetails(long id);
    }
}
