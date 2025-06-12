using Crm.Activities;
using Crm.Blazor.Components.Dialogs.Activities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Activities
{
    public partial class Activity
    {
        [Inject] public NavigationManager? NavigationManager { get; set; }
        [Parameter]  public Guid ActivityId { get; set; }

        #region reference to the modal component
        private List<ActivityDto> activityList = new();
        private bool isActivityModalVisible = false;
        private ActivityDto? selectedActivity;
        private ActivityWithNavigationPropertyDto? selectedActivityWithNav;
        private bool isDeleteModalVisible = false;
        private ActivityCreateModal? activityCreateModal;
        private ActivityEditModal? activityEditModal;
        private EventCallback EventCallback => EventCallback.Factory.Create(this, OnInitializedAsync);
        #endregion

        protected override async Task OnInitializedAsync()
        {
            var allActivities = await ActivityAppService.GetListAllAsync();
            if (allActivities != null)
            {
                activityList = allActivities.Where(a => a.Date > DateTime.Now).OrderBy(a => a.Date).ToList();
            }            
        }
        public async Task ReloadActivities()
        {
            activityList.Clear();
            await InvokeAsync(StateHasChanged);
        }
        public async Task ShowCreateModal()
        {
            if (activityCreateModal != null)
            {
                await activityCreateModal.ShowModal(EventCallback);
            }
        }

        #region Edit 
        private async Task ShowEditModal(ActivityDto activity)
        {
            selectedActivityWithNav = await ActivityAppService.GetWithNavigationPropertiesAsync(activity.Id);
            selectedActivity = selectedActivityWithNav.Activity;
            isActivityModalVisible = true;
        }        

        private async Task EditActivity(ActivityDto activity)
        {
            await activityEditModal!.ShowModal(activity);
        }
        #endregion

        #region Delete 
        private async Task ConfirmDelete()
        {
            if (selectedActivity != null && selectedActivity.Id != Guid.Empty)
            {
                await ActivityAppService.DeleteAsync(selectedActivity.Id);
                isDeleteModalVisible = false;
                isActivityModalVisible = false;

                NavigationManager?.NavigateTo("/activities", forceLoad: true);
            }
        }
        #endregion
    }
}
