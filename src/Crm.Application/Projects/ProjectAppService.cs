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
    public class ProjectAppService : CrmAppService, IProjectAppService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ProjectManager _projectManager;

        public ProjectAppService(
            IProjectRepository projectRepository,
            IMapper mapper,
            ProjectManager projectManager)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _projectManager = projectManager;
        }

        #region GetListPaged
        [AllowAnonymous]
        public virtual async Task<PagedResultDto<ProjectDto>> GetListAsync(GetPagedProjectsInput input)
        {
            var totalCount = await _projectRepository.GetCountAsync(
                input.Name, input.Description, input.StartTime,
                input.EndTime, input.Statues,
                input.Revenue, input.SuccesRate,
                input.EmployeeIds, input.CustomerId);

            var items = await _projectRepository.GetListAllAsync(
                input.Name, input.Description, input.StartTime,
                input.EndTime, input.Statues,
                input.Revenue, input.SuccesRate,
                input.EmployeeIds, input.CustomerId,
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
            var items = await _projectRepository.GetListAsync();
            return _mapper.Map<List<ProjectDto>>(items);
        }
        #endregion

        #region Get
        [AllowAnonymous]
        public virtual async Task<ProjectDto> GetAsync(Guid id)
        {
            var project = await _projectRepository.GetAsync(id);
            return _mapper.Map<ProjectDto>(project);
        }
        #endregion

        #region Create
        [Authorize(CrmPermissions.Projects.Create)]
        public virtual async Task<ProjectDto> CreateAsync(ProjectCreateDto input)
        {
            var project = await _projectManager.CreateAsync(
                input.EmployeeIds,       // List<Guid> artık
                input.CustomerId,
                input.Name,
                input.StartTime!.Value,
                input.EndTime!.Value,
                input.Statues,
                input.Revenue,
                input.SuccesRate,
                input.Description);

            return _mapper.Map<ProjectDto>(project);
        }
        #endregion

        #region Update
        [Authorize(CrmPermissions.Projects.Edit)]
        public virtual async Task<ProjectDto> UpdateAsync(Guid id, ProjectUpdateDto input)
        {
            var project = await _projectManager.UpdateAsync(
                id,
                input.EmployeeIds,       // List<Guid> artık
                input.CustomerId,
                input.Name!,
                input.StartTime!.Value,
                input.EndTime!.Value,
                input.Status,
                input.Revenue,
                input.SuccessRate,
                input.Description);

            return _mapper.Map<ProjectDto>(project);
        }
        #endregion

        #region GetTotalProjectCount
        [AllowAnonymous]
        public async Task<long> GetTotalProjectCountAsync()
        {
            return await _projectRepository.GetCountAsync();
        }
        #endregion

        #region GetSuccessRateAverage
        [AllowAnonymous]
        public async Task<decimal> GetSuccessRateAverageAsync(decimal? successRate = null)
        {
            return await _projectRepository.GetSuccessRateAverageAsync(successRate);
        }
        #endregion

        #region Delete
        [Authorize(CrmPermissions.Projects.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var project = await _projectRepository.GetAsync(id);
            if (project == null)
                throw new BusinessException("Project not found");

            project.IsDeleted = true;
            await _projectRepository.UpdateAsync(project);
        }
        #endregion

        #region Search By Name
        [AllowAnonymous]
        public virtual async Task<List<ProjectDto>> SearchByNameAsync(string name)
        {
            var query = await _projectRepository.GetQueryableAsync();
            var filteredQuery = _projectRepository.ApplyDataFilters(query, name: name);
            var projects = filteredQuery.ToList();
            return _mapper.Map<List<ProjectDto>>(projects);
        }
        #endregion
    }
}
