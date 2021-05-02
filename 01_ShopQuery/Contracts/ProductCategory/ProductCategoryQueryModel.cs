﻿using System;
using System.Collections.Generic;
using System.Text;

namespace _01_ShopQuery.Contracts.ProductCategory
{
    public class ProductCategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public string Slug { get;  set; }
    }
}