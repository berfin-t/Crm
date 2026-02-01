using Blazorise;
using Crm.Common;
using Crm.Customers;
using Crm.Employees;
using Crm.Projects;
using DeviceDetectorNET;
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
        private ProjectCreateDto ProjectCreateDto { get; set; } = new(); 
        private List<EmployeeDto> Employees { get; set; } = new();
        private List<CustomerDto> Customers { get; set; } = new();
        public List<Guid> SelectedEmployeeIds { get; set; } = new();
        private Guid SelectedCustomerId { get; set; }
        private Validations? validations;
        #endregion

        #region Form Fields
        private string? Name { get; set; }
        private string? Description { get; set; }
        private EnumStatus Status { get; set; }
        private decimal Revenue { get; set; }
        private decimal SuccesRate { get; set; }
        #endregion

        protected override async Task OnInitializedAsync()
        {
            Employees = await EmployeeAppService.GetListAllAsync();
            Customers = await CustomerAppService.GetListAllAsync();
        }

        #region Show and Hide Modal
        public async Task ShowModal(EventCallback eventCallback)
        {
            EventCallback = eventCallback;

            if(validations is not null)
                await validations.ClearAll();

            ProjectCreateDto.Name = Name ?? string.Empty;
            ProjectCreateDto.Description = Description ?? string.Empty;
            ProjectCreateDto.StartTime = DateTime.Now;
            ProjectCreateDto.EndTime = DateTime.Now;
            ProjectCreateDto.Statues = EnumStatus.Active;
            ProjectCreateDto.Revenue = 0;
            ProjectCreateDto.SuccesRate = 0;

            SelectedEmployeeIds = new List<Guid>();   
            SelectedCustomerId = Guid.Empty;

            await modalRef!.Show();
        }       

        private Task HideModal()
        {
            return modalRef!.Hide();
        }
        #endregion

        #region Create Project       
        private async Task CreateProjectAsync()
        {
            if(validations is null)
                return;

            var isValid = await validations.ValidateAll();

            if (!isValid)
                return;

            if (SelectedCustomerId == Guid.Empty)
                return;
            ProjectCreateDto.CustomerId = SelectedCustomerId;
            ProjectCreateDto.EmployeeIds = SelectedEmployeeIds;
            ProjectCreateDto.Statues = Status;
            ProjectCreateDto.Revenue = Revenue;
            ProjectCreateDto.SuccesRate = SuccesRate;

            await ProjectEmployeeAppService.CreateAsync(ProjectCreateDto);
                await HideModal();
                await EventCallback.InvokeAsync();
            
        }
        #endregion

        #region Validate
        private void ValidateEmployee(ValidatorEventArgs e)
        {
            var values = e.Value as List<Guid>;

            if (values == null || !values.Any())
            {
                e.Status = ValidationStatus.Error;
            }
            else
            {
                e.Status = ValidationStatus.Success;
            }
        }

        private void ValidateCustomer(ValidatorEventArgs e)
        {
            var value = (Guid?)e.Value;

            if (value == null || value == Guid.Empty)
            {
                e.Status = ValidationStatus.Error;
            }
            else
            {
                e.Status = ValidationStatus.Success;
            }
        }
        private void ValidateRevenue(ValidatorEventArgs e)
        {
            var value = (decimal?)e.Value;

            if (!value.HasValue || value <= 0)
            {
                e.Status = ValidationStatus.Error;
            }
            else
            {
                e.Status = ValidationStatus.Success;
            }
        }

        private void ValidateSuccesRate(ValidatorEventArgs e)
        {
            var value = (decimal?)e.Value;

            if (!value.HasValue || value <= 0)
            {
                e.Status = ValidationStatus.Error;
            }
            else
            {
                e.Status = ValidationStatus.Success;
            }
        }
        #endregion
    }
}
