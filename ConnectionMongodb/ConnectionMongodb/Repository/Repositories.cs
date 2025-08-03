using MongoDB.Driver;

namespace ConnectionMongodb.Repository
{
    public class Repositories
    {
        private readonly IMongoCollection<Models.Producs> _productsCollection;
        public Repositories(IMongoClient client)
        {
            var database = client.GetDatabase("TestApp");
            _productsCollection = database.GetCollection<Models.Producs>("Products");
        }
        public async Task<List<Models.Producs>> GetProducsAsync()
        {
            return await _productsCollection.Find(_ => true).ToListAsync();
        }
        public async Task<Models.Producs> GetProducsByIdAsync(string id)
        {
            return await _productsCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task CreateProducsAsync(Models.Producs product)
        {
            await _productsCollection.InsertOneAsync(product);
        }
        public async Task UpdateProducsAsync(string id,Models.Producs producs)
        {
            await _productsCollection.ReplaceOneAsync(p => p.Id == id, producs);
        }
        public async Task DeleteProducsAsync(string id)
        {
            await _productsCollection.DeleteOneAsync(p => p.Id == id);
        }

    }
}
