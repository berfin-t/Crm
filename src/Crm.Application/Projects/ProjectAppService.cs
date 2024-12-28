﻿using Crm.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Crm.Projects
{
    [RemoteService(IsEnabled = false)]
    //[Authorize(CrmPermissions.Projects.Default)]
    public class ProjectAppService(
        IProjectRepository projectRepository,
        ProjectManager projectManager):CrmAppService, IProjectAppService
    {
        #region GetListPaged
        public virtual async Task<PagedResultDto<ProjectDto>> GetListAsync(GetPagedProjectsInput input)
        {
            var totalCount = await projectRepository.GetCountAsync(
                input.Name, input.Description, input.StartTime,
                input.EndTime, input.Statues,
                input.Revenue, input.SuccesRate,
                input.UserId, input.CustomerId );
            
            var items = await projectRepository.GetListAsync(
                input.Name, input.Description, input.StartTime,
                input.EndTime, input.Statues,
                input.Revenue, input.SuccesRate,
                input.UserId, input.CustomerId,
                input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ProjectDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Project>, List<ProjectDto>>(items)
            };
        }
        #endregion

        #region GetListAll
        public virtual async Task<List<ProjectDto>> GetListAllAsync()
        {
            var items = await projectRepository.GetListAsync();
            return ObjectMapper.Map<List<Project>, List<ProjectDto>>(items);
        }
        #endregion

        #region Get
        public virtual async Task<ProjectDto> GetAsync(Guid id) =>  ObjectMapper.Map<Project, ProjectDto>(await projectRepository.GetAsync(id));
        #endregion

        #region Create
        //[Authorize(CrmPermissions.Projects.Create)]
        public virtual async Task<ProjectDto> CreateAsync(ProjectCreateDto input)
        {
            var project = await projectManager.CreateAsync(
                input.UserId, input.CustomerId, input.Name, input.StartTime, input.EndTime, input.Statues.FirstOrDefault(),
                input.Revenue, input.SuccesRate, input.Description);            

            return ObjectMapper.Map<Project, ProjectDto>(project);
        }
        #endregion

        #region Update

        //[Authorize(CrmPermissions.Projects.Edit)]
        public virtual async Task<ProjectDto> UpdateAsync(Guid id, ProjectUpdateDto input)
        {
            var project = await projectManager.UpdateAsync(
                id, input.UserId, input.CustomerId, input.Name, input.StartTime, input.EndTime, input.Statues.FirstOrDefault(),
                input.Revenue, input.SuccesRate, input.Description);

            return ObjectMapper.Map<Project, ProjectDto>(project);
        }
        #endregion
    }
}