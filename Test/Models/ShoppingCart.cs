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
            var newGuid = Guid.NewGuid();
            ShoppingCart shoppingCart = new ShoppingCart() { ProductId = newGuid };
            shoppingCart.ProductId.Should().Be(newGuid);
        }
    }
}
