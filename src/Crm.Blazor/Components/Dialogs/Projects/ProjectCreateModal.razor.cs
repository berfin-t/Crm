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
        private ProjectEmployeeCreateDto ProjectCreateDto { get; set; } = new(); 
        private List<EmployeeDto> Employees { get; set; } = new();
        private List<CustomerDto> Customers { get; set; } = new();
        public List<Guid> SelectedEmployeeIds { get; set; } = new();
        private Guid SelectedCustomerId { get; set; }
        private Validations? validations;
        #endregion

        #region Form Fields
        private string? Name { get; set; }
        private string? Description { get; set; }
        private DateTime? StartTime { get; set; }
        private DateTime? EndTime { get; set; }
        private EnumStatus Status { get; set; }
        private decimal Revenue { get; set; }
        private decimal SuccesRate { get; set; }
        #endregion

        protected override async Task OnInitializedAsync()
        {
            Employees = await EmployeeAppService.GetListAllAsync();
            Customers = await CustomerAppService.GetListAllAsync();
        }

        public async Task ShowModal(EventCallback eventCallback)
        {
            EventCallback = eventCallback;

            if(validations is not null)
                await validations.ClearAll();

            ProjectCreateDto.Name = Name ?? string.Empty;
            ProjectCreateDto.Description = Description ?? string.Empty;
            ProjectCreateDto.StartTime = DateTime.Now;
            ProjectCreateDto.EndTime = EndTime ?? DateTime.Now;
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

        #region Create Project       
        private async Task CreateProjectAsync()
        {
            if(validations is null)
                return;

            var isValid = await validations.ValidateAll();

            if (!isValid)
                return;

            //ProjectCreateDto.Name = Name!;
            //ProjectCreateDto.Description = Description;
            //ProjectCreateDto.StartTime = StartTime;
            //ProjectCreateDto.EndTime = EndTime;
            //ProjectCreateDto.Statues = Status;
            //ProjectCreateDto.Revenue = Revenue;
            //ProjectCreateDto.SuccesRate = SuccesRate;
            //ProjectCreateDto.EmployeeIds = SelectedEmployeeIds;   // ✔ Çoklu çalışan
            //ProjectCreateDto.CustomerId = SelectedCustomerId;

                await ProjectEmployeeAppService.CreateAsync(ProjectCreateDto);
                await HideModal();
                await EventCallback.InvokeAsync();
            
        }
        #endregion        
    }
}
