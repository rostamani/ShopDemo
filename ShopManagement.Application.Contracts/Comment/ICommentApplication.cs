using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Comment
{
    public interface ICommentApplication
    {
        OperationResult AddComment(AddComment command);
        OperationResult Cancel(long commentId);
        OperationResult Confirm(long commentId);
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
