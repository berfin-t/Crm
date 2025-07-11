﻿using Bogus;
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
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;
using static Bogus.DataSets.Name;
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

    // Customers
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

    // Positions
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


    // Employees
    private async Task<IEnumerable<Employee>> SeedEmployeesAsync(IEnumerable<Guid> positions)
    {
        var faker = new Faker<Employee>("tr")
        .CustomInstantiator(f =>
        {
            var bogusGender = f.PickRandom(Name.Gender.Male, Name.Gender.Female);
            EnumGender gender = bogusGender switch
            {
                Name.Gender.Male => EnumGender.Male,
                Name.Gender.Female => EnumGender.Female,
                _ => throw new InvalidOperationException("Unexpected gender value")
            };

            
            var photoPath = gender == EnumGender.Male
                ? "/images/profile/male.jpg"
                : "/images/profile/female.jpg";

            return new Employee(
                guidGenerator.Create(),
                f.Person.FirstName,
                f.Person.LastName,
                f.Person.Email,
                f.Phone.PhoneNumber(),
                f.Address.FullAddress(),
                f.Date.Past(),
                photoPath,
                gender,
                f.PickRandom(positions)
            );
        });

        var employees = faker.Generate(100);

        await employeeRepository.InsertManyAsync(employees, true);
        return employees;
    }


    //Activity
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

    // Contacts
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

    // CustomerNotes
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

    // Projects
    private async Task<IEnumerable<Project>> SeedProjectsAsync(IEnumerable<Guid> customers, IEnumerable<Guid> employees)
    {
        var faker = new Faker<Project>("tr")
            .CustomInstantiator(f => new Project(
                guidGenerator.Create(),
                f.Commerce.ProductName(),
                f.Lorem.Paragraph(),
                f.Date.Past(),
                f.Date.Future(),
                f.PickRandom<EnumStatus>(),
                f.Random.Decimal(1000, 5000),
                f.Random.Decimal(0, 100),
                f.PickRandom(employees),
                f.PickRandom(customers)
                ));
        var projects = faker.Generate(50);

        await projectRepository.InsertManyAsync(projects, true);
        return projects;
    }

    //Orders
    private async Task<IEnumerable<Order>> SeedOrdersAsync(IEnumerable<Guid> customers, IEnumerable<Guid> projects)
    {
        var faker = new Faker<Order>("tr")
            .CustomInstantiator(f => new Order(
                guidGenerator.Create(),
                f.PickRandom<EnumStatus>(),
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

    //Tasks
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

    //ProjectEmployees
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

}
