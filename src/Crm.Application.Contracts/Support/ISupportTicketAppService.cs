using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Crm.Support
{
    public interface ISupportTicketAppService : IApplicationService
    {
        Task<List<SupportTicketDto>> GetListAllAsync();
        Task<SupportTicketDto> GetAsync(Guid id);
        Task<SupportTicketWithNavigationPropertyDto> GetWithNavigationPropertiesAsync(Guid id);
        Task AssignEmployeeAsync(Guid ticketId, Guid employeeId);
        Task UpdateStatusPriorityAsync(Guid id, UpdateTicketStatusPriorityDto input);
        Task<List<EnumTicketStatus>> GetAllowedStatusesAsync(Guid id);
    }
}
