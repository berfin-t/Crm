using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Crm.Projects
{
    public interface IProjectRepository : IRepository<Project, Guid>
    {
        Task<List<Project>> GetListAsync(string? name = null, string? description = null,
            DateTime? startTime = null, DateTime? endTime = null,
            ICollection<ProjectStatus>? statues = null, decimal? revenue = null,
            decimal? succesRate = null, Guid? userId = null, Guid? customerId = null,
            string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(string? name = null, string? description = null,
            DateTime? startTime = null, DateTime? endTime = null,
            ICollection<ProjectStatus>? statues = null, decimal? revenue = null,
            decimal? succesRate = null, Guid? userId = null,
            Guid? customerId = null, CancellationToken cancellationToken = default);

           }
}
