using Crm.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Crm.Contacts
{
    public class EfCoreContactRepository(IDbContextProvider<CrmDbContext> dbContextProvider)
        : EfCoreRepository<CrmDbContext, Contact, Guid>(dbContextProvider), IContactRepository
    {
        #region GetCountAsync      
        public async Task<long> GetCountAsync(ICollection<EnumType>? type = null, string? contactValue = null, bool? isPrimary = null, Guid? customerId = null, Guid? employeeId = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            query = ApplyDataFilters(query, type, contactValue, isPrimary, customerId, employeeId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }
        #endregion

        #region GetListAsync       
        public async Task<List<Contact>> GetListAsync(ICollection<EnumType>? type = null, string? contactValue = null, bool? isPrimary = null, Guid? customerId = null, Guid? employeeId = null, string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyDataFilters(await GetQueryableAsync(), type, contactValue, isPrimary, customerId, employeeId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ContactConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResults).ToListAsync(cancellationToken);
        }
        #endregion

        #region ApplyDataFilters
        protected virtual IQueryable<Contact> ApplyDataFilters(IQueryable<Contact> query,
            ICollection<EnumType>? type = null, string? contactValue = null, bool? isPrimary = null, Guid? customerId = null, Guid? employeeId = null)
        {
            query
                .WhereIf(type != null && type.Count != 0, e => type!.Contains(e.Type))
                .WhereIf(!string.IsNullOrWhiteSpace(contactValue), x => x.ContactValue.Contains(contactValue!))
                .WhereIf(isPrimary != null, x=>x.IsPrimary==isPrimary)
                .WhereIf(customerId.HasValue, e => e.CustomerId == customerId!.Value)
                .WhereIf(employeeId.HasValue, e => e.EmployeeId == employeeId!.Value);

            return query;
        }
        #endregion

    }
}
