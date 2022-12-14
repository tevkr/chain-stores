using AutoMapper;
using StoreService.Dtos;
using StoreService.Models;

namespace StoreService.Profiles
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            // Source -> Target
            CreateMap<Store, StoreReadDto>();
            CreateMap<StoreWriteDto, Store>();
            CreateMap<StoreReadDto, StorePublishedDto>();
            CreateMap<Store, GrpcStoreModel>()
                .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src =>src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>src.Address));
        }
    }
}