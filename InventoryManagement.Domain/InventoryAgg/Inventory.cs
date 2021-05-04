using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory:EntityBase
    {
        public long ProductId { get;private set; }
        public double UnitPrice { get; private set; }
        public bool IsInStock { get; private set; }
        public List<InventoryOperation> Operations { get; private set; }

        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            IsInStock = false;
        }

        public void Edit(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }

        public long CalculateCurrentCount()
        {
            long increment = this.Operations.Where(o => o.IsIncrement).Sum(o => o.Count);
            long decrement = this.Operations.Where(o => o.IsIncrement == false).Sum(o => o.Count);

            return increment - decrement;
        }

        public void Increase(long count,long operatorId,string description)
        {
            var currentCount = CalculateCurrentCount() + count;
            var operation=new InventoryOperation(true,count,currentCount,operatorId,0,description,Id);
            this.Operations.Add(operation);
            IsInStock = currentCount > 0;
        }

        public void Decrease(long count, long operatorId, string description,long orderId)
        {
            var currentCount = CalculateCurrentCount() - count;
            var operation = new InventoryOperation(false, count, currentCount, operatorId, orderId, description, Id);
            this.Operations.Add(operation);
            IsInStock = currentCount > 0;
        }
    }
}
