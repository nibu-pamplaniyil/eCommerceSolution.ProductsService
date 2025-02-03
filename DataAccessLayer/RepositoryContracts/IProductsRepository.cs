

using eCommerce.DataAccessLayer.Entites;
using System.Linq.Expressions;

namespace eCommerce.DataAccessLayer.RepositoryContracts;
public interface IProductsRepository
{
    //Retrieves all products asynchronously
    Task<IEnumerable<Product>> GetProducts();

    //Retrieves all products based on condition asynchronously
    Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression);

    Task<Product?> GetProductByCondtion(Expression<Func<Product, bool>> conditionExpression);

    //Adding new item to product table asynchronously
    Task<Product?> AddProduct(Product product);

    Task<Product?> UpdateProduct(Product product); 
    Task<bool> DeleteProduct(Guid productID);
}
