using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _01_ShopQuery.Contracts.Slide;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure;

namespace _01_ShopQuery.Contracts.Query
{
    public class SlideQuery:ISlideQuery
    {
        private readonly ShopContext _db;

        public SlideQuery(ShopContext db)
        {
            _db = db;
        }
        public List<SlideQueryViewModel> GetSlides()
        {
            return _db.Slides.Select(s => new SlideQueryViewModel()
            {
                Link = s.Link,
                Heading = s.Heading,
                Title = s.Title,
                Text = s.Text,
                BtnText = s.BtnText,
                Picture = s.Picture,
                PictureAlt = s.PictureAlt,
                PictureTitle = s.PictureTitle
            }).AsNoTracking().ToList();
        }
    }
}
