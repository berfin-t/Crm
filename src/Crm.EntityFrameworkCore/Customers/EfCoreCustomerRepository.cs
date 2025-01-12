using Crm.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Crm.Customers
{
    public class EfCoreCustomerRepository(IDbContextProvider<CrmDbContext> dbContextProvider)
        : EfCoreRepository<CrmDbContext, Customer, Guid>(dbContextProvider), ICustomerRepository
    {
        #region GetCountAsync
        public async Task<long> GetCountAsync(string? name = null, string? surname = null, string? email = null, string? phone = null, string? address = null, string? companyName = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            query = ApplyDataFilters(query, name, surname, email, phone, address, companyName);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }
        #endregion

        #region GetListAsync
        public async Task<List<Customer>> GetListAsync(string? name = null, string? surname = null, string? email = null, string? phone = null, string? address = null, string? companyName = null, string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyDataFilters(await GetQueryableAsync(), name, surname, email, phone, address, companyName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResults).ToListAsync(cancellationToken);
        }
        #endregion

        #region

        protected virtual IQueryable<Customer> ApplyDataFilters(IQueryable<Customer> query, string? name = null, string? surname = null, string? email = null, string? phone = null, string? address = null, string? companyName = null)
        {
            query
                .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!))
                .WhereIf(!string.IsNullOrWhiteSpace(surname), e => e.Surname.Contains(surname!))
                .WhereIf(!string.IsNullOrWhiteSpace(email), e => e.Email.Contains(email!))
                .WhereIf(!string.IsNullOrWhiteSpace(phone), e => e.Phone.Contains(phone!))
                .WhereIf(!string.IsNullOrWhiteSpace(address), e => e.Address.Contains(address!))
                .WhereIf(!string.IsNullOrWhiteSpace(companyName), e => e.CompanyName.Contains(companyName!));
            return query;
        }
        #endregion
    }
}
