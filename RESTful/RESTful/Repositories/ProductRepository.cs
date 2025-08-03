using MongoDB.Driver;
using RESTful.Models;
namespace RESTful.Repositories
{
    public class ProductRepository 
    {
        private readonly IMongoCollection<Product> _collection;
        public ProductRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Product>("Products");
        }
        public async Task<List<Product>> GetAllAsync()
        {
            return await _collection.Find(product => true).ToListAsync();
        }
        public async Task<Product?> GetProductAsync(string id)
        {
            return await _collection.Find(product => product.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Product?> CreateProductAsync(Product product)
        {
            if (product == null)
            {
                return null;
            }
            await _collection.InsertOneAsync(product);
            return product;
        }
        public async Task<Product?> UpdateProductAsync(string id, Product product)
        {
            if (product == null)
            {
                return null;
            }
            var updateResult = await _collection.ReplaceOneAsync(p => p.Id == id, product);
            if (updateResult.MatchedCount == 0)
            {
                return null;
            }
            return product;
        }
        public async Task<bool> DeleteProductAsync(string id)
        {
            var deleteResult = await _collection.DeleteOneAsync(product => product.Id == id);
            return deleteResult.DeletedCount > 0;
        }
    }
}
