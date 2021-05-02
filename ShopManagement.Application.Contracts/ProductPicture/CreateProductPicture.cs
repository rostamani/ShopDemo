using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public long ProductId { get; set; }
    }

    
}
