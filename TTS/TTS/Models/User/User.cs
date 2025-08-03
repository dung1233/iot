using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace TTS.Models.User
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
       
        public string userName { get; set; }
       
        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; }

    }
}
