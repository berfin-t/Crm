using Asp.Versioning;
using Crm.Customers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;

namespace Crm.Controllers.Customers
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Customer")]
    [Route("api/app/customers")]
    public class CustomerController : CrmController
    {
        protected ICustomerAppService _customerAppService;
        public CustomerController(ICustomerAppService customerAppService) => _customerAppService = customerAppService;

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerDto> GetAsync(Guid id) => _customerAppService.GetAsync(id);

        [HttpGet]
        [Route("all")]
        public virtual Task<List<CustomerDto>> GetListAllAsync() => _customerAppService.GetListAllAsync();

        //[HttpGet]
        //[Route("paged")]
        //public virtual Task<PagedResultDto<CustomerDto>> GetListAsync(GetPagedCustomersInput input) => _customerAppService.GetListAsync(input);

        [HttpPost]
        [Route("create")]
        public virtual Task<CustomerDto> CreateAsync(CustomerCreateDto input) => _customerAppService.CreateAsync(input);

        [HttpPut]
        [Route("update/{id}")]
        public virtual Task<CustomerDto> UpdateAsync(Guid id, CustomerUpdateDto input) => _customerAppService.UpdateAsync(id, input);

        [HttpGet]
        [Route("count")]
        public virtual Task<long> GetTotalCustomerCountAsync() => _customerAppService.GetTotalCustomerCountAsync();

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id) => _customerAppService.DeleteAsync(id);

        //[HttpGet("{id}/pdf")]
        //public async Task<IActionResult> DownloadCustomerPdf(Guid id)
        //{
        //    var file = await _customerAppService.GetCustomerPdfAsync(id);

        //    return File(
        //        file.FileBytes,
        //        "application/pdf",
        //        file.FileName
        //    );
        //}
    }
}
