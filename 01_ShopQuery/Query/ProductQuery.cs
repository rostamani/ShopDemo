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
using ShopManagement.Domain.CommentAgg;
using ShopManagement.Domain.ProductPictureAgg;
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
                ProductCategory = p.Category.Name,
                CategorySlug = p.Category.Slug
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
                        product.DiscountEndDate = discount.EndDate.ToDiscountFormat();
                    }
                }
            }

            return result;
        }

        public ProductQueryModel GetDetails(string slug)
        {
            var product = _shopContext.Products.Include(p=>p.Category)
                .Include(p=>p.ProductPictures)
                .Include(p=>p.Comments)
                .Select(p => new ProductQueryModel()
            {
                Id=p.Id,
                Name = p.Name,
                ProductCategory = p.Category.Name,
                Picture = p.Picture,
                PictureTitle = p.PictureTitle,
                PictureAlt = p.PictureAlt,
                MetaDescription = p.MetaDescription,
                Description = p.Description,
                ShortDescription = p.ShortDescription,
                Keywords = p.Keywords,
                CategorySlug = p.Category.Slug,
                Slug = p.Slug,
                ProductPictures = MapProductPictures(p.ProductPictures),
                ProductComments = MapProductComments(p.Comments)

            }).AsNoTracking().FirstOrDefault(p => p.Slug == slug);


            if (product!=null)
            {
                var inventory = _inventoryContext.Inventory
                    .Select(i => new { productId = i.ProductId, Price = i.UnitPrice,IsInStock=i.IsInStock })
                    .AsNoTracking()
                    .FirstOrDefault(d => d.productId == product.Id);

                if (inventory != null)
                {
                    
                    product.IsInStock = inventory.IsInStock;
                    product.Price = inventory.Price.ToMoney();

                    var discount = _discountContext.CustomerDiscounts
                        .Where(d => d.StartDate < DateTime.Now && d.EndDate > DateTime.Now)
                        .Select(d => new { ProductId = d.ProductId, DiscountRate = d.DiscountRate, EndDate = d.EndDate })
                        .AsNoTracking()
                        .FirstOrDefault(d => d.ProductId == product.Id);
                    

                    if (discount!=null)
                    {
                        product.DiscountRate = discount.DiscountRate;
                        product.PriceWithDiscount =
                            Math.Round(inventory.Price - (inventory.Price * product.DiscountRate / 100)).ToMoney();
                        product.HasDiscount = product.DiscountRate > 0;
                        product.DiscountEndDate = discount.EndDate.ToFarsi();
                    }
                }

                return product;
            }


            return new ProductQueryModel();

            


           
        }

        private static List<ProductPictureQueryModel> MapProductPictures(List<ProductPicture> productPictures)
        {
            return productPictures.Select(x => new ProductPictureQueryModel()
            {
                IsRemoved = x.IsRemoved,
                ProductId = x.ProductId,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            }).Where(x => x.IsRemoved == false).ToList();
        }

        private static List<CommentQueryModel> MapProductComments(List<Comment> productComments)
        {
            var result = productComments.Where(c => c.IsConfirmed && c.IsCanceled == false)
                .Select(c => new CommentQueryModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Message = c.Message,
                    CreationDate = c.CreationDate.ToFarsi()
                }).OrderByDescending(c=>c.Id).ToList();

            return result;
        }
    }
}
