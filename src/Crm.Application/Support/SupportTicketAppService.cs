using AutoMapper.Internal.Mappers;
using Bogus.DataSets;
using Crm.Customers;
using Crm.Customers.Events;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;

namespace Crm.Support
{
    [RemoteService(IsEnabled = true)]
    public class SupportTicketAppService(ISupportTicketRepository supportTicketRepository, ICustomerRepository customerRepository,
        SupportTicketManager supportTicketManager) : CrmAppService, ISupportTicketAppService
    {
        private async Task<Guid> GetOrCreateCustomerIdAsync(SupportTicketCreateDto input)
        {
            var nameParts = input.CustomerFullName.Trim().Split(' ', 2);

            var name = nameParts[0];
            var surname = nameParts.Length > 1 ? nameParts[1] : "";

            var customer = await customerRepository.FirstOrDefaultAsync(
                x => x.Email == input.Email
            );

            if (customer != null)
            {
                return customer.Id;
            }

            customer = new Customer(
                GuidGenerator.Create(),
                name,
                surname,
                input.Email,
                phone: string.Empty,
                address: string.Empty,
                companyName: string.Empty,
                EnumCustomer.Lead
            );

            await customerRepository.InsertAsync(customer, autoSave: true);

            return customer.Id;
        }

        #region GetListAll
        [AllowAnonymous]
        public async Task<List<SupportTicketDto>> GetListAllAsync()
        {
            var items = await supportTicketRepository.GetListAsync();
            return ObjectMapper.Map<List<SupportTicket>, List<SupportTicketDto>>(items);
        }
        #endregion

        #region Update
        public async Task<SupportTicketDto> UpdateAsync(Guid id, SupportTicketUpdateDto input)
        {
            var ticket = await supportTicketManager.AdminUpdateAsync(
                id,
                input.TicketStatus,
                input.Priority,
                input.EmployeeId
            );

            return ObjectMapper.Map<SupportTicket, SupportTicketDto>(ticket);
        }
        #endregion

        //#region Create
        //public async Task<SupportTicketDto> CreateAsync(SupportTicketCreateDto input)
        //{
        //    var customerId = await GetOrCreateCustomerIdAsync(input);


        //    var supportTicket = await supportTicketManager.CreateByCustomerAsync(
        //         input.Subject, input.Description, customerId);            

        //    return ObjectMapper.Map<SupportTicket, SupportTicketDto>(supportTicket);
        //}
        //#endregion
        //public async Task<SupportTicketDto> CreateAsync(SupportTicketCreateDto input)
        //{
        //    var customerId = CurrentUser.GetId(); // login olan customer

        //    var ticket = new SupportTicket(
        //        GuidGenerator.Create(),
        //        customerId,
        //        input.Subject,
        //        input.Description
        //    );

        //    await supportTicketRepository.InsertAsync(ticket, autoSave: true);

        //    return ObjectMapper.Map<SupportTicket, SupportTicketDto>(ticket);
        //}


        #region GetWithNavigationProperties
        public virtual async Task<SupportTicketWithNavigationPropertyDto> GetWithNavigationPropertiesAsync(Guid id)
        => ObjectMapper.Map<SupportTicketWithNavigationProperties, SupportTicketWithNavigationPropertyDto>(
            await supportTicketRepository.GetWithNavigationPropertiesAsync(id));
        #endregion

        //[Authorize(Roles = "Admin,Employee")]
        //public async Task UpdateAsync(Guid id, SupportTicketUpdateDto input)
        //{
        //    var ticket = await supportTicketRepository.GetAsync(id);

        //    ticket.AssignEmployee(input.EmployeeId);
        //    ticket.ChangeStatus(input.TicketStatus);
        //    ticket.ChangePriority(input.Priority);

        //    await supportTicketRepository.UpdateAsync(ticket, autoSave: true);
        //}
    }    
}

