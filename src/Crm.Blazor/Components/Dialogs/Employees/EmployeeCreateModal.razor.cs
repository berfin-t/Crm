using Blazorise;
using Crm.Employees;
using Crm.Positions;
using Crm.Projects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Crm.Blazor.Components.Dialogs.Employees
{
    public partial class EmployeeCreateModal
    {
        #region references 
        private EventCallback EventCallback { get; set; }
        private Modal? modalRef;
        private Guid SelectedPositionId { get; set; }
        private List<PositionDto> Positions { get; set; } = new();
        private Validations? validations;
        private EmployeeCreateDto EmployeeCreateDto { get; set; } = new();
        private string PhotoPath { get; set; }
        private IBrowserFile SelectedPhoto { get; set; }      
        private string? FirstName { get; set; }
        private string? LastName { get; set; }
        private DateTime? BirthDate { get; set; }
        private EnumGender Gender { get; set; }
        #endregion

        protected override async Task OnInitializedAsync()
        {
            Positions = await PositionAppService.GetListAllAsync();
        }
        public async Task ShowModal(EventCallback eventCallback)
        {
            EventCallback = eventCallback;

            if(validations is not null)
                await validations.ClearAll();

            EmployeeCreateDto.FirstName = string.Empty;
            EmployeeCreateDto.LastName = string.Empty;
            EmployeeCreateDto.Email = string.Empty;
            EmployeeCreateDto.PhoneNumber = string.Empty;
            EmployeeCreateDto.Address = string.Empty;
            EmployeeCreateDto.BirthDate = BirthDate ?? DateTime.Now;
            EmployeeCreateDto.PositionId = Guid.Empty;
            EmployeeCreateDto.PhotoPath = string.Empty;
            EmployeeCreateDto.Gender = EnumGender.Female;

            await modalRef!.Show();
        }
        private Task HideModal()
        {
            return modalRef!.Hide();
        }
        private async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;

            var uploadFolder = Path.Combine(Environment.CurrentDirectory, "wwwroot", "images", "profile");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var fileName = $"{DateTime.Now.Ticks}_{file.Name}";
            var filePath = Path.Combine(uploadFolder, fileName);

            await using (var stream = File.Create(filePath))
            {
                await file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024).CopyToAsync(stream);
            }

            PhotoPath = $"/images/profile/{fileName}";
        }
        #region Create Employee        
        private async Task CreateEmployeeAsync()
        {
            if (validations is null)
                return;

            var isValid = await validations.ValidateAll();

            if (!isValid)
                return;

            //EmployeeCreateDto.FirstName = FirstName ?? string.Empty;
            //EmployeeCreateDto.LastName = LastName ?? string.Empty;
            //EmployeeCreateDto.Email = Email ?? $"{FirstName}.{LastName}@example.com".ToLower();
            //EmployeeCreateDto.PhoneNumber = PhoneNumber ?? string.Empty;
            //EmployeeCreateDto.Address = EmployeeAddress ?? string.Empty;
            //EmployeeCreateDto.BirthDate = BirthDate ?? DateTime.Now;
            //EmployeeCreateDto.PositionId = SelectedPositionId != Guid.Empty ? SelectedPositionId : Guid.NewGuid();
            //EmployeeCreateDto.PhotoPath = PhotoPath ?? string.Empty;
            //EmployeeCreateDto.Gender = Gender;

            EmployeeCreateDto.User = new IdentityUserCreateDto
            {
                UserName = $"{FirstName}.{LastName}".ToLower(),
                Email = EmployeeCreateDto.Email,
                Name = EmployeeCreateDto.FirstName,
                Surname = EmployeeCreateDto.LastName,
                Password = "123Qwe!"
            };

            await EmployeeAppService.CreateAsync(EmployeeCreateDto);
            await HideModal();
            await EventCallback.InvokeAsync();
        }
        #endregion

        private void ValidatePosition(ValidatorEventArgs e)
        {
            var value = (Guid?)e.Value;
            if (value == null || value == Guid.Empty)
            {
                e.Status = ValidationStatus.Error;
                e.ErrorText = "Position is required.";
            }
            else
            {
                e.Status = ValidationStatus.Success;
            }
        }               

    }
}

