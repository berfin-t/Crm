using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace Crm.Employees
{
    public class EmployeeManager(IEmployeeRepository employeeRepository, UserManager<IdentityUser> userManager):DomainService
    {
        #region Create
        public virtual async Task<Employee> CreateAsync(
            string name, 
            string surname, 
            string email,
            string phoneNumber,
            string address,
            DateTime birthDate, 
            string photoPath,
            EnumGender gender, 
            Guid positionId,
            string userName,
            string password)
        {
            var user = new IdentityUser(GuidGenerator.Create(), userName, email)
            {
                Name = name,
                Surname = surname
            };
            await userManager.CreateAsync(user, password);

            var employee = new Employee(
                GuidGenerator.Create(),
                name,
                surname,
                phoneNumber,
                email,
                address,
                birthDate,
                photoPath,
                gender,
                positionId,
                user.Id
            );

            return await employeeRepository.InsertAsync(employee);
        }
        #endregion

        protected virtual async Task<IdentityUser> CreateEmployeeUserAsync(
        string userName,
        string email,
        string password
    )
        {
            var user = new IdentityUser(GuidGenerator.Create(), userName, email);

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BusinessException($"Kullanıcı oluşturulamadı: {errors}");
            }

            return user;
        }

        #region Update
        public virtual async Task<Employee> UpdateAsync(Guid id, string name, string surname, string email, string phoneNumber, string address, DateTime birthDate, string photoPath, EnumGender gender, Guid positionId)
        {
            var employee = await employeeRepository.GetAsync(id);
            employee.SetFirstName(name);
            employee.SetLastName(surname);
            employee.SetPhoneNumber(phoneNumber);
            employee.SetAddress(address);
            employee.SetBirthDate(birthDate);
            employee.SetPhotoPath(photoPath);
            employee.SetGender(gender);
            employee.SetPositionId(positionId);
            return await employeeRepository.UpdateAsync(employee);
        }
        #endregion
    }
}
