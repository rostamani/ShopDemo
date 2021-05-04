using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        OperationResult Increase(IncreaseInventory command);
        OperationResult Decrease(DecreaseInventory command);
        OperationResult Decrease(List<DecreaseInventory> commands);
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        List<OperationLogViewModel> GetOperations(long id);
    }
}
