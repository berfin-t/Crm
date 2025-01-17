using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Crm.Tasks
{
    public interface ITaskAppService:IApplicationService
    {
        Task<PagedResultDto<TaskDto>> GetListAsync(GetPagedTasksInput input);
        Task<List<TaskDto>> GetListAllAsync();
        Task<TaskDto> GetAsync(Guid id);
        Task<TaskDto> CreateAsync(TaskCreateDto input);
        Task<TaskDto> UpdateAsync(Guid id, TaskUpdateDto input);
    }
}
