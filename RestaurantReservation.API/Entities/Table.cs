using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantReservation.API.Entities;

public class Table
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("restaurant_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string RestaurantId { get; set; }

    [BsonElement("capacity")]
    public int Capacity { get; set; }
}