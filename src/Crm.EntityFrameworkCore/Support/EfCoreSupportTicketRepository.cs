using Crm.Activities;
using Crm.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public virtual async Task<SupportTicketWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id,
            CancellationToken cancellationToken = default) =>
            await (await GetQueryForNavigationPropertiesAsync()).FirstOrDefaultAsync(b => b.SupportTicket.Id == id);
        #endregion
    }
}
