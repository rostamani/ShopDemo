using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.Comment
{
    public class CommentSearchModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public long ProductId { get; set; }
    }
}
