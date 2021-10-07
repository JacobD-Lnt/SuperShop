using System;
using Xunit;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Web2;

namespace test.Web2.Repositories
{
    public class ManuRepositoryTest
    {
        private Database _db;
        private IManuRepository _repo;
        public ManuRepositoryTest()
        {
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();
            var options = new DbContextOptionsBuilder<Database>().UseSqlite(conn).Options;
            _db = new Database(options);
            _db.Database.EnsureDeleted();
            _db.Database.EnsureCreated();
            _repo = new ManuRepository(_db);
        }
        [Fact]
        public async Task ShouldSaveProductToDatabase()
        {
            Product product = new Product();
            await _repo.CreateProduct(product);
            await _repo.SaveAsync();
            _db.Products.Count().Should().Be(1);
        }
        [Fact]
        public async Task ShouldGetAProductFromDatabase()
        {
            ProductDto dto = new ProductDto() { Name = "IPhone 13", Price = 1500, Quantity = 15, Weight = 6.14 };
            Product product = new Product(dto);
            await _repo.CreateProduct(product);
            await _repo.SaveAsync();
            Product newProduct = await _repo.GetProduct(product.Id);
            _db.Products.Count().Should().Be(1);
            newProduct.Name.Should().Be(product.Name);
        }
        [Fact]
        public async Task GetAllProductsSholdGetAllProductsFromDatabase()
        {
            ProductDto dto = new ProductDto() { Name = "IPhone 13", Price = 1500, Quantity = 15, Weight = 6.14 };
            Product product = new Product(dto);
            await _repo.CreateProduct(product);
            await _repo.SaveAsync();
            var products = await _repo.GetAllProducts();
            products.Should().HaveCount(1);
        }
        [Fact]
        public async Task UpdateProductShouldUpdateDatabase()
        {
            ProductDto dto = new ProductDto() { Name = "IPhone 13", Price = 1500, Quantity = 15, Weight = 6.14 };
            ProductDto dto2 = new ProductDto() { Name = "Macbook", Price = 2500, Quantity = 20, Weight = 90 };
            Product product = new Product(dto);
            await _repo.CreateProduct(product);
            await _repo.SaveAsync();
            await _repo.UpdateProduct(product.Id, dto2);
            var testProduct = await _repo.GetProduct(product.Id);
            testProduct.Name.Should().Be("Macbook");
            testProduct.Price.Should().Be(2500);
        }
        [Fact]
        public async Task GetPopularShouldReturnPopularProductsDatabase()
        {
            ProductDto dto = new ProductDto() { Name = "IPhone 13", Price = 1500, Quantity = 15, Weight = 6.14 };
            ProductDto dto2 = new ProductDto() { Name = "Macbook", Price = 2500, Quantity = 20, Weight = 90 };
            Product product = new Product(dto){ AmountSold = 1 };
            Product product2 = new Product(dto2);
            await _repo.CreateProduct(product);
            await _repo.CreateProduct(product2);
            await _repo.SaveAsync();
            var popProducts = await _repo.GetPopular();
            popProducts.Should().HaveCount(1);
        }
    }
}