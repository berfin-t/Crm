using Crm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Support
{
    public class SupportTicketUpdateDto
    {
        public Guid EmployeeId { get; set; }
        public EnumTicketStatus TicketStatus { get; set; }
        public EnumPriority Priority { get; set; }
    }
}
