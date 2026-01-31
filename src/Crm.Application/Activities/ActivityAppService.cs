using Crm.Employees;
using Crm.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Crm.Activities
{
    [RemoteService(IsEnabled = false)]
    public class ActivityAppService(IActivityRepository activityRepository, IEmployeeRepository employeeRepository,
        ActivityManager activityManager) : CrmAppService, IActivityAppService
    {
        #region Create
        [Authorize(CrmPermissions.Activities.Create)]
        public async Task<ActivityDto> CreateAsync(ActivityCreateDto input)
        {
            var activity = await activityManager.CreateAsync(
                input.CustomerId, input.EmployeeId, input.Type,
                input.Description!, input.Date);

            return ObjectMapper.Map<Activity, ActivityDto>(activity);
        }
        #endregion

        //#region Get
        //[AllowAnonymous]
        //public async Task<ActivityDto> GetAsync(Guid id)
        //{
        //    return ObjectMapper.Map<Activity, ActivityDto>(await activityRepository.GetAsync(id));
        //}
        //#endregion

        #region GetListAll
        [AllowAnonymous]
        public async Task<List<ActivityDto>> GetListAllAsync()
        {
            var items = await activityRepository.GetListAsync();
            var sortedItems = items.AsQueryable().OrderByDescending(a => a.Date).ToList();
            return ObjectMapper.Map<List<Activity>, List<ActivityDto>>(sortedItems);
        }
        #endregion

        #region GetListByCustomerAsync
        public async Task<List<ActivityDto>> GetListByCustomerAsync(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new Exception("CustomerId cannot be empty.");
            }
            var items = await activityRepository.GetListByCustomerAsync(customerId);
            return ObjectMapper.Map<List<Activity>, List<ActivityDto>>(items);
        }
        #endregion

        #region GetListByEmployeeAsync
        [Authorize]
        public async Task<List<ActivityDto>> GetListByEmployeeAsync()
        {
            var employee = await employeeRepository
                .FirstOrDefaultAsync(e => e.UserId == CurrentUser.Id);

            if (employee == null)
            {
                var allItems = await activityRepository.GetListAsync();
                return ObjectMapper.Map<List<Activity>, List<ActivityDto>>(allItems);
            }

            var items = await activityRepository
                .GetListByEmployeeIdAsync(employee.Id);

            return ObjectMapper.Map<List<Activity>, List<ActivityDto>>(items);
        }
        #endregion

        //#region GetListPaged
        //[AllowAnonymous]
        //public virtual async Task<PagedResultDto<ActivityDto>> GetListAsync(GetPagedActivitiesInput input)
        //{
        //    var totalCount = await activityRepository.GetCountAsync(
        //        input.Type, input.Description, input.Date,
        //        input.CustomerId, input.EmployeeId);

        //    var items = await activityRepository.GetListAllAsync(
        //        input.Type, input.Description, input.Date,
        //        input.CustomerId, input.EmployeeId);

        //    return new PagedResultDto<ActivityDto>
        //    {
        //        TotalCount = totalCount,
        //        Items = ObjectMapper.Map<List<Activity>, List<ActivityDto>>(items)
        //    };
        //}
        //#endregion

        #region Update
        [Authorize(CrmPermissions.Activities.Edit)]
        public async Task<ActivityDto> UpdateAsync(Guid id, ActivityUpdateDto input)
        {
            var avtivity = await activityManager.UpdateAsync(
                id, input.CustomerId, input.EmployeeId, input.Type,
                input.Description!, input.Date);

            return ObjectMapper.Map<Activity, ActivityDto>(avtivity);
        }
        #endregion

        #region Delete
        [Authorize(CrmPermissions.Activities.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var activity = await activityRepository.GetAsync(id);
            if (activity == null)
            {
                throw new EntityNotFoundException(typeof(Activity), id);
            }
            activity.IsDeleted = true;
            await activityRepository.DeleteAsync(activity);
        }
        #endregion

        #region GetWithNavigationProperties
        [AllowAnonymous]
        public virtual async Task<ActivityWithNavigationPropertyDto> GetWithNavigationPropertiesAsync(Guid id) =>
        ObjectMapper.Map<ActivityWithNavigationProperties, ActivityWithNavigationPropertyDto>
            (await activityRepository.GetWithNavigationPropertiesAsync(id));

        #endregion
    }
}
