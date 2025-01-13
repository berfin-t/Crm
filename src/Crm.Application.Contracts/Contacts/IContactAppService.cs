using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Crm.Contacts
{
    public interface IContactAppService:IApplicationService
    {
        Task<PagedResultDto<ContactDto>> GetListAsync(GetPagedContactsInput input);
        Task<List<ContactDto>> GetListAllAsync();
        Task<ContactDto> GetAsync(Guid id);
        Task<ContactDto> CreateAsync(ContactCreateDto input);
        Task<ContactDto> UpdateAsync(Guid id, ContactUpdateDto input);
    }
}
