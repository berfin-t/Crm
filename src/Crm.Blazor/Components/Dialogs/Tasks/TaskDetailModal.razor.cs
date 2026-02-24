using Blazorise;
using Crm.Common;
using Crm.Tasks;
using Microsoft.AspNetCore.Components;

namespace Crm.Blazor.Components.Dialogs.Tasks
{
    public partial class TaskDetailModal
    {
        private Modal? modalRef;
        [Parameter] public TaskDto? Task { get; set; }
        private TaskDto? selectedTask;
        private TaskEditModal? taskEditModal;        
        [Parameter] public EventCallback OnUpdated { get; set; }

        public void Show() => modalRef?.Show();
        public void Hide() => modalRef?.Hide();

        #region Edit
        private async System.Threading.Tasks.Task OnEditClicked()
        {
            if (Task != null)
                await taskEditModal!.ShowModal(Task, OnUpdated);
        }
        #endregion

        private string GetPriorityTextColor()
        {
            if (Task == null)
                return "#111827";

            return Task.Priority switch
            {
                EnumPriority.Low => "#00FF11",
                EnumPriority.Medium => "#EEFF00",
                EnumPriority.High => "#FF7B00",
                EnumPriority.Critical => "#FF0000",
                _ => "#111827"
            };
        }
    }
}
