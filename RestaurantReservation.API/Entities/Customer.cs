using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantReservation.API.Entities;

public class Customer : User
{
    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;

    [BsonElement("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [BsonElement("user_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; } = null!;
}