using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Crm.Support
{
    public interface ISupportTicketAppService:IApplicationService
    {
        Task<List<SupportTicketDto>> GetListAllAsync();
        //Task<SupportTicketDto> UpdateAsync(Guid id, SupportTicketUpdateDto input);
        //Task<SupportTicketDto> CreateAsync(SupportTicketCreateDto supportTicketCreateDto);
    }
}
