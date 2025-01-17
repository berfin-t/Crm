using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Crm.Employees
{
    [RemoteService(IsEnabled = false)]
    //[Authorize(CrmPermissions.Employees.Default)]
    public class EmployeeAppService(IEmployeeRepository employeeRepository,
        EmployeeManager employeeManager) : CrmAppService, IEmployeeAppService
    {
        #region Create
        //[Authorize(CrmPermissions.Employees.Create)]
        public async Task<EmployeeDto> CreateAsync(EmployeeCreateDto input)
        {
            var employee = await employeeManager.CreateAsync(
                input.FirstName, input.LastName, input.Email, input.PhoneNumber, input.Address,
                input.BirthDate, input.PositionId);

            return ObjectMapper.Map<Employee, EmployeeDto>(employee);
        }
        #endregion

        #region Get
        public async Task<EmployeeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Employee, EmployeeDto>(await employeeRepository.GetAsync(id));
        }
        #endregion

        #region GetListAll
        public async Task<List<EmployeeDto>> GetListAllAsync()
        {
            var items = await employeeRepository.GetListAsync();
            return ObjectMapper.Map<List<Employee>, List<EmployeeDto>>(items);
        }
        #endregion

        #region GetListPaged
        public async Task<PagedResultDto<EmployeeDto>> GetListAsync(GetPagedEmployeesInput input)
        {
            var totalCount = await employeeRepository.GetCountAsync(
                input.FirstName, input.LastName, input.Email, input.PhoneNumber, input.Address, input.BirthDate, input.PositionId);

            var items = await employeeRepository.GetListAsync(
                input.FirstName, input.LastName, input.Email, input.PhoneNumber, input.Address, input.BirthDate, input.PositionId);

            return new PagedResultDto<EmployeeDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Employee>, List<EmployeeDto>>(items)
            };
        }
        #endregion

        #region Update
        public async Task<EmployeeDto> UpdateAsync(Guid id, EmployeeUpdateDto input)
        {
            var employee = await employeeManager.UpdateAsync(
                id, input.FirstName, input.LastName, input.Email, input.PhoneNumber, input.Address,
                input.BirthDate, input.PositionId);

            return ObjectMapper.Map<Employee, EmployeeDto>(employee);
        }
        #endregion
    }
}
