using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Infrastructure.Repository
{
    public class CommentRepository:RepositoryBase<long,Comment>,ICommentRepository
    {
        private readonly ShopContext _shopContext;

        public CommentRepository(ShopContext shopContext) : base(shopContext)
        {
            _shopContext = shopContext;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _shopContext.Comments.Include(c => c.Product)
                .Select(c => new CommentViewModel()
                {
                    Id=c.Id,
                    ProductId = c.ProductId,
                    Name = c.Name,
                    Email = c.Email,
                    Message = c.Message,
                    Product = c.Product.Name,
                    IsConfirmed = c.IsConfirmed,
                    IsCanceled = c.IsCanceled,
                    CreationDate = c.CreationDate.ToFarsi()
                }).AsNoTracking();

            if (searchModel.ProductId!=0)
            {
                query = query.Where(c => c.ProductId == searchModel.ProductId);
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(c => c.Name.Contains(searchModel.Name));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
            {
                query = query.Where(c => c.Name.Contains(searchModel.Email));
            }

            return query.OrderByDescending(c => c.Id).ToList();
        }
    }
}
