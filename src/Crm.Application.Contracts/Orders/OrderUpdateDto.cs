using Crm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Orders
{
    public class OrderUpdateDto
    {
        public EnumStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderCode { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
