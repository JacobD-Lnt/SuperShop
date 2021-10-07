using System;
using Xunit;
using FluentAssertions;
using Web1;

namespace test.Web1.Models
{
  public class ShoppingCartTest
  {
    [Fact]
    public void ShouldCreateShoppingCart()
    {
        ShoppingCart shoppingCart = new ShoppingCart(){ ProductId = "1A2B"};
        shoppingCart.ProductId.Should().Be("1A2B");
    }
  }
}
