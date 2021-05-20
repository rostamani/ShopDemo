﻿using System;
using System.Collections.Generic;
using System.Text;

namespace _01_ShopQuery.Contracts.Product
{
    public class ProductPictureQueryModel
    {
        public long ProductId { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public bool IsRemoved { get; set; }
    }
}
