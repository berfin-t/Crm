using Asp.Versioning;
using Crm.Contacts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Crm.Controllers.Contacts
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Contact")]
    [Route("api/app/contacts")]
    public class ContactController: CrmController, IContactAppService
    {
        protected IContactAppService _contactAppService;
        public ContactController(IContactAppService contactAppService) => _contactAppService = contactAppService;

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ContactDto> GetAsync(Guid id) => _contactAppService.GetAsync(id);

        [HttpGet]
        [Route("all")]
        public virtual Task<List<ContactDto>> GetListAllAsync() => _contactAppService.GetListAllAsync();

        [HttpGet]
        [Route("paged")]
        public virtual Task<PagedResultDto<ContactDto>> GetListAsync(GetPagedContactsInput input) => _contactAppService.GetListAsync(input);

        [HttpPost]
        [Route("create")]
        public virtual Task<ContactDto> CreateAsync(ContactCreateDto input) => _contactAppService.CreateAsync(input);

        [HttpPut]
        [Route("update/{id}")]
        public virtual Task<ContactDto> UpdateAsync(Guid id, ContactUpdateDto input) => _contactAppService.UpdateAsync(id, input);
    }
     
}
