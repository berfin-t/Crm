using Crm.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crm.Positions;
using Crm.Blazor.Components.Dialogs.Employees;
using Microsoft.AspNetCore.Components;
using Blazorise.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Crm.Blazor.Components.Pages.Employees
{
    public partial class Employee
    {
        #region references
        [Inject] public NavigationManager? NavigationManager { get; set; }
        public List<EmployeeDto> Employees = new();
        public List<EmployeeDto> FilteredEmployees = new();
        public List<PositionDto> PositionList { get; set; } = new();
        public int CurrentPage { get; set; } = 0;
        public int PageSize { get; set; } = 9;
        public int TotalCount { get; set; } = 0;
        public string selectedEmployeeName = string.Empty;
        public string selectedEmployeeId = string.Empty;
        public IEnumerable<EmployeeDto>? ReadDataEmployees;
        public IEnumerable<EmployeeDto>? EmployeeDto;
        private EventCallback EventCallback => EventCallback.Factory.Create(this, OnInitializedAsync);
        public string? selectedAutoCompleteText { get; set; }
        private EmployeeCreateModal? employeeCreateModal;
        private EmployeeEditModal? employeeEditModal;
        private bool isDeleteModalVisible = false;
        private EmployeeDto? selectedEmployee;
        private bool isEmployeeModalVisible = false;

        #endregion

        private async Task ShowCreateModal()
        {
            if (employeeCreateModal != null)
            {
                await employeeCreateModal.ShowModal(EventCallback);
            }
        }
        public void OnEntered(KeyboardEventArgs args)
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
                    ReadDataEmployees = EmployeeDto!.Where(x => x.FirstName.StartsWith(autocompleteReadDataEventArgs.SearchValue, StringComparison.InvariantCultureIgnoreCase));
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            PositionList = await PositionAppService.GetListAllAsync();
            await LoadMoreEmployees();

            await base.OnInitializedAsync();
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

        #region Edit
        private async Task EditEmployee(EmployeeDto employee)
        {
            isEmployeeModalVisible = false;
            await employeeEditModal!.ShowModal(employee);
        }
        private async Task OnSelectEmployeeForEdit(EmployeeDto employee)
        {
            selectedEmployee = employee;
            await EditEmployee(employee);
        }
        #endregion

        #region Delete        
        private async Task ConfirmDelete()
        {
            if (selectedEmployee != null && selectedEmployee.Id != Guid.Empty)
            {
                await EmployeeAppService.DeleteAsync(selectedEmployee.Id);
                isDeleteModalVisible = false;
                await OnInitializedAsync();

                NavigationManager?.NavigateTo("/employees", forceLoad: true);
            }
        }
        private void OnSelectEmployeeForDelete(EmployeeDto employee)
        {
            selectedEmployee = employee;
            isDeleteModalVisible = true;
        }
        #endregion

        private Guid? ActiveDropdownEmployeeId { get; set; }

        private void ToggleDropdown(Guid employeeId)
        {
            if (ActiveDropdownEmployeeId == employeeId)
                ActiveDropdownEmployeeId = null;
            else
                ActiveDropdownEmployeeId = employeeId;
        }    

    }

}

