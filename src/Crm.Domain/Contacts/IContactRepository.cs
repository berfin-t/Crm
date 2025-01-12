using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Crm.Contacts
{
    public interface IContactRepository:IRepository<Contact, Guid>
    {
        Task<List<Contact>> GetListAsync(ICollection<EnumType>? type = null, 
            string? contactValue = null, bool? isPrimary=null, Guid? customerId=null,
            Guid? employeeId =null, string? sorting = null,  int maxResults = int.MaxValue, 
            int skipCount = 0,  CancellationToken cancellationToken=default);
        Task<long> GetCountAsync(ICollection<EnumType>? type=null, string? contactValue=null,
            bool? isPrimary=null, Guid? customerId=null,  Guid? employeeId=null, 
            CancellationToken cancellationToken=default);
    }
}
