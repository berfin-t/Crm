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
     
        #region Form Fields
        public string? Description { get; set; }
        private DateTime? ActivityDate { get; set; }
        private EnumType Types { get; set; }
        private string? Employee { get; set; }
        private string? Customer { get; set; }
        #endregion

        #region reference to the modal component
        private Modal modalRef;
        private EventCallback EventCallback { get; set; }
        #endregion

        public Task ShowModal(EventCallback eventCallback)
        {
            //EventCallback = eventCallback.HasDelegate ? eventCallback : EventCallback.Factory.Create(this, () => Task.CompletedTask);
            EventCallback = eventCallback;

            Description = string.Empty;
            ActivityDate = DateTime.Now;
            Types = EnumType.Call; 
            SelectedEmployeeId = Guid.Empty; 
            SelectedCustomerId = Guid.Empty;
            ActivityCreateDto = new ActivityCreateDto();

            return modalRef.Show();
        }

        private Task HideModal()
        {
            return modalRef.Hide();
        }
        protected override async Task OnInitializedAsync()
        {
            Employees = await EmployeeAppService.GetListAllAsync();
            Customers = await CustomerAppService.GetListAllAsync();
        }

        #region Create Activity
        private ActivityCreateDto ActivityCreateDto { get; set; } = new ActivityCreateDto();
        private Guid SelectedEmployeeId { get; set; }
        private Guid SelectedCustomerId { get; set; }

        private async Task CreateActivityAsync()
        {

            ActivityCreateDto.Description = Description;
            ActivityCreateDto.Date = ActivityDate ?? DateTime.MinValue;
            ActivityCreateDto.Type = Types;
            ActivityCreateDto.EmployeeId = SelectedEmployeeId;
            ActivityCreateDto.CustomerId = SelectedCustomerId;

            try
            {
                await ActivityAppService.CreateAsync(ActivityCreateDto);
                await HideModal();
                await EventCallback.InvokeAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }

        }
        #endregion

        #region Employee Select
        private List<EmployeeDto> Employees { get; set; } = new();

        //private async Task EmployeeSelect(ChangeEventArgs e)
        //{
        //    if (Guid.TryParse(e.Value?.ToString(), out var employeeId))
        //    {
        //        SelectedEmployeeId = employeeId;
        //        await InvokeAsync(StateHasChanged);
        //    }
        //}
        #endregion

        #region Customer Select
        private List<CustomerDto> Customers { get; set; } = new();
        //private async Task CustomerSelect(ChangeEventArgs e)
        //{
        //    if (Guid.TryParse(e.Value?.ToString(), out var customerId))
        //    {
        //        SelectedCustomerId = customerId;
        //        await InvokeAsync(StateHasChanged);
        //    }
        //}
        #endregion
    }
}
