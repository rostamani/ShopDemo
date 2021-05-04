﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.ProductAgg
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Picture { get; set; }
        public long CategoryId { get; set; }
        public string Category { get; set; }
        public string CreationDate { get; set; }
    }
}
