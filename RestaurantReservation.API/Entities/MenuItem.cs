using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantReservation.API.Entities;

public class MenuItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("restaurant_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string RestaurantId { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("price")]
    public decimal Price { get; set; }
}