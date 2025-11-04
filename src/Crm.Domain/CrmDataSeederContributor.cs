using Bogus;
using Bogus.DataSets;
using Crm.Activities;
using Crm.Common;
using Crm.Contacts;
using Crm.CustomerNotes;
using Crm.Customers;
using Crm.Employees;
using Crm.Orders;
using Crm.Positions;
using Crm.Projects;
using Crm.Tasks;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;
using Activity = Crm.Activities.Activity;
using EnumType = Crm.Activities.EnumType;
using Task = Crm.Tasks.Task;

namespace Crm;

public class CrmDataSeederContributor(
    IProjectRepository projectRepository,
    ICustomerRepository customerRepository,
    IEmployeeRepository employeeRepository,
    IPositionRepository positionRepository,
    IActivityRepository activityRepository,
    IContactRepository contactRepository,
    ICustomerNoteRepository customerNoteRepository,
    IOrderRepository orderRepository,
    ITaskRepository taskRepository,
    IProjectEmployeeRepository projectEmployeeRepository,
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IPermissionManager permissionManager,
    IGuidGenerator guidGenerator) : IDataSeedContributor, ITransientDependency
{
    public async System.Threading.Tasks.Task SeedAsync(DataSeedContext context)
    {
        await SeedUserAsync("Berfin", "Tek", "employee_berfin", "berfin@gmail.com", "1q2w3E*", "employee");
        var customers = await SeedCustomersAsync();
        var positions = await SeedPositionsAsync();
        var employees = await SeedEmployeesAsync(positions.Select(p => p.Id));        
        var activities = await SeedActivitiesAsync(customers.Select(c => c.Id), employees.Select(e => e.Id));
        var contacts = await SeedContactsAsync(customers.Select(c => c.Id), employees.Select(e=>e.Id));
        var customerNotes = await SeedCustomerNotesAsync(customers.Select(c => c.Id));
        var projects = await SeedProjectsAsync(customers.Select(c => c.Id), employees.Select(e => e.Id));
        var order = await SeedOrdersAsync(customers.Select(c => c.Id), projects.Select(e => e.Id));
        var tasks = await SeedTasksAsync(projects.Select(p => p.Id), employees.Select(e => e.Id));
        var projectEmployees = await SeedProjectEmployeesAsync(projects.Select(p => p.Id), employees.Select(e => e.Id));
    }

    #region User
    private async Task<IdentityUser> SeedUserAsync(
    string name,
    string surname,
    string userName,
    string email,
    string password,
    string? roleName = null,
    IEnumerable<string>? permissionGroups = null
)
    {
        var existingUser = await userManager.FindByNameAsync(userName);
        if (existingUser != null)
        {
            return existingUser;
        }

        var user = new IdentityUser(guidGenerator.Create(), userName, email)
        {
            Name = name,
            Surname = surname
        };

        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errorList = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new Exception($"User creation failed for {userName}: {errorList}");
        }

        if (!roleName.IsNullOrWhiteSpace())
        {
            var role = await SeedRoleAsync(roleName, permissionGroups);
            await userManager.AddToRoleAsync(user, role.Name);
        }

        return user;
    }
    #endregion

    #region Role
    private async Task<IdentityRole> SeedRoleAsync(string roleName, IEnumerable<string>? permissionGroups)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role != null) return role;

        await roleManager.CreateAsync(new IdentityRole(guidGenerator.Create(), roleName));
        role = await roleManager.FindByNameAsync(roleName);
        if (permissionGroups != null)
        {
            await SetDefaultPermissionsAsync(role!.Name, permissionGroups);
        }

        return role!;
    }
    #endregion

    #region Default Permissions
    private async System.Threading.Tasks.Task SetDefaultPermissionsAsync(string role, IEnumerable<string> permissionGroups)
    {
        foreach (var permissionGroup in permissionGroups)
        {
            await permissionManager.SetForRoleAsync(role, $"Crm.{permissionGroup}", true);
        }
    }
    #endregion

    #region Customers
    private async Task<IEnumerable<Customer>> SeedCustomersAsync()
    {
        
        var faker = new Faker<Customer>("tr")
            .CustomInstantiator(f => new Customer(
                guidGenerator.Create(),
                f.Person.FirstName,
                f.Person.LastName,
                f.Person.Email,
                f.Phone.PhoneNumber(),
                f.Address.FullAddress(),
                f.Company.CompanyName()
                ));

        var customers = faker.Generate(100);
        await customerRepository.InsertManyAsync(customers, true);
        return customers;
    }
    #endregion

    #region Positions
    private async Task<IEnumerable<Position>> SeedPositionsAsync()
    {
        var faker = new Faker<Position>("tr")
            .CustomInstantiator(f => new Position(
                guidGenerator.Create(),
                f.Name.JobTitle(),
                f.Lorem.Sentence(),
                f.Random.Decimal(1000, 5000),
                f.Random.Int(1, 5),
                f.Random.Int(6, 10)
                ));
        var positions = faker.Generate(20);

        await positionRepository.InsertManyAsync(positions, true);
        return positions;
    }
    #endregion

    #region Employees
    private async Task<IEnumerable<Employee>> SeedEmployeesAsync(IEnumerable<Guid> positionIds)
    {
        if (await employeeRepository.AnyAsync())
        {
            return await employeeRepository.GetListAsync();
        }

        var faker = new Faker("tr");
        List<Employee> employees = [];

        string[] crmPermissions = ["Activity", "Task", "Meeting", "Note"];

        foreach (var positionId in positionIds)
        {
            int employeeCount = faker.Random.Int(2, 3);
            for (int i = 0; i < employeeCount; i++)
            {
                var gender = faker.PickRandom<Name.Gender>();
                var enumGender = gender == Name.Gender.Male ? EnumGender.Male : EnumGender.Female;
                var firstName = faker.Name.FirstName(gender);
                var lastName = faker.Name.LastName(gender);
                var email = faker.Internet.Email(firstName, lastName);
                var phone = faker.Phone.PhoneNumber();
                var address = faker.Address.FullAddress();
                var birthDate = faker.Date.Past(30, DateTime.Now.AddYears(-18));
                var photoPath = enumGender == EnumGender.Male ? "/images/profile/male.jpg" : "/images/profile/female.jpg";

            //    var user = await SeedUserAsync(
            //    firstName,
            //    lastName,
            //    faker.Internet.UserName(firstName, lastName),
            //    email,
            //    "1q2w3E*",
            //    "employee",
            //    crmPermissions
            //);

                var employee = new Employee(
                    guidGenerator.Create(),
                    firstName,
                    lastName,
                    phone,
                    address,
                    birthDate,
                    photoPath,
                    enumGender,
                    positionId
                );

                employees.Add(employee);
            }
        }

        foreach (var employee in employees)
        {
            var user = await SeedUserAsync(
                employee.FirstName, employee.LastName,
                faker.Internet.UserName(employee.FirstName, employee.LastName),
                faker.Internet.Email(employee.FirstName, employee.LastName), "1q2w3E*", "employee", crmPermissions
                );

            employee.SetUserId(user.Id);
        }

        await SeedEntitiesAsync(employees, e => employeeRepository.InsertManyAsync(e, true));
        return employees;
    }
    #endregion

    #region Generic Entities 
    private async Task<IEnumerable<Guid>> SeedEntitiesAsync<T>(
        IEnumerable<T> entities,
        Func<IEnumerable<T>, System.Threading.Tasks.Task> insertFunction
    )
        where T : AggregateRoot<Guid>
    {
        await insertFunction(entities);
        return entities.Select(e => e.Id).ToList();
    }
    #endregion

    #region Activity
    private async Task<IEnumerable<Activity>> SeedActivitiesAsync(IEnumerable<Guid> customers, IEnumerable<Guid> employees)
    {        
        var faker = new Faker<Activity>("tr")
            .CustomInstantiator(faker => new Activity(
                guidGenerator.Create(),
                faker.PickRandom<EnumType>(),
                faker.Lorem.Sentence(),
                faker.Date.Past(),
                faker.PickRandom(customers),
                faker.PickRandom(employees)
                ));
        var activities = faker.Generate(50);

        await activityRepository.InsertManyAsync(activities, true);
        return activities;
    }
    #endregion

    #region Contacts
    private async Task<IEnumerable<Contact>> SeedContactsAsync(IEnumerable<Guid> customers, IEnumerable<Guid> employees)
    {
        var faker = new Faker<Contact>("tr")
            .CustomInstantiator(f => new Contact(
                guidGenerator.Create(),
                f.PickRandom<Crm.Contacts.EnumType>(),
                f.Phone.PhoneNumber(),
                f.Random.Bool(),
                f.PickRandom(customers),
                f.PickRandom(employees)
                ));
        var contacts = faker.Generate(50);

        await contactRepository.InsertManyAsync(contacts, true);
        return contacts;
    }
    #endregion

    #region Customer Notes
    private async Task<IEnumerable<CustomerNote>> SeedCustomerNotesAsync(IEnumerable<Guid> customers)
    {
        var faker = new Faker<CustomerNote>("tr")
            .CustomInstantiator(f => new CustomerNote(
                guidGenerator.Create(),
                f.Lorem.Sentence(),
                f.Date.Past(),
                f.PickRandom(customers)
                ));
        var customerNotes = faker.Generate(100);

        await customerNoteRepository.InsertManyAsync(customerNotes, true);
        return customerNotes;
    }
    #endregion

    #region Projects
    private async Task<IEnumerable<Project>> SeedProjectsAsync(IEnumerable<Guid> customers, IEnumerable<Guid> employees)
    {
        var faker = new Faker<Project>("tr")
            .CustomInstantiator(f => new Project(
                guidGenerator.Create(),
                f.Commerce.ProductName(),
                f.Lorem.Paragraph(),
                f.Date.Past(),
                f.Date.Future(),
                f.PickRandom<EnumStatus>(Enum.GetValues(typeof(EnumStatus))
                .Cast<EnumStatus>().Where(x => x != 0)),
                f.Random.Decimal(1000, 5000),
                f.Random.Decimal(0, 100),
                f.PickRandom(employees),
                f.PickRandom(customers)
                ));
        var projects = faker.Generate(50);

        await projectRepository.InsertManyAsync(projects, true);
        return projects;
    }
    #endregion

    #region Orders
    private async Task<IEnumerable<Order>> SeedOrdersAsync(IEnumerable<Guid> customers, IEnumerable<Guid> projects)
    {
        var faker = new Faker<Order>("tr")
            .CustomInstantiator(f => new Order(
                guidGenerator.Create(),
                f.PickRandom<EnumStatus>(Enum.GetValues(typeof(EnumStatus))
                .Cast<EnumStatus>().Where(x => x != 0)),
                f.Date.Past(),
                f.Date.Future(),
                f.Random.Decimal(1000, 5000),
                f.Random.String2(6, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"), 
                f.PickRandom(customers),
                f.PickRandom(projects)
                ));
        var orders = faker.Generate(50);

        await orderRepository.InsertManyAsync(orders, true);
        return orders;
    }
    #endregion

    #region Tasks
    private async Task<IEnumerable<Task>> SeedTasksAsync(IEnumerable<Guid> projects, IEnumerable<Guid> employees)
    {
        var faker = new Faker<Task>("tr")
            .CustomInstantiator(f => new Task(
                guidGenerator.Create(),
                f.Lorem.Sentence(),
                f.Lorem.Paragraph(),
                f.Date.Past(),
                f.PickRandom<EnumPriority>(),
                f.PickRandom<EnumStatus>(),
                f.PickRandom(projects),
                f.PickRandom(employees)
                ));
        var tasks = faker.Generate(50);

        await taskRepository.InsertManyAsync(tasks, true);
        return tasks;
    }
    #endregion

    #region Project Employees
    private async Task<IEnumerable<ProjectEmployee>> SeedProjectEmployeesAsync(IEnumerable<Guid> projects, IEnumerable<Guid> employees)
    {
        var faker = new Faker("tr");

        var projectEmployeeList = new List<ProjectEmployee>();

        foreach (var projectId in projects)
        {
            var assignedEmployees = faker.PickRandom(employees, faker.Random.Int(1, 5)).ToList();

            foreach (var employeeId in assignedEmployees)
            {
                var projectEmployee = new ProjectEmployee(
                    guidGenerator.Create(),
                    projectId,
                    employeeId
                );

                if (!projectEmployeeList.Any(pe => pe.ProjectId == projectId && pe.EmployeeId == employeeId))
                {
                    projectEmployeeList.Add(projectEmployee);
                }
            }
        }

        await projectEmployeeRepository.InsertManyAsync(projectEmployeeList, true);

        return projectEmployeeList;
    }
    #endregion
}
