using AutoMapper;
using Crm.Projects;

namespace Crm;

public class CrmApplicationAutoMapperProfile : Profile
{
    public CrmApplicationAutoMapperProfile()
    {
        CreateMap<Project, ProjectDto>();
        CreateMap<ProjectCreateDto, Project>();
        CreateMap<ProjectUpdateDto, Project>();
    }
}
