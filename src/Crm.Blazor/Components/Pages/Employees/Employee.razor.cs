using Crm.Common;
using Crm.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crm.Positions;
using Crm.Projects;
using Crm.Blazor.Components.Dialogs.Employees;
using Microsoft.AspNetCore.Components;
using Blazorise.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Crm.Blazor.Components.Pages.Employees
{
    public partial class Employee
    {
        public List<EmployeeDto> Employees = new();
        public List<EmployeeDto> FilteredEmployees = new();
        public List<PositionDto> PositionList { get; set; } = new();
        public int CurrentPage { get; set; } = 0;
        public int PageSize { get; set; } = 9;
        public int TotalCount { get; set; } = 0;
        public string selectedEmployeeName = string.Empty;
        public string selectedEmployeeId = string.Empty;

        public IEnumerable<EmployeeDto> ReadDataEmployees;
        public IEnumerable<EmployeeDto> EmployeeDto;

        private EventCallback EventCallback => EventCallback.Factory.Create(this, OnInitializedAsync);
        public string selectedAutoCompleteText { get; set; }

        private EmployeeCreateModal employeeCreateModal;

        private async Task ShowCreateModal()
        {
            if (employeeCreateModal != null)
            {
                await employeeCreateModal.ShowModal(EventCallback);
            }
        }
        public async Task OnEntered(KeyboardEventArgs args)
        {
            if (args.Code == "Enter" || args.Code == "NumpadEnter")
            {
                ApplyFilters();
            }
        }
      

        private async Task OnHandleReadData(AutocompleteReadDataEventArgs autocompleteReadDataEventArgs)
        {
            if (!autocompleteReadDataEventArgs.CancellationToken.IsCancellationRequested)
            {
                await Task.Delay(100);
                if (!autocompleteReadDataEventArgs.CancellationToken.IsCancellationRequested)
                {
                    ReadDataEmployees = EmployeeDto.Where(x => x.FirstName.StartsWith(autocompleteReadDataEventArgs.SearchValue, StringComparison.InvariantCultureIgnoreCase));
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            PositionList = await PositionAppService.GetListAllAsync();
            await LoadMoreEmployees();

            await base.OnInitializedAsync();
        }

        private async Task OnEmployeeSelected(string value)
        {
            selectedEmployeeId = value;
        }
        public async Task LoadMoreEmployees()
        {
            var input = new GetPagedEmployeesInput
            {
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize
            };
            var result = await EmployeeAppService.GetListAsync(input);
            if (result?.Items != null)
            {

                Employees.AddRange(result.Items);
                TotalCount = (int)result.TotalCount;
                CurrentPage++;
                ApplyFilters();
            }
        }             

        
        private void ApplyFilters()
        {
            FilteredEmployees = Employees
                .Where(e => string.IsNullOrEmpty(selectedEmployeeName) || e.FirstName.Contains(selectedEmployeeName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // sonradan ekledim
    private Guid? ActiveDropdownEmployeeId { get; set; }

        private void ToggleDropdown(Guid employeeId)
        {
            if (ActiveDropdownEmployeeId == employeeId)
                ActiveDropdownEmployeeId = null;
            else
                ActiveDropdownEmployeeId = employeeId;
        }

        private void EditEmployee(Guid employeeId)
        {
            // Edit işlemini burada gerçekleştir
            Console.WriteLine($"Edit: {employeeId}");
            ActiveDropdownEmployeeId = null;
        }

        private void DeleteEmployee(Guid employeeId)
        {
            // Delete işlemini burada gerçekleştir
            Console.WriteLine($"Delete: {employeeId}");
            ActiveDropdownEmployeeId = null;
        }
    }


}

