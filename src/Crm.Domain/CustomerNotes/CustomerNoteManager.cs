using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Crm.CustomerNotes
{
    public class CustomerNoteManager(ICustomerNoteRepository customerNoteRepository) : DomainService
    {
        #region Create
        public virtual async Task<CustomerNote> CreateAsync(Guid customerId, string note, DateTime noteDate)
        {
            var customerNote = new CustomerNote(
                GuidGenerator.Create(),
                note,
                noteDate,
                customerId
            );
            return await customerNoteRepository.InsertAsync(customerNote);
        }
        #endregion

        #region Update
        public virtual async Task<CustomerNote> UpdateAsync(Guid id, Guid customerId, string name, DateTime noteDate)
        {
            var customerNote = await customerNoteRepository.GetAsync(id);
            customerNote.SetNote(name);
            customerNote.SetNoteDate(noteDate);
            customerNote.SetCustomerId(customerId);
            return await customerNoteRepository.UpdateAsync(customerNote);
        }
        #endregion
    }
}
