using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Crm.Employees
{
    public class EmployeeManager(IEmployeeRepository employeeRepository):DomainService
    {
        #region Create
        public virtual async Task<Employee> CreateAsync(string name, string surname, string email, string phoneNumber, string address, DateTime birthDate, Guid positionId)
        {
            var employee = new Employee(
                GuidGenerator.Create(),
                name,
                surname,
                email,
                phoneNumber,
                address,
                birthDate,
                positionId
            );
            return await employeeRepository.InsertAsync(employee);
        }
        #endregion
        #region Update
        public virtual async Task<Employee> UpdateAsync(Guid id, string name, string surname, string email, string phoneNumber, string address, DateTime birthDate, Guid positionId)
        {
            var employee = await employeeRepository.GetAsync(id);
            employee.SetFirstName(name);
            employee.SetLastName(surname);
            employee.SetEmail(email);
            employee.SetPhoneNumber(phoneNumber);
            employee.SetAddress(address);
            employee.SetBirthDate(birthDate);
            employee.SetPositionId(positionId);
            return await employeeRepository.UpdateAsync(employee);
        }
        #endregion
    }
}
