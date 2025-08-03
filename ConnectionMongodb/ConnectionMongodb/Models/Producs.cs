using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ConnectionMongodb.Models;

public class Producs
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BindNever] // Không bind giá trị Id từ client
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] // Không serialize Id khi null
    public string? Id { get; set; }

    public string Name { get; set; }
    public decimal Price { get; set; }
}
