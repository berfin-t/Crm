using Crm.Activities;
using Crm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Crm.Support
{
    public interface ISupportTicketRepository:IRepository<SupportTicket, Guid>
    {
        Task<SupportTicketWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<List<SupportTicket>> GetSlaRiskTicketsAsync();
        Task<List<SupportTicket>> GetListAsync(Guid? customerId = null, Guid? employeeId = null,
            string? subject = null, string? description = null,
            ICollection<EnumTicketStatus>? ticketStatus = null, ICollection<EnumPriority>? priority = null,
            DateTime? lastResponseTime = null, DateTime? closedTime = null,
            DateTime? slaResponseDeadLine = null, DateTime? slaResolutionDeadline = null,
            string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    }
}
