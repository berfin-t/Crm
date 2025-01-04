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
        Task<List<Order>> GetListAsync(string? firstName=null, string? lastName = null,
            string? email = null, string? phoneNumber = null, string? address = null,
            DateTime? birthDate = null, Guid? positionId = null,
            string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0,
            CancellationToken cancellationToken=default);
        Task<long> GetCountAsync(string? firstName = null, string? lastName = null,
            string? email = null, string? phoneNumber = null, string? address = null,
            DateTime? birthDate = null, Guid? positionId = null, CancellationToken cancellationToken=default);
    }
}
