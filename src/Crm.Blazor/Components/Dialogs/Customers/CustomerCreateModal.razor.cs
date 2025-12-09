using Blazorise;
using Crm.Customers;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Dialogs.Customers
{
    public partial class CustomerCreateModal
    {       
        #region reference to the modal component
        private EventCallback EventCallback { get; set; }
        private Modal? modalRef;
        private CustomerCreateDto CustomerCreateDto { get; set; } = new();
        private Validations? validations;
        #endregion

        public async Task ShowModal(EventCallback eventCallback)
        {
            EventCallback = eventCallback;

            if (validations is not null)
                await validations.ClearAll();

            CustomerCreateDto.Name = string.Empty;
            CustomerCreateDto.Surname = string.Empty;
            CustomerCreateDto.Email = string.Empty;
            CustomerCreateDto.Phone = string.Empty;
            CustomerCreateDto.Address = string.Empty;
            CustomerCreateDto.CompanyName = string.Empty;
            CustomerCreateDto.CustomerType = EnumCustomer.Lead;

            await modalRef!.Show();
        }

        private Task HideModal()
        {
            return modalRef!.Hide();
        }

        #region Create Customer       
        private async Task CreateCustomerAsync()
        {
            if (validations is null)
                return;

            var isValid = await validations.ValidateAll();

            if (!isValid)
                return;

            try
            {
                await CustomerAppService.CreateAsync(CustomerCreateDto);
                await HideModal();
                await EventCallback.InvokeAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion
    }
}
