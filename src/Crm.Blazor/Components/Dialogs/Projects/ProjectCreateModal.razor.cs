using Blazorise;
using Crm.Common;
using Crm.Customers;
using Crm.Employees;
using Crm.Projects;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Dialogs.Projects
{
    public partial class ProjectCreateModal
    {
        #region Reference 
        private EventCallback EventCallback { get; set; }
        private Modal? modalRef;
        private ProjectCreateDto ProjectCreateDto { get; set; } = new ProjectCreateDto();
        private Guid SelectedEmployeeId { get; set; }
        private Guid SelectedCustomerId { get; set; }
        private List<EmployeeDto> Employees { get; set; } = new();
        private List<CustomerDto> Customers { get; set; } = new();
        #endregion

        #region Form Fields
        private string? Name { get; set; }
        private string? Description { get; set; }
        private DateTime? StartTime { get; set; }
        private DateTime? EndTime { get; set; }
        private EnumStatus Status { get; set; }
        private decimal Revenue { get; set; }
        private decimal SuccesRate { get; set; }
        private string? Employee { get; set; }
        private string? Customer { get; set; }
        #endregion

        protected override async Task OnInitializedAsync()
        {
            Employees = await EmployeeAppService.GetListAllAsync();
            Customers = await CustomerAppService.GetListAllAsync();
        }

        public Task ShowModal(EventCallback eventCallback)
        {
            EventCallback = eventCallback;

            Name = Name ?? string.Empty;
            Description = Description ?? string.Empty;
            StartTime = StartTime ?? DateTime.Now;
            EndTime = EndTime ?? DateTime.Now;
            Status = EnumStatus.Active;
            Revenue = 0;
            SuccesRate = 0;
            SelectedEmployeeId = Guid.Empty;
            SelectedCustomerId = Guid.Empty;

            ProjectCreateDto = new ProjectCreateDto();

            return modalRef!.Show();
        }

        private Task HideModal()
        {
            return modalRef!.Hide();
        }

        #region Create Project       
        private async Task CreateProjectAsync()
        {
            ProjectCreateDto.Name = Name!;
            ProjectCreateDto.Description = Description;
            ProjectCreateDto.StartTime = StartTime;
            ProjectCreateDto.EndTime = EndTime;
            ProjectCreateDto.Statues = Status;
            ProjectCreateDto.Revenue = Revenue;
            ProjectCreateDto.SuccesRate = SuccesRate;
            ProjectCreateDto.EmployeeId = SelectedEmployeeId;
            ProjectCreateDto.CustomerId = SelectedCustomerId;

            try
            {
                await ProjectAppService.CreateAsync(ProjectCreateDto);
                await HideModal();
                await EventCallback.InvokeAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }

        }
        #endregion        
    }
}
