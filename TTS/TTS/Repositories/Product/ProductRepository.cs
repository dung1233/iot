using MongoDB.Driver;
using TTS.Models.Products;
namespace TTS.Repositories.Products
{
    public class ProductRepository
    {
        private readonly IMongoCollection<Product> _collection;
        public ProductRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Product>("Product");
        }
        public async Task<List<Product>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
        public async Task<Product?> GetByIdAsync(string id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Product?> CreateProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null");
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
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            if (filter == null)
            {
                return null;
            }
            return product;
        }
        public async Task<bool> DeleteProductAsync(string id)

        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            if (filter == null)
            {
                return false;
            }
            var result = await _collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;


        }
    }
}
