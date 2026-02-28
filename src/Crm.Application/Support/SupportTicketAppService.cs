using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Crm.Support
{
    [RemoteService(IsEnabled = true)]
    public class SupportTicketAppService(ISupportTicketRepository supportTicketRepository)
        : CrmAppService, ISupportTicketAppService
    {

        #region GetListAll
        [AllowAnonymous]
        public async Task<List<SupportTicketDto>> GetListAllAsync()
        {
            var items = await supportTicketRepository.GetListAsync();
            return ObjectMapper.Map<List<SupportTicket>, List<SupportTicketDto>>(items);
        }
        #endregion

        #region Get
        [AllowAnonymous]
        public async Task<SupportTicketDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SupportTicket, SupportTicketDto>(
                await supportTicketRepository.GetAsync(id)
            );
        }
        #endregion

        #region GetWithNavigationProperties
        [AllowAnonymous]
        public virtual async Task<SupportTicketWithNavigationPropertyDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            var entity = await supportTicketRepository.GetWithNavigationPropertiesAsync(id);
            return ObjectMapper.Map<SupportTicketWithNavigationProperties, SupportTicketWithNavigationPropertyDto>(entity);
        }
        #endregion

        #region AssignEmployeeAsync
        [AllowAnonymous]
        public async Task AssignEmployeeAsync(Guid ticketId, Guid employeeId)
        {
            var ticket = await supportTicketRepository.GetAsync(ticketId)
                         ?? throw new UserFriendlyException("Support ticket not found.");

            ticket.AssignEmployee(employeeId);

            await supportTicketRepository.UpdateAsync(ticket, autoSave: true);
        }
        #endregion

        #region Update Status Priority
        [AllowAnonymous]
        public async Task UpdateStatusPriorityAsync(Guid id, UpdateTicketStatusPriorityDto input)
        {
            var ticket = await supportTicketRepository.GetAsync(id)
                         ?? throw new UserFriendlyException("Support ticket not found.");

            ticket.UpdateOperationalInfo(
                input.TicketStatus,
                input.Priority
            );

            await supportTicketRepository.UpdateAsync(ticket, autoSave: true);
        }
        #endregion

        #region Get Allowed Statuses for UI
        [AllowAnonymous]
        public async Task<List<EnumTicketStatus>> GetAllowedStatusesAsync(Guid id)
        {
            var ticket = await supportTicketRepository.GetAsync(id)
                         ?? throw new UserFriendlyException("Support ticket not found.");

            return ticket.GetAllowedStatuses();
        }
        #endregion

        public async Task<List<SupportTicketDto>> GetSlaRiskTicketsAsync()
        {
            var tickets = await supportTicketRepository.GetSlaRiskTicketsAsync();

            var dtos = tickets.Select(ticket =>
            {
                var dto = ObjectMapper.Map<SupportTicket, SupportTicketDto>(ticket);

                var now = DateTime.UtcNow;

                dto.IsResponseOverdue = ticket.SLAResponseDeadline.HasValue &&
                                        (ticket.TicketStatus == EnumTicketStatus.Open
                                         || ticket.TicketStatus == EnumTicketStatus.InProgress
                                         || ticket.TicketStatus == EnumTicketStatus.WaitingForCustomer) &&
                                        now > ticket.SLAResponseDeadline.Value;

                dto.IsResolutionOverdue = ticket.SLAResolutionDeadline.HasValue &&
                                          ticket.TicketStatus != EnumTicketStatus.Resolved &&
                                          ticket.TicketStatus != EnumTicketStatus.Closed &&
                                          now > ticket.SLAResolutionDeadline.Value;

                return dto;
            }).ToList();

            return dtos;
        }
    }
}