﻿using System;
using System.Collections.Generic;
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
        Task<EmployeeDto> UpdatePhotoAsync(Guid employeeId, string photoPath);
        Task DeleteAsync(Guid id);
        Task<EmployeeWithNavigationPropertyDto> GetWithNavigationPropertiesAsync(Guid id);
        Task<List<ProjectEmployeeDto>> GetEmployeesByProjectIdAsync(Guid projectId);

    }
}
