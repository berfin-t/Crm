using Blazorise;
using Crm.Common;
using Crm.Employees;
using Crm.Support;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Supports
{
    public partial class SupportDetail
    {
        [Parameter] public Guid Id { get; set; }

        private SupportTicketDto? ticket;
        private SupportTicketWithNavigationPropertyDto? selectedSupportWithNav;

        private bool assignModalVisible;
        private Guid? SelectedEmployeeId;
        private List<EmployeeDto> employeeList = new();

        protected override async Task OnInitializedAsync()
        {
            selectedSupportWithNav =
                await SupportTicketAppService.GetWithNavigationPropertiesAsync(Id);

            ticket = selectedSupportWithNav?.SupportTicket;

            employeeList = await EmployeeAppService.GetListAllAsync();
        }

        private Color GetStatusColor(EnumTicketStatus? status)
        {
            return status switch
            {
                EnumTicketStatus.Open => "badge bg-secondary",
                EnumTicketStatus.InProgress => "badge bg-info",
                EnumTicketStatus.WaitingForCustomer => "badge bg-warning text-dark",
                EnumTicketStatus.Resolved => "badge bg-success",
                EnumTicketStatus.Closed => "badge bg-danger",
                _ => "badge bg-light text-dark"
            };
        }

        private Color GetPriorityColor(EnumPriority? priority)
        {
            return priority switch
            {
                EnumPriority.Low => "badge bg-secondary",
                EnumPriority.Medium => "badge bg-info",
                EnumPriority.High => "badge bg-warning text-dark",
                EnumPriority.Critical => "badge bg-danger",
                _ => "badge bg-light text-dark"
            };
        }
        private void OpenAssignModal()
        {
            SelectedEmployeeId = selectedSupportWithNav?.Employee?.Id;
            assignModalVisible = true;
        }

        private void CloseAssignModal()
        {
            assignModalVisible = false;
        }

        private async Task SaveAssign()
        {
            if (!SelectedEmployeeId.HasValue)
            {
                await Message.Warn("Lütfen bir personel seçiniz.");
                return;
            }

            await SupportTicketAppService.AssignEmployeeAsync(Id, SelectedEmployeeId.Value);

            selectedSupportWithNav =
                await SupportTicketAppService.GetWithNavigationPropertiesAsync(Id);

            ticket = selectedSupportWithNav?.SupportTicket;

            assignModalVisible = false;

            await InvokeAsync(StateHasChanged);
        }

    }
}
