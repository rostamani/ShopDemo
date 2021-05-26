using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Infrastructure;

namespace InventoryManagement.Configuration.Permissions
{
    public class InventoryPermissionExposer:IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Inventory", new List<PermissionDto>
                    {
                        new PermissionDto("ListInventory", InventoryPermissions.ListInventory),
                        new PermissionDto("SearchInventory", InventoryPermissions.SearchInventory),
                        new PermissionDto("EditInventory", InventoryPermissions.EditInventory),
                        new PermissionDto("CreateInventory ", InventoryPermissions.CreateInventory),
                        new PermissionDto("IncreaseInventory ", InventoryPermissions.Increase),
                        new PermissionDto("DecreaseInventory ", InventoryPermissions.Reduce),
                        new PermissionDto("LogInventory ", InventoryPermissions.OperationLog),
                    }
                }
            };
        }
    }
}
