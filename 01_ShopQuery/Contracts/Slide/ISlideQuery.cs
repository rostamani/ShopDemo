using System;
using System.Collections.Generic;
using System.Text;

namespace _01_ShopQuery.Contracts.Slide
{
    public interface ISlideQuery
    {
        List<SlideQueryViewModel> GetSlides();
    }
}
