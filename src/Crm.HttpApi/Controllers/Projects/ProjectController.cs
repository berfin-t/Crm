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
        protected IProjectAppService _projectAppService;

        public ProjectController(IProjectAppService projectAppService) => _projectAppService = projectAppService;
        
        [HttpGet]
        [Route("{id}")]
        public virtual Task<ProjectDto> GetAsync(Guid id) => _projectAppService.GetAsync(id);
        
        [HttpGet]
        [Route("all")]
        public virtual Task<List<ProjectDto>> GetListAllAsync() => _projectAppService.GetListAllAsync();
       
        //[HttpGet]
        //[Route("paged")]
        //public virtual Task<PagedResultDto<ProjectDto>> GetListAsync(GetPagedProjectsInput input) => _projectAppService.GetListAsync(input);
        
        [HttpPost]
        [Route("create")]
        public virtual Task<ProjectDto> CreateAsync(ProjectCreateDto input) => _projectAppService.CreateAsync(input);
        
        [HttpPut]
        [Route("update/{id}")]
        public virtual Task<ProjectDto> UpdateAsync(Guid id, ProjectUpdateDto input) => _projectAppService.UpdateAsync(id, input);

        [HttpGet]
        [Route("count")]
        public virtual Task<long> GetTotalProjectCountAsync() => _projectAppService.GetTotalProjectCountAsync();

        [HttpGet]
        [Route("success-rate-average")]
        public virtual Task<decimal> GetSuccessRateAverageAsync(decimal? successRate = null) => _projectAppService.GetSuccessRateAverageAsync(successRate);

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id) => _projectAppService.DeleteAsync(id);

        //[HttpGet]
        //[Route("search")]
        //public virtual Task<List<ProjectDto>> SearchByNameAsync(string name) => _projectAppService.SearchByNameAsync(name);
    }
}
