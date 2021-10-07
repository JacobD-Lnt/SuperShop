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

namespace Web2
{
    public class ManuRepository : IManuRepository{

        private Database _db;

        public ManuRepository(Database db){
            _db = db;
        }
        public async Task CreateProduct(Product product){
            await _db.AddAsync(product);
        }
        public async Task<Product> GetProduct(Guid productId){
            return await _db.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Product>> GetAllProducts(){
            return await _db.Products.ToListAsync();
        }
        public async Task UpdateProduct(Guid productId, ProductDto productDto){
            var product = await _db.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Quantity = productDto.Quantity;
            product.Weight = productDto.Weight;
            _db.Update(product);
        }
        public async Task<IEnumerable<Product>> GetPopular(){
            var maxAmountSold = await _db.Products.Select(p => p.AmountSold).MaxAsync();
            var products = await _db.Products.Where(p => p.AmountSold == maxAmountSold).ToListAsync();
            return products;
        }
        public async Task SaveAsync(){
            await _db.SaveChangesAsync();
        }
    }
}