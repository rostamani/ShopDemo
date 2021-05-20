using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
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
        public string Slug { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Keywords { get; set; }

        public string Description { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string ShortDescription { get;  set; }

        [MaxFileSize(3*1024*1024,ErrorMessage = ValidationMessage.MaxFileSizeError)]
        [FileExtensionLimit(new string[] {".jpg",".jpeg",".png"},ErrorMessage = ValidationMessage.InvalidFileFormat)]
        public IFormFile Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public List<SelectProductCategory> Categories { get; set; }

        [Range(1,100000,ErrorMessage = ValidationMessage.IsRequired)]
        public long CategoryId { get; set; }
    }
}
