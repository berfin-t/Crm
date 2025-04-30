using Crm.Common;
using Crm.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crm.Positions;
using Crm.Projects;

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
        public async Task ApplySearch()
        {
            if (!string.IsNullOrEmpty(selectedEmployeeId))
            {
                FilteredEmployees = Employees
                    .Where(e => e.Id.ToString() == selectedEmployeeId)
                    .ToList();
            }
            else
            {
                FilteredEmployees = Employees;
            }

            await InvokeAsync(StateHasChanged);

        }

        private void ApplyFilters()
        {
            FilteredEmployees = Employees
                .Where(e => string.IsNullOrEmpty(selectedEmployeeName) || e.FirstName.Contains(selectedEmployeeName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        
    }
}
