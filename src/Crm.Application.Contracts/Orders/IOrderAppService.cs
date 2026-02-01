using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Crm.Orders
{
    public interface IOrderAppService:IApplicationService
    {
        //Task<PagedResultDto<OrderDto>> GetListAsync(GetPagedOrdersInput input);
        Task<List<OrderDto>> GetListAllAsync();
        //Task<OrderDto> GetAsync(Guid id);
        Task<OrderDto> CreateAsync(OrderCreateDto input);
        Task<OrderDto> UpdateAsync(Guid id, OrderUpdateDto input);
        Task DeleteAsync(Guid id);
    }
}
