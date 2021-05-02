using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly ShopContext _db;

        public SlideRepository(ShopContext db) : base(db)
        {
            _db = db;
        }

        public List<SlideViewModel> Search()
        {
            return _db.Slides.Select(p => new SlideViewModel()
            {
                Id = p.Id,
                Heading = p.Heading,
                Picture = p.Picture,
                Title = p.Title,
                IsRemoved = p.IsRemoved,
                Link = p.Link
            }).AsNoTracking().ToList();
        }

        public EditSlide GetDetails(long id)
        {
            return _db.Slides.Select(s => new EditSlide()
            {
                Id = s.Id,
                Heading = s.Heading,
                Text = s.Text,
                BtnText = s.BtnText,
                Picture = s.Picture,
                PictureAlt = s.PictureAlt,
                PictureTitle = s.PictureTitle,
                Link = s.Link
            }).AsNoTracking().FirstOrDefault(s => s.Id == id);
        }
    }
}
