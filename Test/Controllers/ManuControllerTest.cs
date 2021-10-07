using System;
using Xunit;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Web2;
using System.Threading.Tasks;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace test.Web2.Controllers
{
    public class ManuControllerTest
        {
        private ManuController _controller;
        private Mock<IManuRepository> _mockRepository;
        private DefaultHttpContext _httpContext;
        public ManuControllerTest()
        {
            _mockRepository = new Mock<IManuRepository>();
            _httpContext = new DefaultHttpContext();
            _controller = new ManuController(_mockRepository.Object)
            { ControllerContext = new ControllerContext() { HttpContext = _httpContext } };
        }
        [Fact]
        public async Task ShouldCreateProduct()
        {
            ProductDto dto = new ProductDto() { Name = "IPhone 13", Price = 1500, Quantity = 15, Weight = 6.14 };
            var result = await _controller.CreateProduct(dto);
            var createdActionResult = result as CreatedAtActionResult;
            createdActionResult.StatusCode.Should().Be(201);
            createdActionResult.ActionName.Should().Be("GetProduct");
            createdActionResult.RouteValues["Id"].Should().NotBeNull();
        }
        [Fact]
        public async Task ShouldGetOneProduct()
        {
            Guid guid = Guid.NewGuid();
            _mockRepository.Setup(obj => obj.GetProduct(guid)).Returns(Task.FromResult(new Product() { Id = guid, Name = "IPhone 13", Price = 1500, Quantity = 15, Weight = 6.14, AmountSold = 0 }));
            var result = await _controller.GetProduct(guid);
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            (okResult.Value as Product).Name.Should().Be("IPhone 13");
            _mockRepository.Verify(obj => obj.GetProduct(guid));
        }
        [Fact]
        public async Task ShouldGetAllProduct()
        {
            var result = await _controller.GetAllProducts();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            _mockRepository.Verify(obj => obj.GetAllProducts());
        }
        [Fact]
        public async Task ShouldUpdateAProduct()
        {
            Guid guid = Guid.NewGuid();
            ProductDto dto = new ProductDto() { Name = "Macbook", Price = 2500, Quantity = 20, Weight = 90 };
            _mockRepository.Setup(obj => obj.GetProduct(guid)).Returns(Task.FromResult(new Product() { Name = "Macbook", Price = 2500, Quantity = 20, Weight = 90, AmountSold = 0 }));
            await _controller.UpdateProduct(guid, dto);
            var result = await _controller.GetProduct(guid);
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            (okResult.Value as Product).Name.Should().Be("Macbook");
            _mockRepository.Verify(obj => obj.GetProduct(guid));
        }
        [Fact]
        public async Task ShouldGetPopular()
        {
            var result = await _controller.GetPopularProducts();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            _mockRepository.Verify(obj => obj.GetPopular());
        }
    }
}