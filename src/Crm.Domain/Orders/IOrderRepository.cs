using Crm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Crm.Orders
{
    public interface IOrderRepository:IRepository<Order, Guid>
    {
        Task<List<Order>> GetListAsync(ICollection<EnumStatus>? statuses = null, 
            DateTime? orderDate = null, DateTime? deliveryDate = null, 
            decimal? totalAmount = null, Guid? customerId = null, Guid? projectId = null,
            string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0,
            CancellationToken cancellationToken=default);
        Task<long> GetCountAsync(ICollection<EnumStatus>? statuses=null,
            DateTime? orderDate = null, DateTime? deliveryDate = null,
            decimal? totalAmount = null, Guid? customerId = null, Guid? projectId = null,
            CancellationToken cancellationToken=default);
    }
}
