using Blazorise;
using Crm.Employees;
using Crm.Positions;
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
        private EnumGender selectedGender = Enum.GetValues(typeof(EnumGender)).Cast<EnumGender>().FirstOrDefault();
        private DateTime? selectedBirthDate { get; set; }
        private Guid SelectedPositionId { get; set; }
        private List<PositionDto> PositionList { get; set; } = new();
        #endregion

        protected override async Task OnInitializedAsync()
        {
            PositionList = await PositionAppService.GetListAllAsync();
        }

        public async Task ShowModal(EmployeeDto employee)
        {
            if(employee != null)
            {
                EmployeeUpdateDto.Id = employee.Id;
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

        #region Update Employee
        private async Task UpdateEmployeeAsync()
        {
            try
            {
                EmployeeUpdateDto.Gender = selectedGender;
                EmployeeUpdateDto.BirthDate = selectedBirthDate ?? DateTime.MinValue;
                EmployeeUpdateDto.PositionId = SelectedPositionId;

                await EmployeeAppService.UpdateAsync(EmployeeUpdateDto.Id, EmployeeUpdateDto);
                await HideModal();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating employee: {ex.Message}");
            }
        }
            #endregion
        }
}
