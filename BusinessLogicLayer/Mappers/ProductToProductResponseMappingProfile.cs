using AutoMapper;
using eCommerce.DataAccessLayer.Entites;
using eCommerce.BusinessLogicLayer.DTO;
using Microsoft.AspNetCore.Routing.Constraints;


namespace eCommerce.BusinessLogicLayer.Mappers;
public class ProductToProductResponseMappingProfile : Profile
{
    public ProductToProductResponseMappingProfile()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(des => des.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(des => des.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(des => des.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(des => des.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
            .ForMember(des => des.ProductID, opt => opt.MapFrom(src => src.ProductID));
    }
}
