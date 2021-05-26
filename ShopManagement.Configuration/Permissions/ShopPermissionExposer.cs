using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Infrastructure;

namespace ShopManagement.Configuration.Permissions
{
    public class ShopPermissionExposer:IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Product",new List<PermissionDto>
                    {
                        new PermissionDto("ListProducts",ShopPermissions.ListProducts),
                        new PermissionDto("SearchProducts",ShopPermissions.SearchProducts),
                        new PermissionDto("EditProducts",ShopPermissions.EditProduct),
                        new PermissionDto("CreateProducts",ShopPermissions.CreateProduct),
                    }
                },
                {
                    "ProductCategory",new List<PermissionDto>
                    {
                        new PermissionDto("SearchProductCategories",ShopPermissions.SearchProductCategories),
                        new PermissionDto("ListProductCategories",ShopPermissions.ListProductCategories),
                        new PermissionDto("EditProductCategories",ShopPermissions.EditProductCategory),
                        new PermissionDto("CreateProductCategories",ShopPermissions.CreateProductCategory),
                    }
                },
                {
                    "ProductPicture",new List<PermissionDto>
                    {
                        new PermissionDto("SearchProductPictures",ShopPermissions.SearchProductPictures),
                        new PermissionDto("ListProductPictures",ShopPermissions.ListProductPictures),
                        new PermissionDto("CreateProductPictures",ShopPermissions.CreateProductPicture),
                        new PermissionDto("EditProductPictures",ShopPermissions.EditProductPicture),
                        new PermissionDto("RemoveProductPictures",ShopPermissions.RemoveProductPicture),
                        new PermissionDto("RestoreProductPictures",ShopPermissions.RestoreProductPicture),
                    }
                },
                {
                    "Slide",new List<PermissionDto>
                    {
                        new PermissionDto("ListSlides",ShopPermissions.ListSlides),
                        new PermissionDto("CreateSlide",ShopPermissions.CreateSlide),
                        new PermissionDto("EditSlide",ShopPermissions.EditSlide),
                        new PermissionDto("RemoveSlide",ShopPermissions.RemoveSlide),
                        new PermissionDto("RestoreSlide",ShopPermissions.RestoreSlide),
                    }
                },
                {
                    "Comment",new List<PermissionDto>
                    {
                        new PermissionDto("ListComments",ShopPermissions.ListComments),
                        new PermissionDto("ConfirmComment",ShopPermissions.ConfirmComment),
                        new PermissionDto("CancelComment",ShopPermissions.CancelComment),
                        new PermissionDto("SearchComment",ShopPermissions.SearchComments),
                    }
                },
            };
        }
    }
}
