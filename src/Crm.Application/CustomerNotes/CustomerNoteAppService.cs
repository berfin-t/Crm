using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Crm.CustomerNotes
{
    [RemoteService(IsEnabled = false)]
    //[Authorize(CrmPermissions.CustomerNotes.Default)]
    public class CustomerNoteAppService(ICustomerNoteRepository customerNoteRepository,
        CustomerNoteManager customerNoteManager) : CrmAppService, ICustomerNoteAppService
    {
        #region Create
        //[Authorize(CrmPermissions.CustomerNotes.Create)]
        public async Task<CustomerNoteDto> CreateAsync(CustomerNoteCreateDto input)
        {
            var customerNote = await customerNoteManager.CreateAsync(
                input.CustomerId, input.Note, input.NoteDate);

            return ObjectMapper.Map<CustomerNote, CustomerNoteDto>(customerNote);
        }
        #endregion

        #region Get
        public async Task<CustomerNoteDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerNote, CustomerNoteDto>(await customerNoteRepository.GetAsync(id));
        }
        #endregion

        #region GetListAll
        public async Task<List<CustomerNoteDto>> GetListAllAsync()
        {
            var items = await customerNoteRepository.GetListAsync();
            return ObjectMapper.Map<List<CustomerNote>, List<CustomerNoteDto>>(items);
        }
        #endregion

        #region GetListPaged
        public async Task<PagedResultDto<CustomerNoteDto>> GetListAsync(GetPagedCustomerNotesInput input)
        {
           var totalCount = await customerNoteRepository.GetCountAsync(
               input.Note, input.NoteDate, input.CustomerId);

            var items = await customerNoteRepository.GetListAsync(
               input.Note, input.NoteDate, input.CustomerId);

            return new PagedResultDto<CustomerNoteDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerNote>, List<CustomerNoteDto>>(items)
            };
        }
        #endregion

        #region Update
        public async Task<CustomerNoteDto> UpdateAsync(Guid id, CustomerNoteUpdateDto input)
        {
            var customerNote = await customerNoteManager.UpdateAsync(
                id, input.CustomerId, input.Note, input.NoteDate);

            return ObjectMapper.Map<CustomerNote, CustomerNoteDto>(customerNote);
        }
        #endregion
    }
}
