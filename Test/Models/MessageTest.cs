using System;
using Xunit;
using FluentAssertions;
using Web1;

namespace test.Web1.Models
{
  public class MessageTest
  {
    [Fact]
    public void ShouldCreateMessage()
    {
        Message message = new Message(){ Text = "This is a simple text."};
        message.Text.Should().Be("This is a simple text.");
    }
    [Fact]
    public void ShouldCreateMessageFromDto()
    {
        MessageDto dto = new MessageDto() { Text = "This is a simple text." };
        Message message = new Message(dto);
        message.Text.Should().Be("This is a simple text.");
    }
  }
}
