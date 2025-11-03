using AutoMapper;
using Crm.Common;
using Crm.Permissions;
using Crm.Projects;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Crm.Tasks
{
    [RemoteService(IsEnabled = false)]
    [Authorize(CrmPermissions.Tasks.Menu)]
    public class TaskAppService(ITaskRepository taskRepository, IMapper _mapper,
        TaskManager taskManager) : CrmAppService, ITaskAppService
    {
        #region Create
        public async Task<TaskDto> CreateAsync(TaskCreateDto input)
        {
            var task = await taskManager.CreateAsync(
                input.Name, input.Description, input.DueDate, input.Priority, input.Status, input.ProjectId, input.EmployeeId);

            return _mapper.Map<TaskDto>(task);
        }
        #endregion

        #region Get
        public async Task<TaskDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Task, TaskDto>(await taskRepository.GetAsync(id));
        }
        #endregion

        #region GetListAll
        [AllowAnonymous]
        public async Task<List<TaskDto>> GetListAllAsync()
        {
            var items = await taskRepository.GetListAsync();            
            return _mapper.Map<List<TaskDto>>(items);
        }
        #endregion

        #region GetListPaged
        [AllowAnonymous]
        public async Task<PagedResultDto<TaskDto>> GetListAsync(GetPagedTasksInput input)
        {
            var totalCount = await taskRepository.GetCountAsync(
                input.Title, input.Description, input.DueDate, input.Priorities, input.Statuses, input.ProjectId, input.EmployeeId);

            var items = await taskRepository.GetListAsync(
                input.Title, input.Description, input.DueDate, input.Priorities, input.Statuses, input.ProjectId, input.EmployeeId);

            return new PagedResultDto<TaskDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Task>, List<TaskDto>>(items)
            };
        }
        #endregion

        #region Update
        [AllowAnonymous]
        public async Task<TaskDto> UpdateAsync(Guid id, TaskUpdateDto input)
        {
            var task = await taskManager.UpdateAsync(
                id, input.Name, input.Description, input.DueDate, input.Priority, input.Status, input.ProjectId, input.EmployeeId);

            return ObjectMapper.Map<Task, TaskDto>(task);
        }
        #endregion

        #region GetTotalTaskCount
        [AllowAnonymous]
        public async Task<long> GetTotalTaskCountAsync()
        {
            return await taskRepository.GetCountAsync();
        }
        #endregion

        #region GetTotalTaskCountByProjectId
        [AllowAnonymous]
        public async Task<long> GetTotalTaskCountByProjectIdAsync(Guid projectId)
        {
            return await taskRepository.GetCountAsync(projectId: projectId);
        }
        #endregion

        #region GetCompletedTasksByProjectId
        [AllowAnonymous]
        public async Task<long> GetCompletedTasksByProjectId(Guid projectId)
        {
            return await taskRepository.GetCountAsync(
                statues: new List<EnumStatus> { EnumStatus.Completed }, projectId: projectId);
        }
        #endregion

        #region UpdateStatus
        [AllowAnonymous]
        public async Task<TaskDto> UpdateStatusAsync(Guid id, EnumStatus newStatus)
        {
            var task = await taskRepository.GetAsync(id);
            task.SetStatus(newStatus);

            await taskRepository.UpdateAsync(task);
            return _mapper.Map<TaskDto>(task);
        }
        #endregion
    }
}
