using eCommerce.BusinessLogicLayer.Mappers;
using Microsoft.Extensions.DependencyInjection;


namespace eCommerce.ProductsService.BusinessLogicLayer;
public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        //TO DO: Add business logic layer services into the IoC container

        services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);

        return services;
    }
}
