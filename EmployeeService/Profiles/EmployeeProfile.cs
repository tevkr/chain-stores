using AutoMapper;
using EmployeeService.Dtos;
using EmployeeService.Models;
using StoreService;

namespace EmployeeService.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // Source -> Target
            CreateMap<Store, StoreReadDto>();
            CreateMap<EmployeeWriteDto, Employee>();
            CreateMap<Employee, EmployeeReadDto>();
            CreateMap<StorePublishedDto, Store>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
            CreateMap<GrpcStoreModel, Store>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.StoreId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Employees, opt => opt.Ignore());
        }
    }
}