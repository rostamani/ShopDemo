using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _0_Framework.Application;
using _01_ShopQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure;
using InventoryManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure;


namespace _01_ShopQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _db;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductCategoryQuery(ShopContext db, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _db = db;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
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

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var productsDiscounts = _discountContext.CustomerDiscounts
                .Where(d => d.StartDate < DateTime.Now && d.EndDate > DateTime.Now)
                .Select(d => new {ProductId = d.ProductId, DiscountRate = d.DiscountRate,EndDate=d.EndDate.ToFarsi()})
                .AsNoTracking().ToList();

            var productsInventory = _inventoryContext.Inventory.Select(i => new {Price = i.UnitPrice, ProductId = i.ProductId})
                .AsNoTracking().ToList();

            var result = _db.ProductCategories.Include(c => c.Products).ThenInclude(p => p.Category).Select(c => new ProductCategoryQueryModel()
            {
                Id = c.Id,
                Name = c.Name,
                Slug = c.Slug,
                Picture = c.Picture,
                PictureAlt = c.PictureAlt,
                PictureTitle = c.PictureTitle,
                Description = c.Description,
                Products = MapProducts(c.Products)
            }).ToList();

            foreach (var category in result)
            {
                foreach (var product in category.Products)
                {
                    var inventory = productsInventory.FirstOrDefault(i => i.ProductId == product.Id);
                    if (inventory!=null)
                    {
                        product.Price = inventory.Price.ToMoney();
                        var discount = productsDiscounts.FirstOrDefault(d => d.ProductId == product.Id);
                        if (discount != null)
                        {
                            product.DiscountRate = discount.DiscountRate;
                            double discountAmount = (inventory.Price*product.DiscountRate/100);
                            product.PriceWithDiscount = Math.Round(inventory.Price-discountAmount).ToMoney();
                            product.HasDiscount = product.DiscountRate > 0;
                            product.DiscountEndDate = discount.EndDate;
                        }
                    }
                }
            }

            return result.ToList();
        }

        private static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            var result = products.Select(p => new ProductQueryModel()
            {
                Id = p.Id,
                Name = p.Name,
                ProductCategory = p.Category.Name,
                Picture = p.Picture,
                PictureTitle = p.PictureTitle,
                PictureAlt = p.PictureAlt,
                Slug = p.Slug
            }).OrderByDescending(p=>p.Id).ToList();
            return result;
        }
    }
}
