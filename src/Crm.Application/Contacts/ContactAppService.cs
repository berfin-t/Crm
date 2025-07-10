using Crm.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Crm.Contacts
{
    [RemoteService(IsEnabled = false)]
    [Authorize(CrmPermissions.Contacts.Default)]
    public class ContactAppService(IContactRepository contactRepository,
        ContactManager contactManager) : CrmAppService, IContactAppService
    {
        #region Create
        //[Authorize(CrmPermissions.Contacts.Create)]
        public async Task<ContactDto> CreateAsync(ContactCreateDto input)
        {
            var contact= await contactManager.CreateAsync(
                input.CustomerId, input.EmployeeId, input.Type,
                input.ContactValue, input.IsPrimary);

            return ObjectMapper.Map<Contact, ContactDto>(contact);
        }
        #endregion

        #region Get
        public async Task<ContactDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Contact, ContactDto>(await contactRepository.GetAsync(id));
        }
        #endregion

        #region GetListAll
        public async Task<List<ContactDto>> GetListAllAsync()
        {
            var items = await contactRepository.GetListAsync();
            return ObjectMapper.Map<List<Contact>, List<ContactDto>>(items);
        }
        #endregion

        #region GetListPaged
        public async Task<PagedResultDto<ContactDto>> GetListAsync(GetPagedContactsInput input)
        {
            var totalCount = await contactRepository.GetCountAsync(
                input.Type, input.ContactValue, input.IsPrimary,
                input.CustomerId, input.EmployeeId);

            var items = await contactRepository.GetListAsync(
                input.Type, input.ContactValue, input.IsPrimary,
                input.CustomerId, input.EmployeeId);

            return new PagedResultDto<ContactDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Contact>, List<ContactDto>>(items)
            };
        }
        #endregion

        #region Update
        public async Task<ContactDto> UpdateAsync(Guid id, ContactUpdateDto input)
        {
            var contact = await contactManager.UpdateAsync(
                id, input.CustomerId, input.EmployeeId, input.Type,
                input.ContactValue, input.IsPrimary);

            return ObjectMapper.Map<Contact, ContactDto>(contact);
        }
        #endregion
    }
}
