using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Infrastructure;

namespace DiscountManagement.Infrastructure.Repository
{
    public class ColleagueDiscountRepository:RepositoryBase<long,ColleagueDiscount>,IColleagueDiscountRepository
    {
        private readonly DiscountContext _db;
        private readonly ShopContext _ShopContext;

        public ColleagueDiscountRepository(DiscountContext db,ShopContext shopContext) : base(db)
        {
            _db = db;
            _ShopContext = shopContext;
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var products = _ShopContext.Products.Select(p => new SelectProductViewModel {Id = p.Id, Name = p.Name})
                .AsNoTracking().ToList();
            var query = _db.ColleagueDiscounts.Select(cd => new ColleagueDiscountViewModel()
            {
                Id = cd.Id,
                ProductId = cd.ProductId,
                CreationDate = cd.CreationDate.ToFarsi(),
                IsRemoved = cd.IsRemoved,
                DiscountRate = cd.DiscountRate
            });

            if (searchModel.ProductId!=0)
            {
                query = query.Where(cd => cd.ProductId == searchModel.ProductId);
            }

            var result = query.ToList();
            result.ForEach(cd=>cd.Product=products.FirstOrDefault(p=>p.Id==cd.ProductId)?.Name);
            return result;
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _db.ColleagueDiscounts.Select(cd => new EditColleagueDiscount()
            {
                Id = cd.Id,
                DiscountRate = cd.DiscountRate,
                ProductId = cd.ProductId
            }).AsNoTracking().FirstOrDefault(cd => cd.Id == id);
        }
    }
}
