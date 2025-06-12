using Crm.Common;
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

namespace Crm.Tasks
{
    public class EfCoreTaskRepository(IDbContextProvider<CrmDbContext> dbContextProvider)
        : EfCoreRepository<CrmDbContext, Task, Guid>(dbContextProvider), ITaskRepository
    {
        #region GetCountAsync
        public async Task<long> GetCountAsync(string? title = null, string? description = null, DateTime? dueDate = null, ICollection<EnumPriority>? priorities = null, ICollection<EnumStatus>? statues = null, Guid? employeeId = null, Guid? projectId = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            query = ApplyDataFilters(query, title, description, dueDate, priorities, statues, employeeId, projectId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }
        #endregion

        #region GetListAsync
        public async Task<List<Task>> GetListAsync(string? title = null, string? description = null, DateTime? dueDate = null, ICollection<EnumPriority>? priorities = null, ICollection<EnumStatus>? statues = null, Guid? employeeId = null, Guid? projectId = null, string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyDataFilters(await GetQueryableAsync(), title, description, dueDate, priorities, statues, employeeId, projectId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? OrderConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResults).ToListAsync(cancellationToken);
        }
        #endregion

        #region ApplyDataFilters
        protected virtual IQueryable<Task> ApplyDataFilters(IQueryable<Task> query, string? title = null, string? description = null, DateTime? dueDate = null, ICollection<EnumPriority>? priorities = null, ICollection<EnumStatus>? statues = null, Guid? employeeId = null, Guid? projectId = null)
        {
            query = query.WhereIf(!string.IsNullOrWhiteSpace(title), e => e.Title == title)
                 .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description == description)
                 .WhereIf(dueDate != null, e => e.DueDate == dueDate)
                 .WhereIf(priorities != null && priorities.Any(), e => priorities.Contains(e.Priority))
                 .WhereIf(statues != null && statues.Any(), e => statues.Contains(e.Status))
                 .WhereIf(employeeId != null, e => e.EmployeeId == employeeId)
                 .WhereIf(projectId != null, e => e.ProjectId == projectId);
            return query;
        }
        #endregion
    }
}
