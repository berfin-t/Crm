using Crm.Projects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Crm.Tasks
{
    [RemoteService(IsEnabled = false)]
    //[Authorize(CrmPermissions.Tasks.Default)]
    public class TaskAppService(ITaskRepository taskRepository,
        TaskManager taskManager) : CrmAppService, ITaskAppService
    {
        #region Create
        public async Task<TaskDto> CreateAsync(TaskCreateDto input)
        {
            var task = await taskManager.CreateAsync(
                input.Title, input.Description, input.DueDate, input.Priority, input.Status, input.CustomerId, input.EmployeeId);

            return ObjectMapper.Map<Task, TaskDto>(task);
        }
        #endregion

        #region Get
        public async Task<TaskDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Task, TaskDto>(await taskRepository.GetAsync(id));
        }
        #endregion

        #region GetListAll
        public async Task<List<TaskDto>> GetListAllAsync()
        {
            var items = await taskRepository.GetListAsync();
            return ObjectMapper.Map<List<Task>, List<TaskDto>>(items);
        }
        #endregion

        #region GetListPaged
        public async Task<PagedResultDto<TaskDto>> GetListAsync(GetPagedTasksInput input)
        {
            var totalCount = await taskRepository.GetCountAsync(
                input.Title, input.Description, input.DueDate, input.Priorities, input.Statuses, input.CustomerId, input.EmployeeId);

            var items = await taskRepository.GetListAsync(
                input.Title, input.Description, input.DueDate, input.Priorities, input.Statuses, input.CustomerId, input.EmployeeId);

            return new PagedResultDto<TaskDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Task>, List<TaskDto>>(items)
            };
        }
        #endregion

        #region Update
        public async Task<TaskDto> UpdateAsync(Guid id, TaskUpdateDto input)
        {
            var task = await taskManager.UpdateAsync(
                id, input.Title, input.Description, input.DueDate, input.Priority, input.Status, input.CustomerId, input.EmployeeId);

            return ObjectMapper.Map<Task, TaskDto>(task);
        }
        #endregion

        #region GetTotalTaskCount
        public async Task<long> GetTotalTaskCountAsync()
        {
            return await taskRepository.GetCountAsync();
        }
        #endregion
    }
}
