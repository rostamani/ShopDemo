using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Application.Contracts
{
    public class OperationLogViewModel
    {
        public long Id { get;  set; }
        public bool IsIncrement { get;  set; }
        public long Count { get;  set; }
        public long CurrentCount { get;  set; }
        public long OperatorId { get;  set; }
        public long OrderId { get;  set; }
        public string Operator { get; set; }
        public string OperationDate { get;  set; }
        public string Description { get;  set; }
    }
}
