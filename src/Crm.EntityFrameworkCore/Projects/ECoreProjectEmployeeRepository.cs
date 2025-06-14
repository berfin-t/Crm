using Crm.Common;
using Crm.Employees;
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

namespace Crm.Projects
{
    public class ECoreProjectEmployeeRepository(IDbContextProvider<CrmDbContext> dbContextProvider)
        : EfCoreRepository<CrmDbContext, ProjectEmployee, Guid>(dbContextProvider), IProjectEmployeeRepository
    {
        #region GetListAll
        public async Task<List<ProjectEmployee>> GetListAllAsync(
            Guid? projectId = null,
            Guid? employeeId = null,
            string? sorting = null,
            int maxResults = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();

            if (projectId.HasValue)
            {
                query = query.Where(pe => pe.ProjectId == projectId.Value);
            }

            if (employeeId.HasValue)
            {
                query = query.Where(pe => pe.EmployeeId == employeeId.Value);
            }          

            return await query
                .Skip(skipCount)
                .Take(maxResults)
                .ToListAsync(cancellationToken);
        }
        #endregion

        #region GetCount
        public async Task<long> GetCountAsync(
            Guid? projectId = null,
            Guid? employeeId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();

            if (projectId.HasValue)
            {
                query = query.Where(pe => pe.ProjectId == projectId.Value);
            }

            if (employeeId.HasValue)
            {
                query = query.Where(pe => pe.EmployeeId == employeeId.Value);
            }

            return await query.LongCountAsync(cancellationToken);
        }
        #endregion       

    }
}
