using Asp.Versioning;
using Crm.Projects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Crm.Controllers.ProjectEmployee
{
    [RemoteService]
    [Area("app")]
    [ControllerName("ProjectEmployee")]
    [Route("api/app/projectemployees")]
    public class ProjectEmployeeController:CrmController, IProjectEmployeeAppService
    {
        protected IProjectEmployeeAppService _projectEmployeeAppService;

        public ProjectEmployeeController(IProjectEmployeeAppService projectEmployeeAppService) => _projectEmployeeAppService = projectEmployeeAppService;

        [HttpPost]
        [Route("create")]
        public virtual Task<ProjectDto> CreateAsync(ProjectCreateDto input) => _projectEmployeeAppService.CreateAsync(input);
    }
}
