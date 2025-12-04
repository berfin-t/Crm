using AutoMapper;
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

namespace Crm;

public class CrmApplicationAutoMapperProfile : Profile
{
    public CrmApplicationAutoMapperProfile()
    {
        CreateMap<Activity, ActivityDto>();
        CreateMap<ActivityCreateDto, Activity>();
        CreateMap<ActivityUpdateDto, Activity>();
        CreateMap<ActivityWithNavigationProperties, ActivityDto>().ReverseMap();
        CreateMap<ActivityWithNavigationPropertyDto, Activity>();
        CreateMap<ActivityWithNavigationPropertyDto, ActivityWithNavigationProperties>()
        .ForMember(dest => dest.Activity, opt => opt.MapFrom(src => src.Activity))
        .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
        .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee)).ReverseMap();

        CreateMap<Contact, ContactDto>();
        CreateMap<ContactCreateDto, Contact>();
        CreateMap<ContactUpdateDto, Contact>();

        CreateMap<CustomerNote, CustomerNoteDto>();
        CreateMap<CustomerNoteCreateDto, CustomerNote>();
        CreateMap<CustomerNoteUpdateDto, CustomerNote>();

        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<CustomerCreateDto, Customer>();
        CreateMap<CustomerUpdateDto, Customer>();

        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<EmployeeCreateDto, Employee>();
        CreateMap<EmployeeUpdateDto, Employee>();
        CreateMap<EmployeeWithNavigationPropertyDto, Employee>();
        CreateMap<EmployeeWithNavigationPropertyDto, EmployeeWithNavigationProperties>()
        .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee))
        .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position)).ReverseMap();

        CreateMap<Order, OrderDto>();
        CreateMap<OrderCreateDto, Order>();
        CreateMap<OrderUpdateDto, Order>();

        CreateMap<Position, PositionDto>();
        CreateMap<PositionCreateDto, Position>();
        CreateMap<PositionUpdateDto, Position>();

        CreateMap<Project, ProjectDto>();
        CreateMap<ProjectCreateDto, Project>();
        CreateMap<ProjectUpdateDto, Project>();

        CreateMap<ProjectEmployee, ProjectEmployeeDto>().ReverseMap();
        CreateMap<Employee, ProjectEmployeeDto>().ReverseMap();
        CreateMap<ProjectEmployeeCreateDto, ProjectEmployee>();


        //CreateMap<Task, TaskDto>();
        //CreateMap<TaskCreateDto, Task>();
        //CreateMap<TaskUpdateDto, Task>();

        CreateMap<Task, TaskDto>()
    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
    .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Status.ToString()))
    .ReverseMap();


        CreateMap<TaskCreateDto, Task>()
    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<EnumStatus>(src.Group)))
    .ReverseMap();


        CreateMap<TaskUpdateDto, Task>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                    Enum.IsDefined(typeof(EnumStatus), int.Parse(src.Group))
                        ? (EnumStatus)int.Parse(src.Group)
                        : EnumStatus.Pending))
                .ReverseMap();
    }
}
