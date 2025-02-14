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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
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
        var tasks = await SeedTasksAsync(customers.Select(p => p.Id), employees.Select(e => e.Id));
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

    //Activity
    private async Task<IEnumerable<Activity>> SeedActivitiesAsync(IEnumerable<Guid> customers, IEnumerable<Guid> employees)
    {
        if(await activityRepository.AnyAsync())
        {
            return await activityRepository.GetListAllAsync();
        }
        IEnumerable<Activity> activities = [
            new (guidGenerator.Create(), EnumType.Call, "Description 1", DateTime.Now, customers.ElementAt(0), employees.ElementAt(0)),
            new (guidGenerator.Create(), EnumType.Email, "Description 2", DateTime.Now, customers.ElementAt(1), employees.ElementAt(1)),
            new (guidGenerator.Create(), EnumType.Meeting, "Description 3", DateTime.Now, customers.ElementAt(2), employees.ElementAt(2))
            ];
        await activityRepository.InsertManyAsync(activities, true);
        return activities;
    }

    // Contacts
    private async Task<IEnumerable<Contact>> SeedContactsAsync(IEnumerable<Guid> customers, IEnumerable<Guid> employees)
    {
        if (await contactRepository.AnyAsync())
        {
            return await contactRepository.GetListAsync();
        }
        IEnumerable<Contact> contacts = [
            new(guidGenerator.Create(), Contacts.EnumType.Email, "Contact 1", true, customers.ElementAt(0), employees.ElementAt(0)),
            new(guidGenerator.Create(), Contacts.EnumType.Phone, "Contact 2", true, customers.ElementAt(1), employees.ElementAt(1)),
            new(guidGenerator.Create(), Contacts.EnumType.SocialMedia, "Contact 3", true, customers.ElementAt(2), employees.ElementAt(2))
            ];

        await contactRepository.InsertManyAsync(contacts, true);
        return contacts;
    }

    // CustomerNotes
    private async Task<IEnumerable<CustomerNote>> SeedCustomerNotesAsync(IEnumerable<Guid> customers)
    {
        if (await customerNoteRepository.AnyAsync())
        {
            return await customerNoteRepository.GetListAsync();
        }
        IEnumerable<CustomerNote> customerNotes = [
            new(guidGenerator.Create(), "Note 1", DateTime.Now, customers.ElementAt(0)),
            new(guidGenerator.Create(), "Note 2", DateTime.Now, customers.ElementAt(1)),
            new(guidGenerator.Create(), "Note 3", DateTime.Now, customers.ElementAt(2))
            ];

        await customerNoteRepository.InsertManyAsync(customerNotes, true);
        return customerNotes;
    }

    // Projects
    private async Task<IEnumerable<Project>> SeedProjectsAsync(IEnumerable<Guid> customers, IEnumerable<Guid> employees)
    {
        if (await projectRepository.AnyAsync())
        {
            return await projectRepository.GetListAsync();
        }

        IEnumerable<Project> projects = [
            new (guidGenerator.Create(), "App design and development",
            "With supporting text below as a natural lead-in to additional contenposuere erat a ante. Voluptates, illo, iste itaque voluptas corrupti ratione" +
            " reprehenderit magni similique? Tempore, quos delectus asperiores libero voluptas quod perferendis! Voluptate, quod illo rerum? Lorem ipsum dolor " +
            "sit amet.\r\n\r\nVoluptates, illo, iste itaque voluptas corrupti ratione reprehenderit magni similique? Tempore, quos delectus asperiores libero " +
            "voluptas quod perferendis! Voluptate, quod illo rerum? Lorem ipsum dolor sit amet. With supporting text below as a natural lead-in to additional " +
            "contenposuere erat a ante.", 
            DateTime.Now, DateTime.Now, EnumStatus.Pending, 1000, 0, employees.ElementAt(0), customers.ElementAt(0)),
                new (guidGenerator.Create(), "Multipurpose Landing Template", "Description 2", DateTime.Now, DateTime.Now, EnumStatus.Pending, 2000, 0, employees.ElementAt(0), customers.ElementAt(0)),
                new (guidGenerator.Create(), "Website Redesign", "Description 3", DateTime.Now, DateTime.Now, EnumStatus.Pending, 3000, 0, employees.ElementAt(0), customers.ElementAt(0))
            ];
        await projectRepository.InsertManyAsync(projects, true);
        return projects;
    }

    //Orders
    private async Task<IEnumerable<Order>> SeedOrdersAsync(IEnumerable<Guid> customers, IEnumerable<Guid> projects)
    {
        if (await orderRepository.AnyAsync())
        {
            return await orderRepository.GetListAsync();
        }
        IEnumerable<Order> orders = [
            new(guidGenerator.Create(), EnumStatus.Active,  DateTime.Now, DateTime.Now, 1, customers.ElementAt(0), projects.ElementAt(0)),
            new(guidGenerator.Create(), EnumStatus.Canceled,  DateTime.Now, DateTime.Now, 2, customers.ElementAt(1), projects.ElementAt(1)),
            new(guidGenerator.Create(), EnumStatus.Completed,  DateTime.Now, DateTime.Now, 3, customers.ElementAt(2), projects.ElementAt(2))
            ];
        await orderRepository.InsertManyAsync(orders, true);
        return orders;
    }

    //Tasks
    private async Task<IEnumerable<Task>> SeedTasksAsync(IEnumerable<Guid> customers, IEnumerable<Guid> employees)
    {
        if (await taskRepository.AnyAsync())
        {
            return await taskRepository.GetListAsync();
        }
        IEnumerable<Task> tasks = [
            new(guidGenerator.Create(), "Task 1", "Description 1", DateTime.Now, EnumPriority.Medium, EnumStatus.Pending, customers.ElementAt(0), employees.ElementAt(0)),
            new(guidGenerator.Create(), "Task 2", "Description 2", DateTime.Now, EnumPriority.Critical, EnumStatus.Canceled, customers.ElementAt(1), employees.ElementAt(1)),
            new(guidGenerator.Create(), "Task 3", "Description 3", DateTime.Now, EnumPriority.High, EnumStatus.Completed, customers.ElementAt(2), employees.ElementAt(2))
            ];
        await taskRepository.InsertManyAsync(tasks, true);
        return tasks;
    }

}
