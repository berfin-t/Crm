using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Crm.Activities
{
    public class ActivityManager(IActivityRepository activityRepository) : DomainService
    {
        #region Create
        public virtual async Task<Activity> CreateAsync(Guid customerId, Guid employeeId, EnumType type, string description, DateTime date)
        {
            var activity = new Activity(
                GuidGenerator.Create(),
                type,
                description,
                date,
                customerId,
                employeeId
            );
            return await activityRepository.InsertAsync(activity);
        }
        #endregion

        #region Update
        public virtual async Task<Activity> UpdateAsync(Guid id, Guid customerId, Guid employeeId, EnumType type, string description, DateTime date)
        {
            var activity = await activityRepository.GetAsync(id);
            activity.SetType(type);
            activity.SetDescription(description);
            activity.SetDate(date);
            activity.SetCustomerId(customerId);
            activity.SetEmployeeId(employeeId);
            return await activityRepository.UpdateAsync(activity);
        }
        #endregion
    }
}
