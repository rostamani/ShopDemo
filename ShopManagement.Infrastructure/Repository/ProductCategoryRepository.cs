﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.Repository
{
    public class ProductCategoryRepository:RepositoryBase<long,ProductCategory>,IProductCategoryRepository
    {
        private readonly ShopContext _db;

        public ProductCategoryRepository(ShopContext db) : base(db)
        {
            _db = db;
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query = _db.ProductCategories.Select(p => new ProductCategoryViewModel
            {
                Name = p.Name,
                Id = p.Id,
                CreationDate = p.CreationDate.ToPersianDate()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(c => c.Name.Contains(searchModel.Name));
            }

            return query.OrderByDescending(p => p.Id).ToList();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _db.ProductCategories.Select(p => new EditProductCategory()
            {
                Name = p.Name,
                Id = p.Id,
                MetaDescription = p.MetaDescription,
                Description = p.Description,
                Keywords = p.Keywords,
                Slug = p.Slug,
                Picture = p.Picture,
                PictureTitle = p.PictureTitle,
                PictureAlt = p.PictureAlt,
            }).FirstOrDefault(p => p.Id == id);
        }

    }
}