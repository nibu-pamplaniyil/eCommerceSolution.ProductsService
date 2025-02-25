using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using eCommerce.DataAccessLayer.Entites;
using FluentValidation;
using FluentValidation.Results;

namespace eCommerce.ProductsMicroService.API.APIEndpoints;
public static class ProductAPIEndpoints
{
    public static IEndpointRouteBuilder MapProductAPIEndpoints(this IEndpointRouteBuilder app)
    {
        //GET /api/products
        app.MapGet("/api/products", async (IProductService productsService) =>
        {
            List<ProductResponse?> products = await productsService.GetProducts();
            return Results.Ok(products);
        });

        //GET /api/products/search/productid/{ProductID}
        app.MapGet("/api/products/search/productid/{ProductID:guid}", async (IProductService productsService,Guid ProductID) =>
        {
            ProductResponse? product = await productsService.GetProductByCondition(x=>x.ProductID==ProductID);
            return Results.Ok(product);
        });

        app.MapGet("/api/products/search/{SearchString}", async (IProductService productsService, string SearchString) =>
        {
            List<ProductResponse?> productsByName = await productsService.GetProductsByCondition(x=>x.ProductName!=null && x.ProductName.Contains(SearchString, StringComparison.OrdinalIgnoreCase));
            List<ProductResponse?> productsByCategory = await productsService.GetProductsByCondition(x => x.Category != null && x.Category.Contains(SearchString, StringComparison.OrdinalIgnoreCase));
            var products = productsByName.Union(productsByCategory);
            return Results.Ok(products);
        });

        app.MapPost("/api/products", async (IProductService productsService,IValidator<ProductAddRequest> productAddRequestValidator, ProductAddRequest productAddRequest) =>
        {
            ValidationResult validationResult = await productAddRequestValidator.ValidateAsync(productAddRequest); 
            if(!validationResult.IsValid)
            {
                Dictionary<string,string[]> errors = validationResult.Errors.GroupBy(x=>x.PropertyName).ToDictionary(grp=>grp.Key,grp=>grp.Select(err=>err.ErrorMessage).ToArray());
                return Results.ValidationProblem(errors);
            }
            var addedProduct = await productsService.AddProduct(productAddRequest);
            if (addedProduct != null)
            {
                return Results.Created($"/api/products/search/productid/{addedProduct.ProductID}", addedProduct);
            }
            else
                return Results.Problem("Error in adding product");
            
        });

        app.MapPut("/api/products", async (IProductService productsService, IValidator<ProductUpdateRequest> productUpdateRequestValidator, ProductUpdateRequest productUpdateRequest) =>
        {
            ValidationResult validationResult = await productUpdateRequestValidator.ValidateAsync(productUpdateRequest);
            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors.GroupBy(x => x.PropertyName).ToDictionary(grp => grp.Key, grp => grp.Select(err => err.ErrorMessage).ToArray());
                return Results.ValidationProblem(errors);
            }
            var updatedProduct = await productsService.UpdateProduct(productUpdateRequest);
            if (updatedProduct != null)
            {
                return Results.Ok(updatedProduct);
            }
            else
                return Results.Problem("Error in updating product");

        });

        app.MapDelete("/api/products/{productID:guid}", async (IProductService productsService, Guid productID) =>
        {
            bool isDeleted = await productsService.DeleteProduct(productID);
            if (isDeleted)
            {
                return Results.Ok(true);
            }
            else
                return Results.Problem("Error in deleting product");

        });


        return app;
    }
}
