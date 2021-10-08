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
using Newtonsoft;
using System.Net.Http.Json;
using System.Data;
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
        public async Task<IEnumerable<Receipt>> Checkout(User user)
        {
            var client = new HttpClient();
            List<Product> Products = new List<Product>();
            List<Receipt> receipts = new List<Receipt>();
            var checkoutItems = await _db.ShoppingCarts.Where(s => s.User.Username == user.Username).ToListAsync();
            foreach (var item in checkoutItems)
            {
                var product = await client.GetFromJsonAsync<Product>($"http://localhost:5000/api/products/{item.ProductId}");
                Receipt r = new Receipt(){ Id = Guid.NewGuid(), DateBought = DateTime.Now, AmountSpent =(decimal) ((double)product.Price + ((double)product.Price*0.075)), ProductQuantity=1, ProductName = product.Name, User = user};
                await _db.Receipts.AddAsync(r);
                receipts.Add(r);
                product.Quantity -= 1;
                product.AmountSold += 1;
                Products.Add(product);
                var content = JsonContent.Create(product);
                var updatedProduct = await client.PatchAsync($"http://localhost:5000/api/products/{item.ProductId}", content);
            }
            _db.RemoveRange(checkoutItems);
            return receipts;
        }
        public async Task<IEnumerable<Receipt>> GetReceipts(User user){
            return await _db.Receipts.Where(r => r.User.Username == user.Username).ToListAsync();
        }
        public async Task<Receipt> GetReceipt(Guid receiptId){
            var receipt = await _db.Receipts.Where(r => r.Id == receiptId).FirstOrDefaultAsync();
            return receipt;
        }
        public async Task<decimal> GetTotalSpent(User user){
            var prices =  await _db.Receipts.Where(r => r.User.Username == user.Username).Select(r => r.AmountSpent).ToListAsync();
            var totalAmount = prices.Sum();
            return totalAmount;
        }
        public async Task<ShoppingCart> checkQuantity(HttpClient client, User user, Guid productId){
            var product = await client.GetFromJsonAsync<Product>($"http://localhost:5000/api/products/{productId}");
            if(product.Quantity == 0) return null;
            var newCart = new ShoppingCart() { Id = Guid.NewGuid(), ProductId = productId, Quantity = 1, User = user };
            return newCart;
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}