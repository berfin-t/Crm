using Crm.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Crm.Orders
{
    public class Order: FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual EnumStatus Status { get; private set; }
        [NotNull]
        public virtual DateTime OrderDate { get; private set; }
        [NotNull]
        public virtual DateTime? DeliveryDate { get; private set; }
        [NotNull]
        public virtual decimal TotalAmount { get; private set; }
        public virtual Guid CustomerId { get; private set; }
        public virtual Guid ProjectId { get; private set; }

        protected Order()
        {
            Status = EnumStatus.Pending;
            OrderDate = DateTime.Now;
            TotalAmount = 0;
        }

        public Order(Guid id, EnumStatus status, DateTime orderDate, DateTime? deliveryDate, decimal totalAmount, Guid customerId, Guid projectId)
        {
            SetStatus(status);
            SetOrderDate(orderDate);
            SetDeliveryDate(deliveryDate);
            SetTotalAmount(totalAmount);
            SetCustomerId(customerId);
            SetProjectId(projectId);
        }

        public void SetStatus(EnumStatus status) => Status = Check.NotNull(status, nameof(status));
        public void SetOrderDate(DateTime orderDate) => OrderDate = Check.NotNull(orderDate, nameof(orderDate));
        public void SetDeliveryDate(DateTime? deliveryDate) => DeliveryDate = deliveryDate;
        public void SetTotalAmount(decimal totalAmount) => TotalAmount = Check.NotNull(totalAmount, nameof(totalAmount));
        public void SetCustomerId(Guid customerId) => CustomerId = Check.NotDefaultOrNull<Guid>(customerId, nameof(customerId));
        public void SetProjectId(Guid projectId) => ProjectId = Check.NotDefaultOrNull<Guid>(projectId, nameof(projectId));
    }
}
