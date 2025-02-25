using eCommerce.BusinessLogicLayer.Mappers;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using eCommerce.BusinessLogicLayer.Services;
using FluentValidation;
using eCommerce.BusinessLogicLayer.Validators;

namespace eCommerce.ProductsService.BusinessLogicLayer;
public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        //TO DO: Add business logic layer services into the IoC container
        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();
        services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);
        services.AddScoped<IProductService, eCommerce.BusinessLogicLayer.Services.ProductsService>();

        return services;
    }
}
