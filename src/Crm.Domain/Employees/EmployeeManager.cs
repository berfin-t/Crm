using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var user = await CreateEmployeeUserAsync(userName, email, password);
            var employee = new Employee(
                GuidGenerator.Create(),
                name,
                surname,
                email,
                phoneNumber,
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
            await userManager.CreateAsync(
                new IdentityUser(GuidGenerator.Create(), userName, email)
                , password
            );
            var user = await userManager.FindByNameAsync(userName);
            return user!;
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
