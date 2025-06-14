using Asp.Versioning;
using Crm.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Crm.Controllers.Tasks
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Task")]
    [Route("api/app/tasks")]
    public class TaskController:CrmController, ITaskAppService
    {
        protected ITaskAppService _taskAppService;
        public TaskController(ITaskAppService taskAppService) => _taskAppService = taskAppService;

        [HttpGet]
        [Route("{id}")]
        public virtual Task<TaskDto> GetAsync(Guid id) => _taskAppService.GetAsync(id);

        [HttpGet]
        [Route("all")]
        public virtual Task<List<TaskDto>> GetListAllAsync() => _taskAppService.GetListAllAsync();

        [HttpGet]
        [Route("paged")]
        public virtual Task<PagedResultDto<TaskDto>> GetListAsync(GetPagedTasksInput input) => _taskAppService.GetListAsync(input);

        [HttpPost]
        [Route("create")]
        public virtual Task<TaskDto> CreateAsync(TaskCreateDto input) => _taskAppService.CreateAsync(input);

        [HttpPut]
        [Route("update/{id}")]
        public virtual Task<TaskDto> UpdateAsync(Guid id, TaskUpdateDto input) => _taskAppService.UpdateAsync(id, input);

        [HttpGet]
        [Route("count")]
        public virtual Task<long> GetTotalTaskCountAsync() => _taskAppService.GetTotalTaskCountAsync();

        [HttpGet]
        [Route("count/project/{projectId}")]
        public virtual Task<long> GetTotalTaskCountByProjectIdAsync(Guid projectId) => _taskAppService.GetTotalTaskCountByProjectIdAsync(projectId);

        [HttpGet]
        [Route("completed/project/{projectId}")]
        public virtual Task<long> GetCompletedTasksByProjectId(Guid projectId) => _taskAppService.GetCompletedTasksByProjectId(projectId);
    }
    
}
