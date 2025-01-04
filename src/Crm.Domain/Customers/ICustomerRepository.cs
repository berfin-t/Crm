using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Crm.Customers
{
    public interface ICustomerRepository:IRepository<Customer, Guid>
    {
        Task<List<Customer>> GetListAsync(string? name = null, string? surname = null,
            string? email = null, string? phone = null, string? address = null,
            string? companyName= null,  string? sorting = null, 
            int maxResults = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken =default);
        Task<long> GetCountAsync(string? name = null, string? surname = null,
            string? email = null, string? phone = null, string? address = null,
            string? companyName = null, CancellationToken cancellationToken=default);
    }
}
