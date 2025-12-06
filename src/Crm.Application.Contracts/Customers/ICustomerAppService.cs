using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Crm.Customers
{
    public interface ICustomerAppService:IApplicationService
    {
        Task<PagedResultDto<CustomerDto>> GetListAsync(GetPagedCustomersInput input);
        Task<List<CustomerDto>> GetListAllAsync();
        Task<CustomerDto> GetAsync(Guid id);
        Task<CustomerDto> CreateAsync(CustomerCreateDto input);
        Task<CustomerDto> UpdateAsync(Guid id, CustomerUpdateDto input);
        Task<long> GetTotalCustomerCountAsync();
        Task DeleteAsync(Guid id);
        Task<CustomerFileDto> GetCustomerPdfAsync(Guid id);

    }
}
