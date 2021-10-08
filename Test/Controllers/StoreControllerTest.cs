using System;
using Xunit;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Web1;
using System.Threading.Tasks;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace test.Web1.Controllers
{
    public class StoreControllerTest
    {
        private StoreController _controller;
        private Mock<IStoreRepository> _mockRepository;
        private DefaultHttpContext _httpContext;
        public StoreControllerTest()
        {
            _mockRepository = new Mock<IStoreRepository>();
            _httpContext = new DefaultHttpContext();
            _controller = new StoreController(_mockRepository.Object);
            { ControllerContext ControllerContext = new ControllerContext() { HttpContext = _httpContext }; }
        }
        [Fact]
        public async Task ShouldCreateUser()
        {
            UserDto dto = new UserDto() { Name = "John" };
            var result = await _controller.CreateUser(dto);
            var createdActionResult = result as CreatedAtActionResult;
            createdActionResult.StatusCode.Should().Be(201);
            createdActionResult.ActionName.Should().Be("GetUser");
            createdActionResult.RouteValues["userName"].Should().NotBeNull();
        }
        [Fact]
        public async Task ShouldGetAUser()
        {
            string userName = Guid.NewGuid().ToString();
            _mockRepository.Setup(obj => obj.GetUser(userName)).Returns(Task.FromResult(new User() { Username = userName, Name = "John" }));
            var result = await _controller.GetUser(userName);
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            (okResult.Value as User).Name.Should().Be("John");
            _mockRepository.Verify(obj => obj.GetUser(userName));
        }
        [Fact]
        public async Task ShouldSendMessage()
        {
            MessageDto dto = new MessageDto() { Text = "simple text" };
            User receiver = new User() { Username = Guid.NewGuid().ToString(), Name = "John" };
            User sender = new User() { Username = Guid.NewGuid().ToString(), Name = "Jane" };
            _mockRepository.Setup(obj => obj.GetUser(receiver.Username)).Returns(Task.FromResult(new User() { Username = receiver.Username, Name = "John" }));
            _mockRepository.Setup(obj => obj.GetUser(sender.Username)).Returns(Task.FromResult(new User() { Username = sender.Username, Name = "John" }));
            var result = await _controller.SendAMessage(receiver.Username, sender.Username, dto);
            var createdActionResult = result as CreatedAtActionResult;
            createdActionResult.StatusCode.Should().Be(201);
            createdActionResult.ActionName.Should().Be("GetMessage");
            createdActionResult.RouteValues["id"].Should().NotBeNull();
        }
        [Fact]
        public async Task ShouldGetAMessage()
        {
            Guid mId = Guid.NewGuid();
            _mockRepository.Setup(obj => obj.GetMessage(mId)).Returns(Task.FromResult(new Message() { Text = "John" }));
            var result = await _controller.GetMessage(mId);
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            (okResult.Value as Message).Text.Should().Be("John");
            _mockRepository.Verify(obj => obj.GetMessage(mId));
        }
        [Fact]
        public async Task ShouldGetAllMessagesForAUser()
        {
            User receiver = new User() { Username = Guid.NewGuid().ToString(), Name = "John" };
            _mockRepository.Setup(obj => obj.GetUser(receiver.Username)).Returns(Task.FromResult(new User() { Username = receiver.Username, Name = "John" }));
            var result = await _controller.GetAllMessages(receiver.Username);
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task ShouldGetShoppingCartItems()
        {
            User user = new User() { Username = Guid.NewGuid().ToString(), Name = "John" };
            _mockRepository.Setup(obj => obj.GetUser(user.Username)).Returns(Task.FromResult(new User() { Username = user.Username, Name = "John" }));
            var result = await _controller.GetCartItems(user.Username);
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
        }
    }
}