
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using TTS.Dto;
using TTS.Models.User;
using TTS.Service.Users;
namespace TTS.Repositories.Users
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _collection;
        public UserRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<User>("User");
        }
        public async Task<List<User>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
        public async Task<User> CreateAsyncreated(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }
            await _collection.InsertOneAsync(user);
            return user;
        }
        public async Task<bool> IsUsernameExistsAsync(string userName)
        {
            var existingUser = await _collection.Find(u => u.userName == userName).FirstOrDefaultAsync();
            return existingUser != null;
        }
        public async Task<User> GetByUsernameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Username cannot be null or empty", nameof(userName));
            }
            return await _collection.Find(u => u.userName == userName).FirstOrDefaultAsync();

        }

     }
}
