using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount
{
    public interface IColleagueDiscountApplication
    {
        OperationResult Create(DefineColleagueDiscount command);
        OperationResult Edit(EditColleagueDiscount command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
        EditColleagueDiscount GetDetails(long id);
    }
}
