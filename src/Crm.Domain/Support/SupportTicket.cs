using Crm.Common;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Crm.Support
{
    public class SupportTicket : FullAuditedAggregateRoot<Guid>
    {
        public Guid CustomerId { get; private set; }
        public Guid EmployeeId { get; private set; } 

        [NotNull]
        public string? Subject { get; private set; }

        [NotNull]
        public string? Description { get; private set; }

        public EnumTicketStatus TicketStatus { get; private set; }

        public EnumPriority Priority { get; private set; }

        
        public DateTime? LastResponseTime { get; private set; }

        public DateTime? ClosedTime { get; private set; }

        protected SupportTicket()
        {
            Subject = string.Empty;
            Description = string.Empty;
            TicketStatus = EnumTicketStatus.Open;
            Priority = EnumPriority.Medium;
            LastResponseTime = DateTime.Now;
            ClosedTime = DateTime.Now;
        }

        public SupportTicket(Guid id, Guid customerId, Guid employeeId, string subject, string description, EnumTicketStatus ticketStatus, EnumPriority priority)
            : base(id)
        {
            SetCustomerId(customerId);
            SetEmployeeId(employeeId);
            SetSubject(subject);
            SetDescription(description);
            SetTicketStatus(ticketStatus);
            SetPriority(priority);            
        }
        public void SetCustomerId(Guid customerId) => CustomerId = Check.NotNull(customerId, nameof(customerId));
        public void SetEmployeeId(Guid employeeId) => EmployeeId = Check.NotNull(employeeId, nameof(employeeId));
        public void SetSubject(string subject) => Subject = Check.NotNullOrWhiteSpace(subject, nameof(subject));
        public void SetDescription(string description) => Description = Check.NotNullOrWhiteSpace(description, nameof(description));
        public void SetTicketStatus(EnumTicketStatus ticketStatus) => TicketStatus = Check.NotNull(ticketStatus, nameof(ticketStatus));
        public void SetPriority(EnumPriority priority) => Priority = Check.NotNull(priority, nameof(priority));
        
    }
}
