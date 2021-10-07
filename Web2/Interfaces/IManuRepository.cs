using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web2
{
  public interface IManuRepository
  {
    Task CreateProduct(Product product);
    Task UpdateProduct(Product product);
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> GetProduct(string productId);
    Task<Product> GetPopular();
  }
}
