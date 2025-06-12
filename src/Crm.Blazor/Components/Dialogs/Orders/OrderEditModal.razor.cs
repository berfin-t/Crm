using Blazorise;
using Crm.Common;
using Crm.Customers;
using Crm.Orders;
using Crm.Projects;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Dialogs.Orders
{
    public partial class OrderEditModal
    {
        #region reference to the modal component
        private Modal? modalRef;
        private OrderUpdateDto OrderUpdateDto { get; set; } = new OrderUpdateDto();        
        private List<CustomerDto> CustomerList { get; set; } = new();
        private List<ProjectDto> ProjectList { get; set; } = new();
        private EventCallback EventCallback { get; set; }

        #endregion
        protected override async Task OnInitializedAsync()
        {
            CustomerList = await CustomerAppService.GetListAllAsync();
            ProjectList = await ProjectAppService.GetListAllAsync();
        }
        public async Task ShowModal(OrderDto order, EventCallback eventCallback)
        {
            EventCallback = eventCallback;

            if (order != null)
            {
                OrderUpdateDto.Id = order.Id;
                OrderUpdateDto.Status = order.Status;
                OrderUpdateDto.OrderDate = order.OrderDate;
                OrderUpdateDto.DeliveryDate = order.DeliveryDate;
                OrderUpdateDto.TotalAmount = order.TotalAmount;
                OrderUpdateDto.OrderCode = order.OrderCode;
                OrderUpdateDto.CustomerId = order.CustomerId;
                OrderUpdateDto.ProjectId = order.ProjectId;
            }
            OrderUpdateDto.OrderDate = order!.OrderDate;
            OrderUpdateDto.DeliveryDate = order.DeliveryDate;

            StateHasChanged();
            await modalRef!.Show();
        }
        private Task HideModal()
        {
            return modalRef!.Hide();
        }

        #region Update Order
        private async Task UpdateOrderAsync()
        {
            try
            {             
                await OrderAppService.UpdateAsync(OrderUpdateDto.Id, OrderUpdateDto);
                await HideModal();
                await EventCallback.InvokeAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating order: {ex.Message}");
            }
        }
        #endregion
    }
}
