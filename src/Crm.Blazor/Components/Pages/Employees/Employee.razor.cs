using Blazorise.Components;
using Crm.Blazor.Components.Dialogs.Employees;
using Crm.Blazor.Components.Dialogs.Projects;
using Crm.Employees;
using Crm.Positions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Employees
{
    public partial class Employee
    {
        #region references
        private List<EmployeeDto> AllEmployees = new();
        private List<EmployeeDto> FilteredEmployees = new();
        private IEnumerable<EmployeeDto> ReadDataEmployees { get; set; } = new List<EmployeeDto>();
        private EmployeeCreateModal? employeeCreateModal;

        private string selectedAutoCompleteText = string.Empty;  
        
        private int CurrentPage { get; set; } = 0;
        private int PageSize { get; set; } = 9;

        private readonly Func<EmployeeDto, string> getName = item => item.FullName!;
        private readonly Func<EmployeeDto, string> getId = item => item.Id.ToString();

        [Inject] public NavigationManager? NavigationManager { get; set; }

        private EmployeeDto? selectedEmployee;
        private bool isEmployeeModalVisible = false;
        private bool isDeleteModalVisible = false;
        private EmployeeEditModal? employeeEditModal;
        public List<PositionDto> PositionList { get; set; } = new();

        #endregion
        protected override async Task OnInitializedAsync()
        {
            AllEmployees = (await EmployeeAppService.GetListAllAsync()).OrderBy(e => e.FirstName).ToList();
            ApplyFilters();
        }

        private async Task OnHandleReadData(AutocompleteReadDataEventArgs args)
        {
            if (args.CancellationToken.IsCancellationRequested) return;
            await Task.Delay(100);
            var query = args.SearchValue ?? string.Empty;
            ReadDataEmployees = AllEmployees
                .Where(e => !string.IsNullOrWhiteSpace(e.FullName) &&
                            e.FullName.Contains(query, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
        }
        
        public void LoadMoreEmployees()
        {
            CurrentPage++;
            ApplyFilters();
        }
        public void OnEntered(KeyboardEventArgs args)
        {
            if (args.Code == "Enter" || args.Code == "NumpadEnter")
                ApplyFilters();
        }
        private void ApplyFilters()
        {
            IEnumerable<EmployeeDto> query = AllEmployees;
            if (!string.IsNullOrWhiteSpace(selectedAutoCompleteText))
            {
                query = query.Where(e => e.FullName != null && e.FullName.Contains(selectedAutoCompleteText, StringComparison.OrdinalIgnoreCase));
            }

            int itemsToTake = CurrentPage > 0 ? PageSize * CurrentPage : PageSize;
            FilteredEmployees = query
                .OrderBy(e => e.FullName)
                .Take(itemsToTake)
                .ToList();
        }

        private async Task ShowCreateModal()
            {
                if (employeeCreateModal != null)
                {
                await employeeCreateModal.ShowModal(EventCallback.Factory.Create(this, OnInitializedAsync));
            }
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

