using Crm.Employees;
using Crm.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Crm.Orders
{
    [RemoteService(IsEnabled = false)]
    [Authorize(CrmPermissions.Orders.Default)]
    public class OrderAppService(IOrderRepository orderRepository,
        OrderManager orderManager) : CrmAppService, IOrderAppService
    {
        #region Create
        //[Authorize(CrmPermissions.Orders.Create)]
        public async Task<OrderDto> CreateAsync(OrderCreateDto input)
        {
            var order = await orderManager.CreateAsync(
                input.Status, input.OrderDate, input.DeliveryDate, input.TotalAmount, input.OrderCode, input.CustomerId, input.ProjectId);

            return ObjectMapper.Map<Order, OrderDto>(order);
        }
        #endregion

        #region Get
        public async Task<OrderDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Order, OrderDto>(await orderRepository.GetAsync(id));
        }
        #endregion

        #region GetListAll
        public async Task<List<OrderDto>> GetListAllAsync()
        {
            var items = await orderRepository.GetListAsync();
            return ObjectMapper.Map<List<Order>, List<OrderDto>>(items);
        }
        #endregion

        #region GetListPaged
        public async Task<PagedResultDto<OrderDto>> GetListAsync(GetPagedOrdersInput input)
        {
            var totalCount = await orderRepository.GetCountAsync(
                input.Status, input.OrderDate, input.DeliveryDate, input.TotalAmount, input.OrderCode, input.CustomerId, input.ProjectId);

            var items = await orderRepository.GetListAsync(
                input.Status, input.OrderDate, input.DeliveryDate, input.TotalAmount, input.OrderCode, input.CustomerId, input.ProjectId);

            return new PagedResultDto<OrderDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Order>, List<OrderDto>>(items)
            };
        }
        #endregion

        #region Update
        public async Task<OrderDto> UpdateAsync(Guid id, OrderUpdateDto input)
        {
            var order = await orderManager.UpdateAsync(
                id, input.Status, input.OrderDate, input.DeliveryDate, input.TotalAmount, input.OrderCode, input.CustomerId, input.ProjectId);

            return ObjectMapper.Map<Order, OrderDto>(order);
        }
        #endregion

        #region Delete
        public virtual async Task DeleteAsync(Guid id)
        {
            var order = await orderRepository.GetAsync(id);
            if (order == null)
            {
                throw new EntityNotFoundException(typeof(Order), id);
            }
            order.IsDeleted = true;
            await orderRepository.DeleteAsync(order);
        }
        #endregion
    }
}
