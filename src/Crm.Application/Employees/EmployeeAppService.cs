﻿using Crm.Activities;
using Crm.Permissions;
using Crm.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Crm.Employees
{
    [RemoteService(IsEnabled = false)]
    [Authorize(CrmPermissions.Employees.Default)]
    public class EmployeeAppService(IEmployeeRepository employeeRepository,
        IProjectEmployeeRepository projectEmployeeRepository,
        EmployeeManager employeeManager) : CrmAppService, IEmployeeAppService
    {
        #region Create
        //[Authorize(CrmPermissions.Employees.Create)]
        public async Task<EmployeeDto> CreateAsync(EmployeeCreateDto input)
        {
            var employee = await employeeManager.CreateAsync(
                input.FirstName, input.LastName, input.Email, input.PhoneNumber, input.Address,
                input.BirthDate!.Value, input.PhotoPath, input.Gender, input.PositionId);

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
                input.FirstName, input.LastName, input.Email, input.PhoneNumber, input.Address, input.BirthDate, input.PhotoPath, input.Gender, input.PositionId);

            var items = await employeeRepository.GetListAsync(
                input.FirstName, input.LastName, input.Email, input.PhoneNumber, input.Address, input.BirthDate, input.PhotoPath, input.Gender, input.PositionId);

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
                input.BirthDate, input.PhotoPath, input.Gender, input.PositionId);

            return ObjectMapper.Map<Employee, EmployeeDto>(employee);
        }
        #endregion

        #region Upload Photo
        public async Task<EmployeeDto> UpdatePhotoAsync(Guid employeeId, string photoPath)
        {
            var employee = await employeeRepository.GetAsync(employeeId);
            if (employee == null)
            {
                throw new BusinessException("Employee not found.");
            }

            employee.SetPhotoPath(photoPath);
            await employeeRepository.UpdateAsync(employee);

            return ObjectMapper.Map<Employee, EmployeeDto>(employee);
        }

        public async Task<string> UploadPhotoAsync(Guid employeeId, IFormFile? file)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "~/images/profile");
            Directory.CreateDirectory(uploadsFolder); 

            string fileName;
            if (file == null || file.Length == 0)
            {
                var employee = await employeeRepository.GetAsync(employeeId);
                if (employee == null)
                {
                    throw new BusinessException("Employee not found.");
                }

                fileName = employee.Gender == EnumGender.Male ? "male.jpg" : "female.jpg";
            }
            else
            {
                fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            string photoPath = $"~/images/profile/{fileName}";
            await UpdatePhotoAsync(employeeId, photoPath);

            return photoPath;
        }
        #endregion

        #region Delete
        //[Authorize(CrmPermissions.Employees.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var employee = await employeeRepository.GetAsync(id);
            if (employee == null)
            {
                throw new EntityNotFoundException(typeof(Employee), id);
            }
            employee.IsDeleted = true;
            await employeeRepository.DeleteAsync(employee);
        }
        #endregion

        #region GetWithNavigationProperties
        public virtual async Task<EmployeeWithNavigationPropertyDto> GetWithNavigationPropertiesAsync(Guid id) =>
        ObjectMapper.Map<EmployeeWithNavigationProperties, EmployeeWithNavigationPropertyDto>
            (await employeeRepository.GetWithNavigationPropertiesAsync(id));

        #endregion

        public async Task<List<ProjectEmployeeDto>> GetEmployeesByProjectIdAsync(Guid projectId)
        {
            var projectEmployees = await projectEmployeeRepository.GetListAllAsync(projectId);

            var employeeIds = projectEmployees.Select(pe => pe.EmployeeId).ToList();
            var employees = await employeeRepository.GetListAsync(e => employeeIds.Contains(e.Id));

            var result = (from pe in projectEmployees
                          join e in employees on pe.EmployeeId equals e.Id
                          select new ProjectEmployeeDto
                          {
                              ProjectId = pe.ProjectId,
                              EmployeeId = e.Id,
                              EmployeeName = e.FirstName + " " + e.LastName,
                          }).ToList();

            return result;
        }


    }
}
