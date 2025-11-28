using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Crm.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Crm.Projects
{
    public class EfCoreProjectEmployeeRepository
        : EfCoreRepository<CrmDbContext, ProjectEmployee, Guid>, IProjectEmployeeRepository
    {
        public EfCoreProjectEmployeeRepository(IDbContextProvider<CrmDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<ProjectEmployee>> GetListAllAsync(
            Guid? projectId = null,
            Guid? employeeId = null,
            string? sorting = null,
            int maxResults = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();

            if (projectId != null)
                query = query.Where(pe => pe.ProjectId == projectId);

            if (employeeId != null)
                query = query.Where(pe => pe.EmployeeId == employeeId);

            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? "CreationTime desc" : sorting);

            return await query
                .Skip(skipCount)
                .Take(maxResults)
                .ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            Guid? projectId = null,
            Guid? employeeId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();

            if (projectId != null)
                query = query.Where(pe => pe.ProjectId == projectId);

            if (employeeId != null)
                query = query.Where(pe => pe.EmployeeId == employeeId);

            return await query.LongCountAsync(cancellationToken);
        }
    }
}
