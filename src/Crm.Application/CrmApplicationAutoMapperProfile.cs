using AutoMapper;
using Crm.Activities;
using Crm.Contacts;
using Crm.CustomerNotes;
using Crm.Customers;
using Crm.Employees;
using Crm.Orders;
using Crm.Positions;
using Crm.Projects;
using Crm.Tasks;

namespace Crm;

public class CrmApplicationAutoMapperProfile : Profile
{
    public CrmApplicationAutoMapperProfile()
    {
        CreateMap<Activity, ActivityDto>();
        CreateMap<ActivityCreateDto, Activity>();
        CreateMap<ActivityUpdateDto, Activity>();

        CreateMap<Contact, ContactDto>();
        CreateMap<ContactCreateDto, Contact>();
        CreateMap<ContactUpdateDto, Contact>();

        CreateMap<CustomerNote, CustomerNoteDto>();
        CreateMap<CustomerNoteCreateDto, CustomerNote>();
        CreateMap<CustomerNoteUpdateDto, CustomerNote>();

        CreateMap<Customer, CustomerDto>();
        CreateMap<CustomerCreateDto, Customer>();
        CreateMap<CustomerUpdateDto, Customer>();

        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeCreateDto, Employee>();
        CreateMap<EmployeeUpdateDto, Employee>();

        CreateMap<Order, OrderDto>();
        CreateMap<OrderCreateDto, Order>();
        CreateMap<OrderUpdateDto, Order>();

        CreateMap<Position, PositionDto>();
        CreateMap<PositionCreateDto, Position>();
        CreateMap<PositionUpdateDto, Position>();

        CreateMap<Project, ProjectDto>();
        CreateMap<ProjectCreateDto, Project>();
        CreateMap<ProjectUpdateDto, Project>();

        CreateMap<Task, TaskDto>();
        CreateMap<TaskCreateDto, Task>();
        CreateMap<TaskUpdateDto, Task>();
    }
}
