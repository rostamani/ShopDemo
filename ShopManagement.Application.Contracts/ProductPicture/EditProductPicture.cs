using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class EditProductPicture : CreateProductPicture
    {
        public long Id { get; set; }
    }
}
