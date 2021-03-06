using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.Repository
{
    public class ProductRepository:RepositoryBase<long,Product>,IProductRepository
    {
        private readonly ShopContext _db;

        public ProductRepository(ShopContext db) : base(db)
        {
            _db = db;
        }

        public Product GetProductWithCategory(long id)
        {
            return _db.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _db.Products.Include(p=>p.Category).Select(p=>new ProductViewModel()
            {
                Id = p.Id,
                Category = p.Category.Name,
                CategoryId = p.CategoryId,
                Picture = p.Picture,
                Code = p.Code,
                CreationDate = p.CreationDate.ToPersianDate(),
                Name = p.Name
            });

            if (searchModel.CategoryId!=0)
            {
                query = query.Where(p => p.CategoryId==searchModel.CategoryId);
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(p => p.Name.Contains(searchModel.Name));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
            {
                query = query.Where(p => p.Name.Contains(searchModel.Code));
            }


            return query.OrderByDescending(p => p.CategoryId).ThenBy(p=>p.Id).AsNoTracking().ToList();
        }

        public List<SelectProductViewModel> GetProducts()
        {
            return _db.Products.Select(p => new SelectProductViewModel()
            {
                Name = p.Name,
                Id = p.Id
            }).AsNoTracking().ToList();
        }

        public EditProduct GetDetails(long id)
        {
            return _db.Products.Select(p => new EditProduct()
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                //Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                MetaDescription = p.MetaDescription,
                Keywords = p.Keywords,
                Slug = p.Slug,
                ShortDescription = p.ShortDescription,
                Description = p.Description
            }).AsNoTracking().FirstOrDefault(p => p.Id == id);
        }
    }
}
