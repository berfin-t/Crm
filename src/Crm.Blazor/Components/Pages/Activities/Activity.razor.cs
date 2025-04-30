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
        private List<ActivityDto> activityList = new();

        protected override async Task OnInitializedAsync()
        {
            var allActivities = await ActivityAppService.GetListAllAsync();
            if (allActivities != null)
            {
                activityList = allActivities.Where(a => a.Date > DateTime.Now).ToList();
            }
        }

        private bool isActivityModalVisible = false;
        private ActivityDto selectedActivity;

        private void ShowEditModal(ActivityDto activity)
        {
            selectedActivity = activity;
            isActivityModalVisible = true;
        }

        private void EditActivity(ActivityDto activity)
        {
            Console.WriteLine($"Edit clicked for activity ID: {activity.Id}");
        }

        private bool isDeleteModalVisible = false;

        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public Guid ActivityId { get; set; }

        // Silme işlemi
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
