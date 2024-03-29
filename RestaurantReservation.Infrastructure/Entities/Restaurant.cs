using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantReservation.Infrastructure.Entities;

public class Restaurant
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("address")]
    public string Address { get; set; } = string.Empty;

    [BsonElement("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [BsonElement("opening_hours")]
    public Dictionary<DayOfWeek, string> OpeningHours { get; set; } = new();
}