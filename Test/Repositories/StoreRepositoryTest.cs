using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Web1;

namespace test.Web1.Repositories
{
    public class StoreRepositoryTest
    {
        private Database _db;
        private IStoreRepository _repo;
        public StoreRepositoryTest()
        {
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();
            var options = new DbContextOptionsBuilder<Database>().UseSqlite(conn).Options;
            _db = new Database(options);
            _db.Database.EnsureDeleted();
            _db.Database.EnsureCreated();
            _repo = new StoreRepository(_db);
        }
        [Fact]
        public async Task CreateUserShouldAddUserToDb()
        {
            User user = new User() { Username = Guid.NewGuid().ToString() };
            await _repo.CreateUser(user);
            await _repo.SaveAsync();
            _db.Users.Count().Should().Be(1);
        }
        [Fact]
        public async Task GetUserShouldGetAUSerFromDb()
        {
            User user = new User() { Username = Guid.NewGuid().ToString() };
            await _repo.CreateUser(user);
            await _repo.SaveAsync();
            User newUser = await _repo.GetUser(user.Username);
            newUser.Username.Should().Be(user.Username);
        }
        [Fact]
        public async Task SendMessageShouldAddAMessageToDb()
        {
            User receiver = new User() { Username = Guid.NewGuid().ToString() };
            User sender = new User() { Username = Guid.NewGuid().ToString() };
            await _repo.CreateUser(receiver);
            await _repo.CreateUser(sender);
            await _repo.SaveAsync();
            Message message = new Message() { Text = "Simple Text" };
            await _repo.SendMessage(receiver, sender, message);
            await _repo.SaveAsync();
            _db.Messages.Count().Should().Be(1);
        }
        [Fact]
        public async Task GetMessageShouldReturnAMessageFromDb()
        {
            User receiver = new User() { Username = Guid.NewGuid().ToString() };
            User sender = new User() { Username = Guid.NewGuid().ToString() };
            await _repo.CreateUser(receiver);
            await _repo.CreateUser(sender);
            await _repo.SaveAsync();
            Message message = new Message() { Text = "Simple Text" };
            await _repo.SendMessage(receiver, sender, message);
            await _repo.SaveAsync();
            Message newMessage = await _repo.GetMessage(message.Id);
            newMessage.Text.Should().Be("Simple Text");
        }
        [Fact]
        public async Task GetAllMessagesShouldReturnAllMessagesFromDb()
        {
            User receiver = new User() { Username = Guid.NewGuid().ToString() };
            User sender = new User() { Username = Guid.NewGuid().ToString() };
            await _repo.CreateUser(receiver);
            await _repo.CreateUser(sender);
            await _repo.SaveAsync();
            Message message = new Message() { Text = "Simple Text" };
            await _repo.SendMessage(receiver, sender, message);
            await _repo.SaveAsync();
            var allMessages = await _repo.GetAllMessages(receiver);
            allMessages.Count().Should().Be(1);
        }
        [Fact]
        public async Task MakeShoppingCartAndAddToDb()
        {
            User user = new User() { Username = Guid.NewGuid().ToString() };
            await _repo.CreateUser(user);
            await _repo.SaveAsync();
            var productId = Guid.NewGuid();
            var cart = new ShoppingCart { Id = Guid.NewGuid(), ProductId = productId, Quantity = 1, User = user };
            await _repo.AddToShoppingCart(cart);
            await _repo.SaveAsync();
            _db.ShoppingCarts.Count().Should().Be(1);
        }
        [Fact]
        public async Task GetShoppingCartFromDb()
        {
            User user = new User() { Username = Guid.NewGuid().ToString() };
            await _repo.CreateUser(user);
            await _repo.SaveAsync();
            var productId = Guid.NewGuid();
            var cart = new ShoppingCart { Id = Guid.NewGuid(), ProductId = productId, Quantity = 1, User = user };
            await _repo.AddToShoppingCart(cart);
            await _repo.SaveAsync();
            var newCart = await _repo.GetShoppingCart(user);
            newCart.Count().Should().Be(1);
        }
    }
}