using Crm.Activities;
using Crm.Customers;
using Crm.Employees;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Blazorise;

namespace Crm.Blazor.Components.Dialogs.Activities
{
    public partial class ActivityCreateModal
    {       
        #region reference to the modal component
        private Modal? modalRef;
        private EventCallback EventCallback { get; set; }
        private List<CustomerDto> Customers { get; set; } = new();
        private List<EmployeeDto> Employees { get; set; } = new();
        private ActivityCreateDto ActivityCreateDto { get; set; } = new();
        private Validations? validations;
        #endregion

        public async Task ShowModal(EventCallback eventCallback)
        {            
            EventCallback = eventCallback;

            if(validations is not null)
                await validations.ClearAll();

            ActivityCreateDto.Description = string.Empty;
            ActivityCreateDto.Date = DateTime.Now;
            ActivityCreateDto.Type = EnumType.Call;
            ActivityCreateDto.EmployeeId = Guid.Empty;
            ActivityCreateDto.CustomerId = Guid.Empty;            

            await modalRef!.Show();
        }

        private Task HideModal()
        {
            return modalRef!.Hide();
        }
        protected override async Task OnInitializedAsync()
        {
            Employees = await EmployeeAppService.GetListAllAsync();
            Customers = await CustomerAppService.GetListAllAsync();
        }

        #region Create Activity      
        private async Task CreateActivityAsync()
        {
            if(validations is null)
                return;

            var isValid = await validations.ValidateAll();

            if(!isValid)
                return;          
            
            await ActivityAppService.CreateAsync(ActivityCreateDto);
            await HideModal();
            await EventCallback.InvokeAsync();                   

        }
        #endregion

        #region Validation Methods
        private void ValidateEmployee(ValidatorEventArgs e)
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
        #endregion

    }
}
