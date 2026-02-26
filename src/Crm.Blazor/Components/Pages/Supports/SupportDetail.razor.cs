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
        #region references
        [Parameter] public Guid Id { get; set; }

        private SupportTicketDto? ticket;
        private SupportTicketWithNavigationPropertyDto? selectedSupportWithNav;

        private bool assignModalVisible;
        private Guid? SelectedEmployeeId;
        private List<EmployeeDto> employeeList = new();

        private bool statusModalVisible;
        private UpdateTicketStatusPriorityDto editModel = new();
        private List<EnumTicketStatus> AllowedStatuses = new();
        #endregion

        protected override async Task OnInitializedAsync()
        {
            selectedSupportWithNav =
                await SupportTicketAppService.GetWithNavigationPropertiesAsync(Id);

            ticket = selectedSupportWithNav?.SupportTicket;

            employeeList = await EmployeeAppService.GetListAllAsync();
        }

        #region UI Helpers
        private string GetStatusBadgeClass(EnumTicketStatus? status)
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

        private string GetPriorityBadgeClass(EnumPriority? priority)
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
        #endregion

        #region Assign Modals
        private void OpenAssignModal()
        {
            SelectedEmployeeId = selectedSupportWithNav?.Employee?.Id;
            assignModalVisible = true;
        }

        private void CloseAssignModal() => assignModalVisible = false;

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
        #endregion

        #region Status Modals
        private async Task OpenStatusModal()
        {
            if (ticket == null)
                return;

            AllowedStatuses = await SupportTicketAppService.GetAllowedStatusesAsync(ticket.Id);

            editModel = new UpdateTicketStatusPriorityDto
            {
                TicketStatus = ticket.TicketStatus ?? EnumTicketStatus.Open,
                Priority = ticket.Priority ?? EnumPriority.Medium
            };

            statusModalVisible = true;
        }

        private void CloseStatusModal() => statusModalVisible = false;

        private async Task SaveStatus()
        {
            if (ticket == null)
                return;

            try
            {
                await SupportTicketAppService.UpdateStatusPriorityAsync(ticket.Id, editModel);

                selectedSupportWithNav =
                    await SupportTicketAppService.GetWithNavigationPropertiesAsync(Id);

                ticket = selectedSupportWithNav?.SupportTicket;

                statusModalVisible = false;

                await Message.Success("Ticket updated successfully");
            }
            catch (Exception ex)
            {
                await Message.Error(ex.Message);
            }
        }
        #endregion
    }
}