using AutoMapper;
using ProductService.Dtos;
using ProductService.Models;
using StoreService;

namespace ProductService.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Source -> Target
            CreateMap<Store, StoreReadDto>();
            CreateMap<ProductWriteDto, Product>();
            CreateMap<Product, ProductReadDto>();
            CreateMap<StorePublishedDto, Store>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
            CreateMap<GrpcStoreModel, Store>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.StoreId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Products, opt => opt.Ignore());
        }
    }
}