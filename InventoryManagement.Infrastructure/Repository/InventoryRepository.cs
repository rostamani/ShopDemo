using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contracts;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure;

namespace InventoryManagement.Infrastructure.Repository
{
    public class InventoryRepository:RepositoryBase<long,Inventory>,IInventoryRepository
    {
        private readonly InventoryContext _db;
        private readonly ShopContext _shopContext;

        public InventoryRepository(InventoryContext db, ShopContext shopContext) : base(db)
        {
            _db = db;
            _shopContext = shopContext;
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(p => new {Id = p.Id,Name=p.Name}).AsNoTracking().ToList();
            var query = _db.Inventory.Select(i=>new InventoryViewModel()
            {
                Id = i.Id,
                ProductId = i.ProductId,
                CurrentCount = i.CalculateCurrentCount(),
                IsInStock = i.IsInStock,
                UnitPrice = i.UnitPrice
            });

            if (searchModel.ProductId!=0)
            {
                query = query.Where(i => i.ProductId == searchModel.ProductId);
            }

            if (searchModel.IsNotInStock)
            {
                query = query.Where(i => i.IsInStock == false);
            }

            var result = query.AsNoTracking().ToList();
            result.ForEach(i=>i.Product=products.FirstOrDefault(p=>p.Id==i.ProductId)?.Name);
            return result;
        }

        public EditInventory GetDetails(long id)
        {
            return _db.Inventory.Select(i => new EditInventory()
            {
                Id = i.Id,
                ProductId = i.ProductId,
                UnitPrice = i.UnitPrice,
            }).AsNoTracking().FirstOrDefault(i => i.Id == id);
        }

        public Inventory GetBy(long productId)
        {
            return _db.Inventory.FirstOrDefault(i => i.ProductId == productId);
        }

        public List<OperationLogViewModel> GetOperations(long id)
        {
            var inventory = _db.Inventory.AsNoTracking().FirstOrDefault(i => i.Id == id);
            
            return inventory.Operations.Select(o => new OperationLogViewModel()
            {
                Id = o.Id,
                OperationDate = o.OperationDate.ToFarsi(),
                OperatorId = o.OperatorId,
                Count = o.Count,
                CurrentCount = o.CurrentCount,
                IsIncrement = o.IsIncrement,
                OrderId = o.OrderId,
                Description = o.Description,
                Operator = "مدیر سیستم"
            }).OrderByDescending(o=>o.Id).ToList();
        }
    }
}
