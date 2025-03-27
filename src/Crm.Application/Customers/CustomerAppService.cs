﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Crm.Customers
{
    [RemoteService(IsEnabled = false)]
    //[Authorize(CrmPermissions.Customers.Default)]
    public class CustomerAppService(ICustomerRepository customerRepository,
        CustomerManager customerManager) : CrmAppService, ICustomerAppService
    {
        #region Create
        //[Authorize(CrmPermissions.Customers.Create)]
        public async Task<CustomerDto> CreateAsync(CustomerCreateDto input)
        {
            var customer = await customerManager.CreateAsync(
                input.Name, input.Surname, input.Email, input.Phone, input.Address,
                input.CompanyName);

            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }
        #endregion

        #region Get
        public async Task<CustomerDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Customer, CustomerDto>(await customerRepository.GetAsync(id));
        }
        #endregion

        #region GetListAll
        public async Task<List<CustomerDto>> GetListAllAsync()
        {
            var items= await customerRepository.GetListAsync();
            return ObjectMapper.Map<List<Customer>, List<CustomerDto>>(items);
        }
        #endregion

        #region GetListPaged
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
        public async Task<CustomerDto> UpdateAsync(Guid id, CustomerUpdateDto input)
        {
           var customer = await customerManager.UpdateAsync(
               id, input.Name, input.Surname, input.Email, input.Phone, input.Address, input.CompanyName);
            
            return ObjectMapper.Map<Customer, CustomerDto>(customer);

        }
        #endregion

        #region
        public async Task<long> GetTotalCustomerCountAsync()
        {
            return await customerRepository.GetCountAsync();
        }
        #endregion
    }
}
