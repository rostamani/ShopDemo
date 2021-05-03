using System;
using System.Collections.Generic;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operationResult = new OperationResult();
            if (_customerDiscountRepository.Exists(cd => cd.ProductId == command.ProductId &&
                                                       cd.Reason == command.Reason && cd.DiscountRate == command.DiscountRate))
            {
                return operationResult.Failed(QueryValidationMessage.DuplicateRecord);
            }

            var customerDiscount = new CustomerDiscount(command.ProductId, command.DiscountRate, command.Reason,
                command.StartDate.ToGeorgianDateTime(), command.EndDate.ToGeorgianDateTime());
            _customerDiscountRepository.Create(customerDiscount);
            _customerDiscountRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operationResult = new OperationResult();
            var customerDiscount = _customerDiscountRepository.Get(command.Id);
            if (customerDiscount == null)
            {
                return operationResult.Failed(QueryValidationMessage.NotFound);
            }
            if (_customerDiscountRepository.Exists(cd => cd.ProductId == command.ProductId &&
                                                         cd.Reason == command.Reason && cd.DiscountRate == command.DiscountRate &&
                                                         cd.Id != command.Id))
            {
                return operationResult.Failed(QueryValidationMessage.DuplicateRecord);
            }

            customerDiscount.Edit(command.ProductId, command.DiscountRate, command.Reason,
                 command.StartDate.ToGeorgianDateTime(), command.EndDate.ToGeorgianDateTime());
            _customerDiscountRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var result = new OperationResult();
            var customerDiscount = _customerDiscountRepository.Get(id);
            if (customerDiscount == null)
            {
                return result.Failed(QueryValidationMessage.NotFound);
            }
            customerDiscount.Remove();
            _customerDiscountRepository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var result = new OperationResult();
            var customerDiscount = _customerDiscountRepository.Get(id);
            if (customerDiscount == null)
            {
                return result.Failed(QueryValidationMessage.NotFound);
            }
            customerDiscount.Restore();
            _customerDiscountRepository.SaveChanges();
            return result.Succeeded();
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.Search(searchModel);
        }
    }
}