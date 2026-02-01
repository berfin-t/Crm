using Blazorise;
using Crm.Blazor.Components.Dialogs.Orders;
using Crm.Customers;
using Crm.Orders;
using Crm.Projects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Orders
{
    public partial class Order
    {
        #region References
        [Inject] public NavigationManager? NavigationManager { get; set; }
        private OrderDto? OrderDto { get; set; }
        private List<CustomerDto> CustomerList = new List<CustomerDto>();
        private List<ProjectDto>? ProjectList = new List<ProjectDto>();
        public List<OrderDto>? OrderList { get; set; } = new List<OrderDto>();
        private OrderDto? selectedOrder;
        private OrderCreateModal? orderCreateModal;
        private OrderEditModal? orderEditModal;
        private bool isDeleteModalVisible = false;
        private bool isOrderModalVisible = false;
        private EventCallback EventCallback => EventCallback.Factory.Create(this, OnInitializedAsync);
        private HubConnection? hubConnection;
        private bool showAlert = false;
        private string latestOrderCode = string.Empty;
        #endregion

        protected override async Task OnInitializedAsync()
        {
            OrderList = await OrderAppService.GetListAllAsync();
            CustomerList = await CustomerAppService.GetListAllAsync();
            ProjectList = await ProjectAppService.GetListAllAsync();

            foreach (var order in OrderList!)
            {
                order.CustomerFullName = CustomerList
                    .FirstOrDefault(c => c.Id == order.CustomerId)?.FullName;

                order.ProjectName = ProjectList
                    .FirstOrDefault(p => p.Id == order.ProjectId)?.Name;
            }

            await base.OnInitializedAsync();            

            hubConnection = new HubConnectionBuilder()
                .WithUrl($"{NavigationManager!.BaseUri}crmhub")
                .WithAutomaticReconnect()
                .Build();

            hubConnection.On<OrderDto>("OrderCreated", async (order) =>
            {
                OrderList?.Add(order);
                latestOrderCode = order.OrderCode ?? "New Order";
                showAlert = true;
                await InvokeAsync(StateHasChanged);
            });

            await hubConnection.StartAsync();
        }

        private void OnAlertDismissed()
        {
            showAlert = false;
        }

        #region Modal 
        private async Task ShowCreateModal()
        {
            if (orderCreateModal != null)
            {
                await orderCreateModal.ShowModal(EventCallback);
            }
        }

        private async Task ShowEditModal(OrderDto order)
        {
            if (orderEditModal != null)
            {
                await orderEditModal.ShowModal(order, EventCallback);
            }
        }
        #endregion

        #region Delete 
        private void OnDeleteClicked(OrderDto order)
        {
            selectedOrder = order;
            isDeleteModalVisible = true;
        }
        private async Task ConfirmDelete()
        {
            if (selectedOrder != null && selectedOrder.Id != Guid.Empty)
            {
                await OrderAppService.DeleteAsync(selectedOrder.Id);
                isDeleteModalVisible = false;
                await OnInitializedAsync();
            }
        } 
        #endregion

    }
}
