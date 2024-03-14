using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantReservation.API.Entities;

public class Employee
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("restaurant_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string RestaurantId { get; set; }

    [BsonElement("first_name")]
    public string FirstName { get; set; }

    [BsonElement("last_name")]
    public string LastName { get; set; }

    [BsonElement("position")]
    public string Position { get; set; }
}