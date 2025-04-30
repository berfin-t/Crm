using Blazorise;
using Crm.Common;
using Crm.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Tasks
{
    public partial class Task
    {
        private List<TaskDto> items = new();

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            items = await TaskAppService.GetListAllAsync();
        }

        private System.Threading.Tasks.Task TaskDropped(DraggableDroppedEventArgs<TaskDto> e)
        {
            e.Item.Status = Enum.Parse<EnumStatus>(e.DropZoneName);
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
