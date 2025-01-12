using Crm.Common;
using Crm.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Crm.Orders
{
    public class EfCoreOrderRepository(IDbContextProvider<CrmDbContext> dbContextProvider)
        : EfCoreRepository<CrmDbContext, Order, Guid>(dbContextProvider), IOrderRepository
    {
        #region GetCountAsync
        public async Task<long> GetCountAsync(ICollection<EnumStatus>? statuses=null, DateTime? orderDate=null, DateTime? deliveryDate=null, decimal? totalAmount=null, Guid? customerId=null, Guid? projectId=null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            query = ApplyDataFilters(query, statuses, orderDate, deliveryDate, totalAmount, customerId, projectId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }        
        #endregion

        #region GetListAsync       
        public async Task<List<Order>> GetListAsync(ICollection<EnumStatus>? statuses = null, DateTime? orderDate = null, DateTime? deliveryDate = null, decimal? totalAmount = null, Guid? customerId = null, Guid? projectId = null, string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyDataFilters(await GetQueryableAsync(), statuses, orderDate, deliveryDate, totalAmount, customerId, projectId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? OrderConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResults).ToListAsync(cancellationToken);
        }
        #endregion

        #region ApplyDataFilters
        protected virtual IQueryable<Order> ApplyDataFilters(IQueryable<Order> query, ICollection<EnumStatus>? statuses=null, DateTime? orderDate = null, DateTime? deliveryDate = null, decimal? totalAmount = null, Guid? customerId = null, Guid? projectId = null)
        {
           query.WhereIf(statuses != null && statuses.Any(), e => statuses.Contains(e.Status))
                .WhereIf(orderDate != null, e => e.OrderDate == orderDate)
                .WhereIf(deliveryDate != null, e => e.DeliveryDate == deliveryDate)
                .WhereIf(totalAmount != null, e => e.TotalAmount == totalAmount)
                .WhereIf(customerId != null, e => e.CustomerId == customerId)
                .WhereIf(projectId != null, e => e.ProjectId == projectId);
            return query;

        }
        #endregion
    }
}
