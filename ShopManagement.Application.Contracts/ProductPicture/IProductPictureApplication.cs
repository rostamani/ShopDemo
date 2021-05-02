using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public interface IProductPictureApplication
    {
        OperationResult Create(CreateProductPicture command);

        OperationResult Edit(EditProductPicture command);

        OperationResult Restore(long id);

        OperationResult Remove(long id);

        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);

        EditProductPicture GetDetails(long id);
    }
}
