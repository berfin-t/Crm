using Crm.Common;
using System;
using System.Collections.Generic;

namespace Crm.Orders
{
    public class GetPagedOrdersInput
    {
        public GetPagedOrdersInput() { }

        public ICollection<EnumStatus>? Status { get; set; } = null;
        public DateTime? OrderDate { get; set; } = null;
        public DateTime? DeliveryDate { get; set; } = null;
        public decimal? TotalAmount { get; set; } = null;
        public Guid? CustomerId { get; set; } = null;
        public Guid? ProjectId { get; set; } = null;

    }
}
