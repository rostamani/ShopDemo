using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication:IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IFileUploader fileUploader, IProductRepository productRepository)
        {
            _productPictureRepository = productPictureRepository;
            _fileUploader = fileUploader;
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var result = new OperationResult();
            var product = _productRepository.GetProductWithCategory(command.ProductId);
            var productCategorySlug = product.Category.Slug;
            var productSlug = product.Slug;
            var picturePath = $"{productCategorySlug}/{productSlug}";
            var pictureName = _fileUploader.FileUpload(command.Picture, picturePath);
            var productPicture=new ProductPicture(pictureName,command.PictureAlt,command.PictureTitle,command.ProductId);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return result.Succeeded();

        }

        public OperationResult Edit(EditProductPicture command)
        {
            var result = new OperationResult();
            var productPicture = _productPictureRepository.GetProductPictureWithProduct(command.Id);
            if (productPicture==null)
            {
                return result.Failed(QueryValidationMessage.NotFound);
            }

            var productCategorySlug = productPicture.Product.Category.Slug;
            var productSlug = productPicture.Product.Slug;
            var picturePath = $"{productCategorySlug}/{productSlug}";
            var pictureName = _fileUploader.FileUpload(command.Picture, picturePath);

            productPicture.Edit(pictureName,command.PictureAlt,command.PictureTitle,command.ProductId);
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
