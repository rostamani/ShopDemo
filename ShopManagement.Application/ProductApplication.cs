using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductApplication:IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ProductApplication(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operationResult=new OperationResult();
            if (_productRepository.Exists(p=>p.Name==command.Name))
            {
                return operationResult.Failed(QueryValidationMessage.DuplicateRecord);
            }

            var productCategorySlug = _productCategoryRepository.GetSlugBy(command.CategoryId);
            var productSlug = GenerateSlug.Slugify(command.Slug);
            var picturePath = $"{productCategorySlug}/{productSlug}";

            var pictureName = _fileUploader.FileUpload(command.Picture, picturePath);

            var product=new Product(command.Name,command.Code,productSlug,
                command.MetaDescription,command.Keywords,command.Description,command.ShortDescription,pictureName,
                command.PictureAlt,command.PictureTitle,command.CategoryId);

            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operationResult = new OperationResult();
            var product = _productRepository.GetProductWithCategory(command.Id);
            if (product==null)
            {
                return operationResult.Failed(QueryValidationMessage.NotFound);
            }

            if (_productRepository.Exists(p=>p.Name==command.Name&& p.Id!=command.Id))
            {
                return operationResult.Failed(QueryValidationMessage.DuplicateRecord);
            }

            var productCategorySlug = product.Category.Slug;
            var productSlug = GenerateSlug.Slugify(command.Slug);
            var picturePath = $"{productCategorySlug}/{productSlug}";
            var pictureName = _fileUploader.FileUpload(command.Picture, picturePath);

            product.Edit(command.Name, command.Code, GenerateSlug.Slugify(command.Slug),
                command.MetaDescription, command.Keywords, command.Description, command.ShortDescription, pictureName,
                command.PictureAlt, command.PictureTitle, command.CategoryId);
            _productRepository.SaveChanges();
            return operationResult.Succeeded();
        }


        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }

        public List<SelectProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }
    }
}
