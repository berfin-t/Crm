using Blazorise;
using Crm.Common;
using Crm.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crm.Blazor.Components.Pages.Tasks
{
    public partial class Task
    {
        private List<TaskDto> tasks;      

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            tasks = await TaskAppService.GetListAllAsync(); 
        }

        private async System.Threading.Tasks.Task TaskDropped(DraggableDroppedEventArgs<TaskDto> dropTask)
        {
            dropTask.Item.Status = Enum.Parse<EnumStatus>(dropTask.DropZoneName);
            await TaskAppService.UpdateAsync(dropTask.Item.Id, new TaskUpdateDto
            {
                Title = dropTask.Item.Title,
                Description = dropTask.Item.Description,
                DueDate = dropTask.Item.DueDate,
                Priority = dropTask.Item.Priority,
                Status = dropTask.Item.Status,
                EmployeeId = dropTask.Item.EmployeeId,
                CustomerId = dropTask.Item.CustomerId
            });
        }
    }
}
