using Crm.Activities;
using Crm.Blazor.Components.Dialogs.Activities;
using Crm.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Activities
{
    public partial class Activity
    {
        #region Referances 
        private List<ActivityDto> activityList = new();

        private bool isActivityModalVisible;
        private bool isDeleteModalVisible;

        private ActivityDto? selectedActivity;
        private ActivityWithNavigationPropertyDto? selectedActivityWithNav;

        private ActivityCreateModal? activityCreateModal;
        private ActivityEditModal? activityEditModal;

        private bool canCreateActivity;
        private bool canEditActivity;
        private bool canDeleteActivity;

        private EventCallback RefreshCallback =>
            EventCallback.Factory.Create(this, ReloadActivitiesAsync);
        #endregion

        protected override async Task OnInitializedAsync()
        {
            canCreateActivity = await AuthorizationService.IsGrantedAnyAsync(CrmPermissions.Activities.Create);
            canEditActivity = await AuthorizationService.IsGrantedAnyAsync(CrmPermissions.Activities.Edit);
            canDeleteActivity = await AuthorizationService.IsGrantedAnyAsync(CrmPermissions.Activities.Delete);

            await ReloadActivitiesAsync();
        }

        #region Reload Activities
        private async Task ReloadActivitiesAsync()
        {
            var activities = await ActivityAppService.GetListByEmployeeAsync();

            activityList = activities
                .Where(a => a.Date > DateTime.Now)
                .OrderBy(a => a.Date)
                .ToList();
        }
        #endregion

        #region Modal
        private async Task ShowCreateModal()
        {
            if (activityCreateModal != null)
            {
                await activityCreateModal.ShowModal(RefreshCallback);
            }
        }

        private async Task ShowDetailModal(ActivityDto activity)
        {
            selectedActivityWithNav =
                await ActivityAppService.GetWithNavigationPropertiesAsync(activity.Id);

            selectedActivity = selectedActivityWithNav.Activity;
            isActivityModalVisible = true;
        }
        private async Task CloseDetailModal()
        {
            isActivityModalVisible = false;
            await ReloadActivitiesAsync();
        }
        #endregion

        #region Edit
        private async Task EditActivity(ActivityDto activity)
        {
            await activityEditModal!.ShowModal(activity, RefreshCallback);
        }
        #endregion

        #region Delete
        private async Task ConfirmDelete()
        {
            if (selectedActivity == null) return;

            await ActivityAppService.DeleteAsync(selectedActivity.Id);

            isDeleteModalVisible = false;
            isActivityModalVisible = false;

            await ReloadActivitiesAsync();
        }
        #endregion
    }
}
