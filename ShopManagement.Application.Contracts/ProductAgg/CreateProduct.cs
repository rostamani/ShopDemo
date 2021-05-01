using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Application.Contracts.ProductAgg
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Code { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public double UnitPrice { get;  set; }
        public bool IsInStuck { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Slug { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Keywords { get; set; }

        public string Description { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string ShortDescription { get;  set; }
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public List<SelectProductCategory> Categories { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public long CategoryId { get; set; }
    }
}
