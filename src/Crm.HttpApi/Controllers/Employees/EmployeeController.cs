﻿using Asp.Versioning;
using Crm.Employees;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Crm.Controllers.Employees
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Employee")]
    [Route("api/app/employees")]
    public class EmployeeController:CrmController, IEmployeeAppService
    {
        protected IEmployeeAppService _employeeAppService;
        public EmployeeController(IEmployeeAppService employeeAppService) => _employeeAppService = employeeAppService;

        [HttpGet]
        [Route("{id}")]
        public virtual Task<EmployeeDto> GetAsync(Guid id) => _employeeAppService.GetAsync(id);

        [HttpGet]
        [Route("all")]
        public virtual Task<List<EmployeeDto>> GetListAllAsync() => _employeeAppService.GetListAllAsync();

        [HttpGet]
        [Route("paged")]
        public virtual Task<PagedResultDto<EmployeeDto>> GetListAsync(GetPagedEmployeesInput input) => _employeeAppService.GetListAsync(input);

        [HttpPost]
        [Route("create")]
        public virtual Task<EmployeeDto> CreateAsync(EmployeeCreateDto input) => _employeeAppService.CreateAsync(input);

        [HttpPut]
        [Route("update/{id}")]
        public virtual Task<EmployeeDto> UpdateAsync(Guid id, EmployeeUpdateDto input) => _employeeAppService.UpdateAsync(id, input);

        [HttpPost("upload-photo")]
        public virtual Task<EmployeeDto> UpdatePhotoAsync(Guid employeeId, string photoPath) => _employeeAppService.UpdatePhotoAsync(employeeId, photoPath);

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id) => _employeeAppService.DeleteAsync(id);

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<EmployeeWithNavigationPropertyDto> GetWithNavigationPropertiesAsync(Guid id) => _employeeAppService.GetWithNavigationPropertiesAsync(id);

        [HttpGet]
        public virtual Task<List<ProjectEmployeeDto>> GetEmployeesByProjectIdAsync(Guid projectId) => _employeeAppService.GetEmployeesByProjectIdAsync(projectId);
    }
}
