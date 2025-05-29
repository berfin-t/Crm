using Crm.Activities;
using Crm.Blazor.Components.Dialogs.Activities;
using Crm.Projects;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Activities
{
    public partial class Activity
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter]  public Guid ActivityId { get; set; }

        private List<ActivityDto> activityList = new();
        private bool isActivityModalVisible = false;
        private ActivityDto selectedActivity;
        private bool isDeleteModalVisible = false;
        private ActivityCreateModal activityCreateModal;

        private EventCallback reloadActivitiesCallback => EventCallback.Factory.Create(this, ReloadActivities);

        protected override async Task OnInitializedAsync()
        {
            var allActivities = await ActivityAppService.GetListAllAsync();
            if (allActivities != null)
            {
                activityList = allActivities.Where(a => a.Date > DateTime.Now).ToList();
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
                await activityCreateModal.ShowModal(reloadActivitiesCallback);
            }
        }
        private void ShowEditModal(ActivityDto activity)
        {
            selectedActivity = activity;
            isActivityModalVisible = true;
        }

        private void EditActivity(ActivityDto activity)
        {
            Console.WriteLine($"Edit clicked for activity ID: {activity.Id}");
        }
    
        private async Task ConfirmDelete()
        {
            if (selectedActivity != null && selectedActivity.Id != Guid.Empty)
            {
                await ActivityAppService.DeleteAsync(selectedActivity.Id);
                isDeleteModalVisible = false;
                isActivityModalVisible = false;

                NavigationManager.NavigateTo("/activities", forceLoad: true);
            }
        }

    }
}
