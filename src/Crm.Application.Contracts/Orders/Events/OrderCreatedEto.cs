using Crm.Common;
using System;

namespace Crm.Orders.Events
{
    public class OrderCreatedEto
    {
        public Guid Id { get; set; }
        public string OrderCode { get; set; } = string.Empty;
    }
}
