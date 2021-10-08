using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;

namespace Web1
{
    [ApiController]
    [Route("api")]

    public class StoreController : ControllerBase
    {
        private IStoreRepository _repository;
        public StoreController(IStoreRepository repo)
        {
            _repository = repo;
        }
        [HttpPost("Users")]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            User user = new User(userDto);
            await _repository.CreateUser(user);
            await _repository.SaveAsync();
            return CreatedAtAction("GetUser", new { user.Username }, user);
        }
        [HttpGet("Users/{username}")]
        public async Task<IActionResult> GetUser(string username)
        {
            var user = await _repository.GetUser(username);
            return Ok(user);
        }
        [HttpPost("Messages/{receiverId}/{senderId}")]
        public async Task<IActionResult> SendAMessage(string receiverId, string senderId, MessageDto messageDto)
        {
            var receiver = await _repository.GetUser(receiverId);
            var sender = await _repository.GetUser(senderId);
            if (receiver is null || sender is null) return NotFound();
            Message message = new Message(messageDto);
            await _repository.SendMessage(receiver, sender, message);
            await _repository.SaveAsync();
            return CreatedAtAction("GetMessage", new { message.Id }, message);
        }
        [HttpGet("Messages/{id}")]
        public async Task<IActionResult> GetMessage(Guid Id)
        {
            var user = await _repository.GetMessage(Id);
            return Ok(user);
        }
        [HttpGet("Users/{userName}/Inbox")]
        public async Task<IActionResult> GetAllMessages(string userName)
        {
            var user = await _repository.GetUser(userName);
            if (user is null) return NotFound();
            var messages = await _repository.GetAllMessages(user);
            return Ok(messages);
        }
        [HttpPost("Cart/{userName}/{productId}")]
        public async Task<IActionResult> CreateCartItem(string userName, Guid productId)
        {
            var user = await _repository.GetUser(userName);
            if (user is null) return NotFound();
            var newCart = new ShoppingCart() { Id = Guid.NewGuid(), ProductId = productId, Quantity = 1, User = user };
            await _repository.AddToShoppingCart(newCart);
            await _repository.SaveAsync();
            return CreatedAtAction("GetCartItems", new { user.Username }, newCart);
        }
        [HttpGet("Cart/{userName}")]
        public async Task<IActionResult> GetCartItems(string userName)
        {
            var user = await _repository.GetUser(userName);
            if (user is null) return NotFound();
            var cartItems = await _repository.GetShoppingCart(user);
            return Ok(cartItems);
        }
    }

}