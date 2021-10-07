using System;
using Xunit;
using FluentAssertions;
using Web2;

namespace test.Web2.Models
{
  public class ProductTest
  {
    [Fact]
    public void ShouldCreateProduct()
    {
        Product product = new Product(){ Name = "IPhone 13"};
        product.Name.Should().Be("IPhone 13");
    }
    [Fact]
    public void ShouldCreateProductFromDto()
    {
        ProductDto dto = new ProductDto() { Name = "IPhone 13", Price = 1500, Quantity = 15, Weight = 6.14 };
        Product product = new Product(dto);
        product.Name.Should().Be("IPhone 13");
        product.Price.Should().Be(1500);
        product.Quantity.Should().Be(15);
        product.Weight.Should().Be(6.14);
    }
  }
}
