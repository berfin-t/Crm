using Blazorise;
using Crm.Employees;
using Crm.Positions;
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
        #region Fields

        private Modal? modalRef;
        private Validations? validations;

        private EventCallback OnCreated { get; set; }

        private List<PositionDto> Positions { get; set; } = new();

        private EmployeeCreateDto EmployeeCreateDto { get; set; } = new();

        #endregion

        #region Lifecycle

        protected override async Task OnInitializedAsync()
        {
            Positions = await PositionAppService.GetListAllAsync();
        }

        #endregion

        #region Modal

        public async Task ShowModal(EventCallback onCreated)
        {
            OnCreated = onCreated;

            if (validations is not null)
                await validations.ClearAll();

            EmployeeCreateDto = new EmployeeCreateDto
            {
                BirthDate = DateTime.Now,
                Gender = EnumGender.Female,
                PositionId = Guid.Empty
            };

            await modalRef!.Show();
        }

        private Task HideModal()
            => modalRef!.Hide();

        #endregion

        #region File Upload

        private async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;

            var uploadFolder = Path.Combine(
                Environment.CurrentDirectory,
                "wwwroot",
                "images",
                "profile"
            );

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var fileName = $"{DateTime.Now.Ticks}_{file.Name}";
            var filePath = Path.Combine(uploadFolder, fileName);

            await using var stream = File.Create(filePath);
            await file.OpenReadStream(5 * 1024 * 1024).CopyToAsync(stream);

            EmployeeCreateDto.PhotoPath = $"/images/profile/{fileName}";
        }

        #endregion

        #region Create Employee

        private async Task CreateEmployeeAsync()
        {
            if (validations is null)
                return;

            if (!await validations.ValidateAll())
                return;

            EmployeeCreateDto.User = new IdentityUserCreateDto
            {
                UserName = $"{EmployeeCreateDto.FirstName}.{EmployeeCreateDto.LastName}".ToLower(),
                Email = EmployeeCreateDto.Email,
                Name = EmployeeCreateDto.FirstName,
                Surname = EmployeeCreateDto.LastName,
                Password = "123Qwe!"
            };

            await EmployeeAppService.CreateAsync(EmployeeCreateDto);

            await HideModal();
            await OnCreated.InvokeAsync();
        }

        #endregion

        #region Validation

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

        #endregion
    }
}
