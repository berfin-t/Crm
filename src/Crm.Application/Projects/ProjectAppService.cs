using Crm.Employees;
using Crm.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Uow;
using AutoMapper;


namespace Crm.Projects
{
    [RemoteService(IsEnabled = false)]
    public class ProjectAppService(
        IProjectRepository projectRepository, IMapper _mapper,
        ProjectManager projectManager):CrmAppService, IProjectAppService
    {
        #region GetListPaged
        [AllowAnonymous]
        public virtual async Task<PagedResultDto<ProjectDto>> GetListAsync(GetPagedProjectsInput input)
        {
            var totalCount = await projectRepository.GetCountAsync(
                input.Name, input.Description, input.StartTime,
                input.EndTime, input.Statues,
                input.Revenue, input.SuccesRate,
                input.EmployeeId, input.CustomerId );
            
            var items = await projectRepository.GetListAllAsync(
                input.Name, input.Description, input.StartTime,
                input.EndTime, input.Statues,
                input.Revenue, input.SuccesRate,
                input.EmployeeId, input.CustomerId,
                input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ProjectDto>
            {
                TotalCount = totalCount,
                Items = _mapper.Map<List<ProjectDto>>(items)
            };
        }
        #endregion

        #region GetListAll
        [AllowAnonymous]
        [UnitOfWork]
        public async Task<List<ProjectDto>> GetListAllAsync()
        {
            var items = await projectRepository.GetListAsync();
            return _mapper.Map<List<ProjectDto>>(items);
        }
        #endregion

        #region Get
        [AllowAnonymous]
        public virtual async Task<ProjectDto> GetAsync(Guid id) =>  _mapper.Map<ProjectDto>(await projectRepository.GetAsync(id));
        #endregion

        #region Create
        [Authorize(CrmPermissions.Projects.Create)]
        public virtual async Task<ProjectDto> CreateAsync(ProjectCreateDto input)
        {
            var project = await projectManager.CreateAsync(
                input.EmployeeId, input.CustomerId, input.Name, input.StartTime!.Value, input.EndTime!.Value, input.Statues,
                input.Revenue, input.SuccesRate, input.Description);            

            return _mapper.Map<ProjectDto>(project);
        }
        #endregion

        #region Update
        [Authorize(CrmPermissions.Projects.Edit)]
        public virtual async Task<ProjectDto> UpdateAsync(Guid id, ProjectUpdateDto input)
        {
            var project = await projectManager.UpdateAsync(
                id, input.EmployeeId, input.CustomerId, input.Name!, input.StartTime!.Value, input.EndTime!.Value, input.Status,
                input.Revenue, input.SuccessRate, input.Description);

            return _mapper.Map<ProjectDto>(project);
        }
        #endregion

        #region GetTotalProjectCount
        [AllowAnonymous]
        public async Task<long> GetTotalProjectCountAsync()
        {
            return await projectRepository.GetCountAsync();
        }
        #endregion

        #region GetSuccessRateAverage
        [AllowAnonymous]
        public async Task<decimal> GetSuccessRateAverageAsync(decimal? successRate = null)
        {
            return await projectRepository.GetSuccessRateAverageAsync(successRate);
        }
        #endregion

        #region Delete
        [Authorize(CrmPermissions.Projects.Delete)]
        public virtual async System.Threading.Tasks.Task DeleteAsync(Guid id)
        {
            var project = await projectRepository.GetAsync(id);
            if (project == null)
            {
                throw new BusinessException("Project not found.");
            }

            project.IsDeleted = true;

            await projectRepository.UpdateAsync(project);
        }
        #endregion

        #region Search By Name
        [AllowAnonymous]
        public virtual async Task<List<ProjectDto>> SearchByNameAsync(string name)
        {
            var query = await projectRepository.GetQueryableAsync();
            var filteredQuery = projectRepository.ApplyDataFilters(query, name: name);
            var projects = filteredQuery.ToList();
            return _mapper.Map<List<ProjectDto>>(projects);
        }
        #endregion

    }
}
