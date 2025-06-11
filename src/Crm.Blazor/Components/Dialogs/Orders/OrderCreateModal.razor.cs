﻿using Blazorise;
using Crm.Common;
using Crm.Customers;
using Crm.Orders;
using Crm.Projects;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Dialogs.Orders
{
    public partial class OrderCreateModal
    {
        #region reference to the modal component
        private Modal? modalRef;
        private Guid SelectedCustomerId { get; set; }
        private Guid SelectedProjectId { get; set; }
        private OrderCreateDto OrderCreateDto { get; set; } = new OrderCreateDto();
        private EventCallback EventCallback { get; set; }
        private List<CustomerDto> Customers { get; set; } = new();
        private List<ProjectDto> Projects { get; set; } = new();
        #endregion

        #region Form Fields
        private EnumStatus Status { get; set; } = EnumStatus.Pending;
        private DateTime? OrderDate { get; set; }
        private DateTime? DeliveryDate { get; set; }
        private decimal TotalAmount { get; set; }
        private string? OrderCode { get; set; }
        private string? Customer { get; set; }
        private string? Project { get; set; }
        #endregion

        protected override async Task OnInitializedAsync()
        {
            Customers = await CustomerAppService.GetListAllAsync();
            Projects = await ProjectAppService.GetListAllAsync();
        }       

        public Task ShowModal(EventCallback eventCallback)
        {
            EventCallback = eventCallback;

            Status = EnumStatus.Pending;
            OrderDate = DateTime.Now;
            DeliveryDate = DateTime.Now;
            TotalAmount = 0;
            OrderCode = string.Empty;
            SelectedCustomerId = Guid.Empty;
            SelectedProjectId = Guid.Empty;

            return modalRef!.Show();
        }

        private Task HideModal()
        {
            return modalRef!.Hide();
        }

        //#region Customers and Projects Select      

        //private async Task CustomerSelect(ChangeEventArgs e)
        //{
        //    if (Guid.TryParse(e.Value?.ToString(), out var customerId))
        //    {
        //        SelectedCustomerId = customerId;
        //        await InvokeAsync(StateHasChanged);
        //    }
        //}

        //private async Task ProjectSelect(ChangeEventArgs e)
        //{
        //    if (Guid.TryParse(e.Value?.ToString(), out var projectId))
        //    {
        //        SelectedProjectId = projectId;
        //        await InvokeAsync(StateHasChanged);
        //    }
        //}
        //#endregion

        #region Create Order
        private async Task CreateOrderAsync()
        {
            OrderCreateDto.Status = Status;
            OrderCreateDto.OrderDate = OrderDate ?? DateTime.MinValue;
            OrderCreateDto.DeliveryDate = DeliveryDate ?? DateTime.MinValue;
            OrderCreateDto.TotalAmount = TotalAmount;
            OrderCreateDto.OrderCode = OrderCode;
            OrderCreateDto.CustomerId = SelectedCustomerId;
            OrderCreateDto.ProjectId = SelectedProjectId;

            try
            {
                await OrderAppService.CreateAsync(OrderCreateDto);
                await HideModal();
                await EventCallback.InvokeAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating order: {ex.Message}");
            }
        }
        #endregion
    }
}
