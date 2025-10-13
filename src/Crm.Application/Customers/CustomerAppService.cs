using Crm.Activities;
using Crm.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Crm.Customers
{
    [RemoteService(IsEnabled = false)]
    [Authorize(CrmPermissions.Customers.Menu)]
    public class CustomerAppService(ICustomerRepository customerRepository,
        CustomerManager customerManager) : CrmAppService, ICustomerAppService
    {
        #region Create
        //[Authorize(CrmPermissions.Customers.Create)]
        [AllowAnonymous]

        public async Task<CustomerDto> CreateAsync(CustomerCreateDto input)
        {
            var customer = await customerManager.CreateAsync(
                input.Name, input.Surname, input.Email, input.Phone, input.Address,
                input.CompanyName);

            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }
        #endregion

        #region Get
        [AllowAnonymous]

        public async Task<CustomerDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Customer, CustomerDto>(await customerRepository.GetAsync(id));
        }
        #endregion

        #region GetListAll
        [AllowAnonymous]
        public async Task<List<CustomerDto>> GetListAllAsync()
        {
            var items = await customerRepository.GetListAsync();
            return ObjectMapper.Map<List<Customer>, List<CustomerDto>>(items);
        }
        #endregion

        #region GetListPaged
        [AllowAnonymous]

        public async Task<PagedResultDto<CustomerDto>> GetListAsync(GetPagedCustomersInput input)
        {
            var totalCount = await customerRepository.GetCountAsync(
                input.Name, input.Surname, input.Email, input.Phone, input.Address, input.CompanyName);

            var items = await customerRepository.GetListAsync(
                input.Name, input.Surname, input.Email, input.Phone, input.Address, input.CompanyName);

            return new PagedResultDto<CustomerDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Customer>, List<CustomerDto>>(items)
            };
        }
        #endregion

        #region Update
        [AllowAnonymous]

        public async Task<CustomerDto> UpdateAsync(Guid id, CustomerUpdateDto input)
        {
            var customer = await customerManager.UpdateAsync(
                id, input.Name, input.Surname, input.Email, input.Phone, input.Address, input.CompanyName);

            return ObjectMapper.Map<Customer, CustomerDto>(customer);

        }
        #endregion

        #region GetTotalCustomerCountAsync
        [AllowAnonymous]
        public async Task<long> GetTotalCustomerCountAsync()
        {
            return await customerRepository.GetCountAsync();
        }
        #endregion

        #region Delete
        //[Authorize(CrmPermissions.Customers.Delete)]
        [AllowAnonymous]

        public virtual async Task DeleteAsync(Guid id)
        {
            var customer = await customerRepository.GetAsync(id);
            if (customer == null)
            {
                throw new EntityNotFoundException(typeof(Activity), id);
            }
            customer.IsDeleted = true;
            await customerRepository.DeleteAsync(customer);
        }
        #endregion

    }
}
