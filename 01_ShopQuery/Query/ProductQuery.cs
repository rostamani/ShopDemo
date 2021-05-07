using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _0_Framework.Application;
using _01_ShopQuery.Contracts.Product;
using _01_ShopQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure;
using InventoryManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure;

namespace _01_ShopQuery.Query
{
    public class ProductQuery:IProductQuery
    {
        private readonly InventoryContext _inventoryContext;
        private readonly ShopContext _shopContext;
        private readonly DiscountContext _discountContext;

        public ProductQuery(InventoryContext inventoryContext, ShopContext shopContext, DiscountContext discountContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
            _discountContext = discountContext;
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var discounts = _discountContext.CustomerDiscounts
                .Where(d => d.StartDate < DateTime.Now && d.EndDate > DateTime.Now)
                .Select(d => new {ProductId = d.ProductId, DiscountRate = d.DiscountRate, EndDate = d.EndDate})
                .AsNoTracking()
                .ToList();

            var inventory = _inventoryContext.Inventory
                .Select(i => new {productId = i.ProductId, Price = i.UnitPrice})
                .AsNoTracking()
                .ToList();

            var result=_shopContext.Products.Include(p=>p.Category).Select(p=> new ProductQueryModel()
            {
                Id=p.Id,
                Name = p.Name,
                Slug = p.Slug,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                ProductCategory = p.Category.Name
            }).OrderByDescending(p=>p.Id).Take(6).ToList();


            foreach (var product in result)
            {
                var productInventory = inventory.FirstOrDefault(i => i.productId == product.Id);
                if (productInventory!=null)
                {
                    product.Price = productInventory.Price.ToMoney();
                    var discount = discounts.FirstOrDefault(d => d.ProductId == product.Id);
                    if (discount!=null)
                    {
                        product.DiscountRate = discount.DiscountRate;
                        product.PriceWithDiscount =
                            Math.Round(productInventory.Price-(productInventory.Price * product.DiscountRate / 100)).ToMoney();
                        product.HasDiscount = product.DiscountRate > 0;
                        product.DiscountEndDate = discount.EndDate.ToFarsi();
                    }
                }
            }

            return result;
        }
    }
}
