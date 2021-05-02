using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductAgg;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Picture { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureTitle { get; set; }

        [Range(1,10000,ErrorMessage = ValidationMessage.IsRequired)]
        public long ProductId { get; set; }
        public List<SelectProductViewModel> Products { get; set; }
    }

    
}
