using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Crm.Employees
{
    public interface IEmployeeAppService: IApplicationService
    {
        Task<PagedResultDto<EmployeeDto>> GetListAsync(GetPagedEmployeesInput input);
        Task<List<EmployeeDto>> GetListAllAsync();
        Task<EmployeeDto> GetAsync(Guid id);
        Task<EmployeeDto> CreateAsync(EmployeeCreateDto input);
        Task<EmployeeDto> UpdateAsync(Guid id, EmployeeUpdateDto input);
    }
}
