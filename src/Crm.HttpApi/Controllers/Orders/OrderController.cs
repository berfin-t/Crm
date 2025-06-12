using Asp.Versioning;
using Crm.Orders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Crm.Controllers.Orders
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Order")]
    [Route("api/app/orders")]
    public class OrderController:CrmController, IOrderAppService
    {
        protected IOrderAppService _orderAppService;
        public OrderController(IOrderAppService orderAppService) => _orderAppService = orderAppService;

        [HttpGet]
        [Route("{id}")]
        public virtual Task<OrderDto> GetAsync(Guid id) => _orderAppService.GetAsync(id);

        [HttpGet]
        [Route("all")]
        public virtual Task<List<OrderDto>> GetListAllAsync() => _orderAppService.GetListAllAsync();

        [HttpGet]
        [Route("paged")]
        public virtual Task<PagedResultDto<OrderDto>> GetListAsync(GetPagedOrdersInput input) => _orderAppService.GetListAsync(input);

        [HttpPost]
        [Route("create")]
        public virtual Task<OrderDto> CreateAsync(OrderCreateDto input) => _orderAppService.CreateAsync(input);

        [HttpPut]
        [Route("update/{id}")]
        public virtual Task<OrderDto> UpdateAsync(Guid id, OrderUpdateDto input) => _orderAppService.UpdateAsync(id, input);

        [HttpDelete]
        [Route("delete/{id}")]
        public virtual Task DeleteAsync(Guid id) => _orderAppService.DeleteAsync(id);
    }
}
