using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Crm.Employees
{
    [RemoteService(IsEnabled = false)]
    //[Authorize(CrmPermissions.Employees.Default)]
    public class EmployeeAppService(IEmployeeRepository employeeRepository,
        EmployeeManager employeeManager) : CrmAppService, IEmployeeAppService
    {
        public Task<EmployeeDto> CreateAsync(EmployeeCreateDto input)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeDto>> GetListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Volo.Abp.Application.Dtos.PagedResultDto<EmployeeDto>> GetListAsync(GetPagedEmployeesInput input)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDto> UpdateAsync(Guid id, EmployeeUpdateDto input)
        {
            throw new NotImplementedException();
        }
    }
}
