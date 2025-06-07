using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Crm.Activities
{
    public interface IActivityAppService:IApplicationService
    {
        Task<PagedResultDto<ActivityDto>> GetListAsync(GetPagedActivitiesInput input);
        Task<List<ActivityDto>> GetListAllAsync();
        Task<ActivityDto> GetAsync(Guid id);
        Task<ActivityDto> CreateAsync(ActivityCreateDto input);
        Task<ActivityDto> UpdateAsync(Guid id, ActivityUpdateDto input);
        Task DeleteAsync(Guid id);
        Task<ActivityWithNavigationPropertyDto> GetWithNavigationPropertiesAsync(Guid id);
    }
}
