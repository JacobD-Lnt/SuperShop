using System;
using Xunit;
using FluentAssertions;
using Web1;

namespace test.Web1.Models
{
  public class UserTest
  {
    [Fact]
    public void ShouldCreateUser()
    {
        User user = new User(){ Name = "John"};
        user.Name.Should().Be("John");
    }
    [Fact]
    public void ShouldCreateUserFromDto()
    {
        UserDto dto = new UserDto() { Name = "John" };
        User user = new User(dto);
        user.Name.Should().Be("John");
        user.Username.Should().Contain("John");
    }
  }
}
