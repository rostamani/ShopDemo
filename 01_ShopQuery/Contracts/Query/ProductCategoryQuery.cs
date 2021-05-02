using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _01_ShopQuery.Contracts.ProductCategory;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure;


namespace _01_ShopQuery.Contracts.Query
{
    public class ProductCategoryQuery:IProductCategoryQuery
    {
        private readonly ShopContext _db;

        public ProductCategoryQuery(ShopContext db)
        {
            _db = db;
        }
        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _db.ProductCategories.Select(c => new ProductCategoryQueryModel()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Slug = c.Slug,
                Picture = c.Picture,
                PictureAlt = c.PictureAlt,
                PictureTitle = c.PictureTitle
            }).AsNoTracking().ToList();
        }
    }
}
