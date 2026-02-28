using Crm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Support
{
    public class SupportTicketDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? EmployeeId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public EnumTicketStatus? TicketStatus { get; set; }
        public EnumPriority? Priority { get; set; }
        public DateTime? LastResponseTime { get; set; }
        public DateTime? ClosedTime { get; set; }

        public DateTime? SLAResponseDeadline { get; set; }
        public DateTime? SLAResolutionDeadline { get; set; }

        public bool IsResponseOverdue { get; set; }
        public bool IsResolutionOverdue { get; set; }

    }
}
