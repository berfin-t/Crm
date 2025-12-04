using Asp.Versioning;
using Crm.CustomerNotes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Crm.Controllers.CustomerNotes
{
    [RemoteService]
    [Area("app")]
    [ControllerName("CustomerNote")]
    [Route("api/app/customer-notes")]
    public class CustomerNoteController:CrmController, ICustomerNoteAppService
    {
        protected ICustomerNoteAppService _customerNoteAppService;

        public CustomerNoteController(ICustomerNoteAppService customerNoteAppService) => _customerNoteAppService = customerNoteAppService;

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerNoteDto> GetAsync(Guid id) => _customerNoteAppService.GetAsync(id);
        
        [HttpGet]
        [Route("all")]
        public virtual Task<List<CustomerNoteDto>> GetListAllAsync() => _customerNoteAppService.GetListAllAsync();
       
        [HttpGet]
        [Route("paged")]
        public virtual Task<PagedResultDto<CustomerNoteDto>> GetListAsync(GetPagedCustomerNotesInput input) => _customerNoteAppService.GetListAsync(input);
       
        [HttpPost]
        [Route("create")]
        public virtual Task<CustomerNoteDto> CreateAsync(CustomerNoteCreateDto input) => _customerNoteAppService.CreateAsync(input);

        [HttpPut]
        [Route("update/{id}")]
        public virtual Task<CustomerNoteDto> UpdateAsync(Guid id, CustomerNoteUpdateDto input) => _customerNoteAppService.UpdateAsync(id, input);

        [HttpGet]
        [Route("by-customer/{customerId}")]
        public Task<List<CustomerNoteDto>> GetListByCustomerAsync(Guid customerId) => _customerNoteAppService.GetListByCustomerAsync(customerId);
    }
}
