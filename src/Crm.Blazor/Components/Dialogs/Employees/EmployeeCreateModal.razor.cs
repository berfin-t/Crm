using Blazorise;
using Crm.Employees;
using Crm.Positions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Dialogs.Employees
{
    public partial class EmployeeCreateModal
    {
        #region references 
        private EventCallback EventCallback { get; set; }
        private Modal? modalRef;
        private Guid SelectedPositionId { get; set; }
        private List<PositionDto> Positions { get; set; } = new();
        #endregion

        #region Form Fields
        private string? FirstName { get; set; }
        private string? LastName { get; set; }
        private string? Email { get; set; }
        private string? PhoneNumber { get; set; }
        private string? EmployeeAddress { get; set; }
        private DateTime? BirthDate { get; set; }
        private string? Position { get; set; }
        private string? PhotoPath { get; set; }
        private EnumGender Gender { get; set; }
        #endregion

        protected override async Task OnInitializedAsync()
        {
            Positions = await PositionAppService.GetListAllAsync();
        }
        public Task ShowModal(EventCallback eventCallback)
        {
            EventCallback = eventCallback;
            return modalRef!.Show();
        }
        private Task HideModal()
        {
            return modalRef!.Hide();
        }        

        #region Create Employee
        private EmployeeCreateDto EmployeeCreateDto { get; set; } = new EmployeeCreateDto();
        private async Task CreateEmployeeAsync()
        {
            EmployeeCreateDto.FirstName = FirstName!;
            EmployeeCreateDto.LastName = LastName!;
            EmployeeCreateDto.Email = Email!;
            EmployeeCreateDto.PhoneNumber = PhoneNumber!;
            EmployeeCreateDto.Address = EmployeeAddress!;
            EmployeeCreateDto.BirthDate = BirthDate!;
            EmployeeCreateDto.PositionId = SelectedPositionId;
            EmployeeCreateDto.PhotoPath = PhotoPath!;

            try
            {
                await EmployeeAppService.CreateAsync(EmployeeCreateDto);
                await HideModal();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating employee: {ex.Message}");
            }
        }
        #endregion
    }
}
