using Blazorise.DataGrid;
using Crm.Blazor.Components.Dialogs.Customers;
using Crm.Customers;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Customers
{
    public partial class Customer
    {
        private CustomerDto CustomerDto { get; set; }
        private List<CustomerDto> customerList;        
        private EventCallback EventCallback => EventCallback.Factory.Create(this, OnInitializedAsync);

        protected override async Task OnInitializedAsync()
        {
            customerList = await CustomerAppService.GetListAllAsync();
            await base.OnInitializedAsync();
        }        

        private CustomerCreateModal customerCreateModal;
        private CustomerEditModal customerEditModal;

        private async Task ShowCreateModal()
        {
            if (customerCreateModal != null)
            {

                await customerCreateModal.ShowModal(EventCallback);
            }
        }

        private async Task ShowEditModal(CustomerDto customer)
        {
            if (customerEditModal != null)
            {
                await customerEditModal.ShowModal(customer);
            }
        }

        //private CustomerDto selectedCustomer;
        //private CustomerUpdateDto customerUpdateDto = new();
        //private void EditCustomer(CustomerDto customer)
        //{
        //    customerUpdateDto = new CustomerUpdateDto
        //    {
        //        Id = customer.Id, // Güncelleme için ID gerekli olabilir
        //        Name = customer.Name,
        //        Surname = customer.Surname,
        //        Phone = customer.Phone,
        //        Email = customer.Email,
        //        Address = customer.Address,
        //        CompanyName = customer.CompanyName
        //    };

        //    isEditModalOpen = true;
        //}

        //private void CloseModal()
        //{
        //    isEditModalOpen = false;
        //}

        //private async Task SaveCustomer()
        //{
        //    await CustomerAppService.UpdateAsync(customerUpdateDto.Id, customerUpdateDto);

        //    var updatedCustomer = customerList.FirstOrDefault(c => c.Id == customerUpdateDto.Id);
        //    if (updatedCustomer != null)
        //    {
        //        updatedCustomer.Name = customerUpdateDto.Name;
        //        updatedCustomer.Surname = customerUpdateDto.Surname;
        //        updatedCustomer.Phone = customerUpdateDto.Phone;
        //        updatedCustomer.Email = customerUpdateDto.Email;
        //        updatedCustomer.Address = customerUpdateDto.Address;
        //        updatedCustomer.CompanyName = customerUpdateDto.CompanyName;
        //    }

        //    isEditModalOpen = false;
        //}

        //private void DeleteCustomer(CustomerDto customer)
        //{

        //}
    }
}
