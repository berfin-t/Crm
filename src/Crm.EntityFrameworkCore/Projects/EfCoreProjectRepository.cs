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
        public virtual async Task<List<Project>> GetListAllAsync(string? filterText = null,
            string? name = null, string? description = null,
            DateTime? startTime = null, DateTime? endTime = null,
            ICollection<EnumStatus>? statues = null, decimal? revenue = null,
            decimal? succesRate = null, Guid? employeeId = null, Guid? customerId = null,
            string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ProjectConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResults).ToListAsync(cancellationToken);
        }
        #endregion

        #region GetCount 
        public virtual async Task<long> GetCountAsync(string? filterText = null,
            string? name = null, string? description = null,
            DateTime? startTime = null, DateTime? endTime = null,
            ICollection<EnumStatus>? statues = null, decimal? revenue = null,
            decimal? succesRate = null, Guid? employeeId = null,
            Guid? customerId = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            return await query.LongCountAsync(cancellationToken);
        }

        public Task<List<Project>> GetListAsync(string? name = null, string? description = null, DateTime? startTime = null, DateTime? endTime = null, ICollection<EnumStatus>? statues = null, decimal? revenue = null, decimal? succesRate = null, Guid? employeeId = null, Guid? customerId = null, string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetCountAsync(string? name = null, string? description = null, DateTime? startTime = null, DateTime? endTime = null, ICollection<EnumStatus>? statues = null, decimal? revenue = null, decimal? succesRate = null, Guid? employeeId = null, Guid? customerId = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
