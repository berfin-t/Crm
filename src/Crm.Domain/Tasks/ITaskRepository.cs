using Crm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Crm.Tasks
{
    public interface ITaskRepository:IRepository<Task, Guid>
    {
        Task<List<Task>> GetListAsync(string? title=null, string? description = null,
            DateTime? dueDate = null, ICollection<EnumPriority>? priorities=null, 
            ICollection<EnumStatus>? statues = null,  Guid? employeeId = null,
            Guid? projectId = null,  string? sorting = null, 
            int maxResults = int.MaxValue, int skipCount = 0,
            CancellationToken cancellationToken=default);
        Task<long> GetCountAsync(string? title = null, string? description = null,
            DateTime? dueDate = null, ICollection<EnumPriority>? priorities = null,
            ICollection<EnumStatus>? statues = null, Guid? employeeId = null,
            Guid? projectId = null, CancellationToken cancellationToken=default);
    }
}
