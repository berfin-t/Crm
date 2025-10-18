using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Crm.Projects
{
    public interface IProjectAppService:IApplicationService
    {
        Task<PagedResultDto<ProjectDto>> GetListAsync(GetPagedProjectsInput input);
        Task<List<ProjectDto>> GetListAllAsync();
        Task<ProjectDto> GetAsync(Guid id);
        Task<ProjectDto> CreateAsync(ProjectCreateDto input);
        Task<ProjectDto> UpdateAsync(Guid id, ProjectUpdateDto input);
        Task<long> GetTotalProjectCountAsync();
        Task<decimal> GetSuccessRateAverageAsync(decimal? succesRate);
        Task DeleteAsync(Guid id);
        Task<List<ProjectDto>> SearchByNameAsync(string name);
    }
}
