using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;
using System.Dynamic;

namespace Web2
{
    [ApiController]
    [Route("api")]

    public class ManuController : ControllerBase{
        private IManuRepository _repository;
        public ManuController(IManuRepository repository){
            _repository = repository;
        }
        //Create product
        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct(ProductDto productDto){
            Product product = new Product(productDto);
            await _repository.CreateProduct(product);
            await _repository.SaveAsync();

            return CreatedAtAction("GetProduct", new { product.Id }, product);
        }
        //get a product
        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProduct(Guid Id){
            if(_repository.GetProduct(Id) is null) return NotFound();
            var product = await _repository.GetProduct(Id);
            return Ok(product);
        }
        //Get all products
        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts(){
            if(_repository.GetAllProducts() is null) return NotFound();
            var products = await _repository.GetAllProducts();
            return Ok(products);
        }
        //Get Popular products
        [HttpGet("products/popular")]
        public async Task<IActionResult> GetPopularProducts(){
            if(_repository.GetPopular() is null) return NotFound();
            var products = await _repository.GetPopular();
            return Ok(products);
        }
        //update a product
        [HttpPatch("products/{id}")]
        public async Task<IActionResult> UpdateProduct(Guid Id, ProductDto productDto){
            if(_repository.GetProduct(Id) is null) return NotFound();
            var product = _repository.GetProduct(Id);
            await _repository.UpdateProduct(Id, productDto);
            await _repository.SaveAsync();
            return CreatedAtAction("GetProduct", new { product.Id }, product );
        }
    }
}