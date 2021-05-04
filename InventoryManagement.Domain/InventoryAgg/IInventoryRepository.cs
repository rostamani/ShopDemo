using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Domain;
using InventoryManagement.Application.Contracts;
using InventoryManagement.Application.Contracts.Inventory;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository:IRepository<long,Inventory>
    {
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        EditInventory GetDetails(long id);
        Inventory GetBy(long productId);
        List<OperationLogViewModel> GetOperations(long id);
    }
}
