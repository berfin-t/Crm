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
        private string customerName;

        private List<ActivityDto> activityList = new();
        private bool isActivityModalVisible = false;
        private ActivityDto selectedActivity;
        private ActivityWithNavigationPropertyDto selectedActivityWithNav;
        private bool isDeleteModalVisible = false;
        private ActivityCreateModal activityCreateModal;

        private EventCallback EventCallback => EventCallback.Factory.Create(this, OnInitializedAsync);

        protected override async Task OnInitializedAsync()
        {
            var allActivities = await ActivityAppService.GetListAllAsync();
            if (allActivities != null)
            {
                activityList = allActivities.Where(a => a.Date > DateTime.Now).ToList();
            }
            //var activityWithNav = await ActivityAppService.GetWithNavigationPropertiesAsync(ActivityId);
            //if (activityWithNav != null)
            //{
            //    customerName = activityWithNav.Customer?.Name; 
            //}
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
        private async Task ShowEditModal(ActivityDto activity)
        {
            selectedActivityWithNav = await ActivityAppService.GetWithNavigationPropertiesAsync(activity.Id);
            selectedActivity = selectedActivityWithNav.Activity;
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
