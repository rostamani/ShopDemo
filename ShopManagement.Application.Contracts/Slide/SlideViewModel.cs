using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.Slide
{
    public class SlideViewModel
    {
        public long Id { get; set; }

        public string Picture { get; set; }

        public string Heading { get; set; }

        public string Title { get; set; }
    }
}
