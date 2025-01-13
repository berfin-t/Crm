using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Crm.Customers
{
    [RemoteService(IsEnabled = false)]
    //[Authorize(CrmPermissions.Customers.Default)]
    public class CustomerAppService(ICustomerRepository customerRepository,
        CustomerManager customerManager) : CrmAppService, ICustomerAppService
    {
        public Task<CustomerDto> CreateAsync(CustomerCreateDto input)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CustomerDto>> GetListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Volo.Abp.Application.Dtos.PagedResultDto<CustomerDto>> GetListAsync(GetPagedCustomersInput input)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDto> UpdateAsync(Guid id, CustomerUpdateDto input)
        {
            throw new NotImplementedException();
        }
    }
}
