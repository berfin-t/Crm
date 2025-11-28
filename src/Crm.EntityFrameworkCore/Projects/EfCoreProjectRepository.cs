using Crm.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Crm.Common;

namespace Crm.Projects
{
    public class EfCoreProjectRepository(IDbContextProvider<CrmDbContext> dbContextProvider)
        : EfCoreRepository<CrmDbContext, Project, Guid>(dbContextProvider), IProjectRepository
    {
        #region GetListAll
        public async Task<List<Project>> GetListAllAsync(string? name = null, string? description = null, DateTime? startTime = null, DateTime? endTime = null, ICollection<EnumStatus>? statues = null, decimal? revenue = null, decimal? succesRate = null, List<Guid>? employeeIds = null, Guid? customerId = null, string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyDataFilters(await GetQueryableAsync(), name, description, startTime, endTime, statues, revenue, succesRate, employeeIds, customerId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ProjectConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResults).ToListAsync(cancellationToken);
        }
        #endregion

        #region GetCount             
        public async Task<long> GetCountAsync(string? name = null, string? description = null, DateTime? startTime = null, DateTime? endTime = null, ICollection<EnumStatus>? statues = null, decimal? revenue = null, decimal? succesRate = null, List<Guid>? employeeIds = null, Guid? customerId = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            query = ApplyDataFilters(query, name, description, startTime, endTime, statues, revenue, succesRate, employeeIds, customerId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }
        #endregion

        #region ApplyDataFilters
        public virtual IQueryable<Project> ApplyDataFilters(IQueryable<Project> query, string? name = null, string? description = null, DateTime? startTime = null, DateTime? endTime = null, ICollection<EnumStatus>? statues = null, decimal? revenue = null, decimal? succesRate = null, List<Guid>? employeeIds = null, Guid? customerId = null)
        {
            query.WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name == name)
                 .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description == description)
                 .WhereIf(startTime != null, e => e.StartTime == startTime)
                 .WhereIf(endTime != null, e => e.EndTime == endTime)
                 .WhereIf(statues != null && statues.Any(), e => statues.Contains(e.Status))
                 .WhereIf(revenue != null, e => e.Revenue == revenue)
                 .WhereIf(succesRate != null, e => e.SuccessRate == succesRate)
                 .WhereIf(employeeIds != null && employeeIds.Any(),
                      e => e.ProjectEmployees.Any(pe => employeeIds.Contains(pe.EmployeeId)))
                 .WhereIf(customerId != null, e => e.CustomerId == customerId);
            return query;
        }
        #endregion

        public async Task<decimal> GetSuccessRateAverageAsync(decimal? successRate = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            var average = await query.AverageAsync(p => p.SuccessRate, cancellationToken);
            return Math.Round(average, 2);
        }
        
    }
}
