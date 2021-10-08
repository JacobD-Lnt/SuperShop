using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Web1
{
    public class StoreRepository : IStoreRepository
    {

        private Database _db;

        public StoreRepository(Database db)
        {
            _db = db;
        }
        public async Task CreateUser(User user)
        {
            await _db.AddAsync(user);
        }
        public async Task<User> GetUser(string userId)
        {
            var user = await _db.Users.Where(u => u.Username == userId).FirstOrDefaultAsync();
            return user;
        }
        public async Task SendMessage(User receiver, User sender, Message message)
        {
            message.Receiver = receiver;
            message.Sender = sender;
            await _db.AddAsync(message);
        }
        public async Task<Message> GetMessage(Guid messageId)
        {
            var message = await _db.Messages.Where(m => m.Id == messageId).FirstOrDefaultAsync();
            return message;
        }
        public async Task<IEnumerable<Message>> GetAllMessages(User receiver)
        {
            var messages = await _db.Messages.Where(m => m.Receiver.Username == receiver.Username).ToListAsync();
            return messages;
        }
        public async Task AddToShoppingCart(ShoppingCart shoppingCart)
        {
            await _db.ShoppingCarts.AddAsync(shoppingCart);
        }
        public async Task<IEnumerable<ShoppingCart>> GetShoppingCart(User user)
        {
            return await _db.ShoppingCarts.Where(s => s.User.Username == user.Username).ToListAsync();
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}