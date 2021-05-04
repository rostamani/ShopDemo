using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;
using InventoryManagement.Application.Contracts;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication:IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        public OperationResult Create(CreateInventory command)
        {
            var operationResult = new OperationResult();
            if (_inventoryRepository.Exists(i=>i.ProductId==command.ProductId))
            {
                return operationResult.Failed(QueryValidationMessage.DuplicateRecord);
            }

            var inventory=new Inventory(command.ProductId,command.UnitPrice);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operationResult = new OperationResult();
            var inventory = _inventoryRepository.Get(command.Id);
            if (inventory==null)
            {
                return operationResult.Failed(QueryValidationMessage.NotFound);
            }
            if (_inventoryRepository.Exists(i => i.ProductId == command.ProductId&&i.Id!=command.Id))
            {
                return operationResult.Failed(QueryValidationMessage.DuplicateRecord);
            }

            inventory.Edit(command.ProductId,command.UnitPrice);
            _inventoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operationResult = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
            {
                return operationResult.Failed(QueryValidationMessage.NotFound);
            }
            inventory.Increase(command.Count,1,command.Description);
            _inventoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Decrease(DecreaseInventory command)
        {
            var operationResult = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
            {
                return operationResult.Failed(QueryValidationMessage.NotFound);
            }
            inventory.Decrease(command.Count, 1, command.Description,0);
            _inventoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Decrease(List<DecreaseInventory> commands)
        {
            var operationResult = new OperationResult();
            foreach (var item in commands)
            {
                var inventory = _inventoryRepository.GetBy(item.ProductId);
                inventory.Decrease(item.Count,1,item.Description,item.OrderId);
            }

            _inventoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryRepository.GetDetails(id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);
        }

        public List<OperationLogViewModel> GetOperations(long id)
        {
            return _inventoryRepository.GetOperations(id);
        }
    }
}
