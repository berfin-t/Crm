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
    }
}
