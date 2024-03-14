using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantReservation.API.Entities;

public class Customer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("first_name")]
    public string FirstName { get; set; }
    
    [BsonElement("last_name")]
    public string LastName { get; set; }
    
    [BsonElement("email")]
    public string Email { get; set; }
    
    [BsonElement("phone_number")]
    public string PhoneNumber { get; set; }
}