using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;

        public SlideApplication(ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }

        public OperationResult Create(CreateSlide command)
        {
            var result = new OperationResult();
            if (_slideRepository.Exists(s => s.Picture == command.Picture))
            {
                return result.Failed(QueryValidationMessage.DuplicateRecord);
            }

            var slide = new Slide(command.Picture, command.PictureTitle, command.PictureAlt, command.Heading, command.Title,
                command.Text, command.BtnText,command.Link);
            _slideRepository.Create(slide);
            _slideRepository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Edit(EditSlide command)
        {
            var result = new OperationResult();
            var slide = _slideRepository.Get(command.Id);
            if (slide == null)
            {
                return result.Failed(QueryValidationMessage.NotFound);
            }
            if (_slideRepository.Exists(s => s.Picture == command.Picture && s.Id!=command.Id))
            {
                return result.Failed(QueryValidationMessage.DuplicateRecord);
            }
            slide.Edit(command.Picture, command.PictureTitle, command.PictureAlt, command.Heading, command.Title,
                command.Text, command.BtnText,command.Link);
            _slideRepository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var result = new OperationResult();
            var slide = _slideRepository.Get(id);
            if (slide == null)
            {
                return result.Failed(QueryValidationMessage.NotFound);
            }
            slide.Remove();
            _slideRepository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Restore(long id)
        {

            var result = new OperationResult();
            var slide = _slideRepository.Get(id);
            if (slide == null)
            {
                return result.Failed(QueryValidationMessage.NotFound);
            }
            slide.Restore();
            _slideRepository.SaveChanges();
            return result.Succeeded();
        }

        public EditSlide GetDetails(long id)
        {
            return _slideRepository.GetDetails(id);
        }

        public List<SlideViewModel> Search()
        {
            return _slideRepository.Search();
        }
    }
}
