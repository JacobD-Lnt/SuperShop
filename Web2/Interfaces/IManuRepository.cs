using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web2
{
  public interface IManuRepository
  {
    Task CreateProduct(Product product);
    Task UpdateProduct(Guid productId, ProductDto productDto);
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> GetProduct(Guid productId);
    Task<IEnumerable<Product>> GetPopular();
    Task SaveAsync();
  }
}
