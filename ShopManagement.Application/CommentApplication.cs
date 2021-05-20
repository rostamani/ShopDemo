using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Application
{
    public class CommentApplication:ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public OperationResult AddComment(AddComment command)
        {
            var operationResult = new OperationResult();
            var comment = new Comment(command.ProductId,command.Name,command.Email,command.Message);

            _commentRepository.Create(comment);
            _commentRepository.SaveChanges();

            return operationResult.Succeeded();
        }

        public OperationResult Cancel(long commentId)
        {
            var operationResult = new OperationResult();
            var comment = _commentRepository.Get(commentId);
            if (comment==null)
            {
                return operationResult.Failed(QueryValidationMessage.NotFound);
            }

            comment.Cancel();
            _commentRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Confirm(long commentId)
        {
            var operationResult = new OperationResult();
            var comment = _commentRepository.Get(commentId);
            if (comment == null)
            {
                return operationResult.Failed(QueryValidationMessage.NotFound);
            }

            comment.Confirm();
            _commentRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _commentRepository.Search(searchModel);
        }
    }
}
