using Crm.Common;
using System;
using Volo.Abp.Domain.Services;

namespace Crm.Support
{
    public class SupportTicketManager : DomainService
    {
        private readonly ISupportTicketRepository _supportTicketRepository;

        public SupportTicketManager(ISupportTicketRepository supportTicketRepository)
        {
            _supportTicketRepository = supportTicketRepository;
        }

        public void CheckAndEscalate(SupportTicket ticket)
        {
            var now = DateTime.UtcNow;

            if (ticket.SLAResolutionDeadline.HasValue &&
                ticket.TicketStatus != EnumTicketStatus.Closed &&
                now > ticket.SLAResolutionDeadline.Value)
            {
                ticket.ChangePriority(EnumPriority.Critical);
            }
        }
    }
}