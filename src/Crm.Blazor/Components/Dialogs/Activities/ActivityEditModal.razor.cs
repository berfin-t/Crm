using Crm.Activities;
using Crm.Customers;
using Crm.Employees;
using System.Collections.Generic;
using System;
using Blazorise;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Dialogs.Activities
{
    public partial class ActivityEditModal
    {
        #region reference to the modal component
        private Modal? modalRef;
        private ActivityUpdateDto ActivityUpdateDto { get; set; } = new ActivityUpdateDto();
        private EnumType selectedType = Enum.GetValues(typeof(EnumType)).Cast<EnumType>().FirstOrDefault();
        private DateTime? selectedDate { get; set; }
        private Guid selectedEmployeeId { get; set; }
        private Guid selectedCustomerId { get; set; }
        private List<EmployeeDto> EmployeeList { get; set; } = new();
        private List<CustomerDto> CustomerList { get; set; } = new();
        #endregion

        protected override async Task OnInitializedAsync()
        {
            EmployeeList = await EmployeeAppService.GetListAllAsync();
            CustomerList = await CustomerAppService.GetListAllAsync();
        }        

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

                selectedType = activity.Type;
                selectedDate = activity.Date.ToLocalTime();
                selectedEmployeeId = activity.EmployeeId;
                selectedCustomerId = activity.CustomerId;
            }
            await modalRef!.Show();
        }
        
        private Task HideModal()
        {
            return modalRef!.Hide();
        }

        #region Update Activity
        private async Task UpdateActivityAsync()
        {
            try
            {
                ActivityUpdateDto.Type = selectedType;
                ActivityUpdateDto.Date = selectedDate ?? DateTime.MinValue; ;
                ActivityUpdateDto.EmployeeId = selectedEmployeeId;
                ActivityUpdateDto.CustomerId = selectedCustomerId;

                await ActivityAppService.UpdateAsync(ActivityUpdateDto.Id, ActivityUpdateDto);
                await HideModal();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}

