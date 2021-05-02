using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.Repository
{
    public class ProductPictureRepository:RepositoryBase<long,ProductPicture>,IProductPictureRepository
    {
        private readonly ShopContext _db;

        public ProductPictureRepository(ShopContext db) : base(db)
        {
            _db = db;
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query= _db.ProductPictures.Include(p=>p.Product).Select(p => new ProductPictureViewModel()
            {
                Id = p.Id,
                Picture = p.Picture,
                Product = p.Product.Name,
                CreationDate = p.CreationDate.ToPersianDate(),
                IsRemoved = p.IsRemoved,
                ProductId = p.ProductId
            });

            if (searchModel.ProductId!=0)
            {
                query = query.Where(p => p.ProductId==searchModel.ProductId);
            }

            return query.OrderByDescending(p => p.Id).AsNoTracking().ToList();
        }

        public EditProductPicture GetDetails(long id)
        {
            return _db.ProductPictures.Select(p => new EditProductPicture()
            {
                Id = p.Id,
                ProductId = p.ProductId,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle=p.PictureTitle
            }).FirstOrDefault(p => p.Id == id);
        }
    }
}
