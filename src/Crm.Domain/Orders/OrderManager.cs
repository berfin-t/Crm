using Crm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Crm.Orders
{
    public class OrderManager(IOrderRepository orderRepository) : DomainService
    {
        #region Create
        public virtual async Task<Order> CreateAsync(EnumStatus status, DateTime orderDate, DateTime? deliveryDate, decimal totalAmount, string orderCode, Guid customerId, Guid projectId)
        {
            var order = new Order(
                GuidGenerator.Create(),
                status,
                orderDate,
                deliveryDate,
                totalAmount,
                orderCode,
                customerId,
                projectId
            );
            return await orderRepository.InsertAsync(order);
        }
        #endregion
        #region Update
        public virtual async Task<Order> UpdateAsync(Guid id, EnumStatus status, DateTime orderDate, DateTime? deliveryDate, decimal totalAmount, string orderCode, Guid customerId, Guid projectId)
        {
            var order = await orderRepository.GetAsync(id);
            order.SetStatus(status);
            order.SetOrderDate(orderDate);
            order.SetDeliveryDate(deliveryDate);
            order.SetTotalAmount(totalAmount);
            order.SetCustomerId(customerId);
            order.SetProjectId(projectId);
            order.SetOrderCode(orderCode);
            return await orderRepository.UpdateAsync(order);
        }
        #endregion
    }
}
