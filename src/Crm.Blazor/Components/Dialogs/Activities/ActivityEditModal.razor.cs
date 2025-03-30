using Crm.Activities;
using Crm.Customers;
using Crm.Employees;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System;
using Blazorise;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Dialogs.Activities
{
    public partial class ActivityEditModal
    {
        private Modal modalRef;
        private ActivityUpdateDto ActivityUpdateDto { get; set; } = new ActivityUpdateDto();

        private EnumType selectedType = Enum.GetValues(typeof(EnumType)).Cast<EnumType>().FirstOrDefault();
        private DateTime ActivityDate { get; set; }
        private Guid SelectedEmployeeId { get; set; }
        private Guid SelectedCustomerId { get; set; }

        private List<EmployeeDto> EmployeeList { get; set; } = new();
        private List<CustomerDto> CustomerList { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            EmployeeList = await EmployeeAppService.GetListAllAsync();
            CustomerList = await CustomerAppService.GetListAllAsync();
        }

        #region Employee Select

        private async Task EmployeeSelect(ChangeEventArgs e)
        {
            if (Guid.TryParse(e.Value?.ToString(), out var employeeId))
            {
                SelectedEmployeeId = employeeId;
                await InvokeAsync(StateHasChanged);
            }
        }
        #endregion

        #region Customer Select

        private async Task CustomerSelect(ChangeEventArgs e)
        {
            if (Guid.TryParse(e.Value?.ToString(), out var customerId))
            {
                SelectedCustomerId = customerId;
                await InvokeAsync(StateHasChanged);
            }
        }
        #endregion

        public async Task ShowModal(ActivityDto activity)
        {
            if (activity != null)
            {
                ActivityUpdateDto.Id = activity.Id;
                ActivityUpdateDto.Description = activity.Description;
                ActivityUpdateDto.Type = activity.Type;
                ActivityUpdateDto.Date = activity.Date;
                ActivityUpdateDto.EmployeeId = activity.EmployeeId;
                ActivityUpdateDto.CustomerId = activity.CustomerId;
            }

            await modalRef.Show();
        }

        private Task HideModal()
        {
            return modalRef.Hide();
        }

        private async Task UpdateActivityAsync()
        {
            try
            {
                ActivityUpdateDto.Type = selectedType;
                await ActivityAppService.UpdateAsync(ActivityUpdateDto.Id, ActivityUpdateDto);
                await HideModal();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

