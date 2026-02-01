using Blazorise;
using Crm.Employees;
using Crm.Positions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Dialogs.Employees
{
    public partial class EmployeeEditModal
    {
        #region reference to the modal component
        private Modal? modalRef;
        private EmployeeUpdateDto EmployeeUpdateDto { get; set; } = new EmployeeUpdateDto();
        private EnumGender selectedGender { get; set; }
        private DateTime? selectedBirthDate { get; set; }
        private Guid SelectedPositionId { get; set; }
        private List<PositionDto> PositionList { get; set; } = new();
        private EventCallback EventCallback { get; set; }
        #endregion

        protected override async Task OnInitializedAsync()
        {
            PositionList = await PositionAppService.GetListAllAsync();
        }

        #region Modal Methods
        public async Task ShowModal(EmployeeDto employee, EventCallback eventCallback)
        {
            EventCallback = eventCallback;
            if (employee != null)
            {
                EmployeeUpdateDto.Id = employee.Id;
                EmployeeUpdateDto.UserId = employee.UserId;
                EmployeeUpdateDto.FirstName = employee.FirstName;
                EmployeeUpdateDto.LastName = employee.LastName;
                EmployeeUpdateDto.Email = employee.Email;
                EmployeeUpdateDto.PhoneNumber = employee.PhoneNumber;
                EmployeeUpdateDto.Address = employee.Address;
                EmployeeUpdateDto.BirthDate = employee.BirthDate;
                EmployeeUpdateDto.PositionId = employee.PositionId;
                EmployeeUpdateDto.PhotoPath = employee.PhotoPath!;

                selectedGender = employee.Gender;
                selectedBirthDate = employee.BirthDate.ToLocalTime();
                SelectedPositionId = employee.PositionId;

            }
            StateHasChanged();
            await modalRef!.Show();
        }

        private Task HideModal()
        {
            return modalRef!.Hide();
        }
        #endregion

        #region Update Employee
        private async Task UpdateEmployeeAsync()
        {            
                EmployeeUpdateDto.Gender = selectedGender;
                EmployeeUpdateDto.BirthDate = selectedBirthDate ?? DateTime.MinValue;
                EmployeeUpdateDto.PositionId = SelectedPositionId;

                await EmployeeAppService.UpdateAsync(EmployeeUpdateDto.Id, EmployeeUpdateDto);
                await EventCallback.InvokeAsync();
                await HideModal();            
        }
        #endregion
    }
}
