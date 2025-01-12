using Crm.EntityFrameworkCore;
using Crm.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Crm.Positions
{
    public class EfCorePositionRepository(IDbContextProvider<CrmDbContext> dbContextProvider)
        : EfCoreRepository<CrmDbContext, Position, Guid>(dbContextProvider), IPositionRepository
    {
        #region GetCountAsync
        public async Task<long> GetCountAsync(string? name = null, string? description = null, decimal? salary = null, int? minExperience = null, int? maxExperience = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            query = ApplyDataFilters(query, name, description, salary, minExperience, maxExperience);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }
        #endregion
        #region GetListAsync
        public async Task<List<Position>> GetListAsync(string? name = null, string? description = null, decimal? salary = null, int? minExperience = null, int? maxExperience = null, string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyDataFilters(await GetQueryableAsync(), name, description, salary, minExperience, maxExperience);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? OrderConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResults).ToListAsync(cancellationToken);
        }
        #endregion

        #region ApplyDataFilters
        protected virtual IQueryable<Position> ApplyDataFilters(IQueryable<Position> query, string? name = null, string? description = null, decimal? salary = null, int? minExperience = null, int? maxExperience = null)
        {
            query.WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                 .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                 .WhereIf(salary != null, e => e.Salary == salary)
                 .WhereIf(minExperience != null, e => e.MinExperience == minExperience)
                 .WhereIf(maxExperience != null, e => e.MaxExperience == maxExperience);
            return query;
        }
        #endregion
    }
}
