using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Crm.Orders
{
    [RemoteService(IsEnabled = false)]
    //[Authorize(CrmPermissions.Orders.Default)]
    public class OrderAppService(IOrderRepository orderRepository,
        OrderManager orderManager) : CrmAppService, IOrderAppService
    {
        public Task<OrderDto> CreateAsync(OrderCreateDto input)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderDto>> GetListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Volo.Abp.Application.Dtos.PagedResultDto<OrderDto>> GetListAsync(GetPagedOrdersInput input)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto> UpdateAsync(Guid id, OrderUpdateDto input)
        {
            throw new NotImplementedException();
        }
    }
}
