

using AutoMapper;
using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using eCommerce.DataAccessLayer.Entites;
using eCommerce.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace eCommerce.BusinessLogicLayer.Services;

public class ProductsService : IProductService
{
    private readonly IValidator<ProductAddRequest> _productAddRequestValidator;
    private readonly IValidator<ProductUpdateRequest> _productUpdateRequestValidator;
    private readonly IMapper _mapper;
    private readonly IProductsRepository _productsRepository;

    public ProductsService(IValidator<ProductAddRequest> productAddRequestValidator, IValidator<ProductUpdateRequest> productUpdateRequestValidator,IMapper mapper,IProductsRepository productsRepository)
    {
        _productAddRequestValidator = productAddRequestValidator;
        _productUpdateRequestValidator = productUpdateRequestValidator;
        _mapper = mapper;
        _productsRepository = productsRepository;
    }

    public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
    {
        if(productAddRequest == null)
        {
            throw new ArgumentNullException(nameof(productAddRequest));
        }
        //validate product using fulent validation
        ValidationResult validationResult = await _productAddRequestValidator.ValidateAsync(productAddRequest);

        //check validation result
        if (!validationResult.IsValid)
        {
            string erros = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
            throw new ArgumentException(erros);
        }
        //attenmpt to add product
        Product productInput = _mapper.Map<Product>(productAddRequest);//map productrequest into product type
        Product? addedProduct = await _productsRepository.AddProduct(productInput);
        if (addedProduct == null)
        {
            return null;
        }
        ProductResponse addedProductResponse = _mapper.Map<ProductResponse>(addedProduct);//map added product into productresponse type
        return addedProductResponse;
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        Product? existingProduct = await _productsRepository.GetProductByCondtion(x=>x.ProductID==productId);
        if (existingProduct == null) 
        {
            return false;
        }
        bool isDeleted = await _productsRepository.DeleteProduct(productId);
        return isDeleted;
    }

    public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        Product? existingProduct = await _productsRepository.GetProductByCondtion(conditionExpression);
        if (existingProduct == null)
        {
            return null;
        }
        ProductResponse getProductResponse = _mapper.Map<ProductResponse>(existingProduct);
        return getProductResponse;
    }

    public async Task<List<ProductResponse?>> GetProducts()
    {
        IEnumerable<Product?> existingProducts = await _productsRepository.GetProducts();
        IEnumerable<ProductResponse?> getProductResponse = _mapper.Map<IEnumerable<ProductResponse>>(existingProducts);
        return getProductResponse.ToList();
    }

    public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> condtionExpression)
    {
        IEnumerable<Product?> existingProducts = await _productsRepository.GetProductsByCondition(condtionExpression);
        IEnumerable<ProductResponse?> getProductResponse = _mapper.Map<IEnumerable<ProductResponse>>(existingProducts);
        return getProductResponse.ToList();
    }

    public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest request)
    {
        Product? existingProduct = await _productsRepository.GetProductByCondtion(x=>x.ProductID==request.ProductID);
        if(existingProduct == null) 
        {
            throw new ArgumentException("Invalid productId");
        }
        ValidationResult validationResult = await _productUpdateRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            string erros = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
            throw new ArgumentException(erros);
        }
        Product product = _mapper.Map<Product>(request);
        Product? updatedProduct = await _productsRepository.UpdateProduct(product);
        ProductResponse? updateProduct = _mapper.Map<ProductResponse>(updatedProduct);
        return updateProduct;
    }
}
