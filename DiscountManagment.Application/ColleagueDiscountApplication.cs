using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication:IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var result=new OperationResult();
            if (_colleagueDiscountRepository.Exists(cd=>cd.ProductId==command.ProductId))
            {
                return result.Failed(QueryValidationMessage.DuplicateRecord);
            }

            var colleagueDiscount = new ColleagueDiscount(command.ProductId,command.DiscountRate);
            _colleagueDiscountRepository.Create(colleagueDiscount);
            _colleagueDiscountRepository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var result = new OperationResult();
            var discount = _colleagueDiscountRepository.Get(command.Id);
            if (discount==null)
            {
                return result.Failed(QueryValidationMessage.NotFound);
            }
            if (_colleagueDiscountRepository.Exists(cd => cd.ProductId == command.ProductId && cd.Id!=command.Id))
            {
                return result.Failed(QueryValidationMessage.DuplicateRecord);
            }

            discount.Edit(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var result = new OperationResult();
            var discount = _colleagueDiscountRepository.Get(id);
            if (discount == null)
            {
                return result.Failed(QueryValidationMessage.NotFound);
            }
            discount.Remove();
            _colleagueDiscountRepository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var result = new OperationResult();
            var discount = _colleagueDiscountRepository.Get(id);
            if (discount == null)
            {
                return result.Failed(QueryValidationMessage.NotFound);
            }
            discount.Restore();
            _colleagueDiscountRepository.SaveChanges();
            return result.Succeeded();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _colleagueDiscountRepository.GetDetails(id);
        }
    }
}
