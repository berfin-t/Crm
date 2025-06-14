using Crm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Crm.Projects
{
    public interface IProjectEmployeeRepository:IRepository<ProjectEmployee, Guid>
    {
        Task<List<ProjectEmployee>> GetListAllAsync(Guid? projectId = null, Guid? employeeId = null,
            string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(Guid? projectId = null, Guid? eemployeeId = null, CancellationToken cancellationToken = default);

    }
}
