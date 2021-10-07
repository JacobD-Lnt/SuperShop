using System;
using Xunit;
using FluentAssertions;
using Web1;

namespace test.Web1.Models
{
  public class ReceiptTest
  {
    [Fact]
    public void ShouldCreateReceipt()
    {
        Receipt receipt = new Receipt(){ ProductName = "IPhone 13"};
        receipt.ProductName.Should().Be("IPhone 13");
    }
  }
}
