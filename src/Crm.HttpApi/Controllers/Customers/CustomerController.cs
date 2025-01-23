using Asp.Versioning;
using Crm.Customers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Crm.Controllers.Customers
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Customer")]
    [Route("api/app/customers")]
    public class CustomerController:CrmController, ICustomerAppService
    {
        protected ICustomerAppService _customerAppService;
        public CustomerController(ICustomerAppService customerAppService) => _customerAppService = customerAppService;

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerDto> GetAsync(Guid id) => _customerAppService.GetAsync(id);

        [HttpGet]
        [Route("all")]
        public virtual Task<List<CustomerDto>> GetListAllAsync() => _customerAppService.GetListAllAsync();

        [HttpGet]
        [Route("paged")]
        public virtual Task<PagedResultDto<CustomerDto>> GetListAsync(GetPagedCustomersInput input) => _customerAppService.GetListAsync(input);

        [HttpPost]
        [Route("create")]
        public virtual Task<CustomerDto> CreateAsync(CustomerCreateDto input) => _customerAppService.CreateAsync(input);

        [HttpPut]
        [Route("update/{id}")]
        public virtual Task<CustomerDto> UpdateAsync(Guid id, CustomerUpdateDto input) => _customerAppService.UpdateAsync(id, input);
    }
}
