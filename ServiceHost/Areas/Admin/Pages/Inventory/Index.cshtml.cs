using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAgg;

namespace ServiceHost.Areas.Admin.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        private readonly IInventoryApplication _inventoryApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(IInventoryApplication inventoryApplication, IProductApplication productApplication)
        {
            _inventoryApplication = inventoryApplication;
            _productApplication = productApplication;
        }

        public SelectList Products;
        public InventorySearchModel SearchModel;
        public List<InventoryViewModel> Inventory;

        [NeedsPermission(InventoryPermissions.ListInventory)]
        public void OnGet(InventorySearchModel searchModel)
        {
            Products=new SelectList(_productApplication.GetProducts(),"Id","Name");
            Inventory = _inventoryApplication.Search(searchModel);
        }

        [NeedsPermission(InventoryPermissions.EditInventory)]
        public IActionResult OnGetEdit(long id)
        {
            var inventory = _inventoryApplication.GetDetails(id);
            inventory.Products = _productApplication.GetProducts();
            return Partial("Edit", inventory);
        }

        [NeedsPermission(InventoryPermissions.CreateInventory)]
        public IActionResult OnGetCreate()
        {
            var inventory = new CreateInventory();
            inventory.Products = _productApplication.GetProducts();
            return Partial("Create", inventory);
        }

        [NeedsPermission(InventoryPermissions.CreateInventory)]
        public IActionResult OnPostCreate(CreateInventory command)
        {
            var operationResult = new OperationResult();
            if (ModelState.IsValid)
            {
                operationResult=_inventoryApplication.Create(command);
            }

            return new JsonResult(operationResult);
        }

        [NeedsPermission(InventoryPermissions.EditInventory)]
        public IActionResult OnPostEdit(EditInventory command)
        {
            var operationResult = new OperationResult();
            if (ModelState.IsValid)
            {
                operationResult = _inventoryApplication.Edit(command);
            }

            return new JsonResult(operationResult);
        }

        [NeedsPermission(InventoryPermissions.Increase)]
        public IActionResult OnGetIncrement(long id)
        {
            var increaseInventory = new IncreaseInventory()
            {
                InventoryId = id
            };
            return Partial("Increment",increaseInventory);
        }
        [NeedsPermission(InventoryPermissions.Reduce)]
        public IActionResult OnGetDecrement(long id)
        {
            var decreaseInventory = new DecreaseInventory()
            {
                InventoryId = id
            };
            return Partial("Decrement", decreaseInventory);
        }
        [NeedsPermission(InventoryPermissions.Increase)]
        public IActionResult OnPostIncrement(IncreaseInventory command)
        {
            var operationResult = new OperationResult();
            if (ModelState.IsValid)
            {
                operationResult=_inventoryApplication.Increase(command);
            }
            return new JsonResult(operationResult);
        }
        [NeedsPermission(InventoryPermissions.Reduce)]
        public IActionResult OnPostDecrement(DecreaseInventory command)
        {
            var operationResult = new OperationResult();
            if (ModelState.IsValid)
            {
                operationResult = _inventoryApplication.Decrease(command);
            }
            return new JsonResult(operationResult);
        }

        [NeedsPermission(InventoryPermissions.OperationLog)]
        public IActionResult OnGetOperations(long id)
        {
            var operations = _inventoryApplication.GetOperations(id);
            return Partial("Operations",operations);
        }
    }
}
