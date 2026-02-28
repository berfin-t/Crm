using Crm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Timing;

namespace Crm.Support
{
    public class SupportTicket : FullAuditedAggregateRoot<Guid>
    {
        private static readonly Dictionary<EnumTicketStatus, EnumTicketStatus[]> AllowedTransitions =
    new()
    {
        { EnumTicketStatus.Open,
            new[] { EnumTicketStatus.InProgress, EnumTicketStatus.WaitingForCustomer, EnumTicketStatus.Closed } },

        { EnumTicketStatus.InProgress,
            new[] { EnumTicketStatus.WaitingForCustomer, EnumTicketStatus.Resolved, EnumTicketStatus.Closed } },

        { EnumTicketStatus.WaitingForCustomer,
            new[] { EnumTicketStatus.InProgress, EnumTicketStatus.Resolved, EnumTicketStatus.Closed } },

        { EnumTicketStatus.Resolved,
            new[] { EnumTicketStatus.Closed } },

        { EnumTicketStatus.Closed,
            Array.Empty<EnumTicketStatus>() }
    };
        public Guid CustomerId { get; private set; }
        public Guid? EmployeeId { get; private set; }        

        public string Subject { get; private set; }
        public string Description { get; private set; }

        public EnumTicketStatus TicketStatus { get; private set; }
        public EnumPriority? Priority { get; private set; }

        public DateTime? LastResponseTime { get; private set; }
        public DateTime? ClosedTime { get; private set; }

        public DateTime? SLAResponseDeadline { get; private set; }
        public DateTime? SLAResolutionDeadline { get; private set; }

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
            LastResponseTime = DateTime.UtcNow;

            if (TicketStatus == EnumTicketStatus.Open)
                ChangeStatus(EnumTicketStatus.InProgress);

            ApplySLA();
        }

        public void ChangeStatus(EnumTicketStatus newStatus)
        {
            if (TicketStatus == newStatus)
                return;             

            if (!AllowedTransitions.TryGetValue(TicketStatus, out var allowed))
                throw new BusinessException($"No transition rules defined for {TicketStatus}");

            if (!allowed.Contains(newStatus))
                throw new BusinessException(
                    $"Transition from {TicketStatus} to {newStatus} is not allowed."
                );

            TicketStatus = newStatus;

            if (newStatus == EnumTicketStatus.Closed)
                ClosedTime = DateTime.UtcNow;
        }

        public void ChangePriority(EnumPriority priority)
        {
            Priority = priority;
            ApplySLA();
        }

        public void UpdateOperationalInfo(
    EnumTicketStatus status,
    EnumPriority? priority)
        {
            if (TicketStatus != status)
            {
                ChangeStatus(status);
            }

            if (priority.HasValue && Priority != priority)
            {
                ChangePriority(priority.Value);
            }           

            LastResponseTime = DateTime.UtcNow;
        }

        public List<EnumTicketStatus> GetAllowedStatuses()
        {
            var current = TicketStatus;

            var transitions = AllowedTransitions.TryGetValue(current, out var allowed)
                ? allowed.ToList()
                : new List<EnumTicketStatus>();

            var result = new List<EnumTicketStatus> { current };
            result.AddRange(transitions);

            return result;
        }

        public void ApplySLA()
        {
            if (!Priority.HasValue)
                return;

            var now = DateTime.UtcNow;

            switch (Priority.Value)
            {
                case EnumPriority.Critical:
                    SLAResponseDeadline = now.AddHours(1);
                    SLAResolutionDeadline = now.AddHours(4);
                    break;
                case EnumPriority.High:
                    SLAResponseDeadline = now.AddHours(4);
                    SLAResolutionDeadline = now.AddHours(24);
                    break;
                case EnumPriority.Medium:
                    SLAResponseDeadline = now.AddHours(24);
                    SLAResolutionDeadline = now.AddDays(3);
                    break;
                case EnumPriority.Low:
                    SLAResponseDeadline = now.AddHours(48);
                    SLAResolutionDeadline = now.AddDays(5);
                    break;
            }
        }       

    }
}
