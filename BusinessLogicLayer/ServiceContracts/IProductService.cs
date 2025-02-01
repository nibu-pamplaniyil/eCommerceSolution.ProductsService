using DataAccessLayer.Entites;
using eCommerce.BusinessLogicLayer.DTO;
using System.Linq.Expressions;

namespace eCommerce.BusinessLogicLayer.ServiceContracts;
public interface IProductService
{
    Task<List<ProductResponse?>> GetProducts();

    Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> condtionExpression); 

    Task<ProductResponse?> GetProductByCondition(Expression<Func<Product,bool>> conditionExpression);

    Task<ProductResponse?> AddProduct(ProductAddRequest request);   

    Task<ProductResponse?> UpdateProduct(ProductUpdateRequest request); 

    Task<bool> DeleteProduct(Guid productId);
}
