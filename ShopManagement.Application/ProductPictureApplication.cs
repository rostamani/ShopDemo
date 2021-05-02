using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication:IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var result = new OperationResult();
            if (_productPictureRepository.Exists(p=>p.Picture==command.Picture && p.ProductId==command.ProductId))
            {
                return result.Failed(QueryValidationMessage.DuplicateRecord);
            }

            var productPicture=new ProductPicture(command.Picture,command.PictureAlt,command.PictureTitle,command.ProductId);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return result.Succeeded();

        }

        public OperationResult Edit(EditProductPicture command)
        {
            var result = new OperationResult();
            var productPicture = _productPictureRepository.Get(command.Id);
            if (productPicture==null)
            {
                return result.Failed(QueryValidationMessage.NotFound);
            }

            if (_productPictureRepository.Exists(p => p.Picture == command.Picture && p.ProductId == command.ProductId && p.Id!=command.Id))
            {
                return result.Failed(QueryValidationMessage.DuplicateRecord);
            }
            productPicture.Edit(command.Picture,command.PictureAlt,command.PictureTitle,command.ProductId);
            _productPictureRepository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var result = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture!=null)
            {
                productPicture.Restore();
                _productPictureRepository.SaveChanges();
                return result.Succeeded();
            }

            return result.Failed(QueryValidationMessage.NotFound);
        }

        public OperationResult Remove(long id)
        {
            var result = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture != null)
            {
                productPicture.Remove();
                _productPictureRepository.SaveChanges();
                return result.Succeeded();
            }

            return result.Failed(QueryValidationMessage.NotFound);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }
    }
}
