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
        private Validations? validations;
        private OrderCreateDto OrderCreateDto { get; set; } = new();
        private List<CustomerDto> Customers { get; set; } = new();
        private List<ProjectDto> Projects { get; set; } = new();
        private EventCallback EventCallback { get; set; }
        #endregion

        protected override async Task OnInitializedAsync()
        {
            Customers = await CustomerAppService.GetListAllAsync();
            Projects = await ProjectAppService.GetListAllAsync();
        }

        #region Modal
        public async Task ShowModal(EventCallback callback)
        {
            EventCallback = callback;

            if (validations is not null)
                await validations.ClearAll();

            OrderCreateDto.Status = EnumStatus.Pending;
            OrderCreateDto.OrderDate = DateTime.Now;
            OrderCreateDto.DeliveryDate = DateTime.Now;
            OrderCreateDto.TotalAmount = 0;
            OrderCreateDto.OrderCode = string.Empty;
            OrderCreateDto.CustomerId = Guid.Empty;
            OrderCreateDto.ProjectId = Guid.Empty;

            await modalRef!.Show();
        }

        private Task HideModal() => modalRef!.Hide();
        #endregion

        #region Create Order
        private async Task CreateOrderAsync()
        {
            if (validations is null)
                return;

            var isValid = await validations.ValidateAll();
            if (!isValid)
                return;

            await OrderAppService.CreateAsync(OrderCreateDto);
            await HideModal();
            await EventCallback.InvokeAsync();                        
        }
        #endregion

        #region Validation Methods
        private void ValidateCustomer(ValidatorEventArgs e)
        {
            var value = (Guid?)e.Value;

            if (value == null || value == Guid.Empty)
            {
                e.Status = ValidationStatus.Error;
            }
            else
            {
                e.Status = ValidationStatus.Success;
            }
        }
        private void ValidateProject(ValidatorEventArgs e)
        {
            var value = (Guid?)e.Value;

            if (value == null || value == Guid.Empty)
            {
                e.Status = ValidationStatus.Error;
            }
            else
            {
                e.Status = ValidationStatus.Success;
            }
        }
        #endregion
    }

}
