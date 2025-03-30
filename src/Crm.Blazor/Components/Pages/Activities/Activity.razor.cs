using Crm.Activities;
using Crm.Blazor.Components.Dialogs.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Activities
{
    public partial class Activity
    {
        private List<ActivityDto> activityList = new();
        private ActivityEditModal activityEditModal;

        protected override async Task OnInitializedAsync()
        {
            var allActivities = await ActivityAppService.GetListAllAsync();
            if (activityList != null)
            {
                activityList = allActivities.Where(a => a.Date > DateTime.Now).ToList();
            }
        }

        private async Task ShowEditModal(ActivityDto activity)
        {
            if (activityEditModal != null)
            {
                await activityEditModal.ShowModal(activity);
            }
        }
    }
}

