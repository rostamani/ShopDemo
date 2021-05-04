using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class InventoryOperation
    {
        public long Id { get; private set; }
        public bool IsIncrement { get; private set; }
        public long Count { get; private set; }
        public long CurrentCount { get; private set; }
        public long OperatorId { get; private set; }
        public long OrderId { get; private set; }
        public DateTime OperationDate { get; private set; }
        public string Description { get; private set; }
        public long InventoryId { get; private set; }
        public Inventory Inventory { get; private set; }

        public InventoryOperation(bool isIncrement,long count, long currentCount, long operatorId, long orderId, string description, long inventoryId)
        {
            IsIncrement = isIncrement;
            Count = count;
            CurrentCount = currentCount;
            OperatorId = operatorId;
            OrderId = orderId;
            Description = description;
            InventoryId = inventoryId;
            OperationDate = DateTime.Now;
        }
    }
}
