using Asp.Versioning;
using Crm.Support;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;

namespace Crm.Controllers.Support
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Support")]
    [Route("api/app/suports")]
    public class SupportController:CrmController, ISupportTicketAppService
    {
        protected ISupportTicketAppService _supportTicketAppService;

        public SupportController(ISupportTicketAppService supportTicketAppService) => _supportTicketAppService = supportTicketAppService;

        [HttpGet]
        [Route("all")]
        public virtual Task<List<SupportTicketDto>> GetListAllAsync() => _supportTicketAppService.GetListAllAsync();

        [HttpGet]
        [Route("{id}")]
        public virtual Task<SupportTicketDto> GetAsync(Guid id) => _supportTicketAppService.GetAsync(id);

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<SupportTicketWithNavigationPropertyDto> GetWithNavigationPropertiesAsync(Guid id)
            => _supportTicketAppService.GetWithNavigationPropertiesAsync(id);

        [HttpPost("{ticketId}/assign/{employeeId}")]
        public async Task AssignEmployeeAsync(Guid ticketId, Guid employeeId) =>        
            await _supportTicketAppService.AssignEmployeeAsync(ticketId, employeeId);

        [HttpPost("{id}/update-status-priority")]
        public async Task UpdateStatusPriorityAsync(Guid id, UpdateTicketStatusPriorityDto input) =>
            await _supportTicketAppService.UpdateStatusPriorityAsync(id, input);

        [HttpGet("{id}/allowed-statuses")]
        public async Task<List<EnumTicketStatus>> GetAllowedStatusesAsync(Guid id) =>
            await _supportTicketAppService.GetAllowedStatusesAsync(id);
    }
}
