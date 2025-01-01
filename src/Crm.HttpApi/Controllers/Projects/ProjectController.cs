using Asp.Versioning;
using Crm.Projects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Crm.Controllers.Projects
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Project")]
    [Route("api/app/projects")]
    public class ProjectController:CrmController, IProjectAppService
    {
        protected IProjectAppService projectAppService;

        public ProjectController(IProjectAppService projectAppService) => projectAppService = projectAppService;
        
        [HttpGet]
        [Route("{id}")]
        public Task<ProjectDto> GetAsync(Guid id) => projectAppService.GetAsync(id);
        
        [HttpGet]
        [Route("all")]
        public Task<List<ProjectDto>> GetListAllAsync() => projectAppService.GetListAllAsync();
       
        [HttpGet]
        [Route("paged")]
        public Task<PagedResultDto<ProjectDto>> GetListAsync(GetPagedProjectsInput input) => projectAppService.GetListAsync(input);
        
        [HttpPost]
        [Route("create")]
        public Task<ProjectDto> CreateAsync(ProjectCreateDto input) => projectAppService.CreateAsync(input);
        
        [HttpPut]
        [Route("update/{id}")]
        public Task<ProjectDto> UpdateAsync(Guid id, ProjectUpdateDto input) => projectAppService.UpdateAsync(id, input);
        

    }
}
