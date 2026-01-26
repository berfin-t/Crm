using Crm.Activities;
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
    }
}
