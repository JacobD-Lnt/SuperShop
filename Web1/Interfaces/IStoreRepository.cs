using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
namespace Web1
{
    public interface IStoreRepository
    {
        Task CreateUser(User user);
        Task<User> GetUser(string userId);
        Task SendMessage(User receiver, User Sender, Message message);
        Task<Message> GetMessage(Guid messageId);
        Task<IEnumerable<Message>> GetAllMessages(User receiver);
        Task AddToShoppingCart(ShoppingCart shoppingCart);
        Task<IEnumerable<ShoppingCart>> GetShoppingCart(User user);
        Task<IEnumerable<Receipt>> Checkout(User user);
        Task<IEnumerable<Receipt>> GetReceipts(User user);
        Task<Receipt> GetReceipt(Guid receiptId);
        Task<decimal> GetTotalSpent(User user);
        Task<ShoppingCart> checkQuantity(HttpClient client, User user, Guid productId);
        Task SaveAsync();
    }
}