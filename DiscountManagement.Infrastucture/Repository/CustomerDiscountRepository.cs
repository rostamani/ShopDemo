using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Infrastructure;

namespace DiscountManagement.Infrastructure.Repository
{
    public class CustomerDiscountRepository:RepositoryBase<long,CustomerDiscount>,ICustomerDiscountRepository
    {
        private readonly DiscountContext _db;
        private readonly ShopContext _shopContext;

        public CustomerDiscountRepository(DiscountContext db,ShopContext shopContext) : base(db)
        {
            _db = db;
            _shopContext = shopContext;
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(p => new SelectProductViewModel {Id = p.Id, Name = p.Name})
                .AsNoTracking().ToList();

            var query = _db.CustomerDiscounts.Select(cd => new CustomerDiscountViewModel()
            {
                Id = cd.Id,
                ProductId = cd.ProductId,
                GrStartDate = cd.StartDate,
                GrEndDate = cd.EndDate,
                FaStartDate = cd.StartDate.ToFarsi(),
                FaEndDate = cd.EndDate.ToFarsi(),
                Reason = cd.Reason,
                DiscountRate = cd.DiscountRate,
                IsRemoved = cd.IsRemoved,
                CreationDate = cd.CreationDate.ToFarsi()
            });

            if (searchModel.ProductId!=0)
            {
                query = query.Where(cd => cd.ProductId == searchModel.ProductId);
            }

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
            {
                query = query.Where(cd => cd.GrStartDate >= searchModel.StartDate.ToGeorgianDateTime());
            }

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
            {
                query = query.Where(cd => cd.GrEndDate <= searchModel.EndDate.ToGeorgianDateTime());
            }

            var result = query.AsNoTracking().ToList();
            result.ForEach(cd=>cd.Product=products.FirstOrDefault(p=>p.Id==cd.ProductId)?.Name);
            return result;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _db.CustomerDiscounts.Select(cd => new EditCustomerDiscount()
            {
                Id = cd.Id,
                ProductId = cd.ProductId,
                StartDate = cd.StartDate.ToFarsi(),
                EndDate = cd.EndDate.ToFarsi(),
                Reason = cd.Reason,
                DiscountRate = cd.DiscountRate

            }).AsNoTracking().FirstOrDefault(cd => cd.Id == id);
        }
    }
}
