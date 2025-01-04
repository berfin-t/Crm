using Crm.Common;
using Crm.Customers;
using Crm.Employees;
using Crm.Positions;
using Crm.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace Crm;

public class CrmDataSeederContributor(
    IProjectRepository projectRepository,
    ICustomerRepository customerRepository,
    IEmployeeRepository employeeRepository,
    IPositionRepository positionRepository,
    IGuidGenerator guidGenerator) : IDataSeedContributor, ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {
        var positions = await SeedPositionsAsync(); 
        var customers = await SeedCustomersAsync(); 
        var employees = await SeedEmployeesAsync(positions.Select(p => p.Id)); 
        var projects = await SeedProjectsAsync(customers.Select(c => c.Id), employees.Select(e => e.Id)); 
    }

    // Projects
    private async Task<IEnumerable<Project>> SeedProjectsAsync(IEnumerable<Guid> customers, IEnumerable<Guid> employees)
    {
        if (await projectRepository.AnyAsync())
        {
            return await projectRepository.GetListAsync();
        }

        IEnumerable<Project> projects = [
            new (guidGenerator.Create(), "Project 1", "Description 1", DateTime.Now, DateTime.Now, EnumStatus.Pending, 1000, 0, customers.ElementAt(0), employees.ElementAt(0)),
            new (guidGenerator.Create(), "Project 2", "Description 2", DateTime.Now, DateTime.Now, EnumStatus.Pending, 2000, 0, customers.ElementAt(0), employees.ElementAt(0)),
            new (guidGenerator.Create(), "Project 3", "Description 3", DateTime.Now, DateTime.Now, EnumStatus.Pending, 3000, 0, customers.ElementAt(0), employees.ElementAt(0))
            ];
        await projectRepository.InsertManyAsync(projects, true);
        return projects;
    }

    // Customers
    private async Task<IEnumerable<Customer>> SeedCustomersAsync()
    {
        if (await customerRepository.AnyAsync())
        {
            return await customerRepository.GetListAsync();
        }
        IEnumerable<Customer> customers = [
            new (guidGenerator.Create(), "Customer 1", "Surname 1", "Email 1", "Phone 1", "Address 1", "Company 1"),
            new (guidGenerator.Create(), "Customer 2", "Surname 2", "Email 2", "Phone 2", "Address 2", "Company 2"),
            new (guidGenerator.Create(), "Customer 3", "Surname 3", "Email 3", "Phone 3", "Address 3", "Company 3")
            ];
        await customerRepository.InsertManyAsync(customers, true);
        return customers;
    }

    // Employees
    private async Task<IEnumerable<Employee>> SeedEmployeesAsync(IEnumerable<Guid> positions)
    {
        if (await employeeRepository.AnyAsync())
        {
            return await employeeRepository.GetListAsync();
        }
        IEnumerable<Employee> employees = [
            new (guidGenerator.Create(), "Employee 1", "Surname 1", "Email 1", "Phone 1", "Address 1", DateTime.Now, positions.ElementAt(0)),
            new (guidGenerator.Create(), "Employee 2", "Surname 2", "Email 2", "Phone 2", "Address 2", DateTime.Now, positions.ElementAt(0)),
            new (guidGenerator.Create(), "Employee 3", "Surname 3", "Email 3", "Phone 3", "Address 3",  DateTime.Now, positions.ElementAt(0))
            ];
        await employeeRepository.InsertManyAsync(employees, true);
        return employees;
    }

    // Positions
    private async Task<IEnumerable<Position>> SeedPositionsAsync()
    {
        if (await positionRepository.AnyAsync())
        {
            return await positionRepository.GetListAsync();
        }
        IEnumerable<Position> positions = [
            new (guidGenerator.Create(), "Position 1", "Description 1", 12, 1, 2),
            new (guidGenerator.Create(), "Position 2", "Description 2", 34, 2, 3),
            new (guidGenerator.Create(), "Position 3", "Description 3", 56, 3, 4)
            ];
        await positionRepository.InsertManyAsync(positions, true);
        return positions;
    }
    
}
