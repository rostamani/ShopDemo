﻿using System;
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

        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public OperationResult Create(CreateProduct command)
        {
            var operationResult=new OperationResult();
            if (_productRepository.Exists(p=>p.Name==command.Name))
            {
                return operationResult.Failed(QueryValidationMessage.DuplicateRecord);
            }

            var product=new Product(command.Name,command.Code,GenerateSlug.Slugify(command.Slug),
                command.MetaDescription,command.Keywords,command.Description,command.ShortDescription,command.Picture,
                command.PictureAlt,command.PictureTitle,command.CategoryId);

            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operationResult = new OperationResult();
            var product = _productRepository.Get(command.Id);
            if (product==null)
            {
                return operationResult.Failed(QueryValidationMessage.NotFound);
            }

            if (_productRepository.Exists(p=>p.Name==command.Name&& p.Id!=command.Id))
            {
                return operationResult.Failed(QueryValidationMessage.DuplicateRecord);
            }
            
            product.Edit(command.Name, command.Code, GenerateSlug.Slugify(command.Slug),
                command.MetaDescription, command.Keywords, command.Description, command.ShortDescription, command.Picture,
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
