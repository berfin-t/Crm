using Crm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Crm.Projects
{
    public interface IProjectRepository : IRepository<Project, Guid>
    {
        Task<List<Project>> GetListAllAsync(string? name = null, string? description = null,
            DateTime? startTime = null, DateTime? endTime = null,
            ICollection<EnumStatus>? statues = null, decimal? revenue = null,
            decimal? succesRate = null, List<Guid>? employeeIds = null, Guid? customerId = null,
            string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(string? name = null, string? description = null,
            DateTime? startTime = null, DateTime? endTime = null,
            ICollection<EnumStatus>? statues = null, decimal? revenue = null,
            decimal? succesRate = null, List<Guid>? employeeIds = null,
            Guid? customerId = null, CancellationToken cancellationToken = default);

        Task<decimal> GetSuccessRateAverageAsync(decimal? successRate = null, CancellationToken cancellationToken = default);

        IQueryable<Project> ApplyDataFilters(
            IQueryable<Project> query,
            string? name = null,
            string? description = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            ICollection<EnumStatus>? statues = null,
            decimal? revenue = null,
            decimal? succesRate = null,
            List<Guid>? employeeIds = null,
            Guid? customerId = null);
    }

}

