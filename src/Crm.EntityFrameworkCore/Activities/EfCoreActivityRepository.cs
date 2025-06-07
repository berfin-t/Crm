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
using Crm.Customers;


namespace Crm.Activities
{
    public class EfCoreActivityRepository(IDbContextProvider<CrmDbContext> dbContextProvider)
        : EfCoreRepository<CrmDbContext, Activity, Guid>(dbContextProvider), IActivityRepository
    {
        #region GetCountAsync
        public async Task<long> GetCountAsync(ICollection<EnumType>? type = null, 
            string? description = null, DateTime? date = null, Guid? customerId = null, 
            Guid? employeeId = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            query = ApplyDataFilters(query, type, description, date, customerId, employeeId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));

        }       
        #endregion

        #region GetListAsync
        public async Task<List<Activity>> GetListAllAsync(ICollection<EnumType>? type = null, 
            string? description = null, DateTime? date = null, Guid? customerId = null, 
            Guid? employeeId = null, string? sorting = null, int maxResults = int.MaxValue, 
            int skipCount = 0, CancellationToken cancellationToken = default)
        {
           var query = ApplyDataFilters(await GetQueryableAsync(), type, description, date, customerId, employeeId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ActivityConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResults).ToListAsync(cancellationToken);
        }
        #endregion

        #region ApplyDataFilters
        protected virtual IQueryable<Activity> ApplyDataFilters(IQueryable<Activity> query, 
            ICollection<EnumType>? type = null, string? description = null, 
            DateTime? date = null, Guid? customerId = null, Guid? employeeId = null)
        {
            query
                .WhereIf(type != null && type.Count != 0, e => type!.Contains(e.Type))
                .WhereIf(!string.IsNullOrWhiteSpace(description), x => x.Description.Contains(description!))
                .WhereIf(date != null, x => x.Date == date)
                .WhereIf(customerId.HasValue, e => e.CustomerId == customerId!.Value)
                .WhereIf(employeeId.HasValue, e => e.EmployeeId == employeeId!.Value);

            return query;
        }
        #endregion

        #region GetQueryForNavigationProperties
        protected virtual async Task<IQueryable<ActivityWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            var dbContext = await GetDbContextAsync();
            return from activity in dbContext.Activities
                   join customer in dbContext.Customers on activity.CustomerId equals customer.Id into customers
                   from customer in customers.DefaultIfEmpty()
                   join employee in dbContext.Employees on activity.EmployeeId equals employee.Id into employees
                   from employee in employees.DefaultIfEmpty()

                   select new ActivityWithNavigationProperties
                   {
                       Activity = activity,
                       Customer = customer,
                       Employee = employee
                   };
        }
        #endregion

        #region GetWithNavigationProperties

        public virtual async Task<ActivityWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id,
            CancellationToken cancellationToken = default
        ) =>
            await (await GetQueryForNavigationPropertiesAsync()).FirstOrDefaultAsync(b => b.Activity.Id == id);
        #endregion
    }
}
