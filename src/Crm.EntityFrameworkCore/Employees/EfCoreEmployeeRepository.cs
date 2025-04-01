using Crm.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Crm.Employees
{
    public class EfCoreEmployeeRepository(IDbContextProvider<CrmDbContext> dbContextProvider)
        : EfCoreRepository<CrmDbContext, Employee, Guid>(dbContextProvider), IEmployeeRepository
    {
        #region GetCountAsync
        public async Task<long> GetCountAsync(string? firstName = null, string? lastName = null, string? email = null, string? phoneNumber = null, string? address = null, DateTime? birthDate = null, string? photoPath=null, EnumGender? gender=null, Guid? positionId = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            query = ApplyDataFilters(query, firstName, lastName, email, phoneNumber, address, birthDate, gender, positionId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }
        #endregion

        #region GetListAsync
        public async Task<List<Employee>> GetListAsync(string? firstName = null, string? lastName = null, string? email = null, string? phoneNumber = null, string? address = null, DateTime? birthDate = null, string? photoPath=null, EnumGender? gender = null, Guid? positionId = null, string? sorting = null, int maxResults = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyDataFilters(await GetQueryableAsync(), firstName, lastName, email, phoneNumber, address, birthDate, gender, positionId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResults).ToListAsync(cancellationToken);
        }
        #endregion

        #region ApplyDataFilters
        protected virtual IQueryable<Employee> ApplyDataFilters(IQueryable<Employee> query, string? firstName = null, string? lastName = null, string? email = null, string? phoneNumber = null, string? address = null, DateTime? birthDate = null, EnumGender? gender=null, Guid? positionId = null)
        {
            query
                .WhereIf(!string.IsNullOrWhiteSpace(firstName), e => e.FirstName.Contains(firstName!))
                .WhereIf(!string.IsNullOrWhiteSpace(lastName), e => e.LastName.Contains(lastName!))
                .WhereIf(!string.IsNullOrWhiteSpace(email), e => e.Email.Contains(email!))
                .WhereIf(!string.IsNullOrWhiteSpace(phoneNumber), e => e.PhoneNumber.Contains(phoneNumber!))
                .WhereIf(!string.IsNullOrWhiteSpace(address), e => e.Address.Contains(address!))
                .WhereIf(birthDate.HasValue, e => e.BirthDate == birthDate!.Value)
                .WhereIf(positionId.HasValue, e => e.PositionId == positionId!.Value)
                .WhereIf(gender != null && gender.Value != 0, e => e.Gender == gender);
            return query;
        }
        #endregion
    }
}
