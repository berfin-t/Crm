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
    }
}