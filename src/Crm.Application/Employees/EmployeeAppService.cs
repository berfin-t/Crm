using AutoMapper;
using Crm.Permissions;
using Crm.Projects;
using Crm.Tasks;
using Crm.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;

namespace Crm.Employees
{
    [RemoteService(IsEnabled = true)]
    public class EmployeeAppService(IEmployeeRepository employeeRepository,
        IProjectEmployeeRepository projectEmployeeRepository,
        EmployeeManager employeeManager,
        IUserRules userRules,
        IdentityUserManager userManager,
        ITaskRepository taskRepository,
        IMapper _mapper) : CrmAppService, IEmployeeAppService
    {
        #region Create
        [Authorize(CrmPermissions.Employees.Create)]
        public async Task<EmployeeDto> CreateAsync(EmployeeCreateDto input)
        {
            await userRules.EnsureUsernameNotExistAsync(input.User.UserName);
            await userRules.EnsureEmailNotExistAsync(input.User.Email);

            var employee = await employeeManager.CreateAsync(
                input.FirstName!, input.LastName!,
                input.Email!, input.PhoneNumber!,
                input.Address!, input.BirthDate!,
                input.PhotoPath!, input.Gender, input.PositionId,
                input.User.UserName, input.User.Password);

            return ObjectMapper.Map<Employee, EmployeeDto>(employee);
        }
        #endregion

        //#region Get
        //[AllowAnonymous]
        //public virtual async Task<EmployeeDto> GetAsync(GetEmployeeInput input)
        //{
        //    var employee = await employeeRepository.GetAsync(input.EmployeeId, input.UserId);
        //    var dto = ObjectMapper.Map<Employee, EmployeeDto>(employee);
        //    var user = await userManager.FindByIdAsync(employee.UserId.ToString());
        //    dto.Email = user?.Email!;
        //    return dto;
        //}
        //public async Task<IdentityUserDto?> GetEmployeeUserAsync(Guid userId)
        //{
        //    var user = await userManager.FindByIdAsync(userId.ToString());
        //    GlobalException.ThrowIf(user == null, "Employee user not found");
        //    return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user!);
        //}
        //#endregion

        #region GetListAll
        [AllowAnonymous]
        public async Task<List<EmployeeDto>> GetListAllAsync()
        {
            var items = await employeeRepository.GetListAsync();
            var list = _mapper.Map<List<EmployeeDto>>(items);
            foreach (var item in list)
            {
                var user = await userManager.FindByIdAsync(item.UserId.ToString());
                item.Email = user?.Email!;
            }
            return list;
        }
        #endregion

        //#region GetListPaged
        //[AllowAnonymous]
        //public async Task<PagedResultDto<EmployeeDto>> GetListAsync(GetPagedEmployeesInput input)
        //{
        //    var totalCount = await employeeRepository.GetCountAsync(
        //        input.FirstName, input.LastName, input.Email, input.PhoneNumber, input.Address, input.BirthDate, input.PhotoPath, input.Gender, input.PositionId);

        //    var items = await employeeRepository.GetListAsync(
        //        input.FirstName, input.LastName, input.Email, input.PhoneNumber, input.Address, input.BirthDate, input.PhotoPath, input.Gender, input.PositionId);

        //    return new PagedResultDto<EmployeeDto>
        //    {
        //        TotalCount = totalCount,
        //        Items = ObjectMapper.Map<List<Employee>, List<EmployeeDto>>(items)
        //    };
        //}
        //#endregion

        #region Update
        [Authorize(CrmPermissions.Employees.Edit)]
        public async Task<EmployeeDto> UpdateAsync(Guid id, EmployeeUpdateDto input)
        {
            var employee = await employeeManager.UpdateAsync(
                id, input.FirstName, input.LastName, input.Email, input.PhoneNumber, input.Address,
                input.BirthDate, input.PhotoPath, input.Gender, input.PositionId);

            return ObjectMapper.Map<Employee, EmployeeDto>(employee);
        }
        #endregion

        #region Upload Photo
        [AllowAnonymous]
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

        [AllowAnonymous]
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
        [Authorize(CrmPermissions.Employees.Delete)]
        public virtual async System.Threading.Tasks.Task DeleteAsync(Guid id)
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

        //#region GetWithNavigationProperties
        //[AllowAnonymous]
        //public virtual async Task<EmployeeWithNavigationPropertyDto> GetWithNavigationPropertiesAsync(Guid id) =>
        //ObjectMapper.Map<EmployeeWithNavigationProperties, EmployeeWithNavigationPropertyDto>
        //    (await employeeRepository.GetWithNavigationPropertiesAsync(id));

        //#endregion        

        #region Get Employee By Project Id
        [AllowAnonymous]
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
                              PhotoPath = e.PhotoPath
                          }).ToList();

            return result;
        }
        #endregion

        //[AllowAnonymous]        
        //public async Task<bool> ChangePasswordAsync(Guid userId, EmployeeUserPasswordUpdateDto input)
        //{
        //    var user = await userManager.FindByIdAsync(userId.ToString());
        //    GlobalException.ThrowIf(
        //    !await userManager.CheckPasswordAsync(user!, input.CurrentPassword), "Current password is incorrect."
        //);

        //    var result = await userManager.ChangePasswordAsync(user!, input.CurrentPassword, input.NewPassword);
        //    GlobalException.ThrowIf(!result.Succeeded, result.Errors.Select(e => e.Description));

        //    return result.Succeeded;
        //}

        //[AllowAnonymous]
        //public async Task<bool> UpdateUserAsync(Guid userId, EmployeeUserInformationUpdateDto input)
        //{
        //    var user = await userManager.FindByIdAsync(userId.ToString());

        //    await userRules.EnsureUsernameNotExistForOthersAsync(input.UserName, userId);
        //    await userRules.EnsureEmailNotExistForOthersAsync(input.Email, userId);

        //    var result = await userManager.SetUserNameAsync(user!, input.UserName);
        //    GlobalException.ThrowIf(!result.Succeeded, result.Errors.Select(e => e.Description));

        //    result = await userManager.SetEmailAsync(user!, input.Email);
        //    GlobalException.ThrowIf(!result.Succeeded, result.Errors.Select(e => e.Description));

        //    return result.Succeeded;
        //}

        #region Get Monthly Assigned Task Counts 
        [AllowAnonymous]
        public async Task<List<EmployeeMonthlyTaskCountDto>> GetMonthlyAssignedTaskCountsAsync()
        {
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1);

            var tasks = await taskRepository.GetQueryableAsync();
            var employees = await employeeRepository.GetQueryableAsync();            

            var query =
                from task in tasks
                where task.CreationTime >= startOfMonth
                && task.CreationTime < endOfMonth
                join emp in employees on task.EmployeeId equals emp.Id
                group task by new { emp.Id, emp.FirstName, emp.LastName } into taskcount
                select new EmployeeMonthlyTaskCountDto
                {
                    Id = taskcount.Key.Id,
                    EmployeeName = taskcount.Key.FirstName + " " + taskcount.Key.LastName,
                    TaskCount = taskcount.Count()
                };

            return await AsyncExecuter.ToListAsync(query);
        }
        #endregion
    }
}
