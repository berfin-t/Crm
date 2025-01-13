using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Crm.CustomerNotes
{
    public interface ICustomerNoteAppService:IApplicationService
    {
        Task<PagedResultDto<CustomerNoteDto>> GetListAsync(GetPagedCustomerNotesInput input);
        Task<List<CustomerNoteDto>> GetListAllAsync();
        Task<CustomerNoteDto> GetAsync(Guid id);
        Task<CustomerNoteDto> CreateAsync(CustomerNoteCreateDto input);
        Task<CustomerNoteDto> UpdateAsync(Guid id, CustomerNoteUpdateDto input);
    }
}
