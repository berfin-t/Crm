using Crm.Common;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Timing;

namespace Crm.Support
{
    public class SupportTicket : FullAuditedAggregateRoot<Guid>
    {
        public Guid CustomerId { get; private set; }
        public Guid? EmployeeId { get; private set; }        

        public string Subject { get; private set; }
        public string Description { get; private set; }

        public EnumTicketStatus? TicketStatus { get; private set; }
        public EnumPriority? Priority { get; private set; }

        
        public DateTime? LastResponseTime { get; private set; }
        public DateTime? ClosedTime { get; private set; }

        protected SupportTicket()
        {
            Subject = string.Empty;
            Description = string.Empty;
            TicketStatus = EnumTicketStatus.Open;
        }

        public SupportTicket(Guid id, Guid customerId, string subject, string description)
        : base(id)
        {
            CustomerId = customerId;
            Subject = Check.NotNullOrWhiteSpace(subject, nameof(subject));
            Description = Check.NotNullOrWhiteSpace(description, nameof(description));

            TicketStatus = EnumTicketStatus.Open;
            Priority = null;
            EmployeeId = null;
        }

        public void AssignEmployee(Guid employeeId)
        {
            EmployeeId = employeeId;
            LastResponseTime = DateTime.Now;
        }

        public void ChangeStatus(EnumTicketStatus status)
        {
            TicketStatus = status;

            if (status == EnumTicketStatus.Closed)
                ClosedTime = DateTime.Now;
        }

        public void ChangePriority(EnumPriority priority)
        {
            Priority = priority;
        }

    }
}
