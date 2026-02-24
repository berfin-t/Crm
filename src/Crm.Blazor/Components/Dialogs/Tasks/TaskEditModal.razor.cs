using Blazorise;
using Crm.Activities;
using Crm.Common;
using Crm.Customers;
using Crm.Employees;
using Crm.Projects;
using Crm.Tasks;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crm.Blazor.Components.Dialogs.Tasks
{
    public partial class TaskEditModal
    {
        #region reference to the modal component
        private Modal? modalRef;
        private TaskUpdateDto TaskUpdateDto { get; set; } = new TaskUpdateDto();
        private EnumStatus selectedStatus = Enum.GetValues(typeof(EnumStatus)).Cast<EnumStatus>().FirstOrDefault();
        private EnumPriority selectedPriority = Enum.GetValues(typeof(EnumPriority)).Cast<EnumPriority>().FirstOrDefault();
        private DateTime? selectedDueDate { get; set; }
        private Guid selectedEmployeeId { get; set; }
        private Guid selectedProjectId { get; set; }
        private List<EmployeeDto> EmployeeList { get; set; } = new();
        private List<ProjectDto> ProjectList { get; set; } = new();
        private EventCallback EventCallback { get; set; }
        #endregion

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            EmployeeList = await EmployeeAppService.GetListAllAsync();
            ProjectList = await ProjectAppService.GetListAllAsync();
        }

        #region Modal Methods
        public async System.Threading.Tasks.Task ShowModal(TaskDto task, EventCallback eventCallback)
        {
            EventCallback = eventCallback;

            if (task != null)
            {
                TaskUpdateDto = new TaskUpdateDto   
                {
                    Id = task.Id,
                    Name = task.Name!,
                    Description = task.Description!,
                    Status = task.Status,
                    Priority = task.Priority,
                    DueDate = task.DueDate,
                    EmployeeId = task.EmployeeId,
                    ProjectId = task.ProjectId
                };

                selectedStatus = task.Status;
                selectedPriority = task.Priority;
                selectedDueDate = task.DueDate;
                selectedEmployeeId = task.EmployeeId;
                selectedProjectId = task.ProjectId;
            }

            StateHasChanged(); 
            await modalRef!.Show();
        }
        private System.Threading.Tasks.Task HideModal()
        {
            return modalRef!.Hide();
        }
        #endregion

        #region Update Task
        private async System.Threading.Tasks.Task UpdateTaskAsync()
        {
            try
            {
                TaskUpdateDto.Status = selectedStatus;
                TaskUpdateDto.DueDate = selectedDueDate ?? DateTime.MinValue; ;
                TaskUpdateDto.Priority = selectedPriority;
                TaskUpdateDto.EmployeeId = selectedEmployeeId;
                TaskUpdateDto.ProjectId = selectedProjectId;

                await TaskAppService.UpdateAsync(TaskUpdateDto.Id, TaskUpdateDto);
                await HideModal();
                await EventCallback.InvokeAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
