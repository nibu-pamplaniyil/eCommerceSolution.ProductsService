using AutoMapper;
using eCommerce.DataAccessLayer.Entites;
using eCommerce.BusinessLogicLayer.DTO;
using Microsoft.AspNetCore.Routing.Constraints;


namespace eCommerce.BusinessLogicLayer.Mappers;
public class ProductUpdateRequestToProductMappingProfile : Profile
{
    public ProductUpdateRequestToProductMappingProfile()
    {
        CreateMap<ProductUpdateRequest, Product>()
            .ForMember(des => des.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(des => des.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(des => des.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(des => des.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
            .ForMember(des => des.ProductID, opt => opt.MapFrom(src=>src.ProductID));
    }
}
