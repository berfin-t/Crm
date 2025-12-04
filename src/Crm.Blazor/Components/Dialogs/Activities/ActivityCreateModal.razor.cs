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
        private Guid SelectedEmployeeId { get; set; }
        private Guid SelectedCustomerId { get; set; }
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
            
            try
            {
                await ActivityAppService.CreateAsync(ActivityCreateDto);
                await HideModal();
                await EventCallback.InvokeAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        #endregion

        private void ValidateEmployee(ValidatorEventArgs e)
        {
            var value = (Guid?)e.Value;

            if (value == null || value == Guid.Empty)
            {
                e.Status = ValidationStatus.Error;
                e.ErrorText = "Employee is required";
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
                e.ErrorText = "Customer is required";
            }
            else
            {
                e.Status = ValidationStatus.Success;
            }
        }


    }
}
