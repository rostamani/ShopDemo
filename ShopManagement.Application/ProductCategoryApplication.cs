using System;
using System.Collections.Generic;
using _0_Framework.Application;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }
        

        public OperationResult Create(CreateProductCategory command)
        {
            var operationResult=new OperationResult();
            if (_productCategoryRepository.Exists(p=>p.Name==command.Name))
            {
                return operationResult.Failed("امکان ثبت رکورد تکراری وجود ندارد.");
            }

            var filename = _fileUploader.FileUpload(command.Picture, command.Slug);
            var produceCategory=new ProductCategory(command.Name,command.Description,filename,command.PictureAlt,
                command.PictureTitle,command.Keywords,command.MetaDescription,command.Slug.Slugify());
            _productCategoryRepository.Create(produceCategory);
            _productCategoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operationResult=new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);
            if (productCategory==null)
            {
                return operationResult.Failed("چنین رکوردی در دیتابیس موجود نیست.");
            }

            if (_productCategoryRepository.Exists(p=>p.Name==command.Name && p.Id!=command.Id))
            {
                return operationResult.Failed("امکان ثبت رکورد تکراری وجود ندارد.");
            }

            var filename = _fileUploader.FileUpload(command.Picture, command.Slug);
            productCategory.Edit(command.Name, command.Description, filename, command.PictureAlt,
                command.PictureTitle, command.Keywords, command.MetaDescription, command.Slug.Slugify());

            _productCategoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<SelectProductCategory> GetCategories()
        {
            return _productCategoryRepository.GetCategories();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }
    }
}
