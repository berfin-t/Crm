using Crm.Activities;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Crm.Employees
{
    public interface IEmployeeRepository:IRepository<Employee, Guid>
    {
        Task<List<Employee>> GetListAsync(string? firstName = null, string? lastName = null,
            string? email = null, string? phoneNumber = null, string? address = null,
            DateTime? birthDate = null, string? photoPath=null, EnumGender? gender = null, Guid? positionId = null,
            string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0,
            CancellationToken cancellationToken=default);
        Task<long> GetCountAsync(string? firstName = null, string? lastName = null,
            string? email = null, string? phoneNumber = null, string? address = null,
            DateTime? birthDate = null, string? photoPath=null, EnumGender? gender = null, Guid? positionId = null, CancellationToken cancellationToken=default);
        Task<EmployeeWithNavigationProperties> GetWithNavigationPropertiesAsync(
           Guid id,
           CancellationToken cancellationToken = default);
        Task<Employee> GetAsync(
        Guid? employeeId,
        Guid? userId,
        CancellationToken cancellationToken = default
    );
    }
}
