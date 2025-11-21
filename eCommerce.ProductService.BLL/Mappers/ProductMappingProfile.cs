using AutoMapper;
using eCommerce.ProductService.BLL.DTOs;
using eCommerce.ProductService.DAL.Entities;

namespace eCommerce.ProductService.BLL.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<AddProductRequest, Product>()
            .ForMember(dest => dest.Category,
                opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UnitPrice,
                opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(desc => desc.QuantityInStock,
                opt => opt.MapFrom(src => src.QuantityInStock));
        
        CreateMap<UpdateProductRequest, Product>()
            .ForMember(desc => desc.Id,
                opt => opt.MapFrom(src=>src.Id))
            .ForMember(dest => dest.Category,
                opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UnitPrice,
                opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(desc => desc.QuantityInStock,
                opt => opt.MapFrom(src => src.QuantityInStock));
        
        CreateMap<Product, ProductResponse>();
    }
}