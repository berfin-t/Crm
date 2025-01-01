using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Crm.CustomerNotes
{
    public interface ICustomerNoteRepository:IRepository<CustomerNote, Guid>
    {
        Task<List<CustomerNote>> GetListAsync(string? note = null, DateTime? noteDate = null,
            Guid? customerId = null, string? sorting = null, int maxResults = int.MaxValue,
            int skipCount = 0);

        Task<long> GetCountAsync(string? note = null, DateTime? noteDate = null, Guid? customerId = null);
    }
}
