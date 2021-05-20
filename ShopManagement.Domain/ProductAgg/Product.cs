using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Domain;
using ShopManagement.Domain.CommentAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Domain.ProductAgg
{
    public class Product:EntityBase
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string Slug { get; set; }
        public string MetaDescription { get;private set; }
        public string Keywords { get;private set; }
        public string Description { get; private set; }
        public string ShortDescription { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }

        public long CategoryId { get; private set; }
        public ProductCategory Category { get; private set; }
        public List<Comment> Comments { get; private set; }

        public List<ProductPicture> ProductPictures { get; private set; }

        public Product()
        {
            ProductPictures=new List<ProductPicture>();
        }

        public Product(string name, string code, string slug, string metaDescription, string keywords, string description, string shortDescription, string picture, string pictureAlt, string pictureTitle, long categoryId)
        {
            Name = name;
            Code = code;
            Slug = slug;
            MetaDescription = metaDescription;
            Keywords = keywords;
            Description = description;
            ShortDescription = shortDescription;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            CategoryId = categoryId;
        }

        public void Edit(string name, string code, string slug, string metaDescription, string keywords, string description, string shortDescription, string picture, string pictureAlt, string pictureTitle, long categoryId)
        {
            Name = name;
            Code = code;
            Slug = slug;
            MetaDescription = metaDescription;
            Keywords = keywords;
            Description = description;
            ShortDescription = shortDescription;
            if (!string.IsNullOrWhiteSpace(picture))
            {
                Picture = picture;
            }
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            CategoryId = categoryId;
        }



    }
}
