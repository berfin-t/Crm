using Blazorise;
using Crm.Common;
using Crm.Customers;
using Crm.Employees;
using Crm.Projects;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Crm.Activities;

namespace Crm.Blazor.Components.Dialogs.Projects
{
    public partial class ProjectEditModal
    {
        #region References    
        private Modal? modalRef;
        private List<EmployeeDto> EmployeeList { get; set; } = new();
        private List<CustomerDto> CustomerList { get; set; } = new();
        private ProjectUpdateDto ProjectUpdateDto { get; set; } = new ProjectUpdateDto();
        private EnumStatus selectedStatus = Enum.GetValues(typeof(EnumStatus)).Cast<EnumStatus>().FirstOrDefault();
        private DateTime? selectedStartTime { get; set; }
        private DateTime? selectedEndTime { get; set; }
        private Guid selectedEmployeeId { get; set; }
        private Guid selectedCustomerId { get; set; }
        private EventCallback EventCallback { get; set; }
        #endregion

        protected override async Task OnInitializedAsync()
        {
            EmployeeList = await EmployeeAppService.GetListAllAsync();
            CustomerList = await CustomerAppService.GetListAllAsync();
        }

        public async Task ShowModal(ProjectDto project, EventCallback eventCallback)
        {
            EventCallback = eventCallback;

            if (project != null)
            {
                ProjectUpdateDto.Id = project.Id;
                ProjectUpdateDto.Name = project.Name;
                ProjectUpdateDto.Description = project.Description;
                ProjectUpdateDto.StartTime = project.StartTime;
                ProjectUpdateDto.EndTime = project.EndTime;
                ProjectUpdateDto.Status = project.Status;
                ProjectUpdateDto.Revenue = project.Revenue;
                ProjectUpdateDto.SuccessRate = project.SuccessRate;
                ProjectUpdateDto.EmployeeId = project.EmployeeId;
                ProjectUpdateDto.CustomerId = project.CustomerId;

                selectedStatus = project.Status;
                selectedStartTime = project.StartTime;
                selectedEndTime = project.EndTime;
                selectedEmployeeId = project.EmployeeId;
                selectedCustomerId = project.CustomerId;
            }
            await modalRef!.Show();
        }
        private Task HideModal()
        {
            return modalRef!.Hide();
        }

        #region Update Project
        private async Task UpdateProjectAsync()
        {
            try
            {
                ProjectUpdateDto.Status = selectedStatus;
                ProjectUpdateDto.StartTime = selectedStartTime ?? DateTime.Now;
                ProjectUpdateDto.EndTime = selectedEndTime ?? DateTime.Now;
                ProjectUpdateDto.EmployeeId = selectedEmployeeId;
                ProjectUpdateDto.CustomerId = selectedCustomerId;

                await ProjectAppService.UpdateAsync(ProjectUpdateDto.Id, ProjectUpdateDto);
                await HideModal();
                await EventCallback.InvokeAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating project: {ex.Message}");
            }
        }
        #endregion
    }
}
