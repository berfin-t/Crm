using Microsoft.AspNetCore.Components;
using Blazorise;
using Crm.Tasks;
using System;
using System.Collections.Generic;
using Crm.Common;
using Crm.Employees;
using Crm.Projects;

namespace Crm.Blazor.Components.Dialogs.Tasks
{
    public partial class TaskCreateModal
    {
        private Modal? modalRef;
        private EventCallback<TaskDto> EventCallback;
        private Validations? validations;

        private TaskCreateDto TaskCreateDto { get; set; } = new();

        private List<EmployeeDto> Employees { get; set; } = new();
        private List<ProjectDto> Projects { get; set; } = new();

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            Employees = await EmployeeAppService.GetListAllAsync();
            Projects = await ProjectAppService.GetListAllAsync();
        }

        public async System.Threading.Tasks.Task ShowModal(EventCallback<TaskDto> eventCallback)
        {
            EventCallback = eventCallback;

            if (validations is not null)
                await validations.ClearAll();

            TaskCreateDto = new TaskCreateDto
            {
                Name = string.Empty,
                Description = string.Empty,
                DueDate = DateTime.Now,
                Priority = EnumPriority.Medium,
                Status = EnumStatus.Active,
                EmployeeId = Guid.Empty,
                ProjectId = Guid.Empty,
                Group = null
            };

            await modalRef!.Show();
        }

        private System.Threading.Tasks.Task HideModal()
            => modalRef!.Hide();

        private async System.Threading.Tasks.Task CreateTaskAsync()
        {
            if (validations is null)
                return;

            if (!await validations.ValidateAll())
                return;

            // 🔥 CREATED TASK GERİ ALINIYOR
            var createdTask = await TaskAppService.CreateAsync(TaskCreateDto);

            await HideModal();

            if (EventCallback.HasDelegate)
                await EventCallback.InvokeAsync(createdTask);
        }

        private void ValidateEmployee(ValidatorEventArgs e)
        {
            if (e.Value is not Guid value || value == Guid.Empty)
            {
                e.Status = ValidationStatus.Error;
                e.ErrorText = L["Employee is required"];
            }
            else
            {
                e.Status = ValidationStatus.Success;
            }
        }

        private void ValidateProject(ValidatorEventArgs e)
        {
            if (e.Value is not Guid value || value == Guid.Empty)
            {
                e.Status = ValidationStatus.Error;
                e.ErrorText = L["Project is required"];
            }
            else
            {
                e.Status = ValidationStatus.Success;
            }
        }
    }
}
