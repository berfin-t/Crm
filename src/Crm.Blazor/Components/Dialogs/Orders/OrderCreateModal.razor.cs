using Blazorise;
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
        private Validations? validations;
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

        public async Task ShowModal(EventCallback eventCallback)
        {
            EventCallback = eventCallback;

            if(validations is not null)
                await validations.ClearAll();

            Status = EnumStatus.Pending;
            OrderDate = DateTime.Now;
            DeliveryDate = DateTime.Now;
            TotalAmount = 0;
            OrderCode = string.Empty;
            SelectedCustomerId = Guid.Empty;
            SelectedProjectId = Guid.Empty;

            await modalRef!.Show();
        }

        private Task HideModal()
        {
            return modalRef!.Hide();
        }

        #region Create Order
        private async Task CreateOrderAsync()
        {
            if (validations is not null)
                return;

            var isValid = await validations!.ValidateAll();

            if (!isValid)
                return;

                await OrderAppService.CreateAsync(OrderCreateDto);
                await HideModal();
                await EventCallback.InvokeAsync();            
            
        }
        #endregion
    }
}
