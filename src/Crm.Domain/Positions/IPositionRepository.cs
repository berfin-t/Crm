using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Crm.Positions
{
    public interface IPositionRepository:IRepository<Position, Guid>
    {
        Task<List<Position>> GetListAsync(string? name = null, string? description = null,
            decimal? salary = null, int? minExperience = null, int? maxExperience = null,
            string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0,
            CancellationToken cancellationToken=default);
        Task<long> GetCountAsync(string? name = null, string? description = null,
            decimal? salary = null, int? minExperience = null, int? maxExperience = null,
            CancellationToken cancellationToken=default);
    }
}
