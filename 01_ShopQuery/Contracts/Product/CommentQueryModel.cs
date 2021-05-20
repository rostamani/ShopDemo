using System;
using System.Collections.Generic;
using System.Text;

namespace _01_ShopQuery.Contracts.Product
{
    public class CommentQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CreationDate { get; set; }
        public string Message { get; set; }
    }
}
