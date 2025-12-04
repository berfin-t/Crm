using Crm.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Crm.CustomerNotes
{
    public class EfCoreCustomerNoteRepository(IDbContextProvider<CrmDbContext> provider)
        : EfCoreRepository<CrmDbContext, CustomerNote, Guid>(provider), ICustomerNoteRepository
    {
        #region GetCountAsync
        public async Task<long> GetCountAsync(string? note = null, DateTime? noteDate = null, Guid? customerId = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            query = ApplyDataFilters(query, note, noteDate, customerId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }
        #endregion

        #region GetListAsync
        public async Task<List<CustomerNote>> GetListAsync(string? note = null, DateTime? noteDate = null, Guid? customerId = null, string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyDataFilters(await GetQueryableAsync(), note, noteDate, customerId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerNoteConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResults).ToListAsync(cancellationToken);
        }
        #endregion

        #region GetListByCustomerAsync
        public async Task<List<CustomerNote>> GetListByCustomerAsync(Guid? customerId = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            return await query.Where(x=> x.CustomerId == customerId)
                .OrderByDescending(x=>x.NoteDate)
                .ToListAsync(cancellationToken);
        }
        #endregion

        #region ApplyDataFilters
        protected virtual IQueryable<CustomerNote> ApplyDataFilters(IQueryable<CustomerNote> query, string? note = null, DateTime? noteDate = null, Guid? customerId = null)
        {
            query
                .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Note.Contains(note!))
                .WhereIf(noteDate.HasValue, e => e.NoteDate == noteDate!.Value)
                .WhereIf(customerId.HasValue, e => e.CustomerId == customerId!.Value);
            return query;
        }
        #endregion

    }
}
