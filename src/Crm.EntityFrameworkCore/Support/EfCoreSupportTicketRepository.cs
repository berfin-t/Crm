using Crm.Common;
using Crm.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Crm.Support
{
    public class EfCoreSupportTicketRepository(IDbContextProvider<CrmDbContext> dbContextProvider):
        EfCoreRepository<CrmDbContext, SupportTicket, Guid>(dbContextProvider), ISupportTicketRepository
    {
        #region GetQueryForNavigationProperties
        protected virtual async Task<IQueryable<SupportTicketWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            var dbContext = await GetDbContextAsync();
            return from supportTicket in dbContext.SupportTickets               
                   join customer in dbContext.Customers on supportTicket.CustomerId equals customer.Id into customers
                   from customer in customers.DefaultIfEmpty()
                   join employee in dbContext.Employees on supportTicket.EmployeeId equals employee.Id into employees
                   from employee in employees.DefaultIfEmpty()

                   select new SupportTicketWithNavigationProperties
                   {
                       SupportTicket = supportTicket,
                       Customer = customer,
                       Employee = employee
                   };
        }
        #endregion

        #region GetWithNavigationProperties
        public virtual async Task<SupportTicketWithNavigationProperties?> GetWithNavigationPropertiesAsync(
            Guid id,
            CancellationToken cancellationToken = default) =>
            await (await GetQueryForNavigationPropertiesAsync()).FirstOrDefaultAsync(b => b.SupportTicket.Id == id);
        #endregion

        public async Task<List<SupportTicket>> GetSlaRiskTicketsAsync()
        {
            var now = DateTime.UtcNow;

            var dbSet = await GetDbSetAsync();

            return await dbSet
                .Where(x =>
                    (x.SLAResponseDeadline.HasValue && x.SLAResponseDeadline < now) ||
                    (x.SLAResolutionDeadline.HasValue && x.SLAResolutionDeadline < now))
                .ToListAsync();
        }

        #region GetListAsync
        public async Task<List<SupportTicket>> GetListAsync(Guid? customerId = null, Guid? employeeId = null,
            string? subject = null, string? description = null,
            ICollection<EnumTicketStatus>? ticketStatus = null, ICollection<EnumPriority>? priority = null,
            DateTime? lastResponseTime = null, DateTime? closedTime = null,
            DateTime? slaResponseDeadLine = null, DateTime? slaResolutionDeadline = null,
            string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyDataFilters((await GetQueryableAsync()).AsNoTracking(), customerId,employeeId,subject,description,ticketStatus,priority,lastResponseTime,closedTime,slaResponseDeadLine,slaResolutionDeadline);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? SupportTicketConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResults).ToListAsync(cancellationToken);
        }
        #endregion

        #region ApplyDataFilters
        protected virtual IQueryable<SupportTicket> ApplyDataFilters(
            IQueryable<SupportTicket> query,
            Guid? customerId = null,
            Guid? employeeId = null,
            string? subject = null,
            string? description = null,
            ICollection<EnumTicketStatus>? ticketStatus = null,
            ICollection<EnumPriority>? priority = null,
            DateTime? lastResponseTime = null,
            DateTime? closedTime = null,
            DateTime? slaResponseDeadLine = null,
            DateTime? slaResolutionDeadline = null)
        {
            query = query
                .WhereIf(!string.IsNullOrWhiteSpace(subject),
                    e => e.Subject.Contains(subject!))
                .WhereIf(!string.IsNullOrWhiteSpace(description),
                    e => e.Description.Contains(description!))
                .WhereIf(ticketStatus != null && ticketStatus.Any(),
                    e => ticketStatus!.Contains(e.TicketStatus))
                .WhereIf(priority != null && priority.Any(),
                    e => e.Priority.HasValue && priority!.Contains(e.Priority.Value))
                .WhereIf(employeeId != null,
                    e => e.EmployeeId == employeeId)
                .WhereIf(customerId != null,
                    e => e.CustomerId == customerId)
                .WhereIf(lastResponseTime != null && lastResponseTime != DateTime.MinValue,
                    e => e.LastResponseTime == lastResponseTime!.Value.Date)
                .WhereIf(closedTime != null && closedTime != DateTime.MinValue,
                    e => e.ClosedTime == closedTime!.Value.Date)
                .WhereIf(slaResponseDeadLine != null && slaResponseDeadLine != DateTime.MinValue,
                    e => e.SLAResponseDeadline == slaResponseDeadLine!.Value.Date)
                .WhereIf(slaResolutionDeadline != null && slaResolutionDeadline != DateTime.MinValue,
                    e => e.SLAResolutionDeadline == slaResolutionDeadline!.Value.Date);

            return query;
        }
        #endregion
    }
}
