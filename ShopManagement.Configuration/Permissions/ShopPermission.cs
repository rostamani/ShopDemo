using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Configuration.Permissions
{
    public static class ShopPermissions
    {
        //Product
        public const int ListProducts = 10;
        public const int SearchProducts = 11;
        public const int CreateProduct = 12;
        public const int EditProduct = 13;


        //ProductCategory
        public const int ListProductCategories = 20;
        public const int SearchProductCategories = 21;
        public const int CreateProductCategory = 22;
        public const int EditProductCategory = 23;


        //Comment

        public const int ListComments = 30;
        public const int SearchComments = 31;
        public const int ConfirmComment = 32;
        public const int CancelComment = 33;


        //Slide
        public const int ListSlides = 40;
        public const int RemoveSlide = 41;
        public const int RestoreSlide = 42;
        public const int CreateSlide = 43;
        public const int EditSlide = 44;

        //ProductPicture

        public const int ListProductPictures = 50;
        public const int SearchProductPictures = 50;
        public const int CreateProductPicture = 40;
        public const int EditProductPicture = 40;
        public const int RemoveProductPicture = 40;
        public const int RestoreProductPicture = 40;
    }
}
