using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Crm.Activities
{
    [RemoteService(IsEnabled = false)]
    //[Authorize(CrmPermissions.Activities.Default)]
    public class ActivityAppService(IActivityRepository activityRepository,
        ActivityManager activityManager) : CrmAppService, IActivityAppService
    {
        #region Create
        //[Authorize(CrmPermissions.Activities.Create)]
        public async Task<ActivityDto> CreateAsync(ActivityCreateDto input)
        {
            var activity = await activityManager.CreateAsync(
                input.CustomerId, input.EmployeeId, input.Type,
                input.Description, input.Date);

            return ObjectMapper.Map<Activity, ActivityDto>(activity);
        }
        #endregion

        #region Get
        public async Task<ActivityDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Activity, ActivityDto>(await activityRepository.GetAsync(id));
        }
        #endregion

        #region GetListAll
        public async Task<List<ActivityDto>> GetListAllAsync()
        {
            var items = await activityRepository.GetListAsync();
            var sortedItems = items.AsQueryable().OrderByDescending(a => a.Date).ToList();
            return ObjectMapper.Map<List<Activity>, List<ActivityDto>>(sortedItems);
        }
        #endregion

        #region GetListPaged
        public virtual async Task<PagedResultDto<ActivityDto>> GetListAsync(GetPagedActivitiesInput input)
        {
            var totalCount = await activityRepository.GetCountAsync(
                input.Type, input.Description, input.Date,
                input.CustomerId, input.EmployeeId);

            var items = await activityRepository.GetListAllAsync(
                input.Type, input.Description, input.Date,
                input.CustomerId, input.EmployeeId);

            return new PagedResultDto<ActivityDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Activity>, List<ActivityDto>>(items)
            };
        }
        #endregion

        #region Update
        //[Authorize(CrmPermissions.Activities.Update)]
        public async Task<ActivityDto> UpdateAsync(Guid id, ActivityUpdateDto input)
        {
            var avtivity = await activityManager.UpdateAsync(
                id, input.CustomerId, input.EmployeeId, input.Type,
                input.Description, input.Date);

            return ObjectMapper.Map<Activity, ActivityDto>(avtivity);
        }       
        #endregion
    }
}
