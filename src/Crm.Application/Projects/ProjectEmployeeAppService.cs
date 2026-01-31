using AutoMapper;
using Crm.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Crm.Projects
{
    [RemoteService(IsEnabled = false)]
    public class ProjectEmployeeAppService(
        IMapper _mapper, ProjectEmployeeManager projectEmployeeManager) : CrmAppService, IProjectEmployeeAppService
    {
        #region Create
        [Authorize(CrmPermissions.Projects.Create)]
        public virtual async Task<ProjectDto> CreateAsync(ProjectCreateDto input)
        {
            var project = await projectEmployeeManager.CreateAsync(
                input.EmployeeIds,          
                input.CustomerId,
                input.Name,
                input.StartTime!.Value,
                input.EndTime!.Value,
                input.Statues,
                input.Revenue,
                input.SuccesRate,
                input.Description
            );

            return _mapper.Map<ProjectDto>(project);
        }
        #endregion

    }
}
